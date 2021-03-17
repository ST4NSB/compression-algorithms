using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Predictor;
using BitReaderWriter;
using System.IO;

namespace PredictorFormsApp
{
    public partial class Form1 : Form
    {
        const int HeaderSize = 1078;
        const int imgLength = 256;
       //string imagePath;
        ImagePredictor imgPred;
        BitReader bread;
        BitWriter bwrite;
        byte[,] originalImg, decodedImg;
        FileInfo filePath;
        List<uint> bmpHeader;
        public Form1()
        {
            InitializeComponent();
            methodComboBox.Items.AddRange(new string[] 
            {
                "128",
                "A",
                "B",
                "C",
                "A+B-C",
                "A+(B-C)/2",
                "B+(A-C)/2",
                "(A+B)/2",
                "jpegLS"
            });
            methodComboBox.SelectedIndex = 4;
            histoComboBox.Items.AddRange(new string[]
            {
                "Original",
                "Error Prediction",
                "Decoded"
            });
            histoComboBox.SelectedIndex = 0;
        }

        private void LoadOrigBttn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Images Files(*.bmp)|*.bmp";
            if(open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                var img = Image.FromFile(fileName);
                originalPBox.Image = new Bitmap(img);
                img.Dispose();
                originalImg = new byte[imgLength, imgLength];
                for (int i = 0; i < imgLength; i++)
                    for (int j = 0; j < imgLength; j++)
                        originalImg[i, j] = ((Bitmap)originalPBox.Image).GetPixel(i, j).R;
                filePath = new FileInfo(fileName);
            }
        }

        private void EncodeBttn_Click(object sender, EventArgs e)
        {
            imgPred = new ImagePredictor();
            imgPred.Encode(originalImg, methodNumber:methodComboBox.SelectedIndex);
        }

        private void LoadEncBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                filePath = new FileInfo(FD.FileName);
            }
        }

        private Dictionary<int, int> GetFreq(byte[,] histParam)
        {
            Dictionary<int, int> freq = new Dictionary<int, int>();
            for (int i = 0; i < imgLength; i++)
                for (int j = 0; j < imgLength; j++)
                {
                    if (freq.ContainsKey(histParam[i, j]))
                        freq[histParam[i, j]] += 1;
                    else
                        freq.Add(histParam[i, j], 1);
                }
            return freq;
        }

        private Dictionary<int, int> GetFreq(int[,] histParam)
        {
            Dictionary<int, int> freq = new Dictionary<int, int>();
            for (int i = 0; i < imgLength; i++)
                for (int j = 0; j < imgLength; j++)
                {
                    if (freq.ContainsKey(histParam[i, j]))
                        freq[histParam[i, j]] += 1;
                    else
                        freq.Add(histParam[i, j], 1);
                }
            return freq;
        }

        private void Button1_Click(object sender, EventArgs e) // butonu de histograma
        {
            Dictionary<int, int> freq = new Dictionary<int, int>();
            if (histoComboBox.SelectedIndex == 0)
            {
                byte[,] histParam = new byte[imgLength, imgLength];
                histParam = originalImg;
                freq = GetFreq(histParam);
            }
            else if (histoComboBox.SelectedIndex == 1)
            {
                int[,] histParam = new int[imgLength, imgLength];
                histParam = imgPred.errorMatrix;
                freq = GetFreq(histParam); ;
            }
            else if (histoComboBox.SelectedIndex == 2)
            {
                byte[,] histParam = new byte[imgLength, imgLength];
                histParam = decodedImg;
                freq = GetFreq(histParam);
            }

            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 511.0d;
            chart.ChartAreas[0].AxisX.Minimum = -255;
            chart.ChartAreas[0].AxisX.Maximum = 255;
            chart.Series[0].Points.Clear();
            foreach(var item in freq)
                chart.Series[0].Points.AddXY(item.Key, item.Value);
        }

        private List<uint> readBMPHeader()
        {
            List<uint> values = new List<uint>();
            long fileSize = 8 * new FileInfo(filePath.FullName).Length;
            int count = 0;
            do
            {
                if (count >= HeaderSize)
                    break;
                int readBits = 8;
                uint value = bread.ReadNBits(readBits);
                values.Add(value);
                fileSize -= readBits;
                count++;
            } while (fileSize > 0);
            return values;
        }

        private void StoreBttn_Click(object sender, EventArgs e)
        {
            bread = new BitReader(filePath.FullName);
            bmpHeader = new List<uint>(readBMPHeader());
            bread.Dispose();

            string outputName = filePath.Name + "[" + imgPred.predictionNumber + "].pre";
            bwrite = new BitWriter(Path.Combine(filePath.DirectoryName, outputName));

            foreach (var item in bmpHeader)
                bwrite.WriteNBits(8, item);
                    
            bwrite.WriteNBits(4, (uint)imgPred.predictionNumber);
            for (int i = 0; i < imgLength; i++)
                for (int j = 0; j < imgLength; j++)
                {
                    if (imgPred.errorMatrix[i, j] < 0)
                        bwrite.WriteNBits(1, 1);
                    else if (imgPred.errorMatrix[i, j] >= 0)
                        bwrite.WriteNBits(1, 0);
                    bwrite.WriteNBits(8, (uint)Math.Abs(imgPred.errorMatrix[i, j]));
                }

            bwrite.WriteNBits(7, 1);
            bwrite.Dispose();
        }

        private List<int> readErrorMatrix()
        {
            List<int> values = new List<int>();
            FileInfo fileInfo = new FileInfo(filePath.FullName);
            long fileSize = (8 * fileInfo.Length) - (HeaderSize + 4);
            do
            {
                int readBits = 1;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = bread.ReadNBits(readBits);
                int sign = (int)value;
                fileSize -= readBits;

                readBits = 8;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                value = bread.ReadNBits(readBits);
                if (sign == 0)
                    values.Add((int)value);
                else if (sign == 1)
                    values.Add((-1)*(int)value);
                fileSize -= readBits;
            } while (fileSize > 0);
            return values;
        }

        private void DecodeBttn_Click(object sender, EventArgs e)
        {
            bread = new BitReader(filePath.FullName);
            bmpHeader = readBMPHeader();
            int fileMethod = (int)bread.ReadNBits(4);
            var errorList = readErrorMatrix();
            bread.Dispose();

            int[,] errorMatrix = new int[imgLength, imgLength];
            int k = 0;
            for (int i = 0; i < imgLength; i++)
                for (int j = 0; j < imgLength; j++)
                {
                    errorMatrix[i, j] = (int)errorList[k];
                    k++;
                }

            imgPred = new ImagePredictor();
            imgPred.Decode(errorMatrix, methodName: fileMethod);
            decodedImg = imgPred.origDecodedMatrix;

            DecodedPBox.Image = new Bitmap(imgLength, imgLength);
            for (int i = 0; i < imgLength; i++)
            {
                for (int j = 0; j < imgLength; j++)
                {
                    int color = decodedImg[i, j];
                    ((Bitmap)DecodedPBox.Image).SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }
            //DecodedPBox.Image.Save(filePath.DirectoryName + "\\decodedImage.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void SaveDecBttn_Click(object sender, EventArgs e)
        {
            string outputName = filePath.Name + ".decoded";
            bwrite = new BitWriter(Path.Combine(filePath.DirectoryName, outputName));

            foreach (var item in bmpHeader)
                bwrite.WriteNBits(8, item);

            for (int i = imgLength - 1; i >= 0; i--)
                for (int j = 0; j < imgLength; j++)
                    bwrite.WriteNBits(8, decodedImg[j, i]);

            //bwrite.WriteNBits(7, 1);
            bwrite.Dispose();
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 511.0d * double.Parse(scaleTextBox.Text);
        }

        private void ShowErrorBttn_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(imgPred.errorMatrix.GetLength(0), imgPred.errorMatrix.GetLength(1));
            for (int i = 0; i < imgLength; i++)
                for (int j = 0; j < imgLength; j++)
                {
                    int value = (int)(128.0d + imgPred.errorMatrix[i, j] * double.Parse(errorTxtBox.Text));
                    if (value < 0)
                        value = 0;
                    else if (value > 255)
                        value = 255;
                    bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            //display
            errorPBox.Image = bmp;
        }
    }
}
