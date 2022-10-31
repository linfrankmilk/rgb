using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form   
    {
        variableList pic = new variableList();
        VisionTransform changepic = new VisionTransform();
        string filePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image.Save(saveFile.FileName + ".png");
                
                }
                catch
                {
                    MessageBox.Show("File Saved!");
                }
            }
        }

        private void loadbutton_Click_1(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {   
                    //Get the path of specified file
                    pic.Imgpath = openFile.FileName; ;
                    pic.OriginalImage = new Bitmap(pic.Imgpath);
                    pictureBox1.Image = pic.OriginalImage;                    
                }
                catch 
                {
                    MessageBox.Show("Not Image File");
                }
              
            }
        }

        private void openFile_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFile_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadbutton.Text = "Read\n" + "Image";
            savebutton.Text = "Save\n" + "Image";

        }

        private void Convert_Click(object sender, EventArgs e)
        {
           pic.ChangeImage = new Bitmap(pic.OriginalImage.Width,pic.OriginalImage.Height,System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
           changepic.RGB2Gray(ref pic.OriginalImage, ref pic.ChangeImage);
           ColorPalette pal = pic.ChangeImage.Palette;
            for(int i = 0; i< 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(255,i,i,i);
            }
            pic.ChangeImage.Palette = pal;
            pictureBox1.Image = pic.ChangeImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pic.ChangeImage = new Bitmap(pic.OriginalImage.Width, pic.OriginalImage.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            changepic.RGB2Laplace(ref pic.OriginalImage, ref pic.ChangeImage);
            ColorPalette pal = pic.ChangeImage.Palette;
            for (int i = 0; i < 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            pic.ChangeImage.Palette = pal;
            pictureBox1.Image = pic.ChangeImage;
        }

       
    }
}
