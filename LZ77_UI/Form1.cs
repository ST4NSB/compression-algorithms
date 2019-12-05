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
using LZ77;

namespace LZ77_UI
{ 
    public partial class Form1 : Form
    {
        BitReader bRead;
        BitWriter bWrite;
        Lz77 lz77Obj;
        FileInfo FilePath;

        public Form1()
        {
            InitializeComponent();
            lz77Obj = new Lz77();

            for (int i = 3; i <= 15; i++)
                offsetSizeBox.Items.Add(i);
            offsetSizeBox.SelectedIndex = 0;

            for (int i = 2; i <= 7; i++)
                lengthSizeBox.Items.Add(i);
            lengthSizeBox.SelectedIndex = 0;
        }

        private void EncodeLoadBttn_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                FilePath = new FileInfo(FD.FileName);
                log.AppendText("[Encode Load]File Path: " + FilePath.FullName + "\n");
            }
        }

        private List<uint> readBytesFromFilePath(int bitsSizeRead)
        {
            List<uint> values = new List<uint>();
            long fileSize = 8 * new FileInfo(FilePath.FullName).Length;
            log.AppendText("File Size: " + fileSize + " bits\n");
            do
            {
                int readBits = bitsSizeRead;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = bRead.ReadNBits(readBits);
                values.Add(value);
                fileSize -= readBits;
            } while (fileSize > 0);
            return values;
        }

        private void EncodeExecute_Click(object sender, EventArgs e)
        {
            bRead = new BitReader(FilePath.FullName);
            var readBytes = readBytesFromFilePath(8);
            bRead.Dispose();
            

            lz77Obj.SetOffsetSize(Convert.ToInt32(offsetSizeBox.Text));
            lz77Obj.SetLengthSize(Convert.ToInt32(lengthSizeBox.Text));

            byte[] byteArray = readBytes.Select(x => (byte)x).ToArray();
            var encodedList = lz77Obj.Encode(byteArray);

            if (encodeCheckBox.Checked)
            {
                foreach (var bytes in readBytes)
                    log.AppendText("[Encode]Read byte: " + bytes + "\n");
                log.AppendText("\n[Encoded]LZ77 tokens: \n");
                foreach (var token in encodedList)
                    log.AppendText("[Encoded]Token: (" + token.offset + ", " + token.length + ", " + token.symbol + ")\n");
                log.AppendText("\n");
            }

            string fileName = FilePath.Name + "." + offsetSizeBox.Text + "_" + lengthSizeBox.Text + ".LZ77";
            bWrite = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));

            bWrite.WriteNBits(4, Convert.ToUInt32(offsetSizeBox.Text));
            bWrite.WriteNBits(3, Convert.ToUInt32(lengthSizeBox.Text));

            foreach(var token in encodedList)
            {
                bWrite.WriteNBits(Convert.ToInt32(offsetSizeBox.Text), (uint)token.offset);
                bWrite.WriteNBits(Convert.ToInt32(lengthSizeBox.Text), (uint)token.length);
                bWrite.WriteNBits(8, (uint)token.symbol);
            }

            bWrite.WriteNBits(7, 1);
            bWrite.Dispose();
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

        private List<TokenTuple> readTokens(uint offsetSize, uint lengthSize)
        {
            List<TokenTuple> tokens = new List<TokenTuple>();
            FileInfo fileInfo = new FileInfo(FilePath.FullName);
            long fileSize = (8 * fileInfo.Length) - 7;
            log.AppendText("Tokens Size: " + fileSize + " bits\n");
            do
            {
                TokenTuple token = new TokenTuple();
                int readBits = (int)offsetSize;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = bRead.ReadNBits(readBits);
                token.offset = (int)value;
                fileSize -= readBits;
                readBits = (int)lengthSize;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                value = bRead.ReadNBits(readBits);
                token.length = (int)value;
                fileSize -= readBits;
                readBits = 8;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                value = bRead.ReadNBits(readBits);
                token.symbol = (byte)value;
                fileSize -= readBits;
               // if (fileSize >= 0)
                tokens.Add(token);
            } while (fileSize > 0 && fileSize >= ((int)offsetSize + (int)lengthSize + 8));
            return tokens;
        }

        private void DecodeExecute_Click(object sender, EventArgs e)
        {
            bRead = new BitReader(FilePath.FullName);
            uint offsetDecoded = bRead.ReadNBits(4);
            uint lengthDecoded = bRead.ReadNBits(3);
            var tokens = readTokens(offsetDecoded, lengthDecoded);
            bRead.Dispose();

            lz77Obj.SetOffsetSize((int)offsetDecoded);
            lz77Obj.SetLengthSize((int)lengthDecoded);

            var decodedCodes = lz77Obj.Decode(tokens);

            if (decodeCheckBox.Checked)
            {
                foreach (var token in tokens)
                    log.AppendText("[Decoded]Token: (" + token.offset + ", " + token.length + ", " + token.symbol + ")\n");
                log.AppendText("\n[Decoded]LZ77 codes: \n");
                foreach (var code in decodedCodes)
                    log.AppendText("[Decoded]Code: " + code + "\n");
                log.AppendText("\n");
            }

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

            string fileName = FilePath.Name + "." + offsetDecoded.ToString() + "_" + lengthDecoded.ToString() + ".LZW" + extension;
            bWrite = new BitWriter(Path.Combine(FilePath.DirectoryName, fileName));

            foreach (var code in decodedCodes)
                bWrite.WriteNBits(8, code);
            bWrite.Dispose();
        }

        private void ClrLog_Click(object sender, EventArgs e)
        {
            log.Clear();
        }
    }
}
