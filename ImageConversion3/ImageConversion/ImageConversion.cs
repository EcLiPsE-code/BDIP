
{
    public class ImageConversion
    {
        private Image image;
        private Image stockImage;
        private Bitmap BMP;
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int Colors { get; private set; }

        private float[] luma;
        private byte[] redRange;
        private byte[] greenRange;
        private byte[] blueRange;

        /// <summary>
        /// Uploads a picture. If the file is found, then the Image object is created. 
        /// The characteristics of the picture are set and its copy is created.
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

            SetImageInfo();

            stockImage = (Image)image.Clone();
            return true;
        }

        /// <summary>
        /// Set Image characteristics.
        /// Such as: Size, Luma, Min and Max pixel brightness 
        /// </summary>
        private void SetImageInfo()
        {
            SetImageSizes();
            SetImageLuma();
            SetMinMaxBrightness();
            Colors = 256;
        }

        private void SetImageSizes()
        {
            SizeX = image.Width;
            SizeY = image.Height;
        }

        /// <summary>
        /// Set Min and Max pixel brightness of all channels (RGB)
        /// </summary>
        private void SetMinMaxBrightness()
        {
            redRange = new byte[] { 255, 0 };
            greenRange = new byte[] { 255, 0 };
            blueRange = new byte[] { 255, 0 };

            byte red;
            byte green;
            byte blue;

            using (Bitmap bmp = new Bitmap(image))
            {
                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        red = bmp.GetPixel(x, y).R;
                        green = bmp.GetPixel(x, y).G;
                        blue = bmp.GetPixel(x, y).B;

                        // [0] - Min
                        // [1] - Max
                        redRange[0] = redRange[0] > red ? red : redRange[0];
                        redRange[1] = redRange[1] < red ? red : redRange[1];

                        greenRange[0] = greenRange[0] > green ? green : greenRange[0];
                        greenRange[1] = greenRange[1] < green ? green : greenRange[1];

                        blueRange[0] = blueRange[0] > blue ? blue : blueRange[0];
                        blueRange[1] = blueRange[1] < blue ? blue : blueRange[1];
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the number of pixels with a specific brightness level (0 - 255)
        /// </summary>
        private void SetImageLuma()
        {
            luma = new float[256];

            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = 0;
            }

            using (Bitmap bmp = new Bitmap(image))
            {
                byte r;
                byte g;
                byte b;

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        r = bmp.GetPixel(x, y).R;
                        g = bmp.GetPixel(x, y).G;
                        b = bmp.GetPixel(x, y).B;

                        // [Pixel brightness]
                        luma[Convert.ToInt32(0.2126 * r + 0.7152 * g + 0.0722 * b)] += 1;
                    }
                }
            }

            // Calculates the percentage of pixels with a specific brightness level
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = luma[i] / (SizeX * SizeY) * 100;
            }
        }

        /// <summary>
        /// Reduces the color palette
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DecreaseQuantFrequency(int value)
        {
            if (value % 2 != 0) return false;

            // New palette
            int newColors = Colors / value;
            int[] palette = new int[newColors];
            int defValue = (256 / Colors) * value;

            // Fill palette
            for (int i = 0; i < newColors; i++)
            {
                palette[i] = i * defValue;
            }

            using (Bitmap bmp = new Bitmap(image))
            {
                byte r;
                byte g;
                byte b;

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        r = (byte)palette[bmp.GetPixel(x, y).R / defValue];
                        g = (byte)palette[bmp.GetPixel(x, y).G / defValue];
                        b = (byte)palette[bmp.GetPixel(x, y).B / defValue];

                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                image = (Image)bmp.Clone();
                Colors /= value;
                SetImageLuma();
                SetMinMaxBrightness();
            }

            return true;
        }

        public void DecreaseImageSize(int scale)
        {
            using (Bitmap newBmp = new Bitmap(SizeX / scale, SizeY / scale))
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    for (int x = 0; x < SizeX / scale; x++)
                    {
                        for (int y = 0; y < SizeY / scale; y++)
                        {
                            newBmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x * scale, y * scale).ToArgb()));
                        }
                    }
                }

                image = (Image)newBmp.Clone();
                SetImageSizes();
            }
        }

        public void IncreaseImageSize(int scale)
        {
            using (Bitmap newBmp = new Bitmap(SizeX * scale, SizeY * scale))
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    for (int x = 0; x < SizeX * scale; x++)
                    {
                        for (int y = 0; y < SizeY * scale; y++)
                        {
                            newBmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x / scale, y / scale).ToArgb()));
                        }
                    }
                }

                image = (Image)newBmp.Clone();
                SetImageSizes();
            }
        }

        /// <summary>
        /// Performs linear contrasting of the image in a given range (lmin, lmax)
        /// </summary>
        /// <param name="lmin"></param>
        /// <param name="lmax"></param>
        public void LinearContrast(int lmin, int lmax)
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                byte red;
                byte green;
                byte blue;

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        // Values are calculated based on the formula
                        red = (byte)((bmp.GetPixel(x, y).R - redRange[0]) * (lmax - lmin) / (redRange[1] - redRange[0]) + lmin);
                        green = (byte)((bmp.GetPixel(x, y).G - greenRange[0]) * (lmax - lmin) / (greenRange[1] - greenRange[0]) + lmin);
                        blue = (byte)((bmp.GetPixel(x, y).B - blueRange[0]) * (lmax - lmin) / (blueRange[1] - blueRange[0]) + lmin);

                        bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }

                image = (Image)bmp.Clone();
                SetImageLuma();
                SetMinMaxBrightness();
            }
        }

        /// <summary>
        /// Changes the dynamic range of the image in the specified range
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void ChangeDynamicRange(byte minValue, byte maxValue)
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                byte[] rgb = new byte[3];

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        rgb[0] = bmp.GetPixel(x, y).R;
                        rgb[1] = bmp.GetPixel(x, y).G;
                        rgb[2] = bmp.GetPixel(x, y).B;

                        for (int i = 0; i < rgb.Length; i++)
                        {
                            // If the brightness value is out of the specified range, 
                            // the brightness is set equal to the corresponding border of the range
                            if (rgb[i] < minValue || rgb[i] > maxValue)
                            {
                                if (rgb[i] < minValue) rgb[i] = minValue;
                                else rgb[i] = maxValue;
                            }
                        }

                        bmp.SetPixel(x, y, Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                    }
                }

                image = (Image)bmp.Clone();
                SetImageLuma();
                SetMinMaxBrightness();
            }
        }

        /// <summary>
        /// Sets a random brightness value to a randomly selected pixel
        /// </summary>
        public void SetRandomBrightness()
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                byte red;
                byte green;
                byte blue;

                byte lmin;
                byte lmax;

                Random rand = new Random();

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        lmax = (byte)rand.Next(0, 255);
                        lmin = (byte)rand.Next(0, lmax);

                        if (rand.Next(0, 2) == 1)
                        {
                            red = (byte)((bmp.GetPixel(x, y).R - redRange[0]) * (lmax - lmin) / (redRange[1] - redRange[0]) + lmin);
                            green = (byte)((bmp.GetPixel(x, y).G - greenRange[0]) * (lmax - lmin) / (greenRange[1] - greenRange[0]) + lmin);
                            blue = (byte)((bmp.GetPixel(x, y).B - blueRange[0]) * (lmax - lmin) / (blueRange[1] - blueRange[0]) + lmin);

                            bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
                        }
                    }
                }

                image = (Image)bmp.Clone();
                SetImageLuma();
                SetMinMaxBrightness();
            }
        }

        /// <summary>
        /// Logarithmic conversion
        /// </summary>
        /// <param name="c"></param>
        public void LogConvert(int c)
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                byte[] rgb = new byte[3];

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        rgb[0] = bmp.GetPixel(x, y).R;
                        rgb[1] = bmp.GetPixel(x, y).G;
                        rgb[2] = bmp.GetPixel(x, y).B;

                        for (int i = 0; i < rgb.Length; i++)
                        {
                            rgb[i] = (byte)Convert.ToInt32(c * Math.Log(1 + rgb[i]));
                        }

                        bmp.SetPixel(x, y, Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                    }
                }

                image = (Image)bmp.Clone();
                SetImageLuma();
            }
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

        public byte[] GetMinMaxBrightness()
        {
            byte[] result = new byte[2];

            result[0] = (byte)(0.2126 * redRange[0] + 0.7152 * greenRange[0] + 0.0722 * blueRange[0]);
            result[1] = (byte)(0.2126 * redRange[1] + 0.7152 * greenRange[1] + 0.0722 * blueRange[1]);

            return result;
        }

        public void CutImage(int xStart, int xEnd, int yStart, int yEnd)
        {
            int xTreshold = xEnd - xStart;
            int yTreshold = yEnd - yStart;

            using (Bitmap newBmp = new Bitmap(xTreshold, yTreshold))
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    for (int x = 0; x < xTreshold; x++)
                    {
                        for (int y = 0; y < yTreshold; y++)
                        {
                            newBmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x + xStart, y + yStart).ToArgb()));
                        }
                    }
                }

                image = (Image)newBmp.Clone();
                SetImageSizes();
                SetMinMaxBrightness();
                SetImageLuma();
            }
        }



        
  
        /*
        public void SaveImage(string filepath)
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                bmp.Save(filepath, ImageFormat.Jpeg);
            }
        }*/


    }
}
