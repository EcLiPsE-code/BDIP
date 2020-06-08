using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Numerics;

namespace ProccesingImageCL
{
    public class ImgEditor
    {
        private Image image;
        private Image stockImage; //хранит начальное (входное) изображение
        public int SizeX { get; private set; } //ширина изображения
        public int SizeY { get; private set; } //высота изображения
        public int Colors { get; private set; } //определяет цвет пикселей

        private int[][] imgInt; //матрица, для хранения пикселей изображения

        private float[] luma; //массив, для хранения яркости пикседей изображения
        private byte[] redRange; //массив для хранения уровней красного цвета
        private byte[] greenRange; //массив для хранения уровней зеленого цвета
        private byte[] blueRange; //массив дял хранения уровней синего цвета
        private Complex[][] fftBuf; //матрица, для хранения комплексных значений, для прямого метода фурье

        /// <summary>
        /// Метод, предназначенный для загрузки изображения
        /// </summary>
        /// <param name="newImage">Входное изображение</param>
        /// <returns>Выходное изображения</returns>
        public bool LoadImage(Image newImage)
        {
            image = newImage;
            SetImageSizes(); //устанавливает текущий размер изображения
            imgInt = GetIntFromImage(); //преобразует изображение в матрицу пикселей
            SetImageInfo(); //устанавливает цветовые значения для каждого пискеля изображения
            Colors = 256;

            stockImage = (Image)image.Clone(); //устанавливает первоначальное изображение
            return true;
        }

        /// <summary>
        /// Метод, который возвращает значения яркости для указанного изображения
        /// </summary>
        /// <returns></returns>
        public float[] GetLuma() { return luma; }

        /// <summary>
        /// Метод, который позволяет загружать изображение
        /// </summary>
        /// <param name="newImage">Входное изображение</param>
        /// <returns></returns>
        public bool LoadImage(Bitmap newImage)
        {
            image = newImage;
            SetImageSizes();//устанавливает текущий размер изображения
            imgInt = GetIntFromImage();//преобразует изображение в матрицу пикселей
            SetImageInfo();//устанавливает цветовые значения для каждого пискеля изображения
            Colors = 256;

            stockImage = (Image)image.Clone(); //устанавливает первоначальное значение
            return true;
        }

        /// <summary>
        /// Метод, который устанавливает текущее значение ширины и высоты изображения
        /// </summary>
        private void SetImageSizes()
        {
            SizeX = image.Width;
            SizeY = image.Height;
        }

        /// <summary>
        /// Метод, который рассчитывает определенное кол-во пикселей с определенным уровнем яркости
        /// </summary>
        private void SetImageInfo()
        {
            luma = new float[256]; //диапазон значений яркости

            redRange = new byte[] { 255, 0 }; //диапазон значений красного канала
            greenRange = new byte[] { 255, 0 }; //диапазон значений зеленого канала
            blueRange = new byte[] { 255, 0 }; //диапазон значений синего канала

            //проходим по массиву яркости и устанавливаем начальные значение равные 0
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = 0;  //устанавливаем начальные значения яркости пикселей
            }

            byte r; //уровень яркости для красного пикселя
            byte g; //уровень яркости для зеленого пикселя
            byte b; //уровень яркости для синего пикселя

            //проходимся по матрице пикселей изображения, где SizeX - ширина изображения, а SizY - высота соответственно.
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    r = (byte)((imgInt[y][x] & 0xff0000) >> 0x10); //получаем уровень яркости для синего пикселя
                    g = (byte)((imgInt[y][x] & 0xff00) >> 8); //получаем уровень яркости для зеленого пикселя
                    b = (byte)(imgInt[y][x] & 0xff); //получаем уровень яркости для синего пикселя

                    // [0] - минимальное значение среди яркостей пикселей
                    // [1] - максимальное значение среди яркостей пикселей
                    redRange[0] = redRange[0] > r ? r : redRange[0]; //находим минимальное значение яркости для пикселя красного цвета
                    redRange[1] = redRange[1] < r ? r : redRange[1]; //находим максимальное значение яркости дял пикселя красного цвета

                    greenRange[0] = greenRange[0] > g ? g : greenRange[0]; //находим минимальное значения яркости для пикселя зеленого цвета
                    greenRange[1] = greenRange[1] < g ? g : greenRange[1]; //находим максимальное значение яркости для пикселя зеленого цвета

                    blueRange[0] = blueRange[0] > b ? b : blueRange[0]; //находим минимальное значение яркости для пикселя синего цвета
                    blueRange[1] = blueRange[1] < b ? b : blueRange[1]; //находим максимальное значение яркости для пикселя синего цвета

                    // Яркость пикселей
                    luma[Convert.ToInt32(0.2126 * r + 0.7152 * g + 0.0722 * b)] += 1; 
                }
            }

            // Устанавливает уровень яркости для каждого пикселя
            for (int i = 0; i < luma.Length; i++)
            {
                luma[i] = luma[i] / (SizeX * SizeY) * 100; //устанавливает значение яркости
                System.Diagnostics.Trace.WriteLine(luma[i]); //трассировка выполнения цикла (необязательно, использовал как проверку значений яркости)
            }
        }

        /// <summary>
        /// Метод Баттервота. С помощью данного метода вычисляется передаточная функция низкочастотного фильтра Баттерворта.
        /// Предаточная функция задается формулой 1/1+(D(u,v)/D0)^2n
        /// </summary>
        /// <param name="kernelSize">Размер ядра</param>
        /// <param name="sigma"></param>
        /// <param name="n">Порядок</param>
        public void Battervord(int kernelSize, double sigma, int n)
        {
            int halfSize = (kernelSize - 1) / 2;
            double[,] battervortKernel = new double[kernelSize, kernelSize];
            double x = -halfSize;
            for (int i = 0; i < kernelSize; i++)
            {
                double y = -halfSize;
                for (int j = 0; j < kernelSize; j++)
                {
                    battervortKernel[i, j] = 1 / (1 + Math.Pow((x + y) / (Math.PI * sigma), 2 * n));
                    y += 1.0;
                }
                x += 1.0;
            }
            Convolve(battervortKernel, kernelSize, halfSize);
        }

        /// <summary>
        /// Метод Гаусса
        /// </summary>
        /// <param name="kernelSize">Размер ядра</param>
        /// <param name="sigma"></param>
        public void Gaussian(int kernelSize, double sigma)
        {
            int halfSize = (kernelSize - 1) / 2;
            double[,] gaussianKernel = new double[kernelSize, kernelSize];
            double x = -halfSize;
            for (int i = 0; i < kernelSize; i++)
            {
                double y = -halfSize;
                for (int j = 0; j < kernelSize; j++)
                {
                    gaussianKernel[i, j] = 1 - Math.Exp(-(x * x + y * y) / (2 * Math.PI * sigma * sigma));
                    y += 1.0;
                }
                x += 1.0;
            }
            Convolve(gaussianKernel, kernelSize, halfSize);
        }

        private void Convolve(double[,] kernel, int kernelSize, int halfsize)
        {
            //проходимся по матрице пикселей изображения, где SizeX - ширина, а SizeY - высота изображения
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    double sum = 0;
                    double kernelSum = 0;
                    for (int k = Math.Max(i - halfsize, 0); k < Math.Min(i + halfsize, SizeX - 1); k++)
                    {
                        for (int l = Math.Max(j - halfsize, 0); l < Math.Min(j + halfsize, SizeY - 1); l++)
                        {
                            sum += (imgInt[l][k] & 0xff) * kernel[k % 5, l % 5];
                            kernelSum += kernel[k % kernelSize, l % kernelSize];
                        }
                    }
                    sum /= kernelSum;

                    // int value = (int)sum << 16 | (int)sum << 8 | (int)sum;
                    // byte value1 = (byte)value;
                    imgInt[j][i] = Color.FromArgb((int)sum, (int)sum, (int)sum).ToArgb();
                }
            }
            SetImageInfo();//устанавливает цветовые значения для каждого пискеля изображения
            //преобразует матрицу пикселей в изображение
            image = GetBitmapFromInt();
        }

        /// <summary>
        /// Метод, который возвращает изображение для его отображения в интерфейсе программы.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            return new Bitmap(image);
        }

        /// <summary>
        /// Эквализация гистограммы.Метод предполагает расчет новых значений яркости 
        /// sk для каждого уровня яркости пикселей исходного изображения.
        /// </summary>
        public void EqualizeHistogram()
        {
            byte nS; //переменная которая используется для хранения нового значения яркости пикселя
            //проходимся по матрице пикселей изображения, где SizeY - высота, а SizeX - ширина изображения
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    byte s = (byte)((imgInt[y][x] & 0xff0000) >> 0x10); //получаем уровень яркости для текущего пикселя изображения
                    float nSf = 0;

                    //рассчитываем новые значения яркости для каждого уровня яркости пикселей
                    for (int i = 0; i < s; i++)
                    {
                        nSf += luma[i] / 100 * 255;
                    }

                    nS = (byte)Math.Min((int)(nSf + 0.5), 255); //рассчитываем новое значения яркости для текущего пикселя изображения

                    imgInt[y][x] = Color.FromArgb(nS, nS, nS).ToArgb(); //устанавливаем новые значения яркости для пикселя
                }
            }

            SetImageInfo();//устанавливает цветовые значения для каждого пискеля изображения
            image = GetBitmapFromInt(); //преобразуем матрицу пикселей в изображения
        }

        /// <summary>
        /// Метод, которые вычисляет 
        /// </summary>
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

        /// <summary>
        /// Метод, для внутренного вычисления прмого метода Фурье
        /// </summary>
        /// <param name="param"></param>
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
                    imgInt[y][x] = Color.FromArgb(c, c, c).ToArgb();
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

            CalcFftBufer();

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
                    imgInt[l][k] = Color.FromArgb(t, t, t).ToArgb();
                }
            }
        }

        /// <summary>
        /// Метод, преобразует изображение в матрицу пикселей
        /// </summary>
        /// <returns>Возвращает матрицу пикселей</returns>
        private int[][] GetIntFromImage()
        {
            int[][] array = new int[SizeY][]; //матрица, которая будет хранить пиксели изображения

            //проходим по высоте изображения
            for (int i = 0; i < SizeY; i++)
            {
                array[i] = new int[SizeX]; //заполняем значения SizeX
            }

            using (Bitmap bmp = new Bitmap(image))
            {
                //проходим итерационно по сформированной матрице пикселей
                for (int y = 0; y < SizeY; y++) 
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        array[y][x] = bmp.GetPixel(x, y).ToArgb(); //устанавливаем значения пикселей
                    }
                }
            }
            //Возвращаетм полученную матрицу пикселей
            return array;
        }

        /// <summary>
        /// Метод преобразует матрицу пикселей в изображение
        /// </summary>
        /// <returns>Выходное изображение</returns>
        private Bitmap GetBitmapFromInt()
        {
            Bitmap bmp = new Bitmap(SizeX, SizeY); //получаем изображение с указанными размерами
            //проходим по матрице пикселей
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    bmp.SetPixel(x, y, Color.FromArgb(imgInt[y][x])); //устанавливет цвет для указанного пикселя
                }
            }
            //возвращает полученное изображение.
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
    }
}
