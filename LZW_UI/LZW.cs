using System;
using System.Collections.Generic;
using System.Linq;

namespace LZW
{
    class Lzw
    {
        public const bool FREEZE = false, EMPTY = true;
        private int dictionarySize;
        private bool dictionaryMethod;

        public void SetDictionarySize(int size)
        {
            dictionarySize = (int)(Math.Pow(2, size) - 1);
        }

        public void SetDictionaryMethod(bool dictionaryMethod)
        {
            this.dictionaryMethod = dictionaryMethod;
        }

        private int IsInTable(List<string> dictionary, string searchList)
        {
            int index = dictionary.IndexOf(searchList); // returnare -1 daca nu gaseste
            return index;
        }

        //private int IsInTable(List<string> dictionary, string searchList)
        //{
        //    string[] searchWords = searchList.Split(',');
        //    for (int item = 0; item < dictionary.Count; item++)
        //    {
        //        string[] dictionaryWords = dictionary[item].Split(',');
        //        if (dictionaryWords.Length != searchWords.Length) // daca nu sunt de aceeasi dim. treci mai departe
        //            continue;

        //        bool found = true;
        //        for (int sameIterator = 0; sameIterator < searchWords.Length; sameIterator++)
        //            if (dictionaryWords[sameIterator] != searchWords[sameIterator]) // se verifica elementu de la aceelas index
        //                found = false;

        //        if (found)
        //            return item;
        //    }
        //    return -1;
        //}

        private void loadStaticDictionary(List<string> dictionary)
        {
            for (byte k = 0; k < 256; k++)
            {
                dictionary.Add(k.ToString());
                if (k + 1 == 256) // eroare la k++ cand k == 255
                    break;
            }
        }

        public List<int> Encode(byte[] input)
        {
            List<string> dictionary = new List<string>();
            List<int> codeIndex = new List<int>();
            loadStaticDictionary(dictionary);

            int i = 0;
            while (true)
            {
                string searchString = ""; // o lista care va va verfiica in dictionar - ex. [1] -> [1 2] -> [1 2 6] ...
                byte element = input[i];
                searchString += element.ToString(); // primu element (pt. 65[A] -> searchlist = [65] .. dupa pt b, searchlist = [65 66] ...

                bool found = true;
                int code = -1;

                bool canLeave = false;
                while (found)
                {
                    int copyCode = -1;
                    if ((copyCode = IsInTable(dictionary, searchString)) != -1) 
                    {
                        // code = IsInTable(dictionary, searchString);
                        code = copyCode;
                        if (i + 1 < input.Length)
                        {
                            i++;
                        }
                        else
                        {
                            canLeave = true;
                            break;
                        }

                        element = input[i];
                        searchString += ',';
                        searchString += element; // aici deobicei se adauga al doilea element sau al n-lea .. ex. searchlist = [65 66 ..]
                    }
                    else
                    {
                        found = false;
                    }
                }

                codeIndex.Add(code); // adauga indexu de cod

                if (dictionary.Count >= this.dictionarySize)
                {
                    // freeze nu schimba nimic decat sa nu lase sa intre in conditia else dictionary.add(searchstring)
                    if (this.dictionaryMethod == EMPTY)
                    {
                        dictionary = new List<string>();
                        loadStaticDictionary(dictionary);
                    }
                }
                else
                    dictionary.Add(searchString); // adauga la dictionar searchlistu  

                if (canLeave)
                    break;
            }

            //for (int tk = 0; tk < dictionary.Count; tk++)
            //{
            //    Console.Write(tk + ": ");
            //    foreach (var y in dictionary[tk])
            //        Console.Write(y);
            //    Console.WriteLine();
            //}
            return codeIndex;
        }


        public List<byte> Decode(List<int> codesInput)
        {
            List<string> dictionary = new List<string>();
            List<byte> decodedCodes = new List<byte>();
            loadStaticDictionary(dictionary);

            int index = 0;
            int codeIndex = codesInput[index];
            decodedCodes.Add(Convert.ToByte(dictionary[codeIndex]));
            index++;
            while(true)
            {
                string elems = "";
                codeIndex = codesInput[index - 1];
                string[] dicWords = dictionary[codeIndex].Split(',');
                foreach (string item in dicWords)
                {
                    elems += item;
                    if (codesInput.Count > 0)
                        elems += ',';
                }

                if (codesInput[index] < dictionary.Count)
                {
                    codeIndex = codesInput[index];
                    dicWords = dictionary[codeIndex].Split(',');
                    elems += dicWords[0];
                }
                else
                {
                    codeIndex = codesInput[index - 1];
                    dicWords = dictionary[codeIndex].Split(',');
                    elems += dicWords[0];
                }

                if (dictionary.Count >= this.dictionarySize)
                {
                    if (this.dictionaryMethod == EMPTY)
                    {
                        dictionary = new List<string>();
                        loadStaticDictionary(dictionary);
                    }
                }
                else
                    dictionary.Add(elems);

                codeIndex = codesInput[index];
                dicWords = dictionary[codeIndex].Split(',');
                foreach(var item in dicWords)
                    decodedCodes.Add(Convert.ToByte(item));

                if (++index >= codesInput.Count)
                    break;
            }
            return decodedCodes;
        }
    }
    

    class TesterInput
    {
        public byte[] input = { 65, 66, 67, 68, 65, 66, 67, 68, 65, 66, 68, 67, 65, 66, 67, 68, 65, 66, 68, 68, 67, 68};
        public byte[] input2 = { 68, 65, 66, 66, 66, 66, 68, 66, 66, 66, 90, 66, 68, 65, 69 };
        public byte[] input3 = { 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1 };
        public byte[] input4 = { 1, 2, 3, 1, 2, 3, 1, 1, 5 };
        public byte[] input5 = { 0, 0, 0, 0, 0, 0 };
    }
}