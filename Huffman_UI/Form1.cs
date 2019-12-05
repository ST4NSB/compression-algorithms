using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitReaderWriter;
using HuffmanAlgorithm;

namespace Huffman_UI
{
    public partial class Form1 : Form
    {
        BitReader bRead;
        BitWriter bWrite;
        Huffman huff;
        FileInfo FilePath;

        public Form1()
        {
            InitializeComponent();
            huff = new Huffman();
        }

        private void LoadEncodeBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                FilePath = new FileInfo(FD.FileName);
                log.AppendText("[Encode Load]File Path: " + FilePath.FullName + "\n");
            }
        }

        private void LoadDecodeBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                FilePath = new FileInfo(FD.FileName);
                log.AppendText("[Decode Load]File Path: " + FilePath.FullName + "\n");
            }
        }

        private void ClrLogBttn_Click(object sender, EventArgs e)
        {
            log.Clear();
        }

        private void EncTxtBoxBttn_Click(object sender, EventArgs e)
        {
            byte[] textBoxContentBytes = Encoding.ASCII.GetBytes(encTxtBox.Text);

            List<byte> huffCodes = huff.Encode(textBoxContentBytes);

            if (logCheckBox.Checked)
            {
                var modelCharacters = huff.GetModel();
                foreach (var item in new SortedDictionary<byte, List<byte>>(modelCharacters))
                {
                    log.AppendText(item.Key + ": ");
                    foreach (var code in item.Value)
                        log.AppendText(code.ToString());
                    log.AppendText("\n");
                }
            }

            string fileName = "InputArea.hs";
            bWrite = new BitWriter(fileName);

            var statistic = huff.GetStatistic();
            var sortedStatistic = new SortedDictionary<byte, int>(statistic);
            for (int i = 0; i <= 255; i++)
            {
                if (sortedStatistic.ContainsKey((byte)i))
                    bWrite.WriteNBits(1, 1);
                else
                    bWrite.WriteNBits(1, 0);
            }

            foreach (var item in sortedStatistic)
            {
                bWrite.WriteNBits(8, (uint)item.Value);
            }

            foreach(var code in huffCodes)
            {
                bWrite.WriteNBits(8, code); 
            }

            bWrite.WriteNBits(7, 1);
            bWrite.Dispose();
        }

        private void DecFileBttn_Click(object sender, EventArgs e)
        {
            bRead = new BitReader(FilePath.FullName);

            List<byte> statsList = new List<byte>();
            Dictionary<byte, int> statistic = new Dictionary<byte, int>();

            for (int i = 0; i < 256; i++)
            {
                uint value = bRead.ReadNBits(1);
                if (value == 1)
                    statsList.Add((byte)i);
            }

            int length = statsList.Count;
            for (int i = 0; i < length; i++)
            {
                uint nr = bRead.ReadNBits(8);
                statistic.Add(statsList[i], (int)nr);
            }

            List<byte> codedValues = new List<byte>();
            long fileSize = 8 * new FileInfo(FilePath.FullName).Length - (256 + (length * 8));
            log.AppendText("File Size: " + fileSize + " bits\n");
            do
            {
                int readBits = 8; 
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = bRead.ReadNBits(readBits);
                codedValues.Add(Convert.ToByte(value));
                fileSize -= readBits;
            } while (fileSize > 0);

            bRead.Dispose();

            var decodedValues = huff.Decode(codedValues, statistic);

            if (FilePath.Name.Equals("InputArea.hs"))
            {
                string fileName = "InputArea.txt";
                bWrite = new BitWriter(fileName);
            }
            else
            {
                string extension = "";
                bool foundDot = false;
                foreach (char c in FilePath.Name)
                {
                    if (c == '.' && !foundDot)
                        foundDot = true;
                    else if (c == '.' && foundDot)
                        break;
                    if (foundDot)
                        extension += c;
                }

                string fileName = FilePath.Name + extension;
                bWrite = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));
            }

            foreach (var item in decodedValues)
            {
                bWrite.WriteNBits(8, item);
            }
            bWrite.Dispose();
        }

        private void EncFileBttn_Click(object sender, EventArgs e)
        {
            List<byte> inputList = new List<byte>();
            bRead = new BitReader(FilePath.FullName);
            long fileSize = 8 * new FileInfo(FilePath.FullName).Length;
            log.AppendText("File Size: " + fileSize + " bits\n");
            do
            {
                int readBits = 8;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = bRead.ReadNBits(readBits);
                inputList.Add(Convert.ToByte(value));
                fileSize -= readBits;
            } while (fileSize > 0);

            bRead.Dispose();

            List<byte> huffCodes = huff.Encode(inputList.ToArray());

            if (logCheckBox.Checked)
            {
                var modelCharacters = huff.GetModel();
                foreach (var item in new SortedDictionary<byte, List<byte>>(modelCharacters))
                {
                    log.AppendText(item.Key + ": ");
                    foreach (var code in item.Value)
                        log.AppendText(code.ToString());
                    log.AppendText("\n");
                }
            }

            string fileName = FilePath.Name + ".hs";
            bWrite = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));

            var statistic = huff.GetStatistic();
            var sortedStatistic = new SortedDictionary<byte, int>(statistic);
            for (int i = 0; i <= 255; i++)
            {
                if (sortedStatistic.ContainsKey((byte)i))
                    bWrite.WriteNBits(1, 1);
                else
                    bWrite.WriteNBits(1, 0);
            }

            foreach (var item in sortedStatistic)
            {
                bWrite.WriteNBits(8, (uint)item.Value);
            }

            foreach (var code in huffCodes)
            {
                bWrite.WriteNBits(8, code); 
            }

            bWrite.WriteNBits(7, 1);
            bWrite.Dispose();
        }
    }
}
