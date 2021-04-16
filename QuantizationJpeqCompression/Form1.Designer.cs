namespace PredictorFormsApp
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.originalPBox = new System.Windows.Forms.PictureBox();
            this.loadOrigBttn = new System.Windows.Forms.Button();
            this.DecodedPBox = new System.Windows.Forms.PictureBox();
            this.encodeBttn = new System.Windows.Forms.Button();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.histogramBttn = new System.Windows.Forms.Button();
            this.histoComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorPBox = new System.Windows.Forms.PictureBox();
            this.showErrorBttn = new System.Windows.Forms.Button();
            this.storeBttn = new System.Windows.Forms.Button();
            this.loadEncBttn = new System.Windows.Forms.Button();
            this.decodeBttn = new System.Windows.Forms.Button();
            this.saveDecBttn = new System.Windows.Forms.Button();
            this.errorTxtBox = new System.Windows.Forms.TextBox();
            this.scaleTextBox = new System.Windows.Forms.TextBox();
            this.scaleButton = new System.Windows.Forms.Button();
            this.comboBoxSaveMode = new System.Windows.Forms.ComboBox();
            this.textBoxKVal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxErrorType = new System.Windows.Forms.ComboBox();
            this.buttonCompError = new System.Windows.Forms.Button();
            this.labelMaxError = new System.Windows.Forms.Label();
            this.labelMinError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.originalPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecodedPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // originalPBox
            // 
            this.originalPBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.originalPBox.Location = new System.Drawing.Point(61, 29);
            this.originalPBox.Name = "originalPBox";
            this.originalPBox.Size = new System.Drawing.Size(256, 256);
            this.originalPBox.TabIndex = 0;
            this.originalPBox.TabStop = false;
            // 
            // loadOrigBttn
            // 
            this.loadOrigBttn.Location = new System.Drawing.Point(61, 302);
            this.loadOrigBttn.Name = "loadOrigBttn";
            this.loadOrigBttn.Size = new System.Drawing.Size(75, 23);
            this.loadOrigBttn.TabIndex = 1;
            this.loadOrigBttn.Text = "Load img";
            this.loadOrigBttn.UseVisualStyleBackColor = true;
            this.loadOrigBttn.Click += new System.EventHandler(this.LoadOrigBttn_Click);
            // 
            // DecodedPBox
            // 
            this.DecodedPBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.DecodedPBox.Location = new System.Drawing.Point(1116, 29);
            this.DecodedPBox.Name = "DecodedPBox";
            this.DecodedPBox.Size = new System.Drawing.Size(256, 256);
            this.DecodedPBox.TabIndex = 2;
            this.DecodedPBox.TabStop = false;
            // 
            // encodeBttn
            // 
            this.encodeBttn.Location = new System.Drawing.Point(151, 302);
            this.encodeBttn.Name = "encodeBttn";
            this.encodeBttn.Size = new System.Drawing.Size(75, 23);
            this.encodeBttn.TabIndex = 3;
            this.encodeBttn.Text = "Predict";
            this.encodeBttn.UseVisualStyleBackColor = true;
            this.encodeBttn.Click += new System.EventHandler(this.EncodeBttn_Click);
            // 
            // methodComboBox
            // 
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Location = new System.Drawing.Point(61, 437);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(121, 21);
            this.methodComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Original Img";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1113, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Decoded Img";
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(308, 385);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.MarkerSize = 2;
            series1.Name = "Freq";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(1087, 327);
            this.chart.TabIndex = 7;
            this.chart.Text = "chart1";
            // 
            // histogramBttn
            // 
            this.histogramBttn.Location = new System.Drawing.Point(518, 718);
            this.histogramBttn.Name = "histogramBttn";
            this.histogramBttn.Size = new System.Drawing.Size(97, 23);
            this.histogramBttn.TabIndex = 8;
            this.histogramBttn.Text = "Create Histogram";
            this.histogramBttn.UseVisualStyleBackColor = true;
            this.histogramBttn.Click += new System.EventHandler(this.histogramBttn_Click);
            // 
            // histoComboBox
            // 
            this.histoComboBox.FormattingEnabled = true;
            this.histoComboBox.Location = new System.Drawing.Point(308, 720);
            this.histoComboBox.Name = "histoComboBox";
            this.histoComboBox.Size = new System.Drawing.Size(204, 21);
            this.histoComboBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Error Prediction";
            // 
            // errorPBox
            // 
            this.errorPBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.errorPBox.Location = new System.Drawing.Point(587, 29);
            this.errorPBox.Name = "errorPBox";
            this.errorPBox.Size = new System.Drawing.Size(256, 256);
            this.errorPBox.TabIndex = 10;
            this.errorPBox.TabStop = false;
            // 
            // showErrorBttn
            // 
            this.showErrorBttn.Location = new System.Drawing.Point(587, 331);
            this.showErrorBttn.Name = "showErrorBttn";
            this.showErrorBttn.Size = new System.Drawing.Size(112, 23);
            this.showErrorBttn.TabIndex = 12;
            this.showErrorBttn.Text = "Show error matrix";
            this.showErrorBttn.UseVisualStyleBackColor = true;
            this.showErrorBttn.Click += new System.EventHandler(this.ShowErrorBttn_Click);
            // 
            // storeBttn
            // 
            this.storeBttn.Location = new System.Drawing.Point(242, 302);
            this.storeBttn.Name = "storeBttn";
            this.storeBttn.Size = new System.Drawing.Size(75, 23);
            this.storeBttn.TabIndex = 13;
            this.storeBttn.Text = "Store";
            this.storeBttn.UseVisualStyleBackColor = true;
            this.storeBttn.Click += new System.EventHandler(this.StoreBttn_Click);
            // 
            // loadEncBttn
            // 
            this.loadEncBttn.Location = new System.Drawing.Point(1116, 301);
            this.loadEncBttn.Name = "loadEncBttn";
            this.loadEncBttn.Size = new System.Drawing.Size(85, 23);
            this.loadEncBttn.TabIndex = 14;
            this.loadEncBttn.Text = "Load encoded";
            this.loadEncBttn.UseVisualStyleBackColor = true;
            this.loadEncBttn.Click += new System.EventHandler(this.LoadEncBttn_Click);
            // 
            // decodeBttn
            // 
            this.decodeBttn.Location = new System.Drawing.Point(1206, 301);
            this.decodeBttn.Name = "decodeBttn";
            this.decodeBttn.Size = new System.Drawing.Size(75, 23);
            this.decodeBttn.TabIndex = 15;
            this.decodeBttn.Text = "Decode";
            this.decodeBttn.UseVisualStyleBackColor = true;
            this.decodeBttn.Click += new System.EventHandler(this.DecodeBttn_Click);
            // 
            // saveDecBttn
            // 
            this.saveDecBttn.Location = new System.Drawing.Point(1285, 301);
            this.saveDecBttn.Name = "saveDecBttn";
            this.saveDecBttn.Size = new System.Drawing.Size(87, 23);
            this.saveDecBttn.TabIndex = 16;
            this.saveDecBttn.Text = "Save decoded";
            this.saveDecBttn.UseVisualStyleBackColor = true;
            this.saveDecBttn.Click += new System.EventHandler(this.SaveDecBttn_Click);
            // 
            // errorTxtBox
            // 
            this.errorTxtBox.Location = new System.Drawing.Point(786, 301);
            this.errorTxtBox.Name = "errorTxtBox";
            this.errorTxtBox.Size = new System.Drawing.Size(48, 20);
            this.errorTxtBox.TabIndex = 17;
            this.errorTxtBox.Text = "12.3";
            // 
            // scaleTextBox
            // 
            this.scaleTextBox.Location = new System.Drawing.Point(1116, 718);
            this.scaleTextBox.Name = "scaleTextBox";
            this.scaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.scaleTextBox.TabIndex = 18;
            this.scaleTextBox.Text = "0.12";
            // 
            // scaleButton
            // 
            this.scaleButton.Location = new System.Drawing.Point(1246, 718);
            this.scaleButton.Name = "scaleButton";
            this.scaleButton.Size = new System.Drawing.Size(149, 23);
            this.scaleButton.TabIndex = 19;
            this.scaleButton.Text = "Scale Refresh";
            this.scaleButton.UseVisualStyleBackColor = true;
            this.scaleButton.Click += new System.EventHandler(this.scaleButton_Click);
            // 
            // comboBoxSaveMode
            // 
            this.comboBoxSaveMode.FormattingEnabled = true;
            this.comboBoxSaveMode.Location = new System.Drawing.Point(61, 559);
            this.comboBoxSaveMode.Name = "comboBoxSaveMode";
            this.comboBoxSaveMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSaveMode.TabIndex = 20;
            // 
            // textBoxKVal
            // 
            this.textBoxKVal.Location = new System.Drawing.Point(82, 494);
            this.textBoxKVal.Name = "textBoxKVal";
            this.textBoxKVal.Size = new System.Drawing.Size(100, 20);
            this.textBoxKVal.TabIndex = 21;
            this.textBoxKVal.Text = "2";
            this.textBoxKVal.TextChanged += new System.EventHandler(this.textBoxKVal_TextChanged);
            this.textBoxKVal.Leave += new System.EventHandler(this.textBoxKVal_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(47, 489);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 25);
            this.label4.TabIndex = 22;
            this.label4.Text = "k:";
            // 
            // comboBoxErrorType
            // 
            this.comboBoxErrorType.FormattingEnabled = true;
            this.comboBoxErrorType.Location = new System.Drawing.Point(587, 301);
            this.comboBoxErrorType.Name = "comboBoxErrorType";
            this.comboBoxErrorType.Size = new System.Drawing.Size(171, 21);
            this.comboBoxErrorType.TabIndex = 23;
            // 
            // buttonCompError
            // 
            this.buttonCompError.Location = new System.Drawing.Point(52, 632);
            this.buttonCompError.Name = "buttonCompError";
            this.buttonCompError.Size = new System.Drawing.Size(101, 23);
            this.buttonCompError.TabIndex = 24;
            this.buttonCompError.Text = "Compute Error";
            this.buttonCompError.UseVisualStyleBackColor = true;
            this.buttonCompError.Click += new System.EventHandler(this.buttonCompError_Click);
            // 
            // labelMaxError
            // 
            this.labelMaxError.AutoSize = true;
            this.labelMaxError.Location = new System.Drawing.Point(58, 674);
            this.labelMaxError.Name = "labelMaxError";
            this.labelMaxError.Size = new System.Drawing.Size(55, 13);
            this.labelMaxError.TabIndex = 25;
            this.labelMaxError.Text = "Max Error:";
            // 
            // labelMinError
            // 
            this.labelMinError.AutoSize = true;
            this.labelMinError.Location = new System.Drawing.Point(58, 699);
            this.labelMinError.Name = "labelMinError";
            this.labelMinError.Size = new System.Drawing.Size(52, 13);
            this.labelMinError.TabIndex = 26;
            this.labelMinError.Text = "Min Error:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1423, 765);
            this.Controls.Add(this.labelMinError);
            this.Controls.Add(this.labelMaxError);
            this.Controls.Add(this.buttonCompError);
            this.Controls.Add(this.comboBoxErrorType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxKVal);
            this.Controls.Add(this.comboBoxSaveMode);
            this.Controls.Add(this.scaleButton);
            this.Controls.Add(this.scaleTextBox);
            this.Controls.Add(this.errorTxtBox);
            this.Controls.Add(this.saveDecBttn);
            this.Controls.Add(this.decodeBttn);
            this.Controls.Add(this.loadEncBttn);
            this.Controls.Add(this.storeBttn);
            this.Controls.Add(this.showErrorBttn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.errorPBox);
            this.Controls.Add(this.histoComboBox);
            this.Controls.Add(this.histogramBttn);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.methodComboBox);
            this.Controls.Add(this.encodeBttn);
            this.Controls.Add(this.DecodedPBox);
            this.Controls.Add(this.loadOrigBttn);
            this.Controls.Add(this.originalPBox);
            this.Name = "Form1";
            this.Text = "Predictor";
            ((System.ComponentModel.ISupportInitialize)(this.originalPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecodedPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox originalPBox;
        private System.Windows.Forms.Button loadOrigBttn;
        private System.Windows.Forms.PictureBox DecodedPBox;
        private System.Windows.Forms.Button encodeBttn;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button histogramBttn;
        private System.Windows.Forms.ComboBox histoComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox errorPBox;
        private System.Windows.Forms.Button showErrorBttn;
        private System.Windows.Forms.Button storeBttn;
        private System.Windows.Forms.Button loadEncBttn;
        private System.Windows.Forms.Button decodeBttn;
        private System.Windows.Forms.Button saveDecBttn;
        private System.Windows.Forms.TextBox errorTxtBox;
        private System.Windows.Forms.TextBox scaleTextBox;
        private System.Windows.Forms.Button scaleButton;
        private System.Windows.Forms.ComboBox comboBoxSaveMode;
        private System.Windows.Forms.TextBox textBoxKVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxErrorType;
        private System.Windows.Forms.Button buttonCompError;
        private System.Windows.Forms.Label labelMaxError;
        private System.Windows.Forms.Label labelMinError;
    }
}

