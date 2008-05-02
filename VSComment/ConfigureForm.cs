using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VSComment
{
    public partial class ConfigureForm : Form
    {
        public ConfigureForm()
        {
            InitializeComponent();
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            m_cmbType.Items.Clear();
            m_cmbType.Items.Add(Manager.CommentType_VsXmlDoc);
            m_cmbType.Items.Add(Manager.CommentType_Doxygen);

            string strType = Manager.GetCommentType();
            m_cmbType.SelectedIndex = m_cmbType.FindString(strType);
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            string str = m_cmbType.Items[m_cmbType.SelectedIndex].ToString();
            Manager.SetCommentType(str);
            Close();
        }
    }
}