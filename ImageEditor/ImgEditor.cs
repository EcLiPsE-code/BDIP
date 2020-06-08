using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Numerics;
using Color = java.awt.Color;
using java.awt.image;

namespace ImageEditor
{
    public enum Filter
    {
        Blur,
        Sharpening,
        Laplas,
        Emboss135,
        Emboss90,
        Null
    }

    public static class FilterMethods
    {
        public static string GetString(this Filter fl)
        {
            switch (fl)
            {
                case Filter.Blur:
                    return "Размытие по Гауссу";
                case Filter.Sharpening:
                    return "Заострение";
                case Filter.Laplas:
                    return "Лаплас";
                case Filter.Emboss135:
                    return "Emboss 135 Degres";
                case Filter.Emboss90:
                    return "Emboss 90 Degres";
                default:
                    return "UNKNOWN";
            }
        }

        public static Filter GetConst(string value)
        {
            switch (value)
            {
                case "Gaussian Blur":
                    return Filter.Blur;
                case "Sharpening":
                    return Filter.Sharpening;
                case "Laplas":
                    return Filter.Laplas;
                case "Emboss 135 Degres":
                    return Filter.Emboss135;
                case "Emboss 90 Degres":
                    return Filter.Emboss90;
                default:
                    return Filter.Null;
            }
        }
    }

    public class ImgEditor
    {
        private Image image;
        private Image stockImage;
        public int SizeX { get; private set; } //ширина изображения в пискелях
        public int SizeY { get; private set; } //высота изображения в пискселях
        public int Colors { get; private set; } //цвет изображения (RGB)

        private int[][] imgInt; //хранит цвет пикселей

        private float[] luma; //хранит яркость пикселей
        private byte[] redRange; 
        private byte[] greenRange;
        private byte[] blueRange;
        private Complex[][] fftBuf;

        // Blur (/8)
        private int[] blurFilter = new int[] { 0, 1, 0, 1, 4, 1, 0, 1, 0};

        // Sharpening (/5)
        private int[] sharpFilter = new int[] { 0, -1, 0, -1, 9, -1, 0, -1, 0};

        // Laplasian (/1 + 128)
        private int[] laplasFilter = new int[] { 1, 1, 1, 1, -8, 1, 1, 1, 1};

        // Emboss (/1 + 128)
        private int[] embossFilter = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, -1};

        // Emboss (/2 + 128)
        private int[] embossFilter2 = new int[] { 0, 1, 0, 0, 0, 0, 0, -1, 0};

        /// <summary>
        /// Загрузка изображения
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
            SetImageInfo();
            Colors = 256;

            stockImage = (Image)image.Clone(); //устанавливаем начальное изображение
            return true;
        }

        /// <summary>
        /// Метод для установки размера изображения
        /// </summary>
        private void SetImageSizes()
        {
            SizeX = image.Width;
            SizeY = image.Height;
        }

        /// <summary>
        /// рассчитывает определенное кол-во пикселей с определенным уровнем яркости
        /// </summary>
        private void SetImageInfo()
        {
            luma = new float[256]; //диапазон значений яркости

            redRange = new byte[] { 255, 0 }; //диапазон значений красного цвета
            greenRange = new byte[] { 255, 0 }; //диапазон значений зеленого цвета
            blueRange = new byte[] { 255, 0 }; //диапазон значений синего цвета

            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = 0; //устанавливаем начальные значения яркости пикселей
            }

            byte r;
            byte g;
            byte b;

            for (int y = 0; y < SizeY; y++) //SizeY - высота изображения
            {
                for (int x = 0; x < SizeX; x++) //SizeX - ширина изображения
                {
                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(imgInt[y][x] & 0xff);

                    // [0] - Min
                    // [1] - Max
                    redRange[0] = redRange[0] > r ? r : redRange[0];
                    redRange[1] = redRange[1] < r ? r : redRange[1];

                    greenRange[0] = greenRange[0] > g ? g : greenRange[0];
                    greenRange[1] = greenRange[1] < g ? g : greenRange[1];

                    blueRange[0] = blueRange[0] > b ? b : blueRange[0];
                    blueRange[1] = blueRange[1] < b ? b : blueRange[1];

                    // Яркость пикселей
                    luma[Convert.ToInt32(0.2126 * r + 0.7152 * g + 0.0722 * b)] += 1;
                }
            }

            // Вычисляет процент пикселей с определенным уровнем яркости
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = luma[i] / (SizeX * SizeY) * 100;
                System.Diagnostics.Trace.WriteLine(luma[i]);
            }
        }

        /// <summary>
        /// Метод, который строит гистограмму
        /// </summary>
        /// <param name="bmp">Передаваемое изображение</param>
        /// <returns></returns>
        public static Bitmap CalculateBarChar(Bitmap bmp)
        {
            Bitmap barChart;
            if (bmp != null)
            {
                //определяем размеры гистограммы. В идеале ширина должна быть кратна 768
                //по пикселю на каждый столбик каждого из каналов
                int width = 768, height = 600;

                System.Drawing.Color color;
                //создаем саму гистограмму
                barChart = new Bitmap(width, height);
                //создаем массивы, в которых будут содержаться количества повторений для каждого из значений каналов.
                //индекс соответствует значению канала.
                int[] R = new int[256];
                int[] G = new int[256];
                int[] B = new int[256];
                int i, j;

                //собираем статистику по изображению
                for (i = 0; i < bmp.Width; ++i)
                    for (j = 0; j < bmp.Height; ++j)
                    {
                        color = bmp.GetPixel(i, j); //возвращает цвет указанного пикселя
                        ++R[color.R]; //получает значение красного компонента
                        ++G[color.G]; //получает значение зеленого компонента
                        ++B[color.B]; //получает значение синего компонента
                    }
                //находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
                int max = 0;
                for (i = 0; i < 256; ++i)
                {
                    if (R[i] > max)
                        max = R[i];
                    if (G[i] > max)
                        max = G[i];
                    if (B[i] > max)
                        max = B[i];
                }
                //определяем коэффициент масштабирования по высоте
                double point = (double)max / height;
                //отрисовываем столбец за столбцом нашу гистограмму с учетом масштаба
                for (i = 0; i < width - 3; ++i)
                {
                    for (j = height - 1; j > height - R[i / 3] / point; --j)
                    {
                        barChart.SetPixel(i, j, System.Drawing.Color.Red);
                    }
                    ++i;
                    for (j = height - 1; j > height - G[i / 3] / point; --j)
                    {
                        barChart.SetPixel(i, j, System.Drawing.Color.Green);
                    }
                    ++i;
                    for (j = height - 1; j > height - B[i / 3] / point; --j)
                    {
                        barChart.SetPixel(i, j, System.Drawing.Color.Blue);
                    }
                }
            }
            else
                barChart = new Bitmap(1, 1);
            return barChart;
        }

        public static Bitmap ResizeImg(ref Bitmap bitmapImg, int size)
        {
            return ResizeImg(ref bitmapImg, size);
        }

        /// <summary>
        /// Уменьшает цветовую палитру изображения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DecreaseQuantFrequency(int value)
        {
            if (value %2 != 0 || Colors == 1) return false;
            
            // Новые значения цветовой палитры
            int newColors = Colors / value;
            int[] palette = new int[newColors];
            int defValue = (256 / Colors) * value;

            // Fill palette
            for (int i = 0; i < newColors; i++)
            {
                palette[i] = i * defValue;
            }

            byte r;
            byte g;
            byte b;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(imgInt[y][x] & 0xff);

                    r = (byte)palette[r / defValue];
                    g = (byte)palette[g / defValue];
                    b = (byte)palette[b / defValue];

                    imgInt[y][x] = System.Drawing.Color.FromArgb(r, g, b).ToArgb();
                }
            }

            Colors /= value;
            SetImageInfo();
            image = GetBitmapFromInt();

            return true;
        }

        public async void DecreaseQuantFrequencyAsync(int value)
        {
            await Task.Run(() => DecreaseQuantFrequency(value));
        }

        public void ChangeImageSize(float scale)
        {
            int[][] newImgInt = new int[(int)(SizeY * scale)][];
            int newSizeX = (int)(SizeX * scale);
            int newSizeY = (int)(SizeY * scale);

            for (int i = 0; i < newSizeY; i++)
            {
                newImgInt[i] = new int[newSizeX];
            }

            for (int y = 0; y < newSizeY; y++)
            {
                for (int x = 0; x < newSizeX; x++)
                {
                    newImgInt[y][x] = imgInt[(int)(y / scale)][(int)(x / scale)];
                }
            }

            imgInt = newImgInt;
            SizeX = newSizeX;
            SizeY = newSizeY;
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void ChangeImageSizeAsync(int scale)
        {
            await Task.Run(() => ChangeImageSize(scale));
        }

        /// <summary>
        /// Performs linear contrasting of the image in a given range (lmin, lmax)
        /// </summary>
        /// <param name="lmin"></param>
        /// <param name="lmax"></param>
        public void LinearContrast(int lmin, int lmax)
        {
            byte r, g, b;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(imgInt[y][x] & 0xff);

                    // Values are calculated based on the formula
                    r = (byte)((r - redRange[0]) * (lmax - lmin) / (redRange[1] - redRange[0]) + lmin);
                    g = (byte)((g - greenRange[0]) * (lmax - lmin) / (greenRange[1] - greenRange[0]) + lmin);
                    b = (byte)((b - blueRange[0]) * (lmax - lmin) / (blueRange[1] - blueRange[0]) + lmin);

                    imgInt[y][x] = System.Drawing.Color.FromArgb(r, g, b).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void LinearContrastAsync(int lmin, int lmax)
        {
            await Task.Run(() => LinearContrast(lmin, lmax));
        }

        /// <summary>
        /// Changes the dynamic range of the image in the specified range
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void ChangeDynamicRange(byte minValue, byte maxValue)
        {

            byte[] rgb = new byte[3];

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    rgb[0] = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    rgb[1] = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    rgb[2] = (byte)(imgInt[y][x] & 0xff);

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

                    imgInt[y][x] = System.Drawing.Color.FromArgb(rgb[0], rgb[1], rgb[2]).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void ChangeDynamicRangeAsync(byte minValue, byte maxValue)
        {
            await Task.Run(() => ChangeDynamicRange(minValue, maxValue));
        }

        /// <summary>
        /// Sets a random brightness value to a randomly selected pixel
        /// </summary>
        public void SetRandomBrightness()
        {
            byte r, g, b;
            byte lmin;
            byte lmax;

            Random rand = new Random();

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    lmax = (byte)rand.Next(0, 255);
                    lmin = (byte)rand.Next(0, lmax);

                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(imgInt[y][x] & 0xff);

                    if (rand.Next(0, 2) == 1)
                    {
                        r = (byte)((r - redRange[0]) * (lmax - lmin) / (redRange[1] - redRange[0]) + lmin);
                        g = (byte)((g - greenRange[0]) * (lmax - lmin) / (greenRange[1] - greenRange[0]) + lmin);
                        b = (byte)((b - blueRange[0]) * (lmax - lmin) / (blueRange[1] - blueRange[0]) + lmin);

                        imgInt[y][x] = System.Drawing.Color.FromArgb(r, g, b).ToArgb();
                    }
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void SetRandomBrightnessAsync()
        {
            await Task.Run(() => SetRandomBrightness());
        }

        /// <summary>
        /// Logarithmic conversion
        /// </summary>
        /// <param name="c"></param>
        public void LogConvert(int c)
        {
            byte[] rgb = new byte[3];

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    rgb[0] = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    rgb[1] = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    rgb[2] = (byte)(imgInt[y][x] & 0xff);

                    for (int i = 0; i < rgb.Length; i++)
                    {
                        rgb[i] = (byte)Convert.ToInt32(c * Math.Log(1 + rgb[i]));
                    }

                    imgInt[y][x] = System.Drawing.Color.FromArgb(rgb[0], rgb[1], rgb[2]).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void LogConvertAsync(int c)
        {
            await Task.Run(() => LogConvert(c));
        }

        //возвращает полученное изображение
        public Image GetImage()
        {
            return image;
        }

        //устанавливает начальное изображение
        public void SetStockImage()
        {
            image = (Image)stockImage.Clone();
            SetImageSizes();
            imgInt = GetIntFromImage();
            SetImageInfo();
        }

        //асинхронно вызывается метод SetStockImage
        public async void SetStockImageAsync()
        {
            await Task.Run(() => SetStockImage());
        }

        //Возвращает значения яркости пикселей
        public float[] GetLuma() { return luma; }

        //Возвращает максимальное и минимальное значение яркости
        public byte[] GetMinMaxBrightness()
        {
            byte[] result = new byte[2];

            result[0] = (byte)(0.2126 * redRange[0] + 0.7152 * greenRange[0] + 0.0722 * blueRange[0]);
            result[1] = (byte)(0.2126 * redRange[1] + 0.7152 * greenRange[1] + 0.0722 * blueRange[1]);

            return result;
        }

        //Асинхронно вызывается метод GetMinMaxBrightness
        public async void GetMinMaxBrightnessAsync()
        {
            await Task.Run(() => GetMinMaxBrightness());
        }

        /// <summary>
        /// Метод, который обрезает изоражение по заданным координатам
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yStart"></param>
        /// <param name="yEnd"></param>
        public void CutImage(int xStart, int xEnd, int yStart, int yEnd)
        {
            int xTreshold = xEnd - xStart;
            int yTreshold = yEnd - yStart;

            int[][] newImgInt = new int[yTreshold][];
            int newSizeX = xTreshold;
            int newSizeY = yTreshold;

            for (int i = 0; i < newSizeY; i++)
            {
                newImgInt[i] = new int[newSizeX];
            }


            for (int y = 0; y < yTreshold; y++)
            {
                for (int x = 0; x < xTreshold; x++)
                {
                    newImgInt[y][x] = imgInt[y + yStart][x + xStart];
                }
            }

            imgInt = newImgInt;
            SizeX = newSizeX;
            SizeY = newSizeY;
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        //Асинхронно вызывается метод CutImage
        public async void CutImageAsync(int xStart, int xEnd, int yStart, int yEnd)
        {
            await Task.Run(() => CutImage(xStart, xEnd, yStart, yEnd));
        }

        //Метод, для сохранения отредактированного изображения
        public void SaveImage(string filepath)
        {
            using (Bitmap bmp = new Bitmap(image))
            {
                bmp.Save(filepath, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Эквализация изображения
        /// </summary>
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

        //Асинхронный вызов метода EqualizeHistogram
        public async void EqualizeHistogramAsync()
        {
            await Task.Run(() => EqualizeHistogram());
        }

        /// <summary>
        /// Установка черно-белого изображения
        /// </summary>
        public void SetBlackAndWhite()
        {
            byte r, g, b;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10);
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8);
                    b = (byte)(imgInt[y][x] & 0xff);

                    byte s = (byte)(0.2126 * r + 0.7152 * g + 0.0722 * b);

                    imgInt[y][x] = System.Drawing.Color.FromArgb(s, s, s).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void SetBlackAndWhiteAsync()
        {
            await Task.Run(() => SetBlackAndWhite());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterSize"></param>
        public void LowFrequencyFilter(int filterSize)
        {
            int bias = Convert.ToInt32(Math.Floor(filterSize / 2.0));

            int[][] newImgInt = new int[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                newImgInt[i] = new int[SizeX];
            }

            for (int y = bias; y < SizeY - bias; y++)
            {
                for (int x = bias; x < SizeX - bias; x++)
                {
                    int min = imgInt[y][x] & 255;
                    for (int i = x - bias; i < x + bias; i++)
                    {
                        for (int j = y - bias; j < y + bias; j++)
                        {
                            min = Math.Min(min, imgInt[j][i] & 255);
                        }
                    }
                    newImgInt[y][x] = System.Drawing.Color.FromArgb(min, min, min).ToArgb();
                }
            }

            SizeX -= bias;
            SizeY -= bias;
            imgInt = newImgInt;
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void LowFrequencyFilterAsync(int filterSize)
        {
            await Task.Run(() => LowFrequencyFilter(filterSize));
        }

        /// <summary>
        /// 
        /// </summary>
        public void HightFrequencyFilter()
        {
            int[][] newImgInt = new int[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                newImgInt[i] = new int[SizeX];
            }

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(InnerHff));
                    threads.Add(t);
                    t.Start(new InnerHffParams(i * SizeY / 2, j * SizeX / 2, (i + 1) * SizeY / 2, (j + 1) * SizeX / 2, newImgInt));
                }
            }

            foreach (Thread item in threads)
            {
                item.Join();
            }

            imgInt = newImgInt;
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        private void InnerHff(object param)
        {
            InnerHffParams p = (InnerHffParams)param;

            int xy, x1y1, x1y, xy1, g1, g2;

            for (int y = p.begin1; y < p.end1; y++)
            {
                for (int x = p.begin2; x < p.end2; x++)
                {
                    xy = imgInt[y][x] & 0xff;
                    x1y1 = (x == SizeX - 1 || y == SizeY - 1) ? 0 : (imgInt[y + 1][x + 1] & 0xff);
                    x1y = (x == SizeX - 1) ? 0 : (imgInt[y][x + 1] & 0xff);
                    xy1 = (y == SizeY - 1) ? 0 : (imgInt[y + 1][x] & 0xff);
                    g1 = xy - x1y1;
                    g2 = x1y - xy1;
                    byte g = (byte)Math.Sqrt(g1 * g1 + g2 * g2);

                    p.newImgInt[y][x] = System.Drawing.Color.FromArgb(g, g, g).ToArgb();
                }
            }
        }

        public async void HightFrequencyFilterAsync()
        {
            await Task.Run(() => HightFrequencyFilter());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fl"></param>
        public void SetTripleCoreFilter(Filter fl)
        {
            switch(fl)
            {
                case Filter.Blur:
                    TripleCoreFilter(blurFilter, 8, 0);
                    break;
                case Filter.Sharpening:
                    TripleCoreFilter(sharpFilter, 5, 0);
                    break;
                case Filter.Laplas:
                    TripleCoreFilter(laplasFilter, 1, 128);
                    break;
                case Filter.Emboss135:
                    TripleCoreFilter(embossFilter, 1, 128);
                    break;
                case Filter.Emboss90:
                    TripleCoreFilter(embossFilter2, 2, 128);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="weight"></param>
        /// <param name="add"></param>
        private void TripleCoreFilter(int[] filter, int weight, int add)
        {
            int filterSize = 3;
            int bias = filterSize / 2;
            
            int pixSum;
            byte newC;

            int[][] newImgInt = new int[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                newImgInt[i] = new int[SizeX];
            }

            for (int y = 0; y < bias; y++)
            {
                for (int x = 0; x < bias; x++)
                {
                    newImgInt[y][x] = imgInt[y][x];
                }
            }

            for (int y = bias; y < SizeY - bias; y++)
            {
                for (int x = bias; x < SizeX - bias; x++)
                {
                    pixSum = 0;
                    pixSum += ((imgInt[y - 1][x - 1] & 0xff0000) >> 0x10) * filter[0];
                    pixSum += ((imgInt[y][x - 1] & 0xff0000) >> 0x10) * filter[1];
                    pixSum += ((imgInt[y + 1][x - 1] & 0xff0000) >> 0x10) * filter[2];

                    pixSum += ((imgInt[y - 1][x] & 0xff0000) >> 0x10) * filter[3];
                    pixSum += ((imgInt[y][x] & 0xff0000) >> 0x10) * filter[4];
                    pixSum += ((imgInt[y + 1][x] & 0xff0000) >> 0x10) * filter[5];

                    pixSum += ((imgInt[y - 1][x + 1] & 0xff0000) >> 0x10) * filter[6];
                    pixSum += ((imgInt[y][x + 1] & 0xff0000) >> 0x10) * filter[7];
                    pixSum += ((imgInt[y + 1][x + 1] & 0xff0000) >> 0x10) * filter[8];

                    pixSum = pixSum / weight + add;

                    newC = (byte)Math.Min(Math.Max(0, (int)(pixSum + 0.5)), 255);

                    newImgInt[y][x] = System.Drawing.Color.FromArgb(newC, newC, newC).ToArgb();
                }
            }

            imgInt = newImgInt;
            SetImageInfo();
            image = GetBitmapFromInt();
        }

        public async void TripleCoreFilterAsync(int[] filter, int weight, int add)
        {
            await Task.Run(() => TripleCoreFilter(filter, weight, add));
        }

        public void CalcFftBufer()
        {
            fftBuf = new Complex[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                fftBuf[i] = new Complex[SizeX];
            }

            Complex doublePi = new Complex(-2 * Math.PI, 0);
            doublePi = Complex.Multiply(doublePi, new Complex(0, 1));

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Thread t1 = new Thread(new ParameterizedThreadStart(InnerGetFftBufer));
                    threads.Add(t1);
                    t1.Start(new InnerGetFftBuferParams(i * SizeY / 2, j * SizeX / 2, (i + 1) * SizeY / 2, (j + 1) * SizeX / 2, doublePi));
                }
            }

            foreach (Thread item in threads)
            {
                item.Join();
            }
        }

        private void InnerGetFftBufer(object param)
        {
            InnerGetFftBuferParams p = (InnerGetFftBuferParams)param;

            for (int l = p.begin1; l < p.end1; l++)
            {
                for (int k = p.begin2; k < p.end2; k++)
                {
                    Complex sc = new Complex(0, 0);

                    for (int j = 0; j < SizeY; j++)
                    {
                        for (int i = 0; i < SizeX; i++)
                        {
                            Complex t = new Complex(imgInt[j][i] & 0xff, 0);
                            Complex top = new Complex(((double)(i * k)) / SizeX + ((double)(j * l)) / SizeY, 0);
                            top = Complex.Multiply(top, p.doublePi);
                            t = Complex.Multiply(t, Complex.Exp(top));
                            sc = Complex.Add(sc, t);
                        }
                    }

                    fftBuf[l][k] = sc;
                }
            }
        }

        public void Fft()
        {
            Complex doublePi = Complex.Multiply(new Complex(-2 * Math.PI, 0), new Complex(0, 1));
            int halfWidth = SizeX / 2;
            int halfHeight = SizeY / 2;

            CalcFftBufer();

            double max = -1;

            for (int l = 0; l < SizeY; l++)
            {
                for (int k = 0; k < SizeX; k++)
                {
                    double t = Math.Log(fftBuf[l][k].Magnitude);
                    max = Math.Max(t, max);
                    int x = k + halfWidth * (k < halfWidth ? 1 : -1);
                    int y = l + halfHeight * (l < halfHeight ? 1 : -1);
                    t = t / max * 255.0;
                    int c = (int)t;
                    imgInt[y][x] = System.Drawing.Color.FromArgb(c, c, c).ToArgb();
                }
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }
        
        public void ReverseFft()
        {
            // W = exp(-i * 2 * pi / M) M - max pix numb
            // M = pow(2, n) || M = 2*K

            Complex doublePi = Complex.Multiply(new Complex(2 * Math.PI, 0), new Complex(0, 1));

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Thread t1 = new Thread(new ParameterizedThreadStart(InnerReverseFft));
                    threads.Add(t1);
                    t1.Start(new InnerGetFftBuferParams(i * SizeY / 2, j * SizeX / 2, (i + 1) * SizeY / 2, (j + 1) * SizeX / 2, doublePi));
                }
            }

            foreach (Thread item in threads)
            {
                item.Join();
            }

            SetImageInfo();
            image = GetBitmapFromInt();
        }

        private void InnerReverseFft(object param)
        {
            InnerGetFftBuferParams p = (InnerGetFftBuferParams)param;

            for (int l = p.begin1; l < p.end1; l++)
            {
                for (int k = p.begin2; k < p.end2; k++)
                {
                    Complex sc = new Complex(0, 0);

                    for (int j = 0; j < SizeY; j++)
                    {
                        for (int i = 0; i < SizeX; i++)
                        {
                            Complex tl = new Complex(fftBuf[j][i].Real, fftBuf[j][i].Imaginary);
                            Complex top = new Complex(((double)(i * k)) / SizeX + ((double)(j * l)) / SizeY, 0);
                            top = Complex.Multiply(top, p.doublePi);
                            tl = Complex.Multiply(tl, Complex.Exp(top));
                            sc = Complex.Add(sc, tl);
                        }
                    }

                    int t = (int)(sc.Real / SizeX / SizeY);
                    imgInt[l][k] = System.Drawing.Color.FromArgb(t, t, t).ToArgb();
                }
            }
        }

        /// <summary>
        /// Метод, который устанавливает цвета для пикселей заданного изображения
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Выполняет линейное контрастирование изображения
        /// </summary>
        /// <param name="bitmap">Передаваемое изображение</param>
        /// <returns></returns>
        public static Bitmap SetsContrans(Bitmap bitmap)
        {
            BufferedImage img = new BufferedImage(bitmap);
            Color color;
            for (int i = 0; i < img.getWidth(); i++)
            {
                for (int j = 0; j < img.getHeight(); j++)
                {
                    color = new Color(img.getRGB(i, j));
                    int r = color.getRed();
                    int g = color.getGreen();
                    int b = color.getBlue();

                    if (r > 94) r = 94;
                    else if (r < 28) r = 28;
                    if (g > 94) g = 94;
                    else if (g < 28) g = 28;
                    if (b > 94) b = 94;
                    else if (b < 28) b = 28;

                    img.setRGB(i, j, new Color(r, g, b).getRGB());
                }
            }
            return img.getBitmap();
        }

        /// <summary>
        /// Преобразует в bitmap-овское изображение
        /// </summary>
        /// <returns></returns>
        private Bitmap GetBitmapFromInt()
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

        private class InnerGetFftBuferParams
        {
            public int begin1, begin2, end1, end2;
            public Complex doublePi;

            public InnerGetFftBuferParams(int begin1, int begin2, int end1, int end2, Complex doublePi)
            {
                this.begin1 = begin1;
                this.begin2 = begin2;
                this.end1 = end1;
                this.end2 = end2;
                this.doublePi = doublePi;
            }
        }

        private class InnerHffParams
        {
            public int begin1, begin2, end1, end2;
            public int[][] newImgInt;

            public InnerHffParams(int begin1, int begin2, int end1, int end2, int[][] newImgInt)
            {
                this.begin1 = begin1;
                this.begin2 = begin2;
                this.end1 = end1;
                this.end2 = end2;
                this.newImgInt = newImgInt;
            }
        }
    }
}
