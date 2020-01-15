using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPEG_UI
{
    public partial class Jpeg : Form
    {
        public struct Coords
        {
            public int x, y;
            public Coords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
               
        }

        JpegLogic jpgLogic;
        Coords origCoords, decodedCoords;
        public Jpeg()
        {
            InitializeComponent();
            jpgLogic = new JpegLogic();
            CuantComboBox.Items.AddRange(new string[]
            {
                "ZigZag", 
                "Matrice simpla calculata", 
                "Factor de calitate Jpeg"
            });
            CuantComboBox.SelectedIndex = 1;
            nrTextBox.Text = "10";
        }

        private void TestImage(PictureBox pb)
        {
            Bitmap bmp = new Bitmap(256, 256);
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    byte value = (byte)jpgLogic.Luminance[i, j];
                    bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            pb.Image = bmp;
        }

        private void LoadBttn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Images Files(*.bmp) | *.bmp";
            if(open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                var img = Image.FromFile(fileName);
                originalPictureBox.Image = new Bitmap(img);
                img.Dispose();
            }
        }

        private void FirstStepsBttn_Click(object sender, EventArgs e)
        {
            jpgLogic.ConvertToYCbCr(originalPictureBox);

           // TestImage(errorPictureBox);

            jpgLogic.DCT(jpgLogic.Luminance, type:"Y");
            jpgLogic.DCT(jpgLogic.BlueDifference_Cb, type: "Cb");
            jpgLogic.DCT(jpgLogic.RedDifference_Cr, type: "Cr");

        }


        private void InverseStepsBttn_Click(object sender, EventArgs e)
        {
            int method = CuantComboBox.SelectedIndex;
            int methodTxtBoxNumber = Convert.ToInt32(nrTextBox.Text);
            switch(method)
            {
                case 0:
                    jpgLogic.zigZagQuantization(jpgLogic.Luminance, methodTxtBoxNumber, "Y");
                    jpgLogic.zigZagQuantization(jpgLogic.BlueDifference_Cb, methodTxtBoxNumber, "Cb");
                    jpgLogic.zigZagQuantization(jpgLogic.RedDifference_Cr, methodTxtBoxNumber, "Cr");
                    break;
                case 1:
                    jpgLogic.SimpleMatrixQuantization(jpgLogic.Luminance, methodTxtBoxNumber, "Y");
                    jpgLogic.SimpleMatrixQuantization(jpgLogic.BlueDifference_Cb, methodTxtBoxNumber, "Cb");
                    jpgLogic.SimpleMatrixQuantization(jpgLogic.RedDifference_Cr, methodTxtBoxNumber, "Cr");
                    
                    jpgLogic.SimpleMatrixInverseQuantization(jpgLogic.Luminance, methodTxtBoxNumber, "Y");
                    jpgLogic.SimpleMatrixInverseQuantization(jpgLogic.BlueDifference_Cb, methodTxtBoxNumber, "Cb");
                    jpgLogic.SimpleMatrixInverseQuantization(jpgLogic.RedDifference_Cr, methodTxtBoxNumber, "Cr");
                    break;
                case 2:
                    jpgLogic.qualityFactorJpegQuantization(jpgLogic.Luminance, methodTxtBoxNumber, "Y");
                    jpgLogic.qualityFactorJpegQuantization(jpgLogic.BlueDifference_Cb, methodTxtBoxNumber, "Cb");
                    jpgLogic.qualityFactorJpegQuantization(jpgLogic.RedDifference_Cr, methodTxtBoxNumber, "Cr");

                    jpgLogic.qualityFactorJpegInverseQuantization(jpgLogic.Luminance, methodTxtBoxNumber, "Y");
                    jpgLogic.qualityFactorJpegInverseQuantization(jpgLogic.BlueDifference_Cb, methodTxtBoxNumber, "Cb"); ;
                    jpgLogic.qualityFactorJpegInverseQuantization(jpgLogic.RedDifference_Cr, methodTxtBoxNumber, "Cr");
                    break;
            }

            //TestImage(decodedPictureBox);

            jpgLogic.InverseDct(jpgLogic.Luminance, "Y");
            jpgLogic.InverseDct(jpgLogic.BlueDifference_Cb, "Cb");
            jpgLogic.InverseDct(jpgLogic.RedDifference_Cr, "Cr");

            decodedPictureBox.Image = jpgLogic.ConvertToRGB();
        }

        private void ErrorBttn_Click(object sender, EventArgs e)
        {
            double factor = Convert.ToDouble(factorTextBox.Text);

            Bitmap bmp = new Bitmap(256, 256);
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    byte R = (byte)((((Bitmap)originalPictureBox.Image).GetPixel(i, j).R - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).R) * factor);
                    int G = (byte)((((Bitmap)originalPictureBox.Image).GetPixel(i, j).G - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).G) * factor);
                    int B = (byte)((((Bitmap)originalPictureBox.Image).GetPixel(i, j).B - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).B) * factor);
                    bmp.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            errorPictureBox.Image = bmp;

            double mseR = 0.0, mseG = 0.0, mseB = 0.0;
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    double R = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).R - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).R), 2.0);
                    double G = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).G - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).G), 2.0);
                    double B = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).B - ((Bitmap)decodedPictureBox.Image).GetPixel(i, j).B), 2.0);
                    mseR += R; mseG += G; mseB += B;
                }

            mseR *= (1.0 / (256 * 256));
            mseG *= (1.0 / (256 * 256));
            mseB *= (1.0 / (256 * 256));

            richTextBox.AppendText("Mse_R: " + mseR + "\n");
            richTextBox.AppendText("Mse_G: " + mseG + "\n");
            richTextBox.AppendText("Mse_B: " + mseB + "\n\n");

            double psnrR = 0.0, psnrG = 0.0, psnrB = 0.0;
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    double R = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).R), 2.0);
                    double G = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).G), 2.0);
                    double B = Math.Pow((((Bitmap)originalPictureBox.Image).GetPixel(i, j).B), 2.0);
                    psnrR += R; psnrG += G; psnrB += B;
                }

            psnrR /= mseR; psnrG /= mseG; psnrB /= mseB;
            psnrR = (10 * Math.Log10(psnrR));
            psnrG = (10 * Math.Log10(psnrG));
            psnrB = (10 * Math.Log10(psnrB));

            richTextBox.AppendText("Psnr_R: " + psnrR + "\n");
            richTextBox.AppendText("Psnr_G: " + psnrG + "\n");
            richTextBox.AppendText("Psnr_B: " + psnrB + "\n\n");
        }

        private void DecodedPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            decodedCoords = new Coords(e.X, e.Y);
            Color[,] block = new Color[8, 8];
            int iy = decodedCoords.y / 8;
            int jx = decodedCoords.x / 8;
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    block[y, x] = ((Bitmap)decodedPictureBox.Image).GetPixel(iy + y, jx + x);
                }

            Bitmap bmp = new Bitmap(64, 64);
            for (int i = 0; i < 64; i += 8)
                for (int j = 0; j < 64; j += 8)
                {
                    for (int y = 0; y < 8; y++)
                        for (int x = 0; x < 8; x++)
                        {
                            bmp.SetPixel(i + y, j + x, block[i / 8 , j / 8]);
                        }
                }
            smallDecodePictureBox.Image = bmp;

        }

        private void OriginalPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            origCoords = new Coords(e.X, e.Y);

            Color[,] block = new Color[8, 8];
            int iy = origCoords.y / 8;
            int jx = origCoords.x / 8;
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    block[y, x] = ((Bitmap)originalPictureBox.Image).GetPixel(iy + y, jx + x);
                }

            Bitmap bmp = new Bitmap(64, 64);
            for (int i = 0; i < 64; i += 8)
                for (int j = 0; j < 64; j += 8)
                {
                    for (int y = 0; y < 8; y++)
                        for (int x = 0; x < 8; x++)
                        {
                            bmp.SetPixel(i + y, j + x, block[i/8, j/8]);
                        }
                }
            smallOrigPictureBox.Image = bmp;
        }
    }
}
