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
using LZW;

namespace LZW_UI
{
    public partial class Form1 : Form
    {
        BitReader bitReader;
        BitWriter bitWriter;
        Lzw lzw;
        FileInfo FilePath;
        public Form1()
        {
            InitializeComponent();
            lzw = new Lzw();

            for (int i = 9; i <= 15; i++) 
                indexMenu.Items.Add(i); 
            indexMenu.SelectedIndex = 0; 

            methodType.Items.Add("Freeze"); 
            methodType.Items.Add("Empty");
            methodType.SelectedIndex = 0;
        }

        private void LoadBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                FilePath = new FileInfo(FD.FileName);
                log.AppendText("[Encode Load]File Path: " + FilePath.FullName + "\n");
            }
        }

        private List<uint> readBytesFromFilePath(int bitsSizeRead, bool method)
        {
            long fileSize;
            List<uint> values = new List<uint>();
            if (method) // encoding
            {
                fileSize = 8 * new FileInfo(FilePath.FullName).Length;
                log.AppendText("[Encode]File Size: " + fileSize + " bits\n");
                do
                {
                    int readBits = bitsSizeRead;
                    if (readBits > fileSize)
                    {
                        readBits = (int)fileSize;
                    }
                    uint value = bitReader.ReadNBits(readBits);
                    values.Add(value);
                    fileSize -= readBits;
                } while (fileSize > 0);
            }
            else // decoding
            {
                fileSize = 8 * new FileInfo(FilePath.FullName).Length - 5;
                log.AppendText("[Decode]File Size: " + fileSize + " bits\n");
                do
                {
                    int readBits = bitsSizeRead;
                    if (readBits > fileSize)
                    {
                        readBits = (int)fileSize;
                    }
                    uint value = bitReader.ReadNBits(readBits);
                    values.Add(value);
                    fileSize -= readBits;
                } while (fileSize >= bitsSizeRead); 
            }
            return values;
        }

        private void EncodeBttn_Click(object sender, EventArgs e)
        {
            bitReader = new BitReader(FilePath.FullName);
            var readBytes = readBytesFromFilePath(8, true);
            bitReader.Dispose();

           
            lzw.SetDictionarySize(Convert.ToInt32(indexMenu.Text));
            if (methodType.SelectedIndex == 0)
                lzw.SetDictionaryMethod(Lzw.FREEZE);
            else
                lzw.SetDictionaryMethod(Lzw.EMPTY);


            byte[] byteArray = readBytes.Select(x => (byte)x).ToArray();
            var encodedList = lzw.Encode(byteArray);
            
            if (codeCheckBox.Checked)
            {
               // foreach (var item in readBytes)
               //     log.AppendText(item + ",");
                log.AppendText("[Encoded]LZW codes: \n");
                foreach (var code in encodedList)
                    log.AppendText("[Encoded]Code: " + code + "\n");
                log.AppendText("\n");
            }

            string fileName = FilePath.Name + "." + methodType.Text + "_" + indexMenu.Text + ".LZW";
            bitWriter = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));

            bitWriter.WriteNBits(4, Convert.ToUInt32(indexMenu.Text));

            if (methodType.SelectedIndex == 0)
                bitWriter.WriteNBits(1, 1); // freeze
            else
                bitWriter.WriteNBits(1, 0); // empty

            foreach (var bytes in encodedList)
                bitWriter.WriteNBits(Convert.ToInt32(indexMenu.Text), (uint)bytes);
            bitWriter.WriteNBits(7, 1);
            bitWriter.Dispose();

        }

        private void DecodeLoadBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                FilePath = new FileInfo(FD.FileName);
                log.AppendText("[Decode Load]File Path: " + FilePath.FullName + "\n");
            }
        }

        private void DecodeBttn_Click(object sender, EventArgs e)
        {
            bitReader = new BitReader(FilePath.FullName);
            uint size = bitReader.ReadNBits(4);
            uint method = bitReader.ReadNBits(1);
            var readByteArray = readBytesFromFilePath((int)size, false);
            bitReader.Dispose();

            var byteList = readByteArray.Select(x => (int)x).ToList();

            lzw.SetDictionarySize((int)size);
            if (method == 1)
                lzw.SetDictionaryMethod(Lzw.FREEZE);
            else if (method == 0)
                lzw.SetDictionaryMethod(Lzw.EMPTY);
            else log.AppendText("ERROR at Decode!!!");

            var decodedCodes = lzw.Decode(byteList);

            if (decodeCheckBox.Checked)
            {
                log.AppendText("[Decoded]LZW codes: \n");
                foreach (var code in decodedCodes)
                    log.AppendText("[Decoded]Code: " + code + "\n");
                log.AppendText("\n");
            }

            string methodString = "";
            if (method == 1)
                methodString = "Freeze";
            else methodString = "Empty";

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

            string fileName = FilePath.Name + "." + methodString + "_" + size.ToString() + ".LZW" + extension;
            bitWriter = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));

            foreach (var code in decodedCodes)
                bitWriter.WriteNBits(8, code);
            bitWriter.Dispose();
        }

        private void ClearLogBttn_Click(object sender, EventArgs e)
        {
            log.Clear();
        }
    }
}
