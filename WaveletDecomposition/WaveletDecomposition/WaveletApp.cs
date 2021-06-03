using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaveletDecomposition
{
    public partial class WaveletApp : Form
    {
        private const int _imageLength = 512;
        private FileInfo _filePath;
        private WaveletDecompositionLogic _waveletDecompositionLogic;
        private float[,] _origImage, _imageWaveletProcessed;
        private int _stageLevel, _currentLevelSize = _imageLength;

        public WaveletApp()
        {
            InitializeComponent();
            _waveletDecompositionLogic = new WaveletDecompositionLogic();
            comboBoxCompression.Items.AddRange(new string[] {
                "1", "2", "3", "4", "5"
            });
            comboBoxCompression.SelectedIndex = 4;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void bttnLoad_Click(object sender, EventArgs e)
        {
            _origImage = new float[_imageLength, _imageLength];
            _imageWaveletProcessed = new float[_imageLength, _imageLength];

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Bitmap files (*.bmp)|*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                _filePath = new FileInfo(fileName);
                var img = Image.FromFile(fileName);
                imageOrig.Image = new Bitmap(img);
                img.Dispose();

                // load image
                for (int i = 0; i < _imageLength; i++)
                {
                    for (int j = 0; j < _imageLength; j++)
                    {
                        _origImage[i, j] = ((Bitmap)imageOrig.Image).GetPixel(i, j).R;
                        _imageWaveletProcessed[i, j] = _origImage[i, j];
                    }
                }
            }
        }

        private void bttnAnalysis_Click(object sender, EventArgs e)
        {
            _stageLevel = 1;
            var level = int.Parse(comboBoxCompression.Text);
            while (_stageLevel <= level)
            {
                AnalysisPhase(_stageLevel);
                _stageLevel++;
            }
        }

        private void bttnSynthesis_Click(object sender, EventArgs e)
        {
            _stageLevel = 1;
            var level = int.Parse(comboBoxCompression.Text);
            while (_stageLevel <= level)
            {
                SynthesisPhase(level);
                level--;
            }
        }


        private void AnalysisPhase(int level)
        {
            int levelBound = (int)(Math.Pow(2, (level - 1)));
            int nextLevel = (int)(Math.Pow(2, (level)));

            _currentLevelSize = _imageLength / nextLevel;
            textBoxY.Text = _currentLevelSize.ToString();
            textBoxX.Text = _currentLevelSize.ToString();

            // processing Analysis Horizontal
            var tempImage = new float[_imageLength / levelBound, _imageLength / levelBound];
            for (int lineIndex = 0; lineIndex < (_imageLength / levelBound); lineIndex++)
            {
                float[] lineToProcess = new float[_imageLength / levelBound];
                for (int i = 0; i < (_imageLength / levelBound); i++)
                {
                    lineToProcess[i] = _imageWaveletProcessed[lineIndex, i];
                }
                var resLine = _waveletDecompositionLogic.AnalysisH(lineToProcess, _imageLength / levelBound);
                for (int i = 0; i < resLine.Count; i++)
                {
                    tempImage[lineIndex, i] = resLine[i];
                }
            }

            for (int i = 0; i < _imageLength / levelBound; i++)
            {
                for (int j = 0; j < _imageLength / levelBound; j++)
                {
                    _imageWaveletProcessed[i, j] = tempImage[i, j];
                }
            }

            // processing Analysis Vertical
            tempImage = new float[_imageLength / levelBound, _imageLength / levelBound];
            for (int colIndex = 0; colIndex < _imageLength / levelBound; colIndex++)
            {
                float[] colToProcess = new float[_imageLength / levelBound];
                for (int i = 0; i < (_imageLength / levelBound); i++)
                {
                    colToProcess[i] = _imageWaveletProcessed[i, colIndex];
                }
                var resLine = _waveletDecompositionLogic.AnalysisV(colToProcess, _imageLength / levelBound);
                for (int i = 0; i < resLine.Count; i++)
                {
                    tempImage[i, colIndex] = resLine[i];
                }
            }

            for (int i = 0; i < _imageLength / levelBound; i++)
            {
                for (int j = 0; j < _imageLength / levelBound; j++)
                {
                    _imageWaveletProcessed[i, j] = tempImage[i, j];
                }
            }
        }

        private void SynthesisPhase(int level)
        {
            int levelBound = (int)(Math.Pow(2, (level - 1)));

            _currentLevelSize = _imageLength / levelBound;
            textBoxY.Text = _currentLevelSize.ToString();
            textBoxX.Text = _currentLevelSize.ToString();

            // processing Synthesis Vertical
            var tempImage = new float[_imageLength / levelBound, _imageLength / levelBound];
            for (int colIndex = 0; colIndex < _imageLength / levelBound; colIndex++)
            {
                float[] colToProcess = new float[_imageLength / levelBound];
                for (int i = 0; i < (_imageLength / levelBound); i++)
                {
                    colToProcess[i] = _imageWaveletProcessed[i, colIndex];
                }
                var resLine = _waveletDecompositionLogic.SynthesisV(colToProcess, _imageLength / levelBound);
                for (int i = 0; i < resLine.Count; i++)
                {
                    tempImage[i, colIndex] = resLine[i];
                }
            }

            for (int i = 0; i < _imageLength / levelBound; i++)
            {
                for (int j = 0; j < _imageLength / levelBound; j++)
                {
                    _imageWaveletProcessed[i, j] = tempImage[i, j];
                }
            }

            // processing Synthesis Horizontal
            tempImage = new float[_imageLength / levelBound, _imageLength / levelBound];
            for (int lineIndex = 0; lineIndex < (_imageLength / levelBound); lineIndex++)
            {
                float[] lineToProcess = new float[_imageLength / levelBound];
                for (int i = 0; i < (_imageLength / levelBound); i++)
                {
                    lineToProcess[i] = _imageWaveletProcessed[lineIndex, i];
                }
                var resLine = _waveletDecompositionLogic.SynthesisH(lineToProcess, _imageLength / levelBound);
                for (int i = 0; i < resLine.Count; i++)
                {
                    tempImage[lineIndex, i] = resLine[i];
                }
            }

            for (int i = 0; i < _imageLength / levelBound; i++)
            {
                for (int j = 0; j < _imageLength / levelBound; j++)
                {
                    _imageWaveletProcessed[i, j] = tempImage[i, j];
                }
            }
        }

        private void ShowWaveletImage()
        {
            if (int.Parse(textBoxX.Text) < _currentLevelSize)
            {
                textBoxX.Text = _currentLevelSize.ToString();
            }

            if (int.Parse(textBoxY.Text) < _currentLevelSize)
            {
                textBoxY.Text = _currentLevelSize.ToString();
            }


            Bitmap bmp = new Bitmap(_imageLength, _imageLength);
            for (int i = 0; i < _imageLength; i++)
            {
                for (int j = 0; j < _imageLength; j++)
                {
                    int value = 0;
                    if (i < int.Parse(textBoxY.Text) && j < int.Parse(textBoxX.Text))
                    {
                        var rawValue = Math.Abs((int)_imageWaveletProcessed[i, j]);
                        value = rawValue > 255 ? 255 : rawValue;
                    }
                    else
                    {
                        var rawValue = (int)(double.Parse(textBoxScale.Text) * _imageWaveletProcessed[i, j] + int.Parse(textBoxOffset.Text));
                        rawValue = Math.Abs(rawValue);
                        value = rawValue > 255 ? 255 : rawValue;
                    }

                    bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            }

            imageWavelet.Image = bmp;
        }

        private void bttnVizualise_Click(object sender, EventArgs e)
        {
            ShowWaveletImage();
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            var fileName = _filePath.FullName + ".wvl";
            using (FileStream file = File.Create(fileName))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    for (int i = 0; i < _imageLength; i++)
                    {
                        for (int j = 0; j < _imageLength; j++)
                        {
                            writer.Write(_imageWaveletProcessed[i, j]);
                        }
                    }

                }
            }
        }

        private void bttnLoadWv_Click(object sender, EventArgs e)
        {
            _imageWaveletProcessed = new float[_imageLength, _imageLength];

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wavelet files (*.wvl)|*.wvl";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                _filePath = new FileInfo(fileName);
                using (FileStream fs = File.OpenRead(open.FileName))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int i = 0, j = 0;
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            _imageWaveletProcessed[i, j] = reader.ReadSingle();
                            j++;

                            if (j >= _imageLength)
                            {
                                i++;
                                j = 0;

                                if (i >= _imageLength)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                ShowWaveletImage();
            }
        }

        private void bttnError_Click(object sender, EventArgs e)
        {
            if (_origImage == null || _imageWaveletProcessed == null)
            {
                return;
            }

            int max = int.MinValue, min = int.MaxValue;

            for(int i = 0; i < _imageLength; i++)
            {
                for(int j = 0; j < _imageLength; j++)
                {
                    var errorValue = (int)(_origImage[i, j] - Math.Round(_imageWaveletProcessed[i, j]));

                    if (errorValue > max)
                    {
                        max = errorValue;
                    }

                    if (errorValue < min)
                    {
                        min = errorValue;
                    }
                }
            }

            labelMax.Text = "Max: " + max;
            labelMin.Text = "Min: " + min;
        }
    }
}
