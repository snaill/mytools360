namespace JsMinSharp
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
            this.CompressButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OutTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InListBox
            // 
            this.InListBox.AllowDrop = true;
            this.InListBox.FormattingEnabled = true;
            this.InListBox.ItemHeight = 12;
            this.InListBox.Location = new System.Drawing.Point(15, 32);
            this.InListBox.Name = "InListBox";
            this.InListBox.Size = new System.Drawing.Size(335, 172);
            this.InListBox.TabIndex = 0;
            this.InListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.InListBox_DragDrop);
            this.InListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.InListBox_DragEnter);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(356, 32);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(356, 61);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.Location = new System.Drawing.Point(356, 90);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(75, 23);
            this.UpButton.TabIndex = 3;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(356, 119);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(75, 23);
            this.DownButton.TabIndex = 4;
            this.DownButton.Text = "Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(173, 243);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 5;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.Location = new System.Drawing.Point(356, 211);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(75, 23);
            this.SaveFileButton.TabIndex = 6;
            this.SaveFileButton.Text = "...";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // CompressButton
            // 
            this.CompressButton.Location = new System.Drawing.Point(275, 243);
            this.CompressButton.Name = "CompressButton";
            this.CompressButton.Size = new System.Drawing.Size(75, 23);
            this.CompressButton.TabIndex = 7;
            this.CompressButton.Text = "Compress";
            this.CompressButton.UseVisualStyleBackColor = true;
            this.CompressButton.Click += new System.EventHandler(this.CompressButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Js Files";
            // 
            // OutTextBox
            // 
            this.OutTextBox.Location = new System.Drawing.Point(66, 213);
            this.OutTextBox.Name = "OutTextBox";
            this.OutTextBox.Size = new System.Drawing.Size(284, 21);
            this.OutTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Output:";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(356, 148);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 278);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OutTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompressButton);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.InListBox);
            this.Name = "FormMain";
            this.Text = "JsMin#";
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
        private System.Windows.Forms.Button CompressButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OutTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
    }
}

