using BitReaderWriter;

namespace ArithmeticCoding
{
    public class ArithmeticCodingLogic
    {
        private const uint EOF = 256, TOTAL_SYMBOLS = 257;

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
                uint symbol = DecodeSymbol();
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
                var isUnderFlow = (_high & 0xC0000000) == 0x80000000 && (_low & 0xC0000000) == 0x40000000;

                if (highFirstShift == lowFirstShift)
                {
                    writeToFile(highFirstShift);

                    _low <<= 1;
                    _high <<= 1;
                    _high |= 1;
                }
                else if (isUnderFlow)
                {
                    _underflowCounter++;
                    _low &= 0x3FFFFFFF;
                    _high |= 0x40000000;

                    _low <<= 1;
                    _high <<= 1;
                    _high |= 1;
                }
                else
                {
                    return;
                }
            }
        }

        private uint DecodeSymbol()
        {
            ulong range = (ulong)(_high - _low) + 1;
            var counts = (int)((((ulong)(_decodingValue - _low) + 1) * _sums[TOTAL_SYMBOLS] - 1) / range);
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
                var isUnderFlow = (_high & 0xC0000000) == 0x80000000 && (_low & 0xC0000000) == 0x40000000;

                if (highFirstShift == lowFirstShift)
                {
                    _low <<= 1;
                    _high <<= 1;
                    _high |= 1;
                    _decodingValue <<= 1;
                    _decodingValue |= GetBitsFromEncodedFile();
                }
                else if (isUnderFlow)
                {
                    _decodingValue ^= 0x40000000;
                    _low &= 0x3FFFFFFF; 
                    _high |= 0x40000000;

                    _low <<= 1;
                    _high <<= 1;
                    _high |= 1;
                    _decodingValue <<= 1;
                    _decodingValue |= GetBitsFromEncodedFile();
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
            var underflowBitsLeft = _low >> 30;
            writeToFile(underflowBitsLeft);
        }
    }
}
