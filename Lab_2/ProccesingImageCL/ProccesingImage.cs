using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using java.awt;
using java.awt.image;
using Color = java.awt.Color;

namespace ProccesingImageCL
{
    public class ProccesingImage
    {
        /// <summary>
        /// Метод, который служит для построения гистограммы изображения
        /// </summary>
        /// <param name="bmp">Передаваемое изображение по которому строиться гиистограмма</param>
        /// <returns></returns>
        public static Bitmap CalculateBarChar(Bitmap bmp)
        {
            Bitmap barChart;
            //если передаваемое изображение не пустое
            if (bmp != null)
            {
                //определяем размеры гистограммы. В идеале ширина должна быть кратна 768
                //по пикселю на каждый столбик каждого из каналов
                int width = 768, height = 600;
                //создаем саму гистограмму
                System.Drawing.Color color;

                //создаем массивы, в которых будут содержаться количества повторений для каждого из значений каналов.
                //индекс соответствует значению канала.
                barChart = new Bitmap(width, height);
                int[] R = new int[256]; //массив для хранения уровней яркости красного цвета
                int[] G = new int[256];//массив для хранения уровней яркости зеленого цвета
                int[] B = new int[256];//массив для хранения уровней яркости синего цвета
                int i, j; //переменные счетки

                //проходимся по матрице пикселей
                for (i = 0; i < bmp.Width; ++i) //bmp.Width - шарина передпваемого изображения
                    for (j = 0; j < bmp.Height; ++j) //bmp.Height - шарина передпваемого изображения
                    {
                        color = bmp.GetPixel(i, j); //возвращает цвет указанного пикселя
                        ++R[color.R]; //получает значение красного компонента
                        ++G[color.G]; //получает значение зеленого компонента
                        ++B[color.B]; //получает значение синего компонента
                    }
                //находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
                //это необходимо для нормализации гистограммы
                int max = 0;
                //проходимся по всем цветовым каналам и находим максимальное значение
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

                //отрисовываем столбец за столбцом гистограмму с учетов нормализации
                for (i = 0; i < width - 3; ++i)
                {
                    for (j = height - 1; j > height - R[i / 3] / point; --j)
                    {
                        //устанавливает значение яркости для пикселей красного цвета
                        barChart.SetPixel(i, j, System.Drawing.Color.Red);
                    }
                    ++i;
                    for (j = height - 1; j > height - G[i / 3] / point; --j)
                    {
                        //устанавливает значение яркости для пикселей зеленого цвета
                        barChart.SetPixel(i, j, System.Drawing.Color.Green);
                    }
                    ++i;
                    for (j = height - 1; j > height - B[i / 3] / point; --j)
                    {
                        //устанавливает значение яркости для пикседей синего цвета
                        barChart.SetPixel(i, j, System.Drawing.Color.Blue);
                    }
                }
            }
            else
                //если размеры изображение равны 1x1 
                barChart = new Bitmap(1, 1);
            //возвращаем выходное изображение
            return barChart;
        }

        /// <summary>
        /// Высокочастотный фильтр с помощью оператора Собеля
        /// </summary>
        /// <param name="bitmap">Передаваемое изображение</param>
        /// <returns></returns>
        public static Bitmap HightPassFilterSobole(Bitmap bitmap)
        {
            Bitmap image = bitmap; //входное изображение
            Bitmap newImg = bitmap; //выходное изобржение
            int width = image.Width; //ширина входного изображения
            int height = image.Height; //высота входного исображения

            //рассмотренные ниже маски применяются для получения составляющих градиента Gx и Gy.Они используются
            //для вычисления градиента по формуле: Gradient = |Gx| + |Gy|;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } }; //маска
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } }; //маска

            int[,] allPixR = new int[width, height]; //массив, для хранения пикселей красного цвета
            int[,] allPixG = new int[width, height]; //массив, для хранения пикселей зеленого цевата
            int[,] allPixB = new int[width, height]; //массив, для хранения пикселей синего цвета

            int limit = 256 * 256; //размер окрестности изображения

            //проходимся по матрице пикселей, где width - ширина матрицы пикселей, а height - соответственно высота матрицы пикселей
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = image.GetPixel(i, j).R; //получаем значения пикселей красного цвета
                    allPixG[i, j] = image.GetPixel(i, j).G; //получаем значения пикселей зеленого цвета
                    allPixB[i, j] = image.GetPixel(i, j).B; //получаем значения пикселей синего цвета
                }
            }

            int new_rx, new_ry; //переменные для хранения новых значений пикселей красного цвета
            int new_gx, new_gy; //переменные для хранения новых значений пикселей зеленого цвета
            int new_bx, new_by; //переменные для хранения новых значений пикселей синего цвета
            int rc, gc, bc; 
            //проходимся по матрице пикселей, где width - ее ширина, а height - ее высота
            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    new_rx = 0; //устанавливаем начальные значение для пикселей красного цвета
                    new_ry = 0; //устанавливаем начальные значение для пикселей красного цвета
                    new_gx = 0;//устанавливаем начальные значение для пикселей зеленого цвета
                    new_gy = 0;//устанавливаем начальные значение для пикселей красного цвета
                    new_bx = 0;//устанавливаем начальные значение для пикселей синего цвета
                    new_by = 0;//устанавливаем начальные значение для пикселей синего цвета
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    //проходимся по коэффициентам маски gx
                    for (int wi = -1; wi < 2; wi++)
                    {
                        //проходимся по коэффициентам маски gy
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi]; //получаем текущее значение из массива пикселей красного цвета
                            new_rx += gx[wi + 1, hw + 1] * rc; //устанавливаем новое значение яркости красного цвета в указанной точке изображения
                            new_ry += gy[wi + 1, hw + 1] * rc; //устанавливаем новое значение яркости  крсаного цвета в указанной точке изображения

                            gc = allPixG[i + hw, j + wi]; //получаем текущее значение из массива пикселей зеленого цвета
                            new_gx += gx[wi + 1, hw + 1] * gc; //устанавливаем новое значение яркости зеленго цвета в указанной точке изображения
                            new_gy += gy[wi + 1, hw + 1] * gc;  //устанавливаем новое значение яркости зеленого цвета в указанной точке изображения

                            bc = allPixB[i + hw, j + wi]; //получаем текущее значение из массива пикселей синего цвета
                            new_bx += gx[wi + 1, hw + 1] * bc; //устанавливаем новое значение яркости синего цвета в указанной точке изображения
                            new_by += gy[wi + 1, hw + 1] * bc; //устанавливае новое значение яркости зеленго цвета в указанной точке изображения
                        }
                    }
                    //проверка выходит ли маска за границы изображения
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                        newImg.SetPixel(i, j, System.Drawing.Color.Black); //устанавливаем значение яркости для каждого пикселя покрытого фильтром

                        //newImg.SetPixel (i, j, System.Drawing.Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
                    else
                        newImg.SetPixel(i, j, System.Drawing.Color.Transparent); //устанавливает цвет определенный системой
                }
            }
            //Возвращает полученное изображение
            return newImg;
        }
        /// <summary>
        /// Высокочастотный фильтр
        /// </summary>
        /// <param name="bitmap">Передаваемое изображение</param>
        /// <returns>Выходное изображение</returns>
        public static Bitmap HightPassFilter(Bitmap bitmap)
        {
            //преобразуем Bitmap-овское изображение в BufferedImage для более быстрой работы
            BufferedImage image = new BufferedImage(bitmap); //входное изображение
            BufferedImage newImg = new BufferedImage(bitmap); //выходное изобьражение
            int firstI, firstJ, min;
            int[,] filter = new int[3, 3]; //маска (ядро) фильтра, которая используется для обхода по матрице пикселей и вычислении
                                           //новых значений яркости для каждого пикселя покрытого областью фильтра
            //цикл, для прохода по матрице пикселей изображения, где width - ширана, а height - высота изображения
            for (int i = 0; i < image.getWidth(); i++)
            {
                for (int j = 0; j < image.getHeight(); j++)
                {
                    // индекс пикселя по ширине относительно маски (фильтра), центром фильтра является точка i, j
                    firstI = i - filter.GetUpperBound(0) + 1;
                    int sum = 0;
                    //инициализируем маску (ядро) фильтра.
                    filter = new int[,]
                    {
                        {1, -1, -1 },
                        {-1, 9, -1 },
                        {-1, -1, -1 }
                    };
                    // два цикла для прохождения всех элементов входящик в окрестность маски относительно пикселя i, j
                    for (int c = 0; c < filter.GetUpperBound(0) + 1; c++)
                    {
                        // индекс пикселя по высоте относительно маски (фильтра), центром фильтра является точка i, j
                        firstJ = j - filter.GetUpperBound(1) + 1;
                        firstI += c;
                        

                        for (int p = 0; p < filter.GetUpperBound(1) + 1; p++)
                        {
                            firstJ += p;
                            //проверка выходит ли маска фильтра за границы изображения. 
                            if (firstI < 0 || firstI > image.getWidth() - 1 || firstJ < 0 
                                || firstJ > image.getHeight() - 1)
                            {
                                filter[c, p] = 0; //если маска выходит за границы изображения, устанавливаем значение 0.
                            }
                            else
                            {
                                filter[c, p] *= new Color(image.getRGB(firstI, firstJ)).getRed(); //устанавливаем значение красного цвета
                                //sum += filter[c, p];
                            }
                            
                        }
                    }
                    //находим значение яркости
                    for (int c = 0; c < filter.GetUpperBound(0) + 1; c++)
                    {

                        for (int p = 0; p < filter.GetUpperBound(1) + 1; p++)
                        {
                            sum += filter[c, p];

                        }
                    }
                    //контролируем переполенние переменной sum, которая хранит значение цвета пикселя
                    sum = sum < 0 ? 0 : sum;
                    sum = sum > 255 ? 255 : sum;
                    newImg.setRGB(i, j, new Color(sum, sum, sum).getRGB()); //утсанавливаем значение яркости для конкретного пикселя
                }
            }
            //возвращаем готовое изображение
            return newImg.getBitmap();
        }
        /// <summary>
        /// Низкочастотный фильтр
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>Выходное изображение</returns>
        public static Bitmap LowPassFilter(Bitmap bitmap)
        {
            BufferedImage image = new BufferedImage(bitmap);
            BufferedImage newImg = new BufferedImage(bitmap);
            int firstI, firstJ;
            int[,] filter = new int[5, 5]; //маска фильтра, которая используется для перемещения по окрестности
                                           //и расчета новых значений яркости пикселей
            //проходимся по матрице пикселей, где width - ширина, а height - высота матрицы пикселей
            for (int i = 0; i < image.getWidth(); i++)
            {
                for (int j = 0; j < image.getHeight(); j++)
                {
                    // индекс пикселя по ширине относительно маски (фильтра), центром фильтра является точка i, j
                    firstI = i - filter.GetUpperBound(0) + 1;
                    int sum = 0;

                    // два цикла для прохождения всех элементов входящик в окрестность маски относительно пикселя i, j
                    for (int c = 0; c < filter.GetUpperBound(0) + 1; c++)
                    {
                        // индекс пикселя по высоте относительно маски (фильтра), центром фильтра является точка i, j
                        firstJ = j - filter.GetUpperBound(1) + 1;
                        firstI += c;
                        
                        for (int p = 0; p < filter.GetUpperBound(1) + 1; p++)
                        {
                            firstJ += p;
                            if (firstI < 0 || firstI > image.getWidth() - 1 || firstJ < 0 || firstJ > image.getHeight() - 1)
                            {
                                filter[c, p] = int.MinValue;
                            }
                            else
                            {
                                filter[c, p] = new Color(image.getRGB(firstI, firstJ)).getRed(); //Мб сделать для кажого цвета?
                            }
                        }
                    }
                    //Вычисление среднего значения для значения яркости по красному цвету для пикселей
                    for (int c = 0; c < filter.GetUpperBound(0) + 1; c++)
                    {
                        for (int p = 0; p < filter.GetUpperBound(1) + 1; p++)
                        {
                            if (filter[c, p] != int.MinValue)
                            {
                                filter[c, p] /= (filter.GetUpperBound(0) + 1) * (filter.GetUpperBound(1) + 1);
                                sum += filter[c, p];
                            }
                            
                        }
                    }
                    newImg.setRGB(i, j, new Color(sum, sum, sum).getRGB()); //устанавливаем новые значение яркости для пикселя
                }
            }
            //возвращаем полученное изображене
            return newImg.getBitmap();
        }

        /// <summary>
        /// Пороговая обработка изображения
        /// </summary>
        /// <param name="bitmap">Входное изображение</param>
        /// <param name="rRed">Входноез начение красных пикселей</param>
        /// <param name="rGreen">Входное значение зеленых пикселей</param>
        /// <param name="rBlue">Входное значение синих пикселей</param>
        /// <returns>Выходное изображение</returns>
        public static Bitmap ThresholdProcessing(Bitmap bitmap, int rRed, int rGreen, int rBlue)
        {
            //преобразуем входное изображение в BufferedImage для более быстрой обработки изображения
            BufferedImage img = new BufferedImage(bitmap);
            //проходим по матрице пикселей, где width - ширина матрицы, а height - высота матрицы пикселей
            for (int i = 0; i < img.getWidth(); i++) //бежим по x
            {
                for (int j = 0; j < img.getHeight(); j++) //бужим по y
                {
                    Color color = new Color(img.getRGB(i, j)); //возвращает цвет текущего пикселя изображения
                    int red = color.getRed(); //получаем цветовое значение пикселя красного канала
                    int green = color.getGreen(); //получаем  цветовое значение пикселя зеленого канала
                    int blue = color.getBlue(); //получаем цветовое значение пикселя синего канала

                    //следим за перепонением переменной, которая хранит новые значения яркости текущего пикселя изображения
                    red = red <= rRed ? red : 0; //вычмсляем новые значения для пикселей, которые имеют красный цвет
                    green = green <= rGreen ? green : 0; //вычисляем новые значения для пикселлей, которые имеют зеленый цвет
                    blue = blue <= rBlue ? blue : 0; //вычисляем новые значения для пикселей, которые имеют синий цвет

                    img.setRGB(i, j, new Color(red, green, blue).getRGB()); //устанавливаем новые значение для каждого пикселя изображения
                }
            }
            //выходное изображение
            return img.getBitmap();
        }

        /// <summary>
        /// Метод, который устанавливает новый размер для входного изображения
        /// </summary>
        /// <param name="bitmapImg">Входное изображение</param>
        /// <param name="sizeX">Ширина изображения</param>
        /// <param name="sizeY">Высота изображения</param>
        /// <returns>Выходное изображение с заданным размером</returns>
        public static Bitmap ResizeImg(ref Bitmap bitmapImg, int sizeX, int sizeY)
        {
            //Возвращает новое изображение с новым указанным размером
            return new Bitmap(bitmapImg, new Size(sizeX, sizeY));
        }
        /// <summary>
        /// Метод, который изменяет размер передаваемого изображения
        /// </summary>
        /// <param name="bitmapImg">Входное изображение</param>
        /// <param name="size">Ноый размер изображения</param>
        /// <returns>Выходное изображение с заданным размером</returns>
        public static Bitmap ResizeImg(ref Bitmap bitmapImg, int size)
        {
            //возвращает текущее изображение с новым размером
            return ResizeImg(ref bitmapImg, size, size);
        }
    }
}