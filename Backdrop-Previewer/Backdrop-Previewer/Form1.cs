using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; // for sleep function

namespace Backdrop_Previewer
{
    public partial class Form1 : Form
    {
        
        Image imageFile;
        Random randomGeneratedNumbers = new Random();


        public Form1()
        {
            InitializeComponent();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //file > open
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "PNG(*.PNG)|*.png";
            //string fileName = "";

            if (f.ShowDialog() == DialogResult.OK)
            {
                buttonRandom_Click(sender, e); // random from min and max numbers.
                int x = 0, y = 0, width = 234, height = 156; // size for corner ID 1

                //MessageBox.Show(fileName , "Loaded in"); // shows me the full path.
                imageFile = Image.FromFile(f.FileName); // gets the file forom the Open dialogue. 


                

                //Bitmap source = new Bitmap(imageFile);
                //Bitmap croppedImage = new Bitmap(source.Clone(new System.Drawing.Rectangle(x, y, width, height), source.PixelFormat));
                
                Size backdropSize = new Size(width * 2, height * 2); // makes image x2 as big
                pictureBox1.Size = backdropSize;
                //pictureBox1.Image = croppedImage;

                //CroppedImage cropped = new CroppedImage(0, 0, 234, 156,1,croppedImage);
                CroppedImage cropped = new CroppedImage(0, 0, 234, 156, 1, imageFile);
                CroppedImage cropped2 = new CroppedImage(0, 0, 234, 156, 1, imageFile);
                CroppedImage cropped3 = new CroppedImage(0, 0, 234, 156, 1, imageFile);
                CroppedImage cropped4 = new CroppedImage(0, 0, 234, 156, 1, imageFile);
                cropped.CutImage();
                cropped2.CutImage();
                cropped3.CutImage();
                cropped4.CutImage();
                
                //groupBox1.Controls.Add(cropped.box);

                pictureBox1.Image = cropped.box.Image;
                pictureBox1.Invalidate();


                Image imageFile2 = cropped2.box.Image;
                //geheugen address = croppedImage 

                pictureBox2.Image = imageFile2;
                pictureBox2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); // flips it  like this ---
                pictureBox2.Size = backdropSize;
                pictureBox2.Invalidate();

                Image imageFile3 = cropped3.box.Image;
                
                pictureBox3.Image = imageFile3;
                pictureBox3.Image.RotateFlip(RotateFlipType.RotateNoneFlipY); // flips it  like this |
                pictureBox3.Size = backdropSize;
                pictureBox3.Invalidate();


                Image imageFile4 = cropped4.box.Image;

                pictureBox4.Image = imageFile4;
                imageFile4.RotateFlip(RotateFlipType.RotateNoneFlipXY); // flips it  like this /
                pictureBox4.Size = backdropSize;
                pictureBox4.Invalidate();

                //Image imageFile2 = croppedImage; 
                //pictureBox2.Image = imageFile2;


                //pictureBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipX); // flips it  like this ---

                //Image imageFile3 = Image.FromFile(f.FileName);
                //pictureBox3.Image = imageFile3;
                //pictureBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipY); // flips it  like this |

                //Image imageFile4 = Image.FromFile(f.FileName);
                //pictureBox4.Image = imageFile4;
                //pictureBox4.Image.RotateFlip(RotateFlipType.Rotate180FlipY); // flips it  like this |
                //pictureBox4.Image.RotateFlip(RotateFlipType.Rotate180FlipX); // flips it  like this ---


            }


        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        public void buttonRandom_Click(object sender, EventArgs e)
        {
            //Random takes from 2 values. Debugging reasons currently.
            int minValue = Decimal.ToInt32(numericUpDownMin.Value);
            int maxValue = Decimal.ToInt32(numericUpDownMax.Value) + 1;
            if (minValue < maxValue)
            {
                int randomRoom = randomGeneratedNumbers.Next(minValue, maxValue);
                numericUpDownResult.Value = randomRoom;
            }
        }
    }
}
