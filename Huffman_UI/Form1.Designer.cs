namespace Huffman_UI
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loadEncodeBttn = new System.Windows.Forms.Button();
            this.loadDecodeBttn = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.RichTextBox();
            this.clrLogBttn = new System.Windows.Forms.Button();
            this.logCheckBox = new System.Windows.Forms.CheckBox();
            this.encFileBttn = new System.Windows.Forms.Button();
            this.decFileBttn = new System.Windows.Forms.Button();
            this.encTxtBoxBttn = new System.Windows.Forms.Button();
            this.encTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Encode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Decode";
            // 
            // loadEncodeBttn
            // 
            this.loadEncodeBttn.Location = new System.Drawing.Point(81, 120);
            this.loadEncodeBttn.Name = "loadEncodeBttn";
            this.loadEncodeBttn.Size = new System.Drawing.Size(75, 23);
            this.loadEncodeBttn.TabIndex = 2;
            this.loadEncodeBttn.Text = "Browse";
            this.loadEncodeBttn.UseVisualStyleBackColor = true;
            this.loadEncodeBttn.Click += new System.EventHandler(this.LoadEncodeBttn_Click);
            // 
            // loadDecodeBttn
            // 
            this.loadDecodeBttn.Location = new System.Drawing.Point(341, 120);
            this.loadDecodeBttn.Name = "loadDecodeBttn";
            this.loadDecodeBttn.Size = new System.Drawing.Size(75, 23);
            this.loadDecodeBttn.TabIndex = 3;
            this.loadDecodeBttn.Text = "Browse";
            this.loadDecodeBttn.UseVisualStyleBackColor = true;
            this.loadDecodeBttn.Click += new System.EventHandler(this.LoadDecodeBttn_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(566, 12);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(502, 555);
            this.log.TabIndex = 4;
            this.log.Text = "";
            // 
            // clrLogBttn
            // 
            this.clrLogBttn.Location = new System.Drawing.Point(466, 544);
            this.clrLogBttn.Name = "clrLogBttn";
            this.clrLogBttn.Size = new System.Drawing.Size(75, 23);
            this.clrLogBttn.TabIndex = 5;
            this.clrLogBttn.Text = "Clear Log";
            this.clrLogBttn.UseVisualStyleBackColor = true;
            this.clrLogBttn.Click += new System.EventHandler(this.ClrLogBttn_Click);
            // 
            // logCheckBox
            // 
            this.logCheckBox.AutoSize = true;
            this.logCheckBox.Location = new System.Drawing.Point(81, 235);
            this.logCheckBox.Name = "logCheckBox";
            this.logCheckBox.Size = new System.Drawing.Size(86, 17);
            this.logCheckBox.TabIndex = 6;
            this.logCheckBox.Text = "Show Codes";
            this.logCheckBox.UseVisualStyleBackColor = true;
            // 
            // encFileBttn
            // 
            this.encFileBttn.Location = new System.Drawing.Point(81, 161);
            this.encFileBttn.Name = "encFileBttn";
            this.encFileBttn.Size = new System.Drawing.Size(75, 23);
            this.encFileBttn.TabIndex = 8;
            this.encFileBttn.Text = "Encode File";
            this.encFileBttn.UseVisualStyleBackColor = true;
            this.encFileBttn.Click += new System.EventHandler(this.EncFileBttn_Click);
            // 
            // decFileBttn
            // 
            this.decFileBttn.Location = new System.Drawing.Point(341, 161);
            this.decFileBttn.Name = "decFileBttn";
            this.decFileBttn.Size = new System.Drawing.Size(75, 23);
            this.decFileBttn.TabIndex = 9;
            this.decFileBttn.Text = "Decode File";
            this.decFileBttn.UseVisualStyleBackColor = true;
            this.decFileBttn.Click += new System.EventHandler(this.DecFileBttn_Click);
            // 
            // encTxtBoxBttn
            // 
            this.encTxtBoxBttn.Location = new System.Drawing.Point(81, 317);
            this.encTxtBoxBttn.Name = "encTxtBoxBttn";
            this.encTxtBoxBttn.Size = new System.Drawing.Size(121, 23);
            this.encTxtBoxBttn.TabIndex = 10;
            this.encTxtBoxBttn.Text = "Encode Input Text";
            this.encTxtBoxBttn.UseVisualStyleBackColor = true;
            this.encTxtBoxBttn.Click += new System.EventHandler(this.EncTxtBoxBttn_Click);
            // 
            // encTxtBox
            // 
            this.encTxtBox.Location = new System.Drawing.Point(81, 291);
            this.encTxtBox.Name = "encTxtBox";
            this.encTxtBox.Size = new System.Drawing.Size(231, 20);
            this.encTxtBox.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 579);
            this.Controls.Add(this.encTxtBox);
            this.Controls.Add(this.encTxtBoxBttn);
            this.Controls.Add(this.decFileBttn);
            this.Controls.Add(this.encFileBttn);
            this.Controls.Add(this.logCheckBox);
            this.Controls.Add(this.clrLogBttn);
            this.Controls.Add(this.log);
            this.Controls.Add(this.loadDecodeBttn);
            this.Controls.Add(this.loadEncodeBttn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Huffman UI App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadEncodeBttn;
        private System.Windows.Forms.Button loadDecodeBttn;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button clrLogBttn;
        private System.Windows.Forms.CheckBox logCheckBox;
        private System.Windows.Forms.Button encFileBttn;
        private System.Windows.Forms.Button decFileBttn;
        private System.Windows.Forms.Button encTxtBoxBttn;
        private System.Windows.Forms.TextBox encTxtBox;
    }
}

