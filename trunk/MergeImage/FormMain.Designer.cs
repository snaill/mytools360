namespace MergeImage
{
    partial class FormMain
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
            this.InListBox = new System.Windows.Forms.ListBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.MergeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OutTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.RowTextBox = new System.Windows.Forms.TextBox();
            this.ColumnTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OriginSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.AutoZoomOutRadioButton = new System.Windows.Forms.RadioButton();
            this.AutoZoomInRadioButton = new System.Windows.Forms.RadioButton();
            this.ForceSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.HeightTextBox = new System.Windows.Forms.TextBox();
            this.WidthTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InListBox
            // 
            this.InListBox.AllowDrop = true;
            this.InListBox.FormattingEnabled = true;
            this.InListBox.ItemHeight = 12;
            this.InListBox.Location = new System.Drawing.Point(132, 27);
            this.InListBox.Name = "InListBox";
            this.InListBox.Size = new System.Drawing.Size(335, 244);
            this.InListBox.TabIndex = 0;
            this.InListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.InListBox_DragDrop);
            this.InListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.InListBox_DragEnter);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(230, 306);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(311, 306);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.Location = new System.Drawing.Point(230, 335);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(75, 23);
            this.UpButton.TabIndex = 3;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(311, 335);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(75, 23);
            this.DownButton.TabIndex = 4;
            this.DownButton.Text = "Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(392, 306);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 5;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.Location = new System.Drawing.Point(392, 277);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(75, 23);
            this.SaveFileButton.TabIndex = 6;
            this.SaveFileButton.Text = "...";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // MergeButton
            // 
            this.MergeButton.Location = new System.Drawing.Point(14, 335);
            this.MergeButton.Name = "MergeButton";
            this.MergeButton.Size = new System.Drawing.Size(75, 23);
            this.MergeButton.TabIndex = 7;
            this.MergeButton.Text = "Merge";
            this.MergeButton.UseVisualStyleBackColor = true;
            this.MergeButton.Click += new System.EventHandler(this.MergeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Image Files";
            // 
            // OutTextBox
            // 
            this.OutTextBox.Location = new System.Drawing.Point(65, 279);
            this.OutTextBox.Name = "OutTextBox";
            this.OutTextBox.Size = new System.Drawing.Size(321, 21);
            this.OutTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Output:";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(392, 335);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // RowTextBox
            // 
            this.RowTextBox.Location = new System.Drawing.Point(14, 75);
            this.RowTextBox.Name = "RowTextBox";
            this.RowTextBox.Size = new System.Drawing.Size(107, 21);
            this.RowTextBox.TabIndex = 12;
            this.RowTextBox.Text = "1";
            this.RowTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ColumnTextBox
            // 
            this.ColumnTextBox.Location = new System.Drawing.Point(14, 27);
            this.ColumnTextBox.Name = "ColumnTextBox";
            this.ColumnTextBox.Size = new System.Drawing.Size(107, 21);
            this.ColumnTextBox.TabIndex = 13;
            this.ColumnTextBox.Text = "0";
            this.ColumnTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Column";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Row";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OriginSizeRadioButton);
            this.groupBox1.Controls.Add(this.AutoZoomOutRadioButton);
            this.groupBox1.Controls.Add(this.AutoZoomInRadioButton);
            this.groupBox1.Controls.Add(this.ForceSizeRadioButton);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.HeightTextBox);
            this.groupBox1.Controls.Add(this.WidthTextBox);
            this.groupBox1.Location = new System.Drawing.Point(14, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 164);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // OriginSizeRadioButton
            // 
            this.OriginSizeRadioButton.AutoSize = true;
            this.OriginSizeRadioButton.Checked = true;
            this.OriginSizeRadioButton.Location = new System.Drawing.Point(4, 20);
            this.OriginSizeRadioButton.Name = "OriginSizeRadioButton";
            this.OriginSizeRadioButton.Size = new System.Drawing.Size(89, 16);
            this.OriginSizeRadioButton.TabIndex = 28;
            this.OriginSizeRadioButton.TabStop = true;
            this.OriginSizeRadioButton.Text = "Origin Size";
            this.OriginSizeRadioButton.UseVisualStyleBackColor = true;
            // 
            // AutoZoomOutRadioButton
            // 
            this.AutoZoomOutRadioButton.AutoSize = true;
            this.AutoZoomOutRadioButton.Location = new System.Drawing.Point(4, 66);
            this.AutoZoomOutRadioButton.Name = "AutoZoomOutRadioButton";
            this.AutoZoomOutRadioButton.Size = new System.Drawing.Size(101, 16);
            this.AutoZoomOutRadioButton.TabIndex = 27;
            this.AutoZoomOutRadioButton.Text = "Auto Zoom Out";
            this.AutoZoomOutRadioButton.UseVisualStyleBackColor = true;
            // 
            // AutoZoomInRadioButton
            // 
            this.AutoZoomInRadioButton.AutoSize = true;
            this.AutoZoomInRadioButton.Location = new System.Drawing.Point(4, 44);
            this.AutoZoomInRadioButton.Name = "AutoZoomInRadioButton";
            this.AutoZoomInRadioButton.Size = new System.Drawing.Size(95, 16);
            this.AutoZoomInRadioButton.TabIndex = 26;
            this.AutoZoomInRadioButton.Text = "Auto Zoom In";
            this.AutoZoomInRadioButton.UseVisualStyleBackColor = true;
            // 
            // ForceSizeRadioButton
            // 
            this.ForceSizeRadioButton.AutoSize = true;
            this.ForceSizeRadioButton.Location = new System.Drawing.Point(4, 88);
            this.ForceSizeRadioButton.Name = "ForceSizeRadioButton";
            this.ForceSizeRadioButton.Size = new System.Drawing.Size(83, 16);
            this.ForceSizeRadioButton.TabIndex = 22;
            this.ForceSizeRadioButton.Text = "Force Size";
            this.ForceSizeRadioButton.UseVisualStyleBackColor = true;
            this.ForceSizeRadioButton.CheckedChanged += new System.EventHandler(this.ForceSizeRadioButton_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "Width";
            // 
            // HeightTextBox
            // 
            this.HeightTextBox.Enabled = false;
            this.HeightTextBox.Location = new System.Drawing.Point(45, 137);
            this.HeightTextBox.Name = "HeightTextBox";
            this.HeightTextBox.Size = new System.Drawing.Size(62, 21);
            this.HeightTextBox.TabIndex = 23;
            this.HeightTextBox.Text = "48";
            this.HeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // WidthTextBox
            // 
            this.WidthTextBox.Enabled = false;
            this.WidthTextBox.Location = new System.Drawing.Point(45, 110);
            this.WidthTextBox.Name = "WidthTextBox";
            this.WidthTextBox.Size = new System.Drawing.Size(62, 21);
            this.WidthTextBox.TabIndex = 22;
            this.WidthTextBox.Text = "48";
            this.WidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 366);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ColumnTextBox);
            this.Controls.Add(this.RowTextBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OutTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MergeButton);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.InListBox);
            this.Name = "FormMain";
            this.Text = "MergeImage";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox InListBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OutTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox RowTextBox;
        private System.Windows.Forms.TextBox ColumnTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox HeightTextBox;
        private System.Windows.Forms.TextBox WidthTextBox;
        private System.Windows.Forms.RadioButton AutoZoomOutRadioButton;
        private System.Windows.Forms.RadioButton AutoZoomInRadioButton;
        private System.Windows.Forms.RadioButton ForceSizeRadioButton;
        private System.Windows.Forms.RadioButton OriginSizeRadioButton;
    }
}

