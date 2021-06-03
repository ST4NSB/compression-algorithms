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
            this.bttnVizualise = new System.Windows.Forms.Button();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.bttnAnalysis.Location = new System.Drawing.Point(355, 631);
            this.bttnAnalysis.Name = "bttnAnalysis";
            this.bttnAnalysis.Size = new System.Drawing.Size(75, 23);
            this.bttnAnalysis.TabIndex = 2;
            this.bttnAnalysis.Text = "Analysis";
            this.bttnAnalysis.UseVisualStyleBackColor = true;
            this.bttnAnalysis.Click += new System.EventHandler(this.bttnAnalysis_Click);
            // 
            // bttnSynthesis
            // 
            this.bttnSynthesis.Location = new System.Drawing.Point(355, 660);
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
            this.comboBoxCompression.Location = new System.Drawing.Point(355, 604);
            this.comboBoxCompression.Name = "comboBoxCompression";
            this.comboBoxCompression.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCompression.TabIndex = 4;
            // 
            // bttnError
            // 
            this.bttnError.Location = new System.Drawing.Point(482, 602);
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
            this.labelMax.Location = new System.Drawing.Point(482, 641);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(30, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "Max:";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(482, 670);
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
            this.bttnSave.Location = new System.Drawing.Point(1118, 558);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(75, 23);
            this.bttnSave.TabIndex = 9;
            this.bttnSave.Text = "save";
            this.bttnSave.UseVisualStyleBackColor = true;
            this.bttnSave.Click += new System.EventHandler(this.bttnSave_Click);
            // 
            // bttnLoadWv
            // 
            this.bttnLoadWv.Location = new System.Drawing.Point(1118, 587);
            this.bttnLoadWv.Name = "bttnLoadWv";
            this.bttnLoadWv.Size = new System.Drawing.Size(75, 23);
            this.bttnLoadWv.TabIndex = 10;
            this.bttnLoadWv.Text = "load";
            this.bttnLoadWv.UseVisualStyleBackColor = true;
            this.bttnLoadWv.Click += new System.EventHandler(this.bttnLoadWv_Click);
            // 
            // bttnVizualise
            // 
            this.bttnVizualise.Location = new System.Drawing.Point(1118, 641);
            this.bttnVizualise.Name = "bttnVizualise";
            this.bttnVizualise.Size = new System.Drawing.Size(75, 23);
            this.bttnVizualise.TabIndex = 11;
            this.bttnVizualise.Text = "Visualize ";
            this.bttnVizualise.UseVisualStyleBackColor = true;
            this.bttnVizualise.Click += new System.EventHandler(this.bttnVizualise_Click);
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(719, 587);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(100, 20);
            this.textBoxScale.TabIndex = 12;
            this.textBoxScale.Text = "2.5";
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(719, 613);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(100, 20);
            this.textBoxOffset.TabIndex = 13;
            this.textBoxOffset.Text = "128";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(719, 644);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(100, 20);
            this.textBoxX.TabIndex = 14;
            this.textBoxX.Text = "0";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(719, 672);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(100, 20);
            this.textBoxY.TabIndex = 15;
            this.textBoxY.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(678, 587);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Scale";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(678, 612);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Offset";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(678, 647);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(678, 675);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Y";
            // 
            // WaveletApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 746);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.textBoxScale);
            this.Controls.Add(this.bttnVizualise);
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
        private System.Windows.Forms.Button bttnVizualise;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

