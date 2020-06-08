using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageEditor
{
    public partial class MainWindow : Form
    {

        private int xMouse = 0;
        private int yMouse = 0;

        public ImageCutter ImageCutterWindow { get; set; }
        private Bitmap bitmapImg;
        private ImgEditor imgEditor;
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public bool CloseBar { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            imgEditor = new ImgEditor();

            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;
            bgWorker.WorkerSupportsCancellation = true;

            // Настройки для гистограммы
            myChart.Visible = false;
            myChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            myChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            myChart.ChartAreas[0].BackColor = Color.Transparent;
            myChart.ChartAreas[0].AxisX.Minimum = 0;
            myChart.ChartAreas[0].AxisX.Maximum = 255;
            myChart.ChartAreas[0].AxisX.Interval = 30;
            myChart.ChartAreas[0].AxisY.Minimum = 0;

            // ComboBox с названиями фильтров
            ComboBoxFilters.Items.AddRange(new string[] { Filter.Blur.GetString(), Filter.Sharpening.GetString(),
                Filter.Laplas.GetString(), Filter.Emboss135.GetString(), Filter.Emboss90.GetString() });
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CloseBar = false;
            Application.Run(new FProgressBar(this));
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            //e.Error will contain any exceptions caught by the backgroundWorker
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void DrawHistogram()
        {
            myChart.Visible = true;
            labelHistogram.Visible = true;

            float[] luma = imgEditor.GetLuma();

            int lumaMax = Convert.ToInt32(luma.Max());
            myChart.ChartAreas[0].AxisY.Maximum = lumaMax;
            myChart.ChartAreas[0].AxisY.Interval = lumaMax / 5;

            myChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            myChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            myChart.ChartAreas[0].AxisY.LineColor = Color.White;
            myChart.ChartAreas[0].AxisX.LineColor = Color.White;

            if (myChart.Series.Count != 0) myChart.Series.Clear();

            Series histogram = new Series("Luma");
            histogram.ChartArea = "ChartHistogram";
            histogram.Color = Color.White;

            for (int x = 0; x < luma.Length; x++)
            {
                histogram.Points.AddXY(x, luma[x]);
            }

            myChart.Series.Add(histogram);

            SetMinMaxBrightnessInfo();
        }

        /// <summary>
        /// Выврд в label минимальной и максимальной яркости изображения
        /// </summary>
        private void SetMinMaxBrightnessInfo()
        {
            byte[] minmax = imgEditor.GetMinMaxBrightness();

            labelHistMinValue.Text = minmax[0].ToString();
            labelHistMaxValue.Text = (minmax[1]).ToString();
        }

        /// <summary>
        /// Нажатие на кнопку загрузки изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                bgWorker.RunWorkerAsync();

                imgEditor.LoadImage(fileDialog.FileName);
                LabelFilePath.Text = fileDialog.FileName;

                pictureBoxStock.Image = imgEditor.GetImage();
                pictureBoxEdit.Image = null;

                DrawHistogram();
                WriteImageInfo();

                CloseBar = true;
            }
        }

        /// <summary>
        /// Нажатие на кнопку сохранения изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            SaveFileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                imgEditor.SaveImage(fileDialog.FileName);
            }
        }

        /// <summary>
        /// Нажатие на кнопку изменения линейного контраста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinearContrast_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            int min = Convert.ToInt32(textContrastMin.Text);
            int max = Convert.ToInt32(textContrastMax.Text);

            if (!IsRgbValue(min) || !IsRgbValue(max)) return;

            imgEditor.LinearContrast(min, max);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку уменьшения цветовой палитры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecreaseQuant_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.DecreaseQuantFrequency(2);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку увеличения разрешения изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIncreaseSize_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.ChangeImageSize(2);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку уменьшения разрешения изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecreaseSize_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.ChangeImageSize(0.5f);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку добавления шума
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetRandomBrght_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.SetRandomBrightness();
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку установки дефолтного изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetStockImg_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.SetStockImage();
            pictureBoxStock.Image = imgEditor.GetImage();
            pictureBoxEdit.Image = null;
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку изменения динамического диапазона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrc_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            byte min = Convert.ToByte(textDRLeftRange.Text);
            byte max = Convert.ToByte(textDRRightRange.Text);

            if (!IsRgbValue(min) || !IsRgbValue(max)) return;

            bgWorker.RunWorkerAsync();

            imgEditor.ChangeDynamicRange(min, max);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку логарифмического преобразования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            int value = Convert.ToInt32(textLogConvert.Text);

            imgEditor.LogConvert(value);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        private void textDRLeftRange_TextChanged(object sender, EventArgs e)
        {
            CheckTextBox(textDRLeftRange);
        }

        private void textDRRightRange_TextChanged(object sender, EventArgs e)
        {
            CheckTextBox(textDRRightRange);
        }

        private void textContrastMin_TextChanged(object sender, EventArgs e)
        {
            CheckTextBox(textContrastMin);
        }

        private void textContrastMax_TextChanged(object sender, EventArgs e)
        {
            CheckTextBox(textContrastMax);
        }

        private void textLogConvert_TextChanged(object sender, EventArgs e)
        {
            CheckTextBox(textLogConvert);
        }

        /// <summary>
        /// Проверка введённого в поля значения (цифры)
        /// </summary>
        /// <param name="textBox"></param>
        private void CheckTextBox(TextBox textBox)
        {
            if (textBox.Text.Length == 0) return;

            try
            {
                int s = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Entering");
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            }
        }

        /// <summary>
        /// Проверка: лежит ли число в диапазоне [0; 255]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsRgbValue(int value)
        {
            if (value < 0 || value > 255) return false;
            return true;
        }

        /// <summary>
        /// Записывает в label разрешение изображение и частоту квантования
        /// </summary>
        private void WriteImageInfo()
        {
            labelResolution.Text = imgEditor.SizeX.ToString() + " x " + imgEditor.SizeY.ToString();
            labelQuantLevel.Text = imgEditor.Colors.ToString();
        }

        /// <summary>
        /// Обрезает изображение
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yStart"></param>
        /// <param name="yEnd"></param>
        public void CutImage(int xStart, int xEnd, int yStart, int yEnd)
        {
            imgEditor.CutImage(xStart, xEnd, yStart, yEnd);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();
        }

        /// <summary>
        /// Нажатие на кнопку обрезки изображения
        /// Вызывает окно ImageCutter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCutImage_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            if (ImageCutterWindow == null)
            {
                ImageCutterWindow = new ImageCutter(this, imgEditor.SizeX, imgEditor.SizeY);
                ImageCutterWindow.Show();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Нажатие на кнопку эквализации гистограммы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEqHistogram_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.EqualizeHistogram();
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

      
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + (e.X - xMouse), this.Location.Y + (e.Y - yMouse));
            }
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            xMouse = e.X;
            yMouse = e.Y;
        }

        /// <summary>
        /// Нажатие на кнопку преобразования в чёрно-белое
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetBlackAndWhite_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.SetBlackAndWhite();
            pictureBoxStock.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку низкочастотного фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLHFilter_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.LowFrequencyFilter(5);
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку высокочастотного фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHFFilter_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.HightFrequencyFilter();
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        /// <summary>
        /// Нажатие на кнопку установки фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetFilter_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.SetTripleCoreFilter(FilterMethods.GetConst(ComboBoxFilters.Text));
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void BtnFft_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.Fft();
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        private void BtnReverseFFT_Click(object sender, EventArgs e)
        {
            if (pictureBoxStock.Image == null) return;

            bgWorker.RunWorkerAsync();

            imgEditor.ReverseFft();
            pictureBoxEdit.Image = imgEditor.GetImage();
            DrawHistogram();
            WriteImageInfo();

            CloseBar = true;
        }

        private void contransButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                Bitmap newImg;
                Task.Run(() =>
                {
                    newImg = ImgEditor.SetsContrans(new Bitmap(bitmapImg));
                    pictureBoxEdit.Image = newImg;
                    Invoke((Action)(() =>
                    {
                        pictureBoxEdit.Refresh();
                        pictureBoxStock.Refresh();
                        RefrechBarGraph();
                    }));
                });
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void RefrechBarGraph()
        {
            if (bitmapImg != null)
            {
                Bitmap newImg;
                newImg = ImgEditor.CalculateBarChar(new Bitmap((Bitmap)pictureBoxEdit.Image));
                newImg = ImgEditor.ResizeImg(ref newImg, 256);
                //myChart.Image = newImg;
                pictureBoxEdit.Refresh();
                pictureBoxStock.Refresh();
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }
    }
}
