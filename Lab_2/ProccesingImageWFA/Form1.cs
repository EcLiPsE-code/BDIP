using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using java.awt.image;
using ProccesingImageCL;

namespace ProccesingImageWFA
{
    public partial class Form1 : Form
    {
        Image image;
        Bitmap bitmapImg;
        ImgEditor imgEditor;


        public Form1()
        {
            InitializeComponent();
            imgEditor = new ImgEditor();


            //настройки для гистограммы исходного изображения
            chart1.Visible = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Interval = 30;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            //настройки для гистограммы измененного изоражения
            chart2.Visible = false;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].BackColor = Color.Transparent;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 255;
            chart2.ChartAreas[0].AxisX.Interval = 30;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
        }

        /// <summary>
        /// Метод, предназначенный для выбора изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.bmp, *.png, *.jpg, *jpeg)|*.bmp;*.png;*.jpg;*jpeg|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                image = Image.FromFile(openFileDialog.FileName);
                bitmapImg = new Bitmap(openFileDialog.FileName);
                imgEditor.LoadImage(image);
            }
            catch
            {
                MessageBox.Show("Ошибка чтения картинки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (bitmapImg.Size.Height != 256 || bitmapImg.Width != 256)
            {
                bitmapImg = ProccesingImage.ResizeImg(ref bitmapImg, 256);
            }
            originalPictureBox.Image = bitmapImg;
            newPictureBox.Image = bitmapImg;
            DrawChangedHistogram();
            DrawDefaultHistogram();
        }

        private void ThresholdButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        Invoke((Action)(() =>
                        {
                            newImg = ProccesingImage.ThresholdProcessing(new Bitmap(bitmapImg), redTrackBar.Value,
                                                                                            greenTrackBar.Value,
                                                                                            blueTrackBar.Value);
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }

            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void LowPassButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        Invoke((Action)(() =>
                        {
                            newImg = ProccesingImage.LowPassFilter(new Bitmap(bitmapImg));
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }

            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void HightPassButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        Invoke((Action)(() =>
                        {
                            newImg = ProccesingImage.HightPassFilter(new Bitmap(bitmapImg));
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }

            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void HightPassSoboleButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        Invoke((Action)(() =>
                        {
                            newImg = ProccesingImage.HightPassFilterSobole(new Bitmap(bitmapImg));
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }

            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void EqButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        Invoke((Action)(() =>
                        {
                            imgEditor.LoadImage(new Bitmap(bitmapImg));
                            imgEditor.EqualizeHistogram();
                            newImg = imgEditor.GetBitmap();
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        /// <summary>
        /// Прямой метод Фурье
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FftCalcButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    await Task.Run(() =>
                    {
                        var bmp = new Bitmap(bitmapImg);
                        bmp = ProccesingImage.ResizeImg(ref bmp, 64);
                        imgEditor.LoadImage(bmp);
                        ProccesingImage.HightPassFilter(bmp);
                        bmp = imgEditor.GetBitmap();
                        newImg = ProccesingImage.ResizeImg(ref bmp, 256);
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));
                        
                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private async void ReverseFftCalcButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    await Task.Run(() =>
                    {
                        var bmp = new Bitmap(bitmapImg);
                        bmp = ProccesingImage.ResizeImg(ref bmp, 64);
                        imgEditor.LoadImage(bmp);
                        imgEditor.ReverseFft();
                        bmp = imgEditor.GetBitmap();
                        newImg = ProccesingImage.ResizeImg(ref bmp, 256);
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));

                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private async void BatterButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    await Task.Run(() =>
                    {
                        imgEditor.LoadImage(new Bitmap(bitmapImg));
                        imgEditor.Battervord(15, 4, 2);
                        newImg = imgEditor.GetBitmap();
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));

                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private async void GaussBButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    Bitmap newImg;
                    await Task.Run(() =>
                    {
                        imgEditor.LoadImage(new Bitmap(bitmapImg));
                        imgEditor.Gaussian(5, 4);
                        newImg = imgEditor.GetBitmap();
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Image = newImg;
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            DrawChangedHistogram();
                        }));

                    });
                }
                catch
                {
                    MessageBox.Show("Введено некорректное значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DrawDefaultHistogram()
        {
            chart1.Visible = true;
            label14.Visible = true;
            float[] luma = imgEditor.GetLuma();

            int lumaMax = Convert.ToInt32(luma.Max());
            chart1.ChartAreas[0].AxisY.Maximum = lumaMax;
            chart1.ChartAreas[0].AxisY.Interval = lumaMax / 5;

            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;
            chart1.ChartAreas[0].AxisX.LineColor = Color.White;

            if (chart1.Series.Count != 0) chart1.Series.Clear();

            Series histogram = new Series("Luma");
            histogram.ChartArea = "ChartArea1";
            histogram.Color = Color.White;

            for (int x = 0; x < luma.Length; x++)
            {
                histogram.Points.AddXY(x, luma[x]);
            }

            chart1.Series.Add(histogram);
        }
        private void DrawChangedHistogram()
        {
            chart2.Visible = true;
            label17.Visible = true;
            float[] luma = imgEditor.GetLuma();

            int lumaMax = Convert.ToInt32(luma.Max());
            chart2.ChartAreas[0].AxisY.Maximum = lumaMax;
            chart2.ChartAreas[0].AxisY.Interval = lumaMax / 5;

            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart2.ChartAreas[0].AxisY.LineColor = Color.White;
            chart2.ChartAreas[0].AxisX.LineColor = Color.White;

            if (chart2.Series.Count != 0) chart2.Series.Clear();

            Series histogram = new Series("Luma");
            histogram.ChartArea = "ChartHistogram";
            histogram.Color = Color.White;

            for (int x = 0; x < luma.Length; x++)
            {
                histogram.Points.AddXY(x, luma[x]);
            }

            chart2.Series.Add(histogram);
        }
    }
}
