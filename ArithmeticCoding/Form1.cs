using System;
using System.IO;
using System.Windows.Forms;

namespace ArithmeticCoding
{
    public partial class AppView : Form
    {
        private FileInfo _loadedFileInfo;
        private ArithmeticCodingLogic _arithmeticCodingLogic;

        public AppView()
        {
            InitializeComponent();
            resetControls();
        }

        private void resetControls(string withText = null)
        {
            _loadedFileInfo = null;
            _arithmeticCodingLogic = new ArithmeticCodingLogic();
            labelFilepath.Text = withText == null ? "No file loaded" : withText + " : No file loaded";
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
                _arithmeticCodingLogic.Encode(_loadedFileInfo.FullName);
                resetControls(withText: "File Encoded successfully");
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
                _arithmeticCodingLogic.Decode(_loadedFileInfo.FullName);
                resetControls(withText: "File Decoded successfully");
            }
            else
            {
                MessageBox.Show("Load a file first!");
            }
        }
    }
}
