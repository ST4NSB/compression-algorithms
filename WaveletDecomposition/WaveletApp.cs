using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveletDecomposition
{
    public partial class WaveletApp : Form
    {
        private WaveletDecompositionLogic _waveletDecompositionLogic;

        public WaveletApp()
        {
            InitializeComponent();
            _waveletDecompositionLogic = new WaveletDecompositionLogic();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = new float[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                9,
                9,
                9,
                9,
                9,
                3,
                2,
                7,
                5,
                2,
                8,
                2,
                55,
                2,
                7,
                3,
                1,
                6,
                9,
                1,
                3,
                2,
                66
            };

            var output = new float[]
            {
                1.33364f
                ,3.073267f
                ,5f
                ,6.963367f
                ,8.83318f
                ,8.802874f
                ,9.533891f
                ,3.550919f
                ,5.705122f
                ,3.143611f
                ,17.77056f
                ,17.42257f
                ,2.318916f
                ,5.911796f
                ,2.18338f
                ,19.36972f
                ,0.25f
                ,-7.450581E-09f
                ,-5.215406E-08f
                ,-0.1250003f
                ,-0.1250003f
                ,-0.5476309f
                ,3.767892f
                ,-3.484163f
                ,0.1349129f
                ,3.924443f
                ,58.5579f
                ,2.356987f
                ,-4.85324f
                ,6.123066f
                ,-2.014145f
                ,71.06794f
            };
            
            var res = _waveletDecompositionLogic.SynthesisH(output, 32);

            var k = 0;
            foreach(var item in res)
            {
                Console.WriteLine(input[k] - Math.Round(item));
                k++;
            }
        }
    }
}
