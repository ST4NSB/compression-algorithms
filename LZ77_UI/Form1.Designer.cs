namespace LZ77_UI
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
            this.log = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.offsetSizeBox = new System.Windows.Forms.ComboBox();
            this.lengthSizeBox = new System.Windows.Forms.ComboBox();
            this.encodeLoadBttn = new System.Windows.Forms.Button();
            this.encodeExecute = new System.Windows.Forms.Button();
            this.encodeCheckBox = new System.Windows.Forms.CheckBox();
            this.decodeLoadBttn = new System.Windows.Forms.Button();
            this.decodeCheckBox = new System.Windows.Forms.CheckBox();
            this.decodeExecute = new System.Windows.Forms.Button();
            this.clrLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(514, 12);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(510, 583);
            this.log.TabIndex = 0;
            this.log.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Encode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Decode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "length";
            // 
            // offsetSizeBox
            // 
            this.offsetSizeBox.FormattingEnabled = true;
            this.offsetSizeBox.Location = new System.Drawing.Point(72, 148);
            this.offsetSizeBox.Name = "offsetSizeBox";
            this.offsetSizeBox.Size = new System.Drawing.Size(121, 21);
            this.offsetSizeBox.TabIndex = 5;
            // 
            // lengthSizeBox
            // 
            this.lengthSizeBox.FormattingEnabled = true;
            this.lengthSizeBox.Location = new System.Drawing.Point(72, 181);
            this.lengthSizeBox.Name = "lengthSizeBox";
            this.lengthSizeBox.Size = new System.Drawing.Size(121, 21);
            this.lengthSizeBox.TabIndex = 6;
            // 
            // encodeLoadBttn
            // 
            this.encodeLoadBttn.Location = new System.Drawing.Point(72, 107);
            this.encodeLoadBttn.Name = "encodeLoadBttn";
            this.encodeLoadBttn.Size = new System.Drawing.Size(75, 23);
            this.encodeLoadBttn.TabIndex = 7;
            this.encodeLoadBttn.Text = "Browse";
            this.encodeLoadBttn.UseVisualStyleBackColor = true;
            this.encodeLoadBttn.Click += new System.EventHandler(this.EncodeLoadBttn_Click);
            // 
            // encodeExecute
            // 
            this.encodeExecute.Location = new System.Drawing.Point(115, 240);
            this.encodeExecute.Name = "encodeExecute";
            this.encodeExecute.Size = new System.Drawing.Size(75, 23);
            this.encodeExecute.TabIndex = 8;
            this.encodeExecute.Text = "Encode";
            this.encodeExecute.UseVisualStyleBackColor = true;
            this.encodeExecute.Click += new System.EventHandler(this.EncodeExecute_Click);
            // 
            // encodeCheckBox
            // 
            this.encodeCheckBox.AutoSize = true;
            this.encodeCheckBox.Location = new System.Drawing.Point(105, 208);
            this.encodeCheckBox.Name = "encodeCheckBox";
            this.encodeCheckBox.Size = new System.Drawing.Size(88, 17);
            this.encodeCheckBox.TabIndex = 9;
            this.encodeCheckBox.Text = "Show tokens";
            this.encodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // decodeLoadBttn
            // 
            this.decodeLoadBttn.Location = new System.Drawing.Point(278, 107);
            this.decodeLoadBttn.Name = "decodeLoadBttn";
            this.decodeLoadBttn.Size = new System.Drawing.Size(75, 23);
            this.decodeLoadBttn.TabIndex = 10;
            this.decodeLoadBttn.Text = "Browse";
            this.decodeLoadBttn.UseVisualStyleBackColor = true;
            this.decodeLoadBttn.Click += new System.EventHandler(this.DecodeLoadBttn_Click);
            // 
            // decodeCheckBox
            // 
            this.decodeCheckBox.AutoSize = true;
            this.decodeCheckBox.Location = new System.Drawing.Point(291, 136);
            this.decodeCheckBox.Name = "decodeCheckBox";
            this.decodeCheckBox.Size = new System.Drawing.Size(85, 17);
            this.decodeCheckBox.TabIndex = 11;
            this.decodeCheckBox.Text = "Show codes";
            this.decodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // decodeExecute
            // 
            this.decodeExecute.Location = new System.Drawing.Point(291, 159);
            this.decodeExecute.Name = "decodeExecute";
            this.decodeExecute.Size = new System.Drawing.Size(75, 23);
            this.decodeExecute.TabIndex = 12;
            this.decodeExecute.Text = "Decode";
            this.decodeExecute.UseVisualStyleBackColor = true;
            this.decodeExecute.Click += new System.EventHandler(this.DecodeExecute_Click);
            // 
            // clrLog
            // 
            this.clrLog.Location = new System.Drawing.Point(422, 572);
            this.clrLog.Name = "clrLog";
            this.clrLog.Size = new System.Drawing.Size(75, 23);
            this.clrLog.TabIndex = 13;
            this.clrLog.Text = "Clear Log";
            this.clrLog.UseVisualStyleBackColor = true;
            this.clrLog.Click += new System.EventHandler(this.ClrLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 607);
            this.Controls.Add(this.clrLog);
            this.Controls.Add(this.decodeExecute);
            this.Controls.Add(this.decodeCheckBox);
            this.Controls.Add(this.decodeLoadBttn);
            this.Controls.Add(this.encodeCheckBox);
            this.Controls.Add(this.encodeExecute);
            this.Controls.Add(this.encodeLoadBttn);
            this.Controls.Add(this.lengthSizeBox);
            this.Controls.Add(this.offsetSizeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.log);
            this.Name = "Form1";
            this.Text = "LZ77 UI App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox offsetSizeBox;
        private System.Windows.Forms.ComboBox lengthSizeBox;
        private System.Windows.Forms.Button encodeLoadBttn;
        private System.Windows.Forms.Button encodeExecute;
        private System.Windows.Forms.CheckBox encodeCheckBox;
        private System.Windows.Forms.Button decodeLoadBttn;
        private System.Windows.Forms.CheckBox decodeCheckBox;
        private System.Windows.Forms.Button decodeExecute;
        private System.Windows.Forms.Button clrLog;
    }
}

