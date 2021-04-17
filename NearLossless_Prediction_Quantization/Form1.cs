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
using ArithmeticCoding;

namespace PredictorFormsApp
{
    public partial class Form1 : Form
    {
        const int _headerSize = 1078;
        const int _imgLength = 256;
       //string imagePath;
        ImagePredictor _imgPred;
        BitReader _bitReader;
        BitWriter _bitwriter;
        byte[,] _originalImg, _decodedImg;
        FileInfo _filePath;
        List<uint> _bmpHeader;
        int[,] _errorPredSaved, _mainError, _quantizedError;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 511.0d;
            chart.ChartAreas[0].AxisX.Minimum = -255;
            chart.ChartAreas[0].AxisX.Maximum = 255;
            chart.ChartAreas[0].AxisX.Interval = 51;

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
                "Prediction error",
                "Quantized prediction error",
                "Decoded"
            });
            comboBoxSaveMode.Items.AddRange(new string[]
            {
                "Fixed",
                "Table JPEG",
                "Arithmetic coding"
            });
            comboBoxErrorType.Items.AddRange(new string[]
            {
                "Prediction error",
                "Quantized prediction error",
            });
            histoComboBox.SelectedIndex = 0;
            comboBoxSaveMode.SelectedIndex = 0;
            comboBoxErrorType.SelectedIndex = 0;
            textBoxKVal.Text = 2.ToString();
        }

        private void LoadOrigBttn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Bitmap files (*.bmp)|*.bmp";
            if(open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                var img = Image.FromFile(fileName);
                originalPBox.Image = new Bitmap(img);
                img.Dispose();
                _originalImg = new byte[_imgLength, _imgLength];
                for (int i = 0; i < _imgLength; i++)
                    for (int j = 0; j < _imgLength; j++)
                        _originalImg[i, j] = ((Bitmap)originalPBox.Image).GetPixel(i, j).R;
                _filePath = new FileInfo(fileName);
            }
        }

        private void EncodeBttn_Click(object sender, EventArgs e)
        {
            _imgPred = new ImagePredictor(k: int.Parse(textBoxKVal.Text));
            _imgPred.Encode(_originalImg, methodNumber:methodComboBox.SelectedIndex);
            
            _errorPredSaved = _imgPred._errorPred;
            _mainError = _imgPred._error;
            _quantizedError = _imgPred._errorPredQ;
        }

        private void LoadEncBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            FD.Filter = "Near-lossless prediction files (*.nlp) | *.nlp";
            if (FD.ShowDialog() == DialogResult.OK)
            {
                _filePath = new FileInfo(FD.FileName);
            }
        }

        private List<uint> readBMPHeader()
        {
            List<uint> values = new List<uint>();
            long fileSize = 8 * new FileInfo(_filePath.FullName).Length;
            int count = 0;
            do
            {
                if (count >= _headerSize)
                    break;
                int readBits = 8;
                uint value = _bitReader.ReadNBits(readBits);
                values.Add(value);
                fileSize -= readBits;
                count++;
            } while (fileSize > 0);
            return values;
        }

        private void StoreBttn_Click(object sender, EventArgs e)
        {
            _bitReader = new BitReader(_filePath.FullName);
            _bmpHeader = new List<uint>(readBMPHeader());
            _bitReader.Dispose();

            string outputName = _filePath.Name + ".k" + textBoxKVal.Text +
                 "p" + _imgPred._predictionNumber + comboBoxSaveMode.Text.Substring(0, 1) + ".nlp";
            _bitwriter = new BitWriter(Path.Combine(_filePath.DirectoryName, outputName));

            // write header
            foreach (var item in _bmpHeader)
            {
                _bitwriter.WriteNBits(8, item);
            }
            
            // predictor used
            _bitwriter.WriteNBits(4, (uint)_imgPred._predictionNumber);

            // k number
            _bitwriter.WriteNBits(4, (uint)_imgPred._k);

            // F/T/A mode used
            _bitwriter.WriteNBits(2, (uint)comboBoxSaveMode.SelectedIndex);

            switch (comboBoxSaveMode.SelectedIndex)
            {
                case 0:
                    SaveByFixedMethod(); // 9 bits
                    break;
                case 1:
                    SaveByJpegTable();
                    break;
                case 2:
                    SaveByArithmeticMethod();
                    break;
            }
        }

        private void SaveByArithmeticMethod()
        {
            const uint allSymbols = (_imgLength * 2), eof = allSymbols - 1;
            ArithmeticCodingLogic arithmCoder = new ArithmeticCodingLogic(bitWriterContext: _bitwriter, total_symbols: allSymbols, eof: eof);

            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    uint symbol = (uint)(_imgPred._errorPredQ[i, j]+ (_imgLength - 1));
                    arithmCoder.EncodeImageErrorValue(symbol);
                }
            }

            arithmCoder.SendLastDetailsOfImageError();
            _bitwriter.Dispose();
        }

        private void SaveByJpegTable()
        {
            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    var value = _imgPred._errorPredQ[i, j];

                    if (value == 0)
                    {
                        _bitwriter.WriteNBits(1, 0);
                        continue;
                    }

                    var line = (uint)Math.Log(Math.Abs(value), 2) + 1;
                    var indexCol = value < 0 ? value + ((int)Math.Pow(2, line) - 1) : value;

                    for(int count = 0; count < line; count++)
                    {
                        _bitwriter.WriteNBits(1, 1);
                    }

                    _bitwriter.WriteNBits(1, 0);
                    _bitwriter.WriteNBits((int)line, (uint)indexCol);
                }
            }

            _bitwriter.WriteNBits(7, 1);
            _bitwriter.Dispose();
        }

        private void SaveByFixedMethod()
        {
            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    if (_imgPred._errorPredQ[i, j] < 0)
                    {
                        _bitwriter.WriteNBits(1, 1);
                    }
                    else if (_imgPred._errorPredQ[i, j] >= 0)
                    {
                        _bitwriter.WriteNBits(1, 0);
                    }
                    _bitwriter.WriteNBits(8, (uint)Math.Abs(_imgPred._errorPredQ[i, j]));
                }
            }

            _bitwriter.WriteNBits(7, 1);
            _bitwriter.Dispose();
        }

        private void DecodeBttn_Click(object sender, EventArgs e)
        {
            _bitReader = new BitReader(_filePath.FullName);
            _bmpHeader = readBMPHeader();
            int fileMethod = (int)_bitReader.ReadNBits(4);
            int k = (int)_bitReader.ReadNBits(4);
            int saveMode = (int)_bitReader.ReadNBits(2);

            List<int> errorList = new List<int>();
            switch (saveMode)
            {
                case 0:
                    errorList = new List<int>(readFixedCompressedFile());
                    break;
                case 1:
                    errorList = new List<int>(readJpegTableCompressedFile());
                    break;
                case 2:
                    errorList = new List<int>(readArithmeticCompressedFile());
                    break;
            }

            _bitReader.Dispose();

            if (!errorList.Any())
            {
                throw new Exception("Something didn't work correctly at decoding!");
            }

            int[,] errorPredQ = new int[_imgLength, _imgLength];
            
            // from List to matrix conversion
            int index = 0;
            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    errorPredQ[i, j] = errorList[index];
                    index++;
                }
            }

            _imgPred = new ImagePredictor(k: k);
            _imgPred.Decode(errorPredQ, methodNumber: fileMethod);
            _decodedImg = _imgPred._decod;

            DecodedPBox.Image = new Bitmap(_imgLength, _imgLength);
            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    int color = _decodedImg[i, j];
                    ((Bitmap)DecodedPBox.Image).SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }
            //DecodedPBox.Image.Save(filePath.DirectoryName + "\\decodedImage.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private List<int> readArithmeticCompressedFile()
        {
            const uint allSymbols = (_imgLength * 2), eof = allSymbols - 1;
            List<int> values = new List<int>();
            ArithmeticCodingLogic arithmCoder = new ArithmeticCodingLogic(bitReaderContext: _bitReader, total_symbols: allSymbols, eof: eof);
            
            arithmCoder._decodingValue = _bitReader.ReadNBits(32);
            for (; ; )
            {
                uint symbol = arithmCoder.DecodeSymbol();
                if (symbol == eof) // eof
                {
                    arithmCoder._bitReader.Dispose();
                    break;
                }

                int symbolToErrorRange = (int)symbol - (_imgLength - 1);
                values.Add(symbolToErrorRange);
                arithmCoder.UpdateModel(symbol);
            }

            return values;
        }

        private List<int> readJpegTableCompressedFile()
        {
            List<int> values = new List<int>();

            for(int count = 0; count < (_imgLength * _imgLength); count++)
            {
                int line = 0;
                var bit = _bitReader.ReadNBits(1);
                
                if (bit == 0) 
                {
                    values.Add(0);
                    continue;
                }

                do
                {
                    line++;
                    bit = _bitReader.ReadNBits(1);
                } while (bit != 0);

                var indexCol = (int)_bitReader.ReadNBits(line);
                var decompressedValue = indexCol < (int)Math.Ceiling((Math.Pow(2, line) - 1) / 2) ?
                    indexCol - ((int)Math.Pow(2, line) - 1) :
                    indexCol;

                values.Add(decompressedValue);
            }

            return values;
        }

        private List<int> readFixedCompressedFile()
        {
            List<int> values = new List<int>();
            FileInfo fileInfo = new FileInfo(_filePath.FullName);
            long fileSize = (8 * fileInfo.Length) - (_headerSize + 10); // (10 = 4p + 4k + 2f/t/a)
            do
            {
                int readBits = 1;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = _bitReader.ReadNBits(readBits);
                int sign = (int)value;
                fileSize -= readBits;

                readBits = 8;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                value = _bitReader.ReadNBits(readBits);
                if (sign == 0)
                    values.Add((int)value);
                else if (sign == 1)
                    values.Add((-1) * (int)value);
                fileSize -= readBits;
            } while (fileSize > 0);
            return values;
        }

        private void SaveDecBttn_Click(object sender, EventArgs e)
        {
            string outputName = _filePath.Name + ".bmp";
            _bitwriter = new BitWriter(Path.Combine(_filePath.DirectoryName, outputName));

            foreach (var item in _bmpHeader)
                _bitwriter.WriteNBits(8, item);

            for (int i = _imgLength - 1; i >= 0; i--)
                for (int j = 0; j < _imgLength; j++)
                    _bitwriter.WriteNBits(8, _decodedImg[j, i]);

            //bwrite.WriteNBits(7, 1);
            _bitwriter.Dispose();
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 511.0d * double.Parse(scaleTextBox.Text);
        }

        private void histogramBttn_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();

            switch (histoComboBox.SelectedIndex)
            {
                case 0:
                    DrawHistogram(_originalImg);
                    break;
                case 1:
                    DrawHistogram(_errorPredSaved);
                    break;
                case 2:
                    DrawHistogram(_imgPred._errorPredQ);
                    break;
                case 3:
                    DrawHistogram(_imgPred._decod);
                    break;
            }
        }

        private void DrawHistogram<T>(T[,] histParam)
        {
            foreach (var item in GetFreq(histParam))
            {
                chart.Series[0].Points.AddXY(item.Key, item.Value);
            }
        }

        private Dictionary<T, int> GetFreq<T>(T[,] histParam)
        {
            Dictionary<T, int> freq = new Dictionary<T, int>();
            for (int i = 0; i < _imgLength; i++)
                for (int j = 0; j < _imgLength; j++)
                {
                    if (freq.ContainsKey(histParam[i, j]))
                        freq[histParam[i, j]] += 1;
                    else
                        freq.Add(histParam[i, j], 1);
                }
            return freq;
        }

        private void textBoxKVal_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(textBoxKVal.Text, out value))
            {
                if (value <= 0)
                {
                    value = 0;
                }
                else if (value >= 10)
                {
                    value = 10;
                }
            }
            else
            {
                textBoxKVal.Text = "";
                return;
            }

            textBoxKVal.Text = value.ToString();
        }

        private void textBoxKVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxKVal.Text))
            {
                textBoxKVal.Text = 2.ToString();
            }
        }

        private void buttonCompError_Click(object sender, EventArgs e)
        {
            int max = int.MinValue, min = int.MaxValue;

            for (int i = 0; i < _mainError.GetLength(0); i++)
            {
                for (int j = 0; j < _mainError.GetLength(1); j++)
                {
                    if (_mainError[i, j] > max)
                    {
                        max = _mainError[i, j];
                    }

                    if (_mainError[i, j] < min)
                    {
                        min = _mainError[i, j];
                    }
                }
            }

            labelMaxError.Text = "Max Error: " + max.ToString();
            labelMinError.Text = "Min Error: " + min.ToString();
            
        }

        private void ShowErrorBttn_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(_errorPredSaved.GetLength(0), _errorPredSaved.GetLength(1));
            for (int i = 0; i < _imgLength; i++)
            {
                for (int j = 0; j < _imgLength; j++)
                {
                    int value;
                    switch (comboBoxErrorType.SelectedIndex)
                    {
                        case 0:
                            value = (int)(128.0d + _errorPredSaved[i, j] * double.Parse(errorTxtBox.Text));
                            break;
                        case 1:
                            value = (int)(128.0d + _quantizedError[i, j] * double.Parse(errorTxtBox.Text));
                            break;
                        default:
                            value = 0;
                            break;
                    }

                    if (value <= 0)
                    {
                        value = 0;
                    }
                    else if (value >= 255)
                    {
                        value = 255;
                    }
                    bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            }

            //display
            errorPBox.Image = bmp;
        }
    }
}
