using System;
using System.Collections.Generic;

namespace LZ77
{
    public struct TokenTuple
    {
        public int offset;
        public int length;
        public byte symbol;
        public TokenTuple(int offset, int length, byte symbol)
        {
            this.offset = offset;
            this.length = length;
            this.symbol = symbol;
        }
        public void IncreaseLength()
        {
            this.length++;
        }
    }
		
    class Lz77
    {
        private int offsetSize;
        private int lengthSize;

        public void SetOffsetSize(int size)
        {
            offsetSize = (int)(Math.Pow(2, size) - 1);
        }

        public void SetLengthSize(int size)
        {
            lengthSize = (int)(Math.Pow(2, size) - 1);
        }

        public List<TokenTuple> Encode(byte[] input)
        {
            List<TokenTuple> tokens = new List<TokenTuple>();
            List<byte> lookAheadBuffer = new List<byte>(input);
            int lookAheadBufferLength = lookAheadBuffer.Count;

            int bufferOffset = 0;
            while (bufferOffset < lookAheadBufferLength)
            {
                List<int> characterApparitionsIndex = new List<int>();
                var character = lookAheadBuffer[bufferOffset];

                int k = 0;
                for (int i = bufferOffset - 1; i >= 0; i--)
                {
                    if (character == lookAheadBuffer[i])
                        characterApparitionsIndex.Add(k);
                    //k++;
                    if (++k > this.offsetSize)
                        break;
                }

                //Console.Write("bufferOffset: " + bufferOffset + " | caIndex: "); // debug
                //foreach (var item in characterApparitionsIndex) // debug
                //    Console.Write(item + " "); // debug
                //Console.WriteLine(""); // debug

                if (characterApparitionsIndex.Count.Equals(0))
                {
                    tokens.Add(new TokenTuple(0, 0, character));
                    bufferOffset++;
                }
                else
                {
                    Dictionary<TokenTuple, int> temporarTokensAndBufferOffset = new Dictionary<TokenTuple, int>();

                    foreach (var index in characterApparitionsIndex)
                    {
                        TokenTuple token = new TokenTuple(index, 0, 0);

                        int temporarOffset = bufferOffset;

                        if (temporarOffset == lookAheadBufferLength - 1)
                        {
                            token.symbol = lookAheadBuffer[temporarOffset];
                            temporarOffset++;
                        }
                        else
                        {
                            int actualIndex = bufferOffset - index - 1;
                            int apparitionNext = 0;

                            bool enteredWhile = false;
                            while (lookAheadBuffer[actualIndex + apparitionNext] == lookAheadBuffer[temporarOffset]
                                && token.length < this.lengthSize)
                            {
                                enteredWhile = true;
                                if (temporarOffset + 1 >= lookAheadBufferLength)
                                    break;
                                token.IncreaseLength();
                                temporarOffset++;
                                if (temporarOffset == lookAheadBufferLength)
                                {
                                    temporarOffset--;
                                    break;
                                }
                                apparitionNext++;
                            }

                            token.symbol = lookAheadBuffer[temporarOffset];

                            if (enteredWhile)
                            {
                                temporarOffset++;
                            }
                        }

                        temporarTokensAndBufferOffset.Add(token, temporarOffset);
                    }

                    TokenTuple maxToken = new TokenTuple(0, -1, 0);
                    foreach (var tokenAndOffset in temporarTokensAndBufferOffset)
                    {
                        if (tokenAndOffset.Key.length > maxToken.length)
                        {
                            maxToken = new TokenTuple(
                                tokenAndOffset.Key.offset,
                                tokenAndOffset.Key.length,
                                tokenAndOffset.Key.symbol);
                            bufferOffset = tokenAndOffset.Value;
                        }
                    }

                    tokens.Add(maxToken);

                }
                //foreach (var item in tokens) // debug
                //    Console.WriteLine("token: (" + item.offset + ", " + item.length + ", " + item.symbol + ")"); // debug

            }

            return tokens;
        }

        //public List<TokenTuple> Encode2(byte[] input)
        //{

        //}


        public List<byte> Decode(List<TokenTuple> tokens)
        {
            List<byte> sBuffer = new List<byte>();
            foreach (var token in tokens)
            {
                int size = token.length;
                int k = token.offset;
                List<byte> localBuffer = new List<byte>();
                while (size > 0)
                {
                    var elem = sBuffer[k];
                    localBuffer.Add(elem);
                    if (--k < 0) k = token.offset;
                    size--;
                }
                foreach (var item in localBuffer)
                    sBuffer.Insert(0, item);
                sBuffer.Insert(0, token.symbol);
            }

            sBuffer.Reverse();
            return sBuffer;
        }
    }

    class TesterInput
    {
        //char[] input = { 'a', 'b', 'r', 'a', 'c', 'a', 'd', 'a', 'b', 'r', 'a', 'd' };
        //char[] input2 = { 'a', 'b', 'a', 'b', 'a', 'b', 'c' };
        //char[] input3 = { 'a', 'b', 'a', 'b', 'b'};
        //char[] input4 = { 'a', 'b', 'a', 'b', 'e', 'e' };
        //char[] input5 = { 'a', 'b', 'a', 'b', 'h', 'h', 'h' };
        //char[] input6 = { 'a', 'b', 'a', 'b', 'u', 'u', 'u', 'u' };
        //char[] input7 = { 'a', 'b', 'a', 'b', 'j', 'j', 'j', 'j', 'j' };
        //char[] input8 = { 'j', 'j', 'j', 'j', 'j' ,'a', 'b', 'a', 'b' };
        //char[] input9 = { 'a', 'h', 'h', 'h', 'h', 'a' };
        //char[] input10 = { 'a', 'b', 'd', 'e', 'a', 'f', 'd', 'e', 'a', 'b' };
        //char[] input11 = { 'a', 'c', 'c', 'a', 'b', 'r', 'a', 'c', 'a', 'd'};
        //char[] input12 = { 'z', 'x', 'z', 'x', 'x' };
        //char[] input13 = { 'q', 'w', 'q', 'w', 'w', 'w' };

        public byte[] input = { 0, 1, 10, 0, 2, 0, 3, 0, 1, 10, 0, 3 };
        public byte[] input2 = { 0, 1, 0, 1, 0, 1, 2 };
        public byte[] input3 = { 0, 1, 0, 1, 1, 1 };
        public byte[] input4 = { 254, 255, 254, 255, 255 };
        public byte[] input5 = { 10, 11, 10, 11, 200, 200, 200 };
        public byte[] input6 = { 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 3 };
        public byte[] input7 = { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 9 };
    }

}