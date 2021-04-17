using System;

namespace Predictor
{
    class ImagePredictor
    {
        public byte[,] _decod;
        public byte[,] _predInit, _pred2FromDecod;
        public int[,] _error, _errorPred, _errorPredQ, _errorPredDQ;
        public int _predictionNumber, _k;
        private byte _firstPixelValue, _upperLimit;

        public ImagePredictor(int k, byte firstPixelValue = 128, byte upperLimit = 255)
        {
            _k = k;
            _firstPixelValue = firstPixelValue;
            _upperLimit = upperLimit;
        }

        public void Encode(byte[,] original, int methodNumber)
        {
            this._predictionNumber = methodNumber;
            int rows = original.GetLength(0), cols = original.GetLength(1);
            
            _predInit = new byte[rows, cols];
            _decod = new byte[rows, cols];
            _error = new int[rows, cols];
            _errorPred = new int[rows, cols];
            _errorPredQ = new int[rows, cols];
            _errorPredDQ = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    ProcessEncoding(original[i, j], i, j, methodNumber);
                    _error[i, j] = original[i, j] - _decod[i, j];
                }
            }         
        }
        
        public void Decode(int[,] errorPredQ, int methodNumber)
        {
            this._predictionNumber = methodNumber;
            int rows = errorPredQ.GetLength(0), cols = errorPredQ.GetLength(1);
            
            _errorPredQ = errorPredQ;

            _pred2FromDecod = new byte[rows, cols];
            _decod = new byte[rows, cols];
            _errorPredDQ = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    ProcessDecoding(i, j, methodNumber);
                }
            }
        }

        private void ProcessEncoding(byte origValue, int row, int col, int methodNumber)
        {
            if (row == 0 && col == 0)
            {
                _predInit[row, col] = (byte)CompressJpegMethod(0);
            }
            else if (row == 0)
            {
                _predInit[row, col] = (byte)CompressJpegMethod(1, A: _decod[row, col - 1]);
            }
            else if (col == 0)
            {
                _predInit[row, col] = (byte)CompressJpegMethod(2, B: _decod[row - 1, col]);
            }
            else
            {
                byte A = _decod[row, col - 1];
                byte B = _decod[row - 1, col];
                byte C = _decod[row - 1, col - 1];
                _predInit[row, col] = LimitIntToByte(CompressJpegMethod(methodNumber, A, B, C));
            }

            _errorPred[row, col] = origValue - _predInit[row, col];
            _errorPredQ[row, col] = QuantizationRule(_errorPred[row, col]);
            _errorPredDQ[row, col] = DequantizationRule(_errorPredQ[row, col]);

            int decodSum = _predInit[row, col] + _errorPredDQ[row, col]; // predInit(used above) is same with pred2, use pred2 in decode proc.
            _decod[row, col] = LimitIntToByte(decodSum);
        }

        private void ProcessDecoding(int row, int col, int methodNumber)
        {
            if (row == 0 && col == 0)
            {
                _pred2FromDecod[row, col] = (byte)CompressJpegMethod(0);
            }
            else if (row == 0)
            {
                _pred2FromDecod[row, col] = (byte)CompressJpegMethod(1, A: _decod[row, col - 1]);
            }
            else if (col == 0)
            {
                _pred2FromDecod[row, col] = (byte)CompressJpegMethod(2, B: _decod[row - 1, col]);
            }
            else
            {
                byte A = _decod[row, col - 1];
                byte B = _decod[row - 1, col];
                byte C = _decod[row - 1, col - 1];
                _pred2FromDecod[row, col] = LimitIntToByte(CompressJpegMethod(methodNumber, A, B, C));
            }

            _errorPredDQ[row, col] = DequantizationRule(_errorPredQ[row, col]);
            
            int decodSum = _pred2FromDecod[row, col] + _errorPredDQ[row, col];
            _decod[row, col] = LimitIntToByte(decodSum);
        }

        private int QuantizationRule(int ep)
        {
            decimal res = ((decimal)ep + _k) / (2 * _k + 1);
            return (int)Math.Floor(res);
        }

        private int DequantizationRule(int epq)
        {
            return epq * (2 * _k + 1);
        }

        private int CompressJpegMethod(int methodNumber, int A = 0, int B = 0, int C = 0)
        {
            switch (methodNumber)
            {
                case 0: return _firstPixelValue;
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

        private byte LimitIntToByte(int number)
        {
            if (number <= 0)
            {
                return 0;
            }

            if (number >= _upperLimit)
            {
                return _upperLimit;
            }

            return (byte)number;
        }

    }
}