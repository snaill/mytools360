using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JavaScriptSupport;

namespace JsMinSharp
{
    public partial class FormMain : Form
    {
        const string TempFile = "tempJs.js";

        public FormMain()
        {
            InitializeComponent();
        }

        private void ImportPath(string strPath)
        {
            if (System.IO.File.Exists(strPath))
            {
                if ( strPath.EndsWith( ".js", StringComparison.OrdinalIgnoreCase ) )
                {
                    if ( !InListBox.Items.Contains( strPath ) )
                        InListBox.Items.Add(strPath);
                }
            }
            else if (System.IO.Directory.Exists(strPath))
            {
                string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                for (int i = 0; i < strDirs.Length; i++)
                    ImportPath(strDirs[i]);

                string[] strFiles = System.IO.Directory.GetFiles(strPath);
                for ( int i = 0; i < strFiles.Length; i ++ )
                    ImportPath(strFiles[i]);
            }
        }

        private void InListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < str.Length; i++)
                    ImportPath(str[i]);
            }
        }

        private void CompressButton_Click(object sender, EventArgs e)
        {
            string strResult = "";
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OutTextBox.Text, false, System.Text.Encoding.Default);

                JavaScriptMinifier jsm = new JavaScriptMinifier();
                for (int i = 0; i < InListBox.Items.Count; i++)
                {
                    string strSrc = InListBox.Items[i].ToString();
                    jsm.Minify(strSrc, TempFile);

                    System.IO.StreamReader sr = new System.IO.StreamReader(TempFile, System.Text.Encoding.Default);
                    sw.Write(sr.ReadToEnd());
                    sr.Close();
                }

                sw.Close();

                System.IO.File.Delete(TempFile);

                strResult = "Compress finish.";
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            MessageBox.Show(strResult, "JsMin#");
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "JavaScript files|*.js";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            OutTextBox.Text = dlg.FileName;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            ImportPath(dlg.SelectedPath);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JavaScript files|*.js";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            InListBox.Items.AddRange( dlg.FileNames );
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            InListBox.Items.RemoveAt(InListBox.SelectedIndex);
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (InListBox.SelectedIndex == 0)
                return;

            int nSelect = InListBox.SelectedIndex;
            object obj = InListBox.Items[nSelect];
            InListBox.Items.RemoveAt(nSelect);
            InListBox.Items.Insert(nSelect - 1, obj);
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (InListBox.SelectedIndex == InListBox.Items.Count - 1)
                return;

            int nSelect = InListBox.SelectedIndex;
            object obj = InListBox.Items[nSelect];
            InListBox.Items.RemoveAt(nSelect);
            InListBox.Items.Insert(nSelect + 1, obj);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            InListBox.Items.Clear();
        }

        private void InListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else 
                e.Effect = DragDropEffects.None; 
        }
    }
}
