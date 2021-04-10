using BitReaderWriter;
using System;
using System.IO;

namespace ArithmeticCoding
{
    public class ArithmeticCodingLogic
    {
        private const uint TOTAL_SYMBOLS = 257; // ASCII range
        private const uint EOF = 256;

        private BitReader _bitReader;
        private BitWriter _bitWriter;

        private uint _high, _low, _underflowCounter, _decodingValue;
        private uint[] _counts, _sums;

        public ArithmeticCodingLogic()
        {
            _low = uint.MinValue;
            _high = uint.MaxValue;
            _underflowCounter = uint.MinValue;
            _decodingValue = uint.MinValue;

            _counts = new uint[TOTAL_SYMBOLS];
            _sums = new uint[TOTAL_SYMBOLS + 1];
            CreateDictionaryAndSum();
        }

        private void CreateDictionaryAndSum()
        {
            _sums[0] = 0;
            for (uint i = 0; i < TOTAL_SYMBOLS; i++)
            {
                _counts[i] = 1;
                _sums[i + 1] = _sums[i] + _counts[i];
            }
        }

        public void Encode(string inputFile, int bitsToRead = 8)
        {
            _bitReader = new BitReader(inputFile);
            string outputFileName = inputFile + ".ac";
            _bitWriter = new BitWriter(outputFileName);
            var fileSize = bitsToRead * inputFile.Length;
            
            do
            {
                int readBits = bitsToRead;
                if (readBits > fileSize)
                {
                    readBits = fileSize;
                }

                var symbol = _bitReader.ReadNBits(readBits);
                EncodeSymbol(symbol);
                UpdateModel(symbol);

                fileSize -= readBits;
            } while (fileSize > 0);
            
            _bitReader.Dispose();
            
            EncodeSymbol(EOF);
            FlushBuffer();
            _bitWriter.WriteNBits(7, 1);
            _bitWriter.Dispose();
        }

        public void Decode(string inputFile, int bitsToWrite = 8)
        {
            _bitReader = new BitReader(inputFile);
            var splittedInput = inputFile.Split('.');
            var extension = splittedInput[splittedInput.Length - 2];
            string outputFileName = inputFile + ".decoded." + extension;
            _bitWriter = new BitWriter(outputFileName);

            _decodingValue |= GetBitsFromEncodedFile(32);
            for (; ; )
            {
                uint symbol = DecodeSymbol(inputFile);
                if (symbol >= EOF) // eof
                {
                    break;
                }

                _bitWriter.WriteNBits(bitsToWrite, symbol);
                UpdateModel(symbol);
            }
            
            _bitReader.Dispose();
            _bitWriter.WriteNBits(7, 1);
            _bitWriter.Dispose();
        }

        private uint GetBitsFromEncodedFile(int bitsToRead = 1)
        {
            var bits = _bitReader.ReadNBits(bitsToRead);
            return bits;
        }

        private void EncodeSymbol(uint symbol)
        {
            ulong range = (ulong)(_high - _low) + 1;
            _high = _low + (uint)((range * _sums[symbol + 1]) / _sums[TOTAL_SYMBOLS] - 1);
            _low = _low +  (uint)((range * _sums[symbol]) / _sums[TOTAL_SYMBOLS]);

            for (; ; )
            {
                var highFirstShift = (_high & 0x80000000) >> 31;
                var lowFirstShift = (_low & 0x80000000) >> 31;

                var isHighUnderflow = ((_high & 0xC0000000) >> 30) == 2;
                var isLowUnderflow = ((_low & 0xC0000000) >> 30) == 1;

                if (highFirstShift == lowFirstShift)
                {
                    writeToFile(highFirstShift & lowFirstShift);

                    _high <<= 1;
                    _high |= highFirstShift;
                    _low <<= 1;
                    _low |= lowFirstShift;
                }
                else if (isHighUnderflow && isLowUnderflow)
                {
                    _underflowCounter += 1;

                    _high <<= 1;
                    _high |= highFirstShift;
                    _low <<= 1;
                    _low |= lowFirstShift;

                    _high |= 0x80000000; // set MSB to 1
                    _low &= 0x7FFFFFFF; // set MSB to 0
                }
                else
                {
                    break;
                }
            }
        }

        private uint DecodeSymbol(string inputFile)
        {
            ulong range = (ulong)(_high - _low) + 1;
            uint counts = (uint)((((ulong)(_decodingValue - _low) + 1) * _sums[TOTAL_SYMBOLS] - 1) / range);
            uint symbol;

            for (symbol = EOF; counts < _sums[symbol]; symbol--)
            {
                if (symbol <= 0)
                {
                    symbol = 0;
                    break;
                }
            }

            if (symbol >= EOF)
            {
                return EOF;
            }

            _high = _low + (uint)((range * _sums[symbol + 1]) / _sums[TOTAL_SYMBOLS] - 1);
            _low = _low + (uint)((range * _sums[symbol]) / _sums[TOTAL_SYMBOLS]);
            
            for (; ; )
            {
                var highFirstShift = (_high & 0x80000000) >> 31;
                var lowFirstShift = (_low & 0x80000000) >> 31;

                var isHighUnderflow = ((_high & 0xC0000000) >> 30) == 2;
                var isLowUnderflow = ((_low & 0xC0000000) >> 30) == 1;

                if (highFirstShift == lowFirstShift)
                {
                    _high <<= 1;
                    _high |= highFirstShift;
                    _low <<= 1;
                    _low |= lowFirstShift;

                    _decodingValue <<= 1;
                    _decodingValue |= GetBitsFromEncodedFile();
                }
                else if (isHighUnderflow && isLowUnderflow)
                {
                    _high <<= 1;
                    _high |= highFirstShift;
                    _low <<= 1;
                    _low |= lowFirstShift;

                    _decodingValue <<= 1;
                    _decodingValue |= GetBitsFromEncodedFile();

                    _high |= 0x80000000; // set MSB to 1
                    _low &= 0x7FFFFFFF; // set MSB to 0
                    _decodingValue ^= 0x80000000;
                }
                else
                {
                    break;
                }
            }

            return symbol;
        }

        private void writeToFile(uint value)
        {
            value = value & 0x00000001;
            _bitWriter.WriteNBits(1, value);

            while (_underflowCounter > 0)
            {
                uint negated = (uint)(value == 0x00000001 ? 0x00000000 : 0x00000001);

                _bitWriter.WriteNBits(1, negated);
                _underflowCounter--;
            }						
        }

        private void UpdateModel(uint symbol)
        {
            _counts[symbol]++;         
            while (true)
            {
                symbol++;
                if (symbol <= 0 || symbol > TOTAL_SYMBOLS)
                {
                    break;
                }
                _sums[symbol]++;
            }
        }

        private void FlushBuffer()
        {
            _underflowCounter++;
            var firstQuarter = _low >> 31;
            writeToFile(firstQuarter);
        }
    }
}
