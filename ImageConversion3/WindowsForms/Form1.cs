using ClassLibraryWorkImage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsForms
{
    public partial class Form1 : Form
    {

        private EditImageLib imgEditor;

        public Form1()
        {
            InitializeComponent();
            imgEditor = new EditImageLib();
            
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                imgEditor.LoadImage(fileDialog.FileName);
                pictureBox1.Image = imgEditor.GetImage();
                imgEditor.GrayImage();
                pictureBox3.Image = imgEditor.GetImage();
                pictureBox2.Image = null;
            }
        }
     
        private void buttonLowPass_Click(object sender, EventArgs e)
        {
            imgEditor.GrayImage();
            imgEditor.LowPass();
            pictureBox2.Image = imgEditor.GetImage();
        }


        private void buttonHighPass_Click(object sender, EventArgs e)
        {
            imgEditor.GrayImage();
            pictureBox2.Image = imgEditor.Sobel3x3Filter(imgEditor.GetBitmapFromInt());
        }

        private void buttonSetStart_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
          
        }

        private void buttonTrebleBoost_Click(object sender, EventArgs e)
        {
            
            pictureBox2.Image = imgEditor.TrebleBoost();
        }
    }
}
