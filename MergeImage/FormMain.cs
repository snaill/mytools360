using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace MergeImage
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void UpdateRowColumn()
        {
            // 
            int nRow = Int32.Parse(RowTextBox.Text);
            if (nRow == 0)
                nRow = 1;
            ColumnTextBox.Text = ((InListBox.Items.Count + nRow - 1) / nRow).ToString();
            RowTextBox.Text = nRow.ToString();
         }

        private void ImportPath(string strPath)
        {
            if (System.IO.File.Exists(strPath))
            {
                if (strPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
                    || strPath.EndsWith(".ico", StringComparison.OrdinalIgnoreCase)
                    || strPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                    || strPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                    || strPath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                {
                    if (!InListBox.Items.Contains(strPath))
                        InListBox.Items.Add(strPath);
                }
            }
            else if (System.IO.Directory.Exists(strPath))
            {
                string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                for (int i = 0; i < strDirs.Length; i++)
                    ImportPath(strDirs[i]);

                string[] strFiles = System.IO.Directory.GetFiles(strPath);
                for (int i = 0; i < strFiles.Length; i++)
                    ImportPath(strFiles[i]);
            }

            UpdateRowColumn();
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

        private void MergeButton_Click(object sender, EventArgs e)
        {
            if (OutTextBox.Text == "")
            {
                OutTextBox.Focus();
                return;
            }

            //
            UpdateRowColumn();

            // Load Images
            int rows = Int32.Parse(RowTextBox.Text);
            int cols = Int32.Parse(ColumnTextBox.Text);
            if ( cols == 0 )
                return;

            int nFinalWidth = 0;
            int nFinalHeight = 0;
            ImageRow[] aImageRows = new ImageRow[ rows ];
            for (int i = 0, j = 0; i < rows; i++)
            {
                ImageRow ir = new ImageRow();
                aImageRows[i] = ir;
                ir.nHeight = 0;
                ir.nWidth = 0;
                ir.nImageWidth = 0;
                ir.aImages = new Image[cols];
                for (int k = 0; k < cols; k++)
                {
                    if (j >= InListBox.Items.Count)
                        break;

                    ir.aImages[k] = Image.FromFile(InListBox.Items[j].ToString());

                    if (ir.aImages[k] == null)
                        throw new Exception("Merge failed!");

                    if ( k == 0 )
                    {
                        ir.nWidth = ir.aImages[k].Width;
                        ir.nHeight = ir.aImages[k].Height;
                        ir.nImageWidth = ir.aImages[k].Width;
                    } 
                    else if ( AutoZoomInCheckBox.Checked )
                    {
                        if (ir.nImageWidth < ir.aImages[k].Width)
                            ir.nImageWidth = ir.aImages[k].Width;
                        if (ir.nHeight < ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }
                    else if (AutoZoomOutCheckBox.Checked)
                    {
                        if (ir.nImageWidth > ir.aImages[k].Width)
                            ir.nImageWidth = ir.aImages[k].Width;
                        if (ir.nHeight > ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }
                    else
                    {
                        ir.nImageWidth = 0;
                        ir.nWidth += ir.aImages[k].Width;
                        if (ir.nHeight < ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }

                    ir.nImageCount++;
                    j++;
                }

                if (AutoZoomInCheckBox.Checked || AutoZoomOutCheckBox.Checked)
                    ir.nWidth = ir.nImageWidth * ir.nImageCount;


            }

            // set final image width/height
            for (int i = 0; i < aImageRows.Length; i++)
            {
                ImageRow ir = aImageRows[i];
                ir.nWidth = 0;
                for (int j = 0; j < ir.nImageCount; j++)
                {
                    if (AutoZoomInCheckBox.Checked || AutoZoomOutCheckBox.Checked)
                    {
                        int desWidth = 0;
                        int desHeight = 0;
                        GetActualSize(ir.aImages[j], ir.nImageWidth, ir.nHeight, ref desWidth, ref desHeight);
                        ir.nWidth += desWidth;
                    }
                    else
                    {
                        ir.nWidth += ir.aImages[j].Width;
                    }
                }

                if (ir.nWidth > nFinalWidth)
                    nFinalWidth = ir.nWidth;
                nFinalHeight += ir.nHeight;
            }

            // create final bitmap
            Bitmap aFinalImage = new Bitmap(nFinalWidth, nFinalHeight);
            Graphics g = Graphics.FromImage(aFinalImage);
            int y = 0;
            for (int i = 0; i < aImageRows.Length; i++)
            {
                int x = 0;
                ImageRow ir = aImageRows[i];
                for ( int j = 0; j < ir.nImageCount; j ++ )
                {
                    if (AutoZoomInCheckBox.Checked || AutoZoomOutCheckBox.Checked)
                    {
                        int desWidth = 0;
                        int desHeight = 0;
                        GetActualSize(ir.aImages[j], ir.nImageWidth, ir.nHeight, ref desWidth, ref desHeight );
                        g.DrawImage(ir.aImages[j], x, y, (float)desWidth, (float)desHeight);
                        x += desWidth;
                    }
                    else
                    {
                        g.DrawImage(ir.aImages[j], x, y, ir.aImages[j].Width, ir.aImages[j].Height);
                        x += ir.aImages[j].Width;
                    }

                }

                y += ir.nHeight;
           }
            
            aFinalImage.Save( OutTextBox.Text );
            MessageBox.Show( "Merge success!");
        }

        private void GetActualSize( Image img, int width, int height, ref int destWidth, ref int destHeight )
        {
            decimal desWidth; decimal desHeight;
            int imgWidth = width;
            int imgHeight = height;

            decimal radioAct = (decimal)img.Width / (decimal)img.Height;
            decimal radioLoc = (decimal)imgWidth / (decimal)imgHeight;
            if (radioAct > radioLoc)                                                       
            {
                decimal dcmZoom = (decimal)imgWidth / (decimal)img.Width;
                desHeight = img.Height * dcmZoom;
                desWidth = imgWidth;
            }
            else
            {
                decimal dcmZoom = (decimal)imgHeight / (decimal)img.Height;
                desWidth = img.Width * dcmZoom;
                desHeight = imgHeight;
            }

            destWidth = (int)desWidth;
            destHeight = (int)desHeight;
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bitmap files|*.bmp|Jpeg files|*.jpg|PNG files|*.png|GIF Files|*.gif";
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
            dlg.Filter = "Bitmap files|*.bmp|Icon files|*.ico|Jpeg files|*.jpg|PNG files|*.png|GIF Files|*.gif";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            InListBox.Items.AddRange(dlg.FileNames);
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

        private void AutoZoomInCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoZoomInCheckBox.Checked)
                AutoZoomOutCheckBox.Checked = false;
        }

        private void AutoZoomOutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoZoomOutCheckBox.Checked)
                AutoZoomInCheckBox.Checked = false;
        }
    }

    class ImageRow
    {
        public int nWidth;
        public int nHeight;
        public int nImageWidth;
        public int nImageCount;
        public Image[] aImages;
    }
}
