using System;

namespace Predictor
{
    class ImagePredictor
    {
        public byte[,] origDecodedMatrix;
        public byte[,] predictionMatrix;
        public int[,] errorMatrix;
        public int predictionNumber;

        private int Method(int methodNumber, int A = 0, int B = 0, int C = 0)
        {
            this.predictionNumber = methodNumber;
            switch (methodNumber)
            {
                case 0: return 128;
                case 1: return A;
                case 2: return B;
                case 3: return C;
                case 4: return A + B - C;
                case 5: return A + (B - C) / 2;
                case 6: return B + (A - C) / 2;
                case 7: return (A + B) / 2;
                case 8:
                    if (C >= Math.Max(A, B))
                        return Math.Min(A, B);
                    else if (C <= Math.Min(A, B))
                        return Math.Max(A, B);
                    return A + B + C;
                default: return -1;
            }
        }

        public void Encode(byte[,] original, int methodNumber)
        {
            int rows = original.GetLength(0), cols = original.GetLength(1);
            this.predictionMatrix = new byte[rows, cols];
            this.errorMatrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        this.predictionMatrix[i, j] = (byte)this.Method(0);
                    }
                    else if (i == 0)
                    {
                        int A = original[i, j - 1];
                        this.predictionMatrix[i, j] = (byte)this.Method(1, A: A);
                    }
                    else if (j == 0)
                    {
                        int B = original[i - 1, j];
                        this.predictionMatrix[i, j] = (byte)this.Method(2, B: B);
                    }
                    else
                    {
                        byte A = original[i, j - 1];
                        byte B = original[i - 1, j];
                        byte C = original[i - 1, j - 1];
                        this.predictionMatrix[i, j] = (byte)this.Method(methodNumber, A, B, C);
                    }
                    int error = original[i, j] - predictionMatrix[i,j];
                    this.errorMatrix[i, j] = error;
                }
            }         
        }
        
        public void Decode(int[,] errorMatrix, int methodName)
        {
            int rows = errorMatrix.GetLength(0), cols = errorMatrix.GetLength(1);
            this.errorMatrix = errorMatrix;
            this.predictionMatrix = new byte[rows, cols];
            this.origDecodedMatrix = new byte[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        predictionMatrix[i, j] = (byte)this.Method(0);
                    }
                    else if (i == 0)
                    {
                        int A = origDecodedMatrix[i, j - 1];
                        this.predictionMatrix[i, j] = (byte)this.Method(1, A: A);
                    }
                    else if (j == 0)
                    {
                        int B = origDecodedMatrix[i - 1, j];
                        this.predictionMatrix[i, j] = (byte)this.Method(2, B: B);
                    }
                    else
                    {
                        byte A = origDecodedMatrix[i, j - 1];
                        byte B = origDecodedMatrix[i - 1, j];
                        byte C = origDecodedMatrix[i - 1, j - 1];
                        this.predictionMatrix[i, j] = (byte)this.Method(methodName, A, B, C);
                    }
                    int sum = predictionMatrix[i, j] + errorMatrix[i, j];
                    origDecodedMatrix[i, j] = (byte)sum;
                }
        }

    }
}