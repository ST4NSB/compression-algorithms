namespace WaveletDecomposition
{
    partial class WaveletApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageOrig = new System.Windows.Forms.PictureBox();
            this.bttnLoadImg = new System.Windows.Forms.Button();
            this.bttnAnalysis = new System.Windows.Forms.Button();
            this.bttnSynthesis = new System.Windows.Forms.Button();
            this.comboBoxCompression = new System.Windows.Forms.ComboBox();
            this.bttnError = new System.Windows.Forms.Button();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.imageWavelet = new System.Windows.Forms.PictureBox();
            this.bttnSave = new System.Windows.Forms.Button();
            this.bttnLoadWv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageOrig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWavelet)).BeginInit();
            this.SuspendLayout();
            // 
            // imageOrig
            // 
            this.imageOrig.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imageOrig.Location = new System.Drawing.Point(45, 26);
            this.imageOrig.Name = "imageOrig";
            this.imageOrig.Size = new System.Drawing.Size(512, 512);
            this.imageOrig.TabIndex = 0;
            this.imageOrig.TabStop = false;
            // 
            // bttnLoadImg
            // 
            this.bttnLoadImg.Location = new System.Drawing.Point(45, 560);
            this.bttnLoadImg.Name = "bttnLoadImg";
            this.bttnLoadImg.Size = new System.Drawing.Size(111, 23);
            this.bttnLoadImg.TabIndex = 1;
            this.bttnLoadImg.Text = "Load image (bmp)";
            this.bttnLoadImg.UseVisualStyleBackColor = true;
            this.bttnLoadImg.Click += new System.EventHandler(this.bttnLoad_Click);
            // 
            // bttnAnalysis
            // 
            this.bttnAnalysis.Location = new System.Drawing.Point(281, 641);
            this.bttnAnalysis.Name = "bttnAnalysis";
            this.bttnAnalysis.Size = new System.Drawing.Size(75, 23);
            this.bttnAnalysis.TabIndex = 2;
            this.bttnAnalysis.Text = "Analysis";
            this.bttnAnalysis.UseVisualStyleBackColor = true;
            this.bttnAnalysis.Click += new System.EventHandler(this.bttnAnalysis_Click);
            // 
            // bttnSynthesis
            // 
            this.bttnSynthesis.Location = new System.Drawing.Point(281, 670);
            this.bttnSynthesis.Name = "bttnSynthesis";
            this.bttnSynthesis.Size = new System.Drawing.Size(75, 23);
            this.bttnSynthesis.TabIndex = 3;
            this.bttnSynthesis.Text = "Synthesis";
            this.bttnSynthesis.UseVisualStyleBackColor = true;
            this.bttnSynthesis.Click += new System.EventHandler(this.bttnSynthesis_Click);
            // 
            // comboBoxCompression
            // 
            this.comboBoxCompression.FormattingEnabled = true;
            this.comboBoxCompression.Location = new System.Drawing.Point(281, 614);
            this.comboBoxCompression.Name = "comboBoxCompression";
            this.comboBoxCompression.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCompression.TabIndex = 4;
            // 
            // bttnError
            // 
            this.bttnError.Location = new System.Drawing.Point(469, 614);
            this.bttnError.Name = "bttnError";
            this.bttnError.Size = new System.Drawing.Size(75, 23);
            this.bttnError.TabIndex = 5;
            this.bttnError.Text = "test error";
            this.bttnError.UseVisualStyleBackColor = true;
            this.bttnError.Click += new System.EventHandler(this.bttnError_Click);
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(469, 653);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(30, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "Max:";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(469, 682);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(27, 13);
            this.labelMin.TabIndex = 7;
            this.labelMin.Text = "Min:";
            // 
            // imageWavelet
            // 
            this.imageWavelet.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imageWavelet.Location = new System.Drawing.Point(681, 26);
            this.imageWavelet.Name = "imageWavelet";
            this.imageWavelet.Size = new System.Drawing.Size(512, 512);
            this.imageWavelet.TabIndex = 8;
            this.imageWavelet.TabStop = false;
            // 
            // bttnSave
            // 
            this.bttnSave.Location = new System.Drawing.Point(681, 560);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(75, 23);
            this.bttnSave.TabIndex = 9;
            this.bttnSave.Text = "save";
            this.bttnSave.UseVisualStyleBackColor = true;
            // 
            // bttnLoadWv
            // 
            this.bttnLoadWv.Location = new System.Drawing.Point(681, 589);
            this.bttnLoadWv.Name = "bttnLoadWv";
            this.bttnLoadWv.Size = new System.Drawing.Size(75, 23);
            this.bttnLoadWv.TabIndex = 10;
            this.bttnLoadWv.Text = "load";
            this.bttnLoadWv.UseVisualStyleBackColor = true;
            // 
            // WaveletApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 746);
            this.Controls.Add(this.bttnLoadWv);
            this.Controls.Add(this.bttnSave);
            this.Controls.Add(this.imageWavelet);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.bttnError);
            this.Controls.Add(this.comboBoxCompression);
            this.Controls.Add(this.bttnSynthesis);
            this.Controls.Add(this.bttnAnalysis);
            this.Controls.Add(this.bttnLoadImg);
            this.Controls.Add(this.imageOrig);
            this.Name = "WaveletApp";
            this.Text = "Wavelet Decomposition";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageOrig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWavelet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageOrig;
        private System.Windows.Forms.Button bttnLoadImg;
        private System.Windows.Forms.Button bttnAnalysis;
        private System.Windows.Forms.Button bttnSynthesis;
        private System.Windows.Forms.ComboBox comboBoxCompression;
        private System.Windows.Forms.Button bttnError;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.PictureBox imageWavelet;
        private System.Windows.Forms.Button bttnSave;
        private System.Windows.Forms.Button bttnLoadWv;
    }
}

