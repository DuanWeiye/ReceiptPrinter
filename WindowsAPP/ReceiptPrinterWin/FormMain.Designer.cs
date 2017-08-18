namespace ReceiptPrinterWin
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.radioButtonJob = new System.Windows.Forms.RadioButton();
            this.radioButtonNow = new System.Windows.Forms.RadioButton();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.checkBoxTitleBold = new System.Windows.Forms.CheckBox();
            this.checkBoxTitle = new System.Windows.Forms.CheckBox();
            this.comboBoxTitlePos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTitleSize = new System.Windows.Forms.ComboBox();
            this.checkBoxText = new System.Windows.Forms.CheckBox();
            this.checkBoxTextBold = new System.Windows.Forms.CheckBox();
            this.comboBoxTextSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTextPos = new System.Windows.Forms.ComboBox();
            this.checkBoxPic = new System.Windows.Forms.CheckBox();
            this.textBoxPic = new System.Windows.Forms.TextBox();
            this.buttonPicUpload = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.radioButtonPicFile = new System.Windows.Forms.RadioButton();
            this.radioButtonPicURL = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(109, 493);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerTime);
            this.groupBox1.Controls.Add(this.dateTimePickerDate);
            this.groupBox1.Controls.Add(this.radioButtonJob);
            this.groupBox1.Controls.Add(this.radioButtonNow);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划打印";
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTime.Location = new System.Drawing.Point(156, 94);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.Size = new System.Drawing.Size(103, 19);
            this.dateTimePickerTime.TabIndex = 3;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDate.Location = new System.Drawing.Point(33, 94);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(103, 19);
            this.dateTimePickerDate.TabIndex = 2;
            // 
            // radioButtonJob
            // 
            this.radioButtonJob.AutoSize = true;
            this.radioButtonJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonJob.Location = new System.Drawing.Point(25, 58);
            this.radioButtonJob.Name = "radioButtonJob";
            this.radioButtonJob.Size = new System.Drawing.Size(70, 16);
            this.radioButtonJob.TabIndex = 1;
            this.radioButtonJob.Text = "预约时间";
            this.radioButtonJob.UseVisualStyleBackColor = true;
            // 
            // radioButtonNow
            // 
            this.radioButtonNow.AutoSize = true;
            this.radioButtonNow.Checked = true;
            this.radioButtonNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonNow.Location = new System.Drawing.Point(25, 27);
            this.radioButtonNow.Name = "radioButtonNow";
            this.radioButtonNow.Size = new System.Drawing.Size(70, 16);
            this.radioButtonNow.TabIndex = 0;
            this.radioButtonNow.TabStop = true;
            this.radioButtonNow.Text = "立即打印";
            this.radioButtonNow.UseVisualStyleBackColor = true;
            this.radioButtonNow.CheckedChanged += new System.EventHandler(this.radioButtonNow_CheckedChanged);
            // 
            // textBoxText
            // 
            this.textBoxText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxText.Location = new System.Drawing.Point(10, 289);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxText.Size = new System.Drawing.Size(277, 57);
            this.textBoxText.TabIndex = 3;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTitle.Location = new System.Drawing.Point(10, 196);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(226, 19);
            this.textBoxTitle.TabIndex = 4;
            // 
            // checkBoxTitleBold
            // 
            this.checkBoxTitleBold.AutoSize = true;
            this.checkBoxTitleBold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTitleBold.Location = new System.Drawing.Point(242, 196);
            this.checkBoxTitleBold.Name = "checkBoxTitleBold";
            this.checkBoxTitleBold.Size = new System.Drawing.Size(45, 16);
            this.checkBoxTitleBold.TabIndex = 7;
            this.checkBoxTitleBold.Text = "加粗";
            this.checkBoxTitleBold.UseVisualStyleBackColor = true;
            // 
            // checkBoxTitle
            // 
            this.checkBoxTitle.AutoSize = true;
            this.checkBoxTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTitle.Location = new System.Drawing.Point(12, 174);
            this.checkBoxTitle.Name = "checkBoxTitle";
            this.checkBoxTitle.Size = new System.Drawing.Size(45, 16);
            this.checkBoxTitle.TabIndex = 8;
            this.checkBoxTitle.Text = "标题";
            this.checkBoxTitle.UseVisualStyleBackColor = true;
            this.checkBoxTitle.CheckedChanged += new System.EventHandler(this.checkBoxTitle_CheckedChanged);
            // 
            // comboBoxTitlePos
            // 
            this.comboBoxTitlePos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTitlePos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTitlePos.FormattingEnabled = true;
            this.comboBoxTitlePos.Location = new System.Drawing.Point(45, 227);
            this.comboBoxTitlePos.Name = "comboBoxTitlePos";
            this.comboBoxTitlePos.Size = new System.Drawing.Size(87, 20);
            this.comboBoxTitlePos.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "位置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "字号";
            // 
            // comboBoxTitleSize
            // 
            this.comboBoxTitleSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTitleSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTitleSize.FormattingEnabled = true;
            this.comboBoxTitleSize.Location = new System.Drawing.Point(210, 227);
            this.comboBoxTitleSize.Name = "comboBoxTitleSize";
            this.comboBoxTitleSize.Size = new System.Drawing.Size(77, 20);
            this.comboBoxTitleSize.TabIndex = 12;
            // 
            // checkBoxText
            // 
            this.checkBoxText.AutoSize = true;
            this.checkBoxText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxText.Location = new System.Drawing.Point(12, 267);
            this.checkBoxText.Name = "checkBoxText";
            this.checkBoxText.Size = new System.Drawing.Size(45, 16);
            this.checkBoxText.TabIndex = 13;
            this.checkBoxText.Text = "正文";
            this.checkBoxText.UseVisualStyleBackColor = true;
            this.checkBoxText.CheckedChanged += new System.EventHandler(this.checkBoxText_CheckedChanged);
            // 
            // checkBoxTextBold
            // 
            this.checkBoxTextBold.AutoSize = true;
            this.checkBoxTextBold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTextBold.Location = new System.Drawing.Point(242, 267);
            this.checkBoxTextBold.Name = "checkBoxTextBold";
            this.checkBoxTextBold.Size = new System.Drawing.Size(45, 16);
            this.checkBoxTextBold.TabIndex = 14;
            this.checkBoxTextBold.Text = "加粗";
            this.checkBoxTextBold.UseVisualStyleBackColor = true;
            // 
            // comboBoxTextSize
            // 
            this.comboBoxTextSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTextSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextSize.FormattingEnabled = true;
            this.comboBoxTextSize.Location = new System.Drawing.Point(210, 357);
            this.comboBoxTextSize.Name = "comboBoxTextSize";
            this.comboBoxTextSize.Size = new System.Drawing.Size(77, 20);
            this.comboBoxTextSize.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "字号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "位置";
            // 
            // comboBoxTextPos
            // 
            this.comboBoxTextPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTextPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextPos.FormattingEnabled = true;
            this.comboBoxTextPos.Location = new System.Drawing.Point(45, 357);
            this.comboBoxTextPos.Name = "comboBoxTextPos";
            this.comboBoxTextPos.Size = new System.Drawing.Size(87, 20);
            this.comboBoxTextPos.TabIndex = 15;
            // 
            // checkBoxPic
            // 
            this.checkBoxPic.AutoSize = true;
            this.checkBoxPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxPic.Location = new System.Drawing.Point(12, 396);
            this.checkBoxPic.Name = "checkBoxPic";
            this.checkBoxPic.Size = new System.Drawing.Size(45, 16);
            this.checkBoxPic.TabIndex = 19;
            this.checkBoxPic.Text = "图片";
            this.checkBoxPic.UseVisualStyleBackColor = true;
            this.checkBoxPic.CheckedChanged += new System.EventHandler(this.checkBoxPic_CheckedChanged);
            // 
            // textBoxPic
            // 
            this.textBoxPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPic.Location = new System.Drawing.Point(12, 445);
            this.textBoxPic.Name = "textBoxPic";
            this.textBoxPic.Size = new System.Drawing.Size(224, 19);
            this.textBoxPic.TabIndex = 20;
            // 
            // buttonPicUpload
            // 
            this.buttonPicUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPicUpload.Location = new System.Drawing.Point(245, 441);
            this.buttonPicUpload.Name = "buttonPicUpload";
            this.buttonPicUpload.Size = new System.Drawing.Size(42, 23);
            this.buttonPicUpload.TabIndex = 21;
            this.buttonPicUpload.Text = "...";
            this.buttonPicUpload.UseVisualStyleBackColor = true;
            this.buttonPicUpload.Click += new System.EventHandler(this.buttonPicUpload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "jpg";
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // radioButtonPicFile
            // 
            this.radioButtonPicFile.AutoSize = true;
            this.radioButtonPicFile.Location = new System.Drawing.Point(45, 423);
            this.radioButtonPicFile.Name = "radioButtonPicFile";
            this.radioButtonPicFile.Size = new System.Drawing.Size(71, 16);
            this.radioButtonPicFile.TabIndex = 22;
            this.radioButtonPicFile.TabStop = true;
            this.radioButtonPicFile.Text = "本地上传";
            this.radioButtonPicFile.UseVisualStyleBackColor = true;
            this.radioButtonPicFile.CheckedChanged += new System.EventHandler(this.radioButtonPicFile_CheckedChanged);
            // 
            // radioButtonPicURL
            // 
            this.radioButtonPicURL.AutoSize = true;
            this.radioButtonPicURL.Location = new System.Drawing.Point(168, 423);
            this.radioButtonPicURL.Name = "radioButtonPicURL";
            this.radioButtonPicURL.Size = new System.Drawing.Size(45, 16);
            this.radioButtonPicURL.TabIndex = 23;
            this.radioButtonPicURL.TabStop = true;
            this.radioButtonPicURL.Text = "URL";
            this.radioButtonPicURL.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 530);
            this.Controls.Add(this.radioButtonPicURL);
            this.Controls.Add(this.radioButtonPicFile);
            this.Controls.Add(this.buttonPicUpload);
            this.Controls.Add(this.textBoxPic);
            this.Controls.Add(this.checkBoxPic);
            this.Controls.Add(this.comboBoxTextSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxTextPos);
            this.Controls.Add(this.checkBoxTextBold);
            this.Controls.Add(this.checkBoxText);
            this.Controls.Add(this.comboBoxTitleSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTitlePos);
            this.Controls.Add(this.checkBoxTitle);
            this.Controls.Add(this.checkBoxTitleBold);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "ReceiptPrinter Win";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.RadioButton radioButtonJob;
        private System.Windows.Forms.RadioButton radioButtonNow;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.CheckBox checkBoxTitleBold;
        private System.Windows.Forms.CheckBox checkBoxTitle;
        private System.Windows.Forms.ComboBox comboBoxTitlePos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTitleSize;
        private System.Windows.Forms.CheckBox checkBoxText;
        private System.Windows.Forms.CheckBox checkBoxTextBold;
        private System.Windows.Forms.ComboBox comboBoxTextSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTextPos;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.CheckBox checkBoxPic;
        private System.Windows.Forms.TextBox textBoxPic;
        private System.Windows.Forms.Button buttonPicUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton radioButtonPicFile;
        private System.Windows.Forms.RadioButton radioButtonPicURL;
    }
}

