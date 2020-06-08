using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using java.awt.image;
using ProccesingImageCL;

namespace ProccesingImageWFA
{
    public partial class Form1 : Form
    {
        Image image;
        Bitmap bitmapImg;

        public Form1()
        {
            InitializeComponent();
        }

        private void openImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.bmp, *.png, *.jpg, *jpeg)|*.bmp;*.png;*.jpg;*jpeg|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                image = Image.FromFile(openFileDialog.FileName);
                bitmapImg = new Bitmap(openFileDialog.FileName);
            }
            catch (OutOfMemoryException ex)
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
            
        }

        /// <summary>
        /// Метод, который осуществляет квантование изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuantButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    int n = int.Parse(quantComboBox.Text);
                    Bitmap newImg = ProccesingImage.Quantization(new Bitmap(bitmapImg), n);
                    newPictureBox.Image = newImg;
                    newPictureBox.Refresh();
                    originalPictureBox.Refresh();
                    RefrechBarGraph();
                }
                catch
                {
                    MessageBox.Show("Выберите значение.");
                }
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void ResizeButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    int n = int.Parse(resizeComboBox.Text);
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        newImg = ProccesingImage.DecreaseImageResolution(new Bitmap(bitmapImg), n);
                        newPictureBox.Image = newImg;
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            RefrechBarGraph();
                        }));
                    });
                }
                catch
                {
                    MessageBox.Show("Выберите значение.");
                }
                
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
            
        }

        private void ContransButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                Bitmap newImg;
                Task.Run(() =>
                {
                    newImg = ProccesingImage.SetsContrans(new Bitmap(bitmapImg));
                    newPictureBox.Image = newImg;
                    Invoke((Action)(() =>
                    {
                        newPictureBox.Refresh();
                        originalPictureBox.Refresh();
                        RefrechBarGraph();
                    }));
                });
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void FragmentButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                Bitmap newImg;
                Task.Run(() =>
                {
                    newImg = ProccesingImage.FragmentCut(new Bitmap(bitmapImg));
                    newPictureBox.Image = newImg;
                    Invoke((Action)(() =>
                    {
                        newPictureBox.Refresh();
                        originalPictureBox.Refresh();
                        RefrechBarGraph();
                    }));
                });
            }
            else
            {
                MessageBox.Show("Изображение не загружено.");
            }
        }

        private void RandomReplaceButton_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                try
                {
                    int n = Int16.Parse(precentTextBox.Text);
                    if (n > 100) throw new Exception();
                    Bitmap newImg;
                    Task.Run(() =>
                    {
                        newImg = ProccesingImage.RandomReplacePixel(new Bitmap(bitmapImg), n);
                        newPictureBox.Image = newImg;
                        Invoke((Action)(() =>
                        {
                            newPictureBox.Refresh();
                            originalPictureBox.Refresh();
                            RefrechBarGraph();
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

        //пороговая обработка
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
                            RefrechBarGraph();
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

        private void RefrechBarGraph()
        {
            if (bitmapImg != null)
            {
                Bitmap newImg;
                newImg = ProccesingImage.CreateBarGraph(new Bitmap((Bitmap)newPictureBox.Image));
                barGraphPictureBox.Image = newImg;
                newPictureBox.Refresh();
                originalPictureBox.Refresh();
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

        /// <summary>
        /// Черно-белое изображение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (bitmapImg != null)
            {
                for (int y = 0; y < bitmapImg.Height; y++)
                {
                    for (int x = 0; x < bitmapImg.Width; x++)
                    {
                        Color color = bitmapImg.GetPixel(x, y);
                        byte rgb = (byte)(0.3 * color.R + 0.59 * color.G + 0.11 * color.G);
                        bitmapImg.SetPixel(x, y, Color.FromArgb(color.A, rgb, rgb, rgb));
                    }
                }
                newPictureBox.Image = bitmapImg;
            }
            else
            {
                MessageBox.Show("Изображение не загружено");
            }
        }
    }
}
