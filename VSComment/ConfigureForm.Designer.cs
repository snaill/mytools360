namespace VSComment
{
    partial class ConfigureForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.m_lblCommentType = new System.Windows.Forms.Label();
            this.m_cmbType = new System.Windows.Forms.ComboBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblCommentType
            // 
            this.m_lblCommentType.AutoSize = true;
            this.m_lblCommentType.Location = new System.Drawing.Point(14, 15);
            this.m_lblCommentType.Name = "m_lblCommentType";
            this.m_lblCommentType.Size = new System.Drawing.Size(65, 12);
            this.m_lblCommentType.TabIndex = 0;
            this.m_lblCommentType.Text = "注释类型：";
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.FormattingEnabled = true;
            this.m_cmbType.Items.AddRange(new object[] {
            "Doxygen",
            "Visual Studio XMLDoc"});
            this.m_cmbType.Location = new System.Drawing.Point(24, 40);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(237, 20);
            this.m_cmbType.TabIndex = 1;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(156, 93);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "放弃";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(54, 93);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "确定";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 149);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_cmbType);
            this.Controls.Add(this.m_lblCommentType);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureForm";
            this.Text = "配置VSComment";
            this.Load += new System.EventHandler(this.ConfigureForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblCommentType;
        private System.Windows.Forms.ComboBox m_cmbType;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnOk;
    }
}