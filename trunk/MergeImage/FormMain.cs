using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MergeImage
{
    public partial class FormMain : Form
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        public FormMain()
        {
            InitializeComponent();

            string strOutput = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
            if (!strOutput.EndsWith("\\"))
                strOutput += '\\';
            OutTextBox.Text = strOutput + "MergeImage.bmp";
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
                if (null != GetImageFormat(strPath))
                {
                    InListBox.Items.Add(strPath);
                }
                else if (strPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(strPath, System.Text.Encoding.Default);
                    while (!sr.EndOfStream)
                    {
                        string strFilename = sr.ReadLine();
                        if (System.IO.File.Exists(strFilename) && null != GetImageFormat(strFilename))
                            InListBox.Items.Add(strFilename);
                    }
                    sr.Close();
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

            ImageFormat imgFormat = GetImageFormat(OutTextBox.Text);
            if ( null == imgFormat )
                throw new Exception("Output image format is invalid!");
                        
            //
            UpdateRowColumn();

            // Load Images
            int rows = Int32.Parse(RowTextBox.Text);
            int cols = Int32.Parse(ColumnTextBox.Text);
            if ( cols == 0 )
                return;

            ImageRow[] aImageRows = new ImageRow[ rows ];
            for (int i = 0, j = 0; i < rows; i++)
            {
                ImageRow ir = new ImageRow();
                aImageRows[i] = ir;
                ir.aImages = new Image[cols];
                ir.aImageSize = new Size[cols];
                for (int k = 0; k < cols; k++)
                {
                    if (j >= InListBox.Items.Count)
                        break;

                    string strPath = InListBox.Items[j].ToString();
                    if (strPath.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                    {
                        Icon icon = new Icon(strPath);
                        ir.aImages[k] = IconToAlphaBitmap(icon);
                    }
                    else
                    {
                        ir.aImages[k] = Image.FromFile(strPath);
                    }
                    if (ir.aImages[k] == null)
                        throw new Exception("Merge failed!");

                    ir.nImageCount++;
                    j++;
                }
                
                //
                ir.nHeight = 0;
                ir.nWidth = 0;

                if (AutoZoomInRadioButton.Checked)
                {
                    // get row width/height
                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        if (ir.nHeight < ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }

                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        ir.aImageSize[k] = new Size();
                        ir.aImageSize[k].Height = ir.nHeight;
                        ir.aImageSize[k].Width = ir.aImages[k].Width * ir.nHeight / ir.aImages[k].Height;

                        ir.nWidth += ir.aImageSize[k].Width;
                    }
                }
                else if (AutoZoomOutRadioButton.Checked)
                {
                    ir.nHeight = Int32.MaxValue;
                    ir.nWidth = 0;
                    int nImageWidth = Int32.MaxValue;
                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        if (nImageWidth > ir.aImages[k].Width)
                            nImageWidth = ir.aImages[k].Width;
                        if (ir.nHeight > ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }

                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        ir.aImageSize[k] = new Size();
                        ir.aImageSize[k].Height = ir.nHeight;
                        ir.aImageSize[k].Width = ir.aImages[k].Width * ir.nHeight / ir.aImages[k].Height;
                        ir.nWidth += ir.aImageSize[k].Width;
                    }
                }
                else if (ForceSizeRadioButton.Checked)
                {
                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        ir.aImageSize[k] = new Size();
                        ir.aImageSize[k].Width = Int32.Parse(WidthTextBox.Text);
                        ir.aImageSize[k].Height = Int32.Parse(HeightTextBox.Text);

                        ir.nWidth += Int32.Parse(WidthTextBox.Text);
                    }
                    ir.nHeight = Int32.Parse(HeightTextBox.Text);
                }
                else
                {
                    for (int k = 0; k < ir.nImageCount; k++)
                    {
                        ir.aImageSize[k] = new Size();
                        ir.aImageSize[k].Width = ir.aImages[k].Width;
                        ir.aImageSize[k].Height = ir.aImages[k].Height;

                        ir.nWidth += ir.aImages[k].Width;
                        if (ir.nHeight < ir.aImages[k].Height)
                            ir.nHeight = ir.aImages[k].Height;
                    }
                }
            }

            // set final image width/height
            int nFinalWidth = 0;
            int nFinalHeight = 0;
            for (int i = 0; i < aImageRows.Length; i++)
            {
                ImageRow ir = aImageRows[i];
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
                    g.DrawImage(ir.aImages[j], x, y, ir.aImageSize[j].Width, ir.aImageSize[j].Height);
                    x += ir.aImageSize[j].Width;
                }

                y += ir.nHeight;
           }

           aFinalImage.Save(OutTextBox.Text, imgFormat );
           MessageBox.Show( "Merge success!");

           //
           System.Diagnostics.Process p = new System.Diagnostics.Process();
           p.EnableRaisingEvents = false;
           p.StartInfo = new System.Diagnostics.ProcessStartInfo(OutTextBox.Text);
           p.Start();
           p.Close();
        }

        private ImageFormat GetImageFormat(string strPath)
        {
            if (strPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Bmp;
            else if (strPath.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Icon;
            else if (strPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Jpeg;
            else if (strPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Png;
            else if (strPath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Gif;
            else if (strPath.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
                return ImageFormat.Tiff;

            return null;
        }
        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bitmap files|*.bmp|Jpeg files|*.jpg|PNG files|*.png|GIF Files|*.gif|TIFF Files|*.tiff";
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
            if (InListBox.SelectedIndex == 0 || InListBox.SelectedIndex == -1)
                return;

            int nSelect = InListBox.SelectedIndex;
            object obj = InListBox.Items[nSelect];
            InListBox.Items.RemoveAt(nSelect);
            InListBox.Items.Insert(nSelect - 1, obj);
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (InListBox.SelectedIndex == InListBox.Items.Count - 1 || InListBox.SelectedIndex == -1)
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

        private void ForceSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ForceSizeRadioButton.Checked == true)
            {
                WidthTextBox.Enabled = true;
                HeightTextBox.Enabled = true;
            }
            else
            {
                WidthTextBox.Enabled = false;
                HeightTextBox.Enabled = false;
            }
        }

        private Bitmap IconToAlphaBitmap(Icon ico)
        {
            ICONINFO ii = new ICONINFO();
            GetIconInfo(ico.Handle, out ii);
            Bitmap bmp = Bitmap.FromHbitmap(ii.hbmColor);
            DeleteObject(ii.hbmColor);
            DeleteObject(ii.hbmMask);

            if (Bitmap.GetPixelFormatSize(bmp.PixelFormat) < 32)
                return ico.ToBitmap();

            BitmapData bmData;
            Rectangle bmBounds = new Rectangle(0,0,bmp.Width,bmp.Height);

            bmData = bmp.LockBits(bmBounds,ImageLockMode.ReadOnly, bmp.PixelFormat);
                    
            Bitmap dstBitmap=new Bitmap(bmData.Width, bmData.Height, bmData.Stride, PixelFormat.Format32bppArgb, bmData.Scan0);

            bool IsAlphaBitmap = false;

            for (int y=0; y <= bmData.Height-1; y++)
            {
                for (int x=0; x <= bmData.Width-1; x++)
                {
                    Color PixelColor = Color.FromArgb(Marshal.ReadInt32(bmData.Scan0, (bmData.Stride * y) + (4 * x)));
                    if (PixelColor.A > 0 & PixelColor.A < 255)
                    {
                        IsAlphaBitmap = true;
                        break;
                    }
                }
                if (IsAlphaBitmap) break;
            }

            bmp.UnlockBits(bmData);

            if (IsAlphaBitmap==true)
                return new Bitmap(dstBitmap);
            else
                return new Bitmap(ico.ToBitmap());
                  
        }
    }

    class ImageRow
    {
        public int nWidth;
        public int nHeight;
        public int nImageCount;
        public Size[] aImageSize;
        public Image[] aImages;
    }

    public struct ICONINFO
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }


}
