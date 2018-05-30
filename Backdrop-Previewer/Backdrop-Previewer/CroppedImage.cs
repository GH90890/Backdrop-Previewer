using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backdrop_Previewer
{
    class CroppedImage
    {
        //propperties
        
        int posX;
        int posY;
        int width;
        int height;
        int floorID;
        Image image;
        public PictureBox box { get; set; }

        //constructor
        //basic
        public CroppedImage()
        {
            this.posX = 0;
            this.posY = 0;
            this.width = 234;
            this.height = 156;
            this.floorID = 1;
            this.box = new PictureBox();
            //this.image = ;
        }

        //custom
        public CroppedImage(int posX , int posY , int width , int height , int floorID , Image image)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.floorID = floorID;
            this.image = image;

            this.box = new PictureBox();
            box.Height = height;
            box.Width = width;
            box.Image = image;
            box.Location = new Point(posX, posY);
        }

        //methodes , functies

        public void FlipImage(RotateFlipType flipType)
        {
            image.RotateFlip(flipType);
        }

        public static Bitmap Crop(Bitmap bmp) // Code from internet
        {
            int w = bmp.Width;
            int h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                    if (bmp.GetPixel(i, row).R != 255)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                    if (bmp.GetPixel(col, i).R != 255)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        public void CutImage()
        {
            Bitmap source = new Bitmap(image);
            Image croppedImage = new Bitmap(source.Clone(new System.Drawing.Rectangle(posX, posY, width, height), source.PixelFormat));
            this.box.Image = croppedImage;
        }

        // getters en setters


    }
}
