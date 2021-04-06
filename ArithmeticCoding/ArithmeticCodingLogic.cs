using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticCoding
{
    public class ArithmeticCodingLogic<T>
    {
        private uint _high, _low;
        private ulong _range;
        
        private List<uint> _sum;
        private List<Tuple<T, uint>> _counts;

        private int _underflowCounter;

        public ArithmeticCodingLogic()
        {
            _low = uint.MinValue;
            _high = uint.MaxValue;
            _range = ulong.MinValue;
            _counts = new List<Tuple<T, uint>>();
            _sum = new List<uint>();
            _underflowCounter = 0;
        }

        public uint Encode(T[] input)
        {
            CreateDictionaryAndSum(input);
            var output = default(uint);

            for(int i = 0; i < input.Length; i++)
            {
                var symbol = input[i];

                _range = ((ulong)_high - _low) + 1;
                _high = _low + (uint)((_range * GetSumAtSymbol(symbol, 1)) / _sum.Last() - 1);
                _low = _low + (uint)((_range * GetSumAtSymbol(symbol)) / _sum.Last());

                var firstShiftOutput = CheckFirstShift();
                if (firstShiftOutput != null)
                {
                    ShiftWithBitAdd(ref output, (uint)firstShiftOutput);
                }

                CheckSecondShift();
            }

            return output;
        }

        public T[] Decode(T[] input)
        {
            return new T[] { };
        }

        private uint? CheckFirstShift()
        {
            var highMSB = (_high & 0x80000000) >> 31;
            var lowMSB = (_low & 0x80000000) >> 31;

            if (highMSB == lowMSB)
            {
                ShiftWithBitAdd(ref _high, highMSB);
                ShiftWithBitAdd(ref _low, lowMSB);

                return lowMSB & highMSB;
            }

            return null;
        }

        private void CheckSecondShift()
        {
            var isHighUnderflow = ((_high & 0x80000000) >> 31) == 1 
                               && ((_high & 0x40000000) >> 30) == 0;
            var isLowUnderflow = ((_low & 0x80000000) >> 31) == 0
                               && ((_low & 0x40000000) >> 30) == 1;

            if (isHighUnderflow && isLowUnderflow)
            {
                ShiftWithBitAdd(ref _high, 1);
                ShiftWithBitAdd(ref _low, 0);

                _high |= 0x80000000; // set MSB to 1
                _low &= 0x7FFFFFFF; // set MSB to 0

                _underflowCounter++;
            }
        }

        private void ShiftWithBitAdd(ref uint number, uint bit)
        {
            number <<= 1;
            while (bit != 0)
            {
                uint carry = number & bit;
                number = number ^ bit;
                bit = carry << 1;
            }
        }

        private ulong GetSumAtSymbol(T element, int positionAdder = 0)
        {
            var index = _counts.IndexOf(_counts.First(x => x.Item1.Equals(element))) + positionAdder;
            return _sum[index];
        }

        private void CreateDictionaryAndSum(T[] input)
        {
            _counts = input.GroupBy(x => x)
                           .ToDictionary(y => y.Key, y => (uint)y.Count())
                           .Select(z => new Tuple<T, uint>(z.Key, z.Value)).ToList();
            
            for (var i = -1; i < _counts.Count; i++)
            {
                var sumOfPrecedentCounts = _counts.Where((elem, index) => index <= i).Sum(x => x.Item2);
                _sum.Add((uint)sumOfPrecedentCounts);
            }
        }
    }
}
