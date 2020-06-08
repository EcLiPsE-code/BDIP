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
    public partial class ImageCutter : Form
    {

        private MainWindow parent;
        private int xMax;
        private int yMax;

        public ImageCutter()
        {
            InitializeComponent();
        }

        public ImageCutter(MainWindow parent, int xMax, int yMax)
        {
            this.parent = parent;
            this.xMax = xMax;
            this.yMax = yMax;
            InitializeComponent();

            labelXMax.Text = xMax.ToString();
            labelYMax.Text = yMax.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            KillThis();
        }

        private void KillThis()
        {
            parent.ImageCutterWindow = null;
            Close();
        }

        private void TextOnNumberChecker(TextBox textBox)
        {
            if (textBox.Text.Length == 0)
            {
                textBox.Text = "0";
            }

            else if (textBox.Text[textBox.Text.Length - 1] == '-')
            {
                MessageBox.Show("Only positive numbers!");
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            }
            else
            {
                try
                {
                    Convert.ToInt32(textBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ony numbers!");
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            int xStart = Convert.ToInt32(textXStart.Text);
            int xEnd = Convert.ToInt32(textXEnd.Text);
            int yStart = Convert.ToInt32(textYStart.Text);
            int yEnd = Convert.ToInt32(textYEnd.Text);

            if ((xStart >= xEnd) || (yStart >= yEnd) || (xEnd > xMax) || (yEnd > yMax))
            {
                MessageBox.Show("Wrong data!");
            }
            else
            {
                parent.CutImage(xStart, xEnd, yStart, yEnd);
            }

            KillThis();
        }

        private void textXStart_TextChanged(object sender, EventArgs e)
        {
            TextOnNumberChecker(textXStart);
        }

        private void textXEnd_TextChanged(object sender, EventArgs e)
        {
            TextOnNumberChecker(textXEnd);
        }

        private void textYStart_TextChanged(object sender, EventArgs e)
        {
            TextOnNumberChecker(textYStart);
        }

        private void textYEnd_TextChanged(object sender, EventArgs e)
        {
            TextOnNumberChecker(textYEnd);
        }
    }
}
