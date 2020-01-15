using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace JPEG_UI
{
    class JpegLogic
    {
        public double[,] Luminance;
        public double[,] BlueDifference_Cb, RedDifference_Cr;

        public void ConvertToYCbCr(PictureBox matrix)
        {
            double[,] CbCopy, CrCopy;
            Luminance = new double[256, 256];
            CbCopy = new double[256, 256];
            CrCopy = new double[256, 256];

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    byte R = ((Bitmap)matrix.Image).GetPixel(i, j).R;
                    byte G = ((Bitmap)matrix.Image).GetPixel(i, j).G;
                    byte B = ((Bitmap)matrix.Image).GetPixel(i, j).B;
                    var Y = (0.299 * R + 0.587 * G + 0.114 * B + 128);
                    var Cb = (-0.172 * R - 0.339 * G + 0.511 * B);
                    var Cr = (0.511 * R - 0.428 * G - 0.083 * B + 128);
                    this.Luminance[i, j] = Y;
                    CbCopy[i, j] = Cb;
                    CrCopy[i, j] = Cr;
                }
            }

            SubSamplingCbCr(CbCopy, CrCopy);
        }

        private void SubSamplingCbCr(double[,] CbCopy, double[,] CrCopy)
        {
            BlueDifference_Cb = new double[128, 128];
            RedDifference_Cr = new double[128, 128];

            int line = 0, col = 0;
            for (int i = 0; i < 256; i+=2)
            {
                for (int j = 0; j < 256; j+=2)
                {
                    var cb1 = CbCopy[i, j];
                    var cb2 = CbCopy[i + 1, j];
                    var cb3 = CbCopy[i, j + 1];
                    var cb4 = CbCopy[i + 1, j + 1];
                    var meancb = (cb1 + cb2 + cb3 + cb4) / 4.0;
                    this.BlueDifference_Cb[line, col] = meancb;

                    var cr1 = CrCopy[i, j];
                    var cr2 = CrCopy[i + 1, j];
                    var cr3 = CrCopy[i, j + 1];
                    var cr4 = CrCopy[i + 1, j + 1];
                    var meancr = (cr1 + cr2 + cr3 + cr4) / 4.0;
                    this.RedDifference_Cr[line, col] = meancr;
                    col++;
                }
                line++;
                col = 0;
            }
        }

        public Bitmap ConvertToRGB()
        {
            double[,] newCb, newCr;
            newCb = new double[256, 256];
            newCr = new double[256, 256];

            for (int i = 0; i < 256; i+=2)
                for (int j = 0; j < 256; j+=2)
                {
                    var cbval = this.BlueDifference_Cb[i/2, j/2];
                    newCb[i, j] = newCb[i + 1, j] = newCb[i, j + 1] = newCb[i + 1, j + 1] = cbval;

                    var crval = this.RedDifference_Cr[i/2, j/2];
                    newCr[i, j] = newCr[i + 1, j] = newCr[i, j + 1] = newCr[i + 1, j + 1] = crval;
                }

            Bitmap decodedImage = new Bitmap(256, 256);

            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    var Y = this.Luminance[i, j];
                    var Cb = newCb[i, j];
                    var Cr = newCr[i, j];
                    byte R = (byte)(Y + 0.000 * (Cb - 128) + 1.371 * (Cr - 128));
                    byte G = (byte)(Y - 0.336 * (Cb - 128) - 0.698 * (Cr - 128));
                    byte B = (byte)(Y + 1.732 * (Cb - 128) + 0.000 * (Cr - 128));
                    decodedImage.SetPixel(i, j, Color.FromArgb(R, G, B));
                }

            return decodedImage;
        }

        public void DCT(double[,] p, string type)
        {
            int length = p.GetLength(0);
            double[,] dctMatrix = new double[length, length];

            for (int i = 0; i < length; i += 8) 
            {
                for (int j = 0; j < length; j += 8) 
                {
                    double sum = 0.0;
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            sum += p[x + i, y + j] * (Math.Cos(((2 * x + 1) * i * Math.PI)) / 16.0) * (Math.Cos(((2 * y + 1) * j * Math.PI)) / 16.0);

                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            dctMatrix[x + i, y + j] = 0.25 * Cx(i) * Cx(j) * sum;
                }
            }

            switch (type)
            {
                case "Y": this.Luminance = dctMatrix; break;
                case "Cb": this.BlueDifference_Cb = dctMatrix; break;
                case "Cr": this.RedDifference_Cr = dctMatrix; break;
            }
        }

        public void InverseDct(double[,] dct, string type)
        {
            int length = dct.GetLength(0);
            double[,] normalMatrix = new double[length, length];

            for (int i = 0; i < length; i += 8)
            {
                for (int j = 0; j < length; j += 8)
                {
                    double sum = 0.0;
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            sum += (Cx(x) * Cx(y) * (Math.Cos(((2 * x + 1) * i * Math.PI)) / 16.0) * (Math.Cos(((2 * y + 1) * j * Math.PI)) / 16.0));

                    for (int x = 0; x < 8; x++) 
                        for (int y = 0; y < 8; y++) 
                        {
                            normalMatrix[x + i, y + j] = (0.25 * sum);
                            normalMatrix[x + i, y + j] = dct[x + i, y + j] / normalMatrix[x + i, y + j];
                        }
                }
            }

            switch (type)
            {
                case "Y": this.Luminance = normalMatrix; break;
                case "Cb": this.BlueDifference_Cb = normalMatrix; break;
                case "Cr": this.RedDifference_Cr = normalMatrix; break;
            }
        }

        private double Cx(int x)
        {
            if (x == 0)
                return 1.0 / Math.Sqrt(2.0);
            else 
                return 1.0;
        }

        public void zigZagQuantization(double[,] matrix, int Nr, string type)
        {
            int length = matrix.GetLength(0);

            double[,] newMat = new double[length, length];

            var zigIndexes = findIndexesZigZag(Nr, length);
            for (int i = 0; i < length; i += 8)
            {
                for (int j = 0; j < length; j += 8)
                {
                    List<List<int>> tempIndexes = new List<List<int>>(zigIndexes);
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                        {
                            foreach (var item in tempIndexes)
                                if (item[0] == x && item[1] == y)
                                {
                                    newMat[i + x, j + y] = matrix[i + x, j + y];
                                    tempIndexes.Remove(item);
                                    break;
                                }
                           
                        }
                }
            }

            switch (type)
            {
                case "Y": this.Luminance = newMat; break;
                case "Cb": this.BlueDifference_Cb = newMat; break;
                case "Cr": this.RedDifference_Cr = newMat; break;
            }
        }

        private List<List<int>> findIndexesZigZag(int Nr, int length)
        {
            int[,] zigzag =
            {
                {0,1,5,6,14,15,27,28 },
                {2,4,7,13,16,26,29,42 },
                {3,8,12,17,25,30,41,43 },
                {9,11,18,24,31,40,44,53 },
                {10,19,23,32,39,45,52,54 },
                {20,22,33,38,46,51,55,60 },
                {21,34,37,47,50,56,59,61 },
                {35,36,48,49,57,58,62,63 }
            };
            int nr = 0;
            List<List<int>> zigIndex = new List<List<int>>();

            while (nr - 1 != Nr)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (nr == zigzag[i, j])
                        {
                            List<int> lineColIndex = new List<int>();
                            lineColIndex.Add(i);
                            lineColIndex.Add(j);
                            zigIndex.Add(lineColIndex);
                            if (nr++ == Nr)
                                return zigIndex;
                            i = 0; j = 0;
                        }
            }
            return zigIndex;
        }

        public void SimpleMatrixQuantization(double[,] matrix, int R_quant, string type)
        {
            int length = matrix.GetLength(0);
            double[,] quantMatrix = new double[length, length];

            for (int i = 0; i < length; i += 8)
                for (int j = 0; j < length; j += 8)
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            quantMatrix[x + i, y + j] = matrix[x + i, j + y] + (1 + (x + y) * R_quant);

            switch (type)
            {
                case "Y": this.Luminance = quantMatrix; break;
                case "Cb": this.BlueDifference_Cb = quantMatrix; break;
                case "Cr": this.RedDifference_Cr = quantMatrix; break;
            }
        }

        public void SimpleMatrixInverseQuantization(double[,] matrix, int R_quant, string type)
        {
            int length = matrix.GetLength(0);
            double[,] invQuantMatrix = new double[length, length];

            for (int i = 0; i < length; i += 8)
                for (int j = 0; j < length; j += 8)
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            invQuantMatrix[x + i, y + j] = matrix[x + i, j + y] - (1 + (x + y) * R_quant);

            switch (type)
            {
                case "Y": this.Luminance = invQuantMatrix; break;
                case "Cb": this.BlueDifference_Cb = invQuantMatrix; break;
                case "Cr": this.RedDifference_Cr = invQuantMatrix; break;
            }
        }


        private int[,] getMatrixType(string type)
        {
            int[,] luminanceImplicit =
            {
                {16,11,10,16,24,40,51,61 },
                {12,12,14,19,26,58,60,55 },
                {14,13,16,24,40,57,69,56 },
                {14,17,22,29,51,87,80,62 },
                {18,22,37,56,68,109,103,77 },
                {24,35,55,64,81,104,113,92 },
                {49,64,78,87,103,121,120,101 },
                {72,92,95,98,112,100,103,99 }
            };

            int[,] crcbImplicit =
            {
                {17,18,24,47,99,99,99,99 },
                {18,21,26,66,99,99,99,99 },
                {24,26,56,99,99,99,99,99 },
                {47,66,99,99,99,99,99,99 },
                {99,99,99,99,99,99,99,99 },
                {99,99,99,99,99,99,99,99 },
                {99,99,99,99,99,99,99,99 },
                {99,99,99,99,99,99,99,99 }
            };
            switch (type)
            {
                case "Y": return luminanceImplicit; 
                case "Cb": return crcbImplicit; 
                case "Cr": return crcbImplicit;
            }

            int[,] non = { { 0, 0 } };
            return non;
        }

        public void qualityFactorJpegQuantization(double[,] matrix, int q_jpeg, string type)
        {
            
            int[,] q_implicit = new int[8, 8];
            q_implicit = getMatrixType(type);
           
            int length = matrix.GetLength(0);
            double[,] QuantMatrix = new double[length, length];

            for (int i = 0; i < length; i += 8)
                for (int j = 0; j < length; j += 8)
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                        {
                            if (q_jpeg >= 1 && q_jpeg <= 50)
                                QuantMatrix[i + x, j + y] = (50.0 / q_jpeg) * q_implicit[x, y];
                            else if (q_jpeg > 50 && q_jpeg <= 99)
                                QuantMatrix[i + x, j + y] = ((2.0 - q_jpeg) / 50.0) * q_implicit[x, y];
                            else if (q_jpeg >= 100)
                                QuantMatrix[i + x, y + j] = 1;
                            QuantMatrix[i + x, y + j] += matrix[i + x, j + y]; 
                        }

            switch (type)
            {
                case "Y": this.Luminance = QuantMatrix; break;
                case "Cb": this.BlueDifference_Cb = QuantMatrix; break;
                case "Cr": this.RedDifference_Cr = QuantMatrix; break;
            }
        }

        public void qualityFactorJpegInverseQuantization(double[,] matrix, int q_jpeg, string type)
        {
            int length = matrix.GetLength(0);
            double[,] QuantInverseMatrix = new double[length, length];
            int[,] q_implicit = new int[8, 8];
            q_implicit = getMatrixType(type);

            for (int i = 0; i < length; i += 8)
                for (int j = 0; j < length; j += 8)
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                        {
                            if (q_jpeg >= 1 && q_jpeg <= 50)
                                QuantInverseMatrix[i + x, j + y] = (50.0 / q_jpeg) * q_implicit[x, y];
                            else if (q_jpeg > 50 && q_jpeg <= 99)
                                QuantInverseMatrix[i + x, j + y] = ((2.0 - q_jpeg) / 50.0) * q_implicit[x, y];
                            else if (q_jpeg >= 100)
                                QuantInverseMatrix[i + x, y + j] = 1;
                            QuantInverseMatrix[i + x, y + j] = matrix[i + x, j + y] - QuantInverseMatrix[i + x, y + j];
                        }

            switch (type)
            {
                case "Y": this.Luminance = QuantInverseMatrix; break;
                case "Cb": this.BlueDifference_Cb = QuantInverseMatrix; break;
                case "Cr": this.RedDifference_Cr = QuantInverseMatrix; break;
            }
        }

    }
}
