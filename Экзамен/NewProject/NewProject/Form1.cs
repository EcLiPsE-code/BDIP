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

namespace NewProject
{
    public partial class Form1 : Form
    {
        private ImageEditor imageEditor;
        public Form1()
        {
            InitializeComponent();
            imageEditor = new ImageEditor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                imageEditor.LoadImage(fileDialog.FileName);
                pictureBox1.Image = imageEditor.GetImage();
                pictureBox2.Image = imageEditor.GetImage();
                panel1.Visible = true;
                label3.Text = (imageEditor.SizeY).ToString();
                label4.Text = (imageEditor.SizeX).ToString();
            }
        }
        private void DrawChangedtHistogram()
        {
            changedChart.Visible = true;
            label6.Visible = true;

            float[] luma = imageEditor.GetLuma();

            int lumaMax = Convert.ToInt32(luma.Max());
            changedChart.ChartAreas[0].AxisY.Maximum = lumaMax;
            changedChart.ChartAreas[0].AxisY.Interval = lumaMax / 5;

            changedChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            changedChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            changedChart.ChartAreas[0].AxisY.LineColor = Color.White;
            changedChart.ChartAreas[0].AxisX.LineColor = Color.White;

            if (changedChart.Series.Count != 0) changedChart.Series.Clear();

            Series histogram = new Series("Luma");
            histogram.ChartArea = "ChartHistogram2";
            histogram.Color = Color.White;

            for (int x = 0; x < luma.Length; x++)
            {
                histogram.Points.AddXY(x, luma[x]);
            }

            changedChart.Series.Add(histogram);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageEditor.EqualizeHistogram();
            pictureBox2.Refresh();
            pictureBox2.Image = imageEditor.GetImage();
            
            DrawChangedtHistogram();
        }
    }
}
