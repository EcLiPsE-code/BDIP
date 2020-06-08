using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class FProgressBar : Form
    {
        private MainWindow parent;

        public FProgressBar(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            Opacity = 0;
            StartMainAnim();
        }

        public void StartMainAnim()
        {
            Timer timer = new Timer();
            timer.Interval = 32;
            int max = 30;
            int count = 0;
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (parent.CloseBar)
                {
                    timer.Stop();
                    CloseAnim();
                }
                if (count >= max)
                {
                    Opacity += 0.1;
                    if (Opacity == 1)
                    {
                        timer.Stop();
                        StartAnim();
                    }
                }

                count++;
            });

            timer.Start();
        }

        private void StartAnim()
        {
            Timer timer = new Timer();
            timer.Interval = 5;

            int x = -50;

            Bitmap bmp = new Bitmap(PictureBox.Width, PictureBox.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            g.FillRectangle(Brushes.Black, x, 1, 70, PictureBox.Height - 2);
            PictureBox.Image = bmp;

            timer.Tick += new EventHandler((o, ev) =>
            {
                if (parent.CloseBar)
                {
                    timer.Stop();
                    CloseAnim();
                }

                x += 5;
                g.Clear(Color.White);
                g.FillRectangle(Brushes.Black, x, 1, 70, PictureBox.Height - 2);
                PictureBox.Image = bmp;

                if (x > PictureBox.Width)
                {
                    x = -70;
                }
            });

            timer.Start();
        }

        private void FProgressBar_Load(object sender, EventArgs e)
        {

        }

        private void CloseAnim()
        {
            Timer timer = new Timer();
            timer.Interval = 16;
            timer.Tick += new EventHandler((o, ev) =>
            {
                Opacity -= 0.1;
                if (Opacity == 0)
                {
                    timer.Stop();
                    Close();
                }
            });

            timer.Start();
        }
    }
}
