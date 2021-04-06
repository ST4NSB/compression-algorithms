using BitReaderWriter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ArithmeticCoding
{
    public partial class AppView : Form
    {
        private FileInfo _loadedFileInfo;
        private ArithmeticCodingLogic<string> _arithmeticCodingLogic;
        private BitReader _bitReader;
        private BitWriter _bitWriter;

        public AppView()
        {
            InitializeComponent();
            reset();
        }

        private void reset()
        {
            _loadedFileInfo = null;
            _arithmeticCodingLogic = new ArithmeticCodingLogic<string>();
            labelFilepath.Text = "no file loaded";
        }

        private List<uint> readBytesFromFilePath(int bitsSizeRead)
        {
            List<uint> values = new List<uint>();
            long fileSize = 8 * new FileInfo(_loadedFileInfo.FullName).Length;
            do
            {
                int readBits = bitsSizeRead;
                if (readBits > fileSize)
                {
                    readBits = (int)fileSize;
                }
                uint value = _bitReader.ReadNBits(readBits);
                values.Add(value);
                fileSize -= readBits;
            } while (fileSize > 0);
            return values;
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                labelFilepath.Text = FD.FileName;
                _loadedFileInfo = new FileInfo(FD.FileName);
            }
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            if (_loadedFileInfo != null)
            {
                // read file
                //_bitReader = new BitReader(_loadedFileInfo.FullName);
                //var fileReadUints = readBytesFromFilePath(8);
                //_bitReader.Dispose();
                
                //// encode
                //byte[] fileByteArray = fileReadUints.Select(x => (byte)x).ToArray();
                //var encodedBytes = _arithmeticCodingLogic.Encode(fileByteArray);

                // write file

                reset();
            }
            else
            {
                MessageBox.Show("Load a file first!");
            }
        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            if (_loadedFileInfo != null)
            {
                // read file
                //_bitReader = new BitReader(_loadedFileInfo.FullName);
                //var fileReadUints = readBytesFromFilePath(8);
                //_bitReader.Dispose();

                //// encode
                //byte[] fileByteArray = fileReadUints.Select(x => (byte)x).ToArray();
                //var decodedBytes = _arithmeticCodingLogic.Decode(fileByteArray);

                // write file

                reset();
            }
            else
            {
                MessageBox.Show("Load a file first!");
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            string[] values = new string[] { "B", "C", "C", "A", "A", "A", "B" };
            var encodedBytes = _arithmeticCodingLogic.Encode(values);
            var x = 1;
        }
    }
}
