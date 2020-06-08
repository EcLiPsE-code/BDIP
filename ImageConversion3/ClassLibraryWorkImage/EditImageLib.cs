using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ClassLibraryWorkImage
{
    public class EditImageLib
    {
        
        private Image image;
        private Image stockImage;
        private int[][] imgInt;
        private int[][] imgIntStart;
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int Colors { get; private set; }

        private float[] luma;
        private byte[] redRange;
        private byte[] greenRange;
        private byte[] blueRange;

        /// <summary>
        /// Метод для загрузки изображения
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool LoadImage(string filePath)
        {
            try
            {
                image = Image.FromFile(filePath);
                
            }
            catch (FileNotFoundException)
            {
                return false;
            }

            SetImageSizes();
            imgInt = GetIntFromImage();
            imgIntStart = GetIntFromImage();
            SetImageInfo();
            Colors = 256;

            stockImage = (Image)image.Clone();
            return true;
        }

        private void SetImageInfo()
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
                for (int x = 0; x < SizeX; x++)
                {
                    r = (byte)((imgIntStart[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgIntStart[y][x] & 0xff00) >> 8);
                    b = (byte)(imgIntStart[y][x] & 0xff);

                    // [0] - Min
                    // [1] - Max
                    redRange[0] = redRange[0] > r ? r : redRange[0];
                    redRange[1] = redRange[1] < r ? r : redRange[1];

                    greenRange[0] = greenRange[0] > g ? g : greenRange[0];
                    greenRange[1] = greenRange[1] < g ? g : greenRange[1];

                    blueRange[0] = blueRange[0] > b ? b : blueRange[0];
                    blueRange[1] = blueRange[1] < b ? b : blueRange[1];

                    // [Pixel brightness]
                    luma[Convert.ToInt32(0.2126 * r + 0.7152 * g + 0.0722 * b)] += 1;
                }
            }

            // Calculates the percentage of pixels with a specific brightness level
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = luma[i] / (SizeX * SizeY) * 100;
            }
        }

        private void SetImageSizes()
        {
            SizeX = image.Width;
            SizeY = image.Height;
        }

        /// <summary>
        /// Метод преобразования изображения в чёрно-белый
        /// </summary>
        public void GrayImage()
        {

                byte r, g, b;

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        r = (byte)((imgIntStart[y][x] & 0xff0000) >> 0x10);
                        g = (byte)((imgIntStart[y][x] & 0xff00) >> 8);
                        b = (byte)(imgIntStart[y][x] & 0xff);

                        int val = Convert.ToInt32(r * 0.2126) + Convert.ToInt32(g * 0.7152) +
                                Convert.ToInt32(b * 0.0722);

                        imgInt[y][x] = Color.FromArgb(val, val, val).ToArgb();
                    }
                }

                SetImageInfo();
                image = GetBitmapFromInt();
        }

        /// <summary>
        /// Метод низкочастотного фильтра
        /// </summary>
        /// <param name="c"></param>
        public void LowPass()
        {

            List<int> sumR = new List<int>();

            byte r, g, b;

            byte[] rgb = new byte[3];

            for (int y = 0; y < SizeY - 5; y++)
            {
                for (int x = 0; x < SizeX - 5; x++)
                {
                    int sumRElem = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            r = (byte)((imgIntStart[y + j][x + i] & 0xff0000) >> 0x10);

                            sumRElem += r;

                        }
                    }
                    sumR.Add(sumRElem / 25);
                }
            }

            int index = 0;
            for (int y = 0; y < SizeY - 5; y++)
            {
                for (int x = 0; x < SizeX - 5; x++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (i == 2 && j == 2)
                            {
                                r = (byte)Math.Min(Math.Max(0, Math.Abs(sumR[index])), 255);

                                imgInt[y][x] = Color.FromArgb(r, r, r).ToArgb();
                                index++;
                            }

                        }
                    }

                }
            }
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        /// <summary>
        /// Применение фильтрации.
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="grayscale"></param>
        /// <returns></returns>
        public Bitmap Sobel3x3Filter(Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap,
                                  Sobel3x3Horizontal,
                                    Sobel3x3Vertical,
                                          1.0, 0, grayscale);


            return resultBitmap;
        }

        // матрица, где каждая точка содержит приближенные производные по x
        public double[,] Sobel3x3Horizontal
        {
            get
            {
                return new double[,]
                { { -1,  0,  1, },
                  { -2,  0,  2, },
                  { -1,  0,  1, }, };
            }
        }

        // матрица, где каждая точка содержит приближенные производные по y
        public double[,] Sobel3x3Vertical
        {
            get
            {
                return new double[,]
                { {  1,  2,  1, },
                  {  0,  0,  0, },
                  { -1, -2, -1, }, };
            }
        }

        /// <summary>
        /// Преобразование изображения.
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="xFilterMatrix"></param>
        /// <param name="yFilterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <param name="grayscale"></param>
        /// <returns></returns>
        public Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                        double[,] xFilterMatrix,
                                        double[,] yFilterMatrix,
                                              double factor = 1,
                                                   int bias = 0,
                                         bool grayscale = false)
        {
            BitmapData sourceData =
                           sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                                             ImageLockMode.ReadOnly,
                                        PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;


            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;


            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;


            int filterOffset = 1;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;


                    blueTotal = greenTotal = redTotal = 0.0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blueX += (double)
                                      (pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenX += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redX += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            blueY += (double)
                                      (pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenY += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redY += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];
                        }
                    }


                    blueTotal = Math.Sqrt((blueX * blueX) +
                                          (blueY * blueY));


                    greenTotal = Math.Sqrt((greenX * greenX) +
                                           (greenY * greenY));


                    redTotal = Math.Sqrt((redX * redX) +
                                         (redY * redY));


                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }


                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }


                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }


                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public Bitmap TrebleBoost()
        {
            GrayImage();
            LowPass();
            return Sobel3x3Filter(GetBitmapFromInt());
        }

        public Image GetImage()
        {
            return image;
        }

        public void SetStockImage()
        {
            image = (Image)stockImage.Clone();
            SetImageInfo();
        }

        public float[] GetLuma() { return luma; }

        private int[][] GetIntFromImage()
        {
            int[][] array = new int[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                array[i] = new int[SizeX];
            }

            using (Bitmap bmp = new Bitmap(image))
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        array[y][x] = bmp.GetPixel(x, y).ToArgb();
                    }
                }
            }

            return array;
        }

        public Bitmap GetBitmapFromInt()
        {
            Bitmap bmp = new Bitmap(SizeX, SizeY);
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    bmp.SetPixel(x, y, Color.FromArgb(imgInt[y][x]));
                }
            }

            return bmp;
        }

    }
}
