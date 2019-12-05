namespace LZW_UI
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
            this.encodeLoadBttn = new System.Windows.Forms.Button();
            this.indexMenu = new System.Windows.Forms.ComboBox();
            this.methodType = new System.Windows.Forms.ComboBox();
            this.log = new System.Windows.Forms.RichTextBox();
            this.encodeBttn = new System.Windows.Forms.Button();
            this.codeCheckBox = new System.Windows.Forms.CheckBox();
            this.decodeLoadBttn = new System.Windows.Forms.Button();
            this.decodeBttn = new System.Windows.Forms.Button();
            this.decodeCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clearLogBttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // encodeLoadBttn
            // 
            this.encodeLoadBttn.Location = new System.Drawing.Point(34, 117);
            this.encodeLoadBttn.Name = "encodeLoadBttn";
            this.encodeLoadBttn.Size = new System.Drawing.Size(75, 23);
            this.encodeLoadBttn.TabIndex = 0;
            this.encodeLoadBttn.Text = "Browse";
            this.encodeLoadBttn.UseVisualStyleBackColor = true;
            this.encodeLoadBttn.Click += new System.EventHandler(this.LoadBttn_Click);
            // 
            // indexMenu
            // 
            this.indexMenu.FormattingEnabled = true;
            this.indexMenu.Location = new System.Drawing.Point(34, 146);
            this.indexMenu.Name = "indexMenu";
            this.indexMenu.Size = new System.Drawing.Size(121, 21);
            this.indexMenu.TabIndex = 2;
            // 
            // methodType
            // 
            this.methodType.FormattingEnabled = true;
            this.methodType.Location = new System.Drawing.Point(34, 173);
            this.methodType.Name = "methodType";
            this.methodType.Size = new System.Drawing.Size(121, 21);
            this.methodType.TabIndex = 3;
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(480, 30);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(562, 513);
            this.log.TabIndex = 4;
            this.log.Text = "";
            // 
            // encodeBttn
            // 
            this.encodeBttn.Location = new System.Drawing.Point(80, 237);
            this.encodeBttn.Name = "encodeBttn";
            this.encodeBttn.Size = new System.Drawing.Size(75, 23);
            this.encodeBttn.TabIndex = 5;
            this.encodeBttn.Text = "Encode";
            this.encodeBttn.UseVisualStyleBackColor = true;
            this.encodeBttn.Click += new System.EventHandler(this.EncodeBttn_Click);
            // 
            // codeCheckBox
            // 
            this.codeCheckBox.AutoSize = true;
            this.codeCheckBox.Location = new System.Drawing.Point(69, 200);
            this.codeCheckBox.Name = "codeCheckBox";
            this.codeCheckBox.Size = new System.Drawing.Size(86, 17);
            this.codeCheckBox.TabIndex = 6;
            this.codeCheckBox.Text = "Show Codes";
            this.codeCheckBox.UseVisualStyleBackColor = true;
            // 
            // decodeLoadBttn
            // 
            this.decodeLoadBttn.Location = new System.Drawing.Point(264, 117);
            this.decodeLoadBttn.Name = "decodeLoadBttn";
            this.decodeLoadBttn.Size = new System.Drawing.Size(75, 23);
            this.decodeLoadBttn.TabIndex = 7;
            this.decodeLoadBttn.Text = "Browse";
            this.decodeLoadBttn.UseVisualStyleBackColor = true;
            this.decodeLoadBttn.Click += new System.EventHandler(this.DecodeLoadBttn_Click);
            // 
            // decodeBttn
            // 
            this.decodeBttn.Location = new System.Drawing.Point(264, 171);
            this.decodeBttn.Name = "decodeBttn";
            this.decodeBttn.Size = new System.Drawing.Size(75, 23);
            this.decodeBttn.TabIndex = 8;
            this.decodeBttn.Text = "Decode";
            this.decodeBttn.UseVisualStyleBackColor = true;
            this.decodeBttn.Click += new System.EventHandler(this.DecodeBttn_Click);
            // 
            // decodeCheckBox
            // 
            this.decodeCheckBox.AutoSize = true;
            this.decodeCheckBox.Location = new System.Drawing.Point(264, 148);
            this.decodeCheckBox.Name = "decodeCheckBox";
            this.decodeCheckBox.Size = new System.Drawing.Size(85, 17);
            this.decodeCheckBox.TabIndex = 9;
            this.decodeCheckBox.Text = "Show codes";
            this.decodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Encode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Decode";
            // 
            // clearLogBttn
            // 
            this.clearLogBttn.Location = new System.Drawing.Point(379, 520);
            this.clearLogBttn.Name = "clearLogBttn";
            this.clearLogBttn.Size = new System.Drawing.Size(75, 23);
            this.clearLogBttn.TabIndex = 12;
            this.clearLogBttn.Text = "Clear Log";
            this.clearLogBttn.UseVisualStyleBackColor = true;
            this.clearLogBttn.Click += new System.EventHandler(this.ClearLogBttn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 569);
            this.Controls.Add(this.clearLogBttn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decodeCheckBox);
            this.Controls.Add(this.decodeBttn);
            this.Controls.Add(this.decodeLoadBttn);
            this.Controls.Add(this.codeCheckBox);
            this.Controls.Add(this.encodeBttn);
            this.Controls.Add(this.log);
            this.Controls.Add(this.methodType);
            this.Controls.Add(this.indexMenu);
            this.Controls.Add(this.encodeLoadBttn);
            this.Name = "Form1";
            this.Text = "LZW UI App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button encodeLoadBttn;
        private System.Windows.Forms.ComboBox indexMenu;
        private System.Windows.Forms.ComboBox methodType;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button encodeBttn;
        private System.Windows.Forms.CheckBox codeCheckBox;
        private System.Windows.Forms.Button decodeLoadBttn;
        private System.Windows.Forms.Button decodeBttn;
        private System.Windows.Forms.CheckBox decodeCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clearLogBttn;
    }
}

