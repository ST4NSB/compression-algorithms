using BitReaderWriter;
using System.IO;

namespace ArithmeticCoding
{
    public class ArithmeticCodingLogic
    {
        public BitReader _bitReader;
        private BitWriter _bitWr;

        private uint _eof, _totalSymbols;
        private uint _high, _low, _underflowCounter;
        public uint _decodingValue;
        private uint[] _counts, _sums;

        public ArithmeticCodingLogic(BitWriter bitWriterContext = null, BitReader bitReaderContext = null, uint total_symbols = 257, uint eof = 256)
        {
            _totalSymbols = total_symbols;
            _eof = eof;

            _low = uint.MinValue;
            _high = uint.MaxValue;
            _underflowCounter = uint.MinValue;
            _decodingValue = uint.MinValue;

            _counts = new uint[_totalSymbols];
            _sums = new uint[_totalSymbols + 1];

            _bitReader = bitReaderContext;
            _bitWr = bitWriterContext;
            CreateDictionaryAndSum();
        }

        private void CreateDictionaryAndSum()
        {
            _sums[0] = 0;
            for (uint i = 0; i < _totalSymbols; i++)
            {
                _counts[i] = 1;
                _sums[i + 1] = _sums[i] + _counts[i];
            }
        }

        public void Encode(string inputFile, int bitsToRead = 8)
        {
            _bitReader = new BitReader(inputFile);
            string outputFileName = inputFile + ".ac";
            _bitWr = new BitWriter(outputFileName);
            var fileSize = bitsToRead * new FileInfo(inputFile).Length; // !!!!!!!!!!!!!!!! very important, no inputFile.length
            
            do
            {
                int readBits = bitsToRead;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }

                var symbol = _bitReader.ReadNBits(readBits);
                EncodeSymbol(symbol);
                UpdateModel(symbol);

                fileSize -= readBits;
            } while (fileSize > 0);
            
            _bitReader.Dispose();
            
            EncodeSymbol(_eof);
            FlushBuffer();
            _bitWr.WriteNBits(7, 1);
            _bitWr.Dispose();
        }

        public void Decode(string inputFile, int bitsToWrite = 8)
        {
            _bitReader = new BitReader(inputFile);
            var splittedInput = inputFile.Split('.');
            var extension = splittedInput[splittedInput.Length - 2];
            string outputFileName = inputFile + ".decoded." + extension;
            _bitWr = new BitWriter(outputFileName);

            _decodingValue = GetBitsFromEncodedFile(32);
            for (; ; )
            {
                uint symbol = DecodeSymbol();
                if (symbol >= _eof) // eof
                {
                    break;
                }

                _bitWr.WriteNBits(bitsToWrite, symbol);
                UpdateModel(symbol);
            }
            
            _bitReader.Dispose();
            _bitWr.WriteNBits(7, 1);
            _bitWr.Dispose();
        }

        public void EncodeImageErrorValue(uint symbol)
        {
            EncodeSymbol(symbol);
            UpdateModel(symbol);
        }

        public void SendLastDetailsOfImageError()
        {
            EncodeSymbol(_eof);
            FlushBuffer();
            _bitWr.WriteNBits(7, 1);
            _bitWr.Dispose();
        }

        private uint GetBitsFromEncodedFile(int bitsToRead = 1)
        {
            var bits = _bitReader.ReadNBits(bitsToRead);
            return bits;
        }

        private void EncodeSymbol(uint symbol)
        {
            ulong range = (ulong)(_high - _low) + 1;
            _high = _low + (uint)((range * _sums[symbol + 1]) / _sums[_totalSymbols] - 1);
            _low = _low +  (uint)((range * _sums[symbol]) / _sums[_totalSymbols]);

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

        public uint DecodeSymbol()
        {
            ulong range = (ulong)(_high - _low) + 1;
            var counts = (uint)(((ulong)(_decodingValue - _low + 1) * _sums[_totalSymbols] - 1) / range);
            uint symbol;

            for (symbol = _eof; counts < _sums[symbol]; symbol--)
            {
                if (symbol == 0)
                {
                    break;
                }
            }

            if (symbol == _eof)
            {
                return _eof;
            }

            _high = _low + (uint)((range * _sums[symbol + 1]) / _sums[_totalSymbols] - 1);
            _low = _low + (uint)((range * _sums[symbol]) / _sums[_totalSymbols]);
            
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
            _bitWr.WriteNBits(1, value);

            uint negated = (uint)(value == 0x00000001 ? 0x00000000 : 0x00000001);
            while (_underflowCounter > 0)
            {
                _bitWr.WriteNBits(1, negated);
                _underflowCounter--;
            }						
        }

        public void UpdateModel(uint symbol)
        {
            _counts[symbol]++;
            while (true)
            {
                symbol++;
                if (symbol > _totalSymbols)
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
