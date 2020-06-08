using java.awt.image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = java.awt.Color;

namespace NewProject
{
    public class ImageEditor
    {
        private Image image;
        private Image defaultImage;
        private int[][] imgInt;
        private int[][] defaultImgInt;

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int Colors { get; private set; }

        private float[] luma;
        private byte[] redRange;
        private byte[] greenRange;
        private byte[] blueRange;

        public bool LoadImage(string pathFile)
        {
            try
            {
                image = Image.FromFile(pathFile);
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            SetImageSize();
            imgInt = SetIntFromImage();
            defaultImgInt = SetIntFromImage();
            SetImageInfo();
            Colors = 256;
            defaultImage = (Image)image.Clone();
            return true;
        }

        public void SetImageSize()
        {
            image = new Bitmap(image, 256, 256);
            this.SizeX = image.Width;
            this.SizeY = image.Height;
        }
        public int[][] SetIntFromImage()
        {
            int[][] array = new int[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                array[i] = new int[SizeX];
            }

            using(Bitmap bmp = new Bitmap(image))
            {
                for(int y = 0; y < SizeY; y++)
                {
                    for(int x = 0; x < SizeX; x++)
                    {
                        array[y][x] = bmp.GetPixel(x, y).ToArgb();
                    }
                }
            }
            return array;
        }
        public void SetImageInfo()
        {
            luma = new float[256];
            redRange = new byte[] { 255, 0 };
            greenRange = new byte[] { 255, 0 };
            blueRange = new byte[] { 255, 0 };

            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = 0;
            }

            byte r;
            byte g;
            byte b;

            for (int y = 0; y < SizeY; y++)
            {
                for(int x = 0; x < SizeX; x++)
                {
                    r = (byte)((defaultImgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((defaultImgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(defaultImgInt[y][x] & 0xff);

                    // [0] - Min
                    // [1] - Max
                    redRange[0] = redRange[0] > r ? r : redRange[0];
                    redRange[1] = redRange[1] < r ? r : redRange[1];

                    greenRange[0] = greenRange[0] > g ? g : greenRange[0];
                    greenRange[1] = greenRange[1] < g ? g : greenRange[1];

                    blueRange[0] = blueRange[0] > b ? b : blueRange[0];
                    blueRange[1] = blueRange[1] < b ? b : blueRange[1];

                    luma[Convert.ToInt32(0.2126 * r + 0.7152 * g + 0.0722 * b)] += 1;
                }
            }
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = luma[i] / (SizeX * SizeY) * 100;
            }
        }
       

        public void EqualizeHistogram()
        {
            byte nS;
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    byte s = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    float nSf = 0;

                    for (int i = 0; i < s; i++)
                    {
                        nSf += luma[i] / 100 * 255;
                    }

                    nS = (byte)Math.Min((int)(nSf + 0.5), 255);

                    imgInt[y][x] = System.Drawing.Color.FromArgb(nS, nS, nS).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }
        public Image GetImage() { return image; }
        public float[] GetLuma() { return luma; }
        public Bitmap GetBitmapFromInt()
        {
            Bitmap bmp = new Bitmap(SizeX, SizeY);
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(imgInt[y][x]));
                }
            }

            return bmp;
        }
    }
}
