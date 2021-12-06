using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGlab5PI_82
{
    public partial class Form1 : Form
    {
        readonly int CAPACITY = 256;
        private RedactorComand PR;
        public Form1()
        {
            InitializeComponent();
            PR = new RedactorComand(MainPictureBox.Width);
            enabledControlElement(false);
           
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void enabledControlElement(bool value)
        {
            удалитьToolStripMenuItem.Enabled = value;
            brightnessTrackBar.Enabled = value;
            contrastTrackBar.Enabled = value;
            BWTrackBar.Enabled = false;
            button1.Enabled = value;
            button2.Enabled = value;
            button3.Enabled = value;
            button4.Enabled = value;
            button5.Enabled = value;
            cancelChangesBrightness.Enabled = value;

        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PR.loadImage())
            {
                drawImage();
                enabledControlElement(true);
                drawGistogramm(sender, e);
            }


        }

        private void drawImage()
        {
            MainPictureBox.Image = PR.getImageForPrint();
            MainPictureBox.Invalidate();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPictureBox.Image = null;
            MainPictureBox.Invalidate();
            enabledControlElement(false);
        }

        private void drawGistogramm(object sender, EventArgs e)
        {
            Dictionary<int, int> gistogramm = PR.getBrigGistogramm();
            Bitmap brightness = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics grf = Graphics.FromImage(brightness);
            Pen gistogrammPen = new Pen(Color.Cyan);

            int maxValue = 0;
            for (int i = 0; i < CAPACITY; i++)
            {// находим максимальное значение
                if (gistogramm[i] > maxValue)
                    maxValue = gistogramm[i];
            }

            int raznost = maxValue / brightness.Height;
            grf.FillRectangle(Brushes.White, 0, 0, Width, Height);
            for (int i = 0; i < CAPACITY; i++)
            {// рисуем линии
             
                int period = maxValue / 10;
                int schet = -period;
                for (int h = brightness.Height; h > 0; h -= period / raznost)
                {
                    grf.DrawLine((new Pen(Color.Black)), new Point(0, h), new Point(brightness.Width, h));
                    grf.DrawString((schet+= period).ToString(), new Font("Arial", 7), Brushes.Gray, 0, h-10);

                }
                grf.DrawLine((new Pen(Color.Gray)), new Point(i+1, brightness.Height), new Point(i+1, brightness.Height - (int)(((float)brightness.Height / maxValue) * gistogramm[i])));
            }
            pictureBox2.Image = brightness;
            pictureBox2.Refresh();
            GC.Collect();

           
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            brightnessValueTB.Text = brightnessTrackBar.Value.ToString();

            try
            {
                int value = Convert.ToInt32(brightnessValueTB.Text);
                brightnessTrackBar.Value = value;
                PR.changeBrightness(value);
                drawImage();
                drawGistogramm(sender, e);
                BWValueTB.Text = "0";
                BWTrackBar.Enabled = false;
                contrastValueTB.Text = "0";
                BWTrackBar.Value = 127;
           
                contrastTrackBar.Value = 0;


            }
            catch
            {
                DialogResult rezult = MessageBox.Show("Ошибка изменения яркости",
                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                brightnessTrackBar.Value = 0;
            }
        }

        private void brightnessValueTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            contrastValueTB.Text = contrastTrackBar.Value.ToString();


            try
            {
                double value = Convert.ToDouble(contrastValueTB.Text);
                contrastTrackBar.Value = Convert.ToInt32(value);
                //   value = value * 100 / 101; // ну да, захардкодил, и что с того???
                PR.changeContrast(value);
                drawImage();
                BWValueTB.Text = "0";
                BWTrackBar.Enabled = false;
                BWTrackBar.Value = 127;
                brightnessTrackBar.Value = 0;
                brightnessValueTB.Text = "0";

                drawGistogramm(sender, e);
            }
            catch
            {
                DialogResult rezult = MessageBox.Show("Ошибка изменения  контрастности",
                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                brightnessTrackBar.Value = 0;
            }
        }

        private void contrastValueTB_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PR.cancelChanges();
            BWValueTB.Text = "0";
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            drawImage();
            BWTrackBar.Enabled = false;
            drawGistogramm(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BWTrackBar.Enabled = true;
            BWValueTB.Text = "127";
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            PR.makeBlackWhiteImageOnly();
            drawImage();
            drawGistogramm(sender, e);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            PR.applyChanges();
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            BWValueTB.Text = "0";
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            drawImage();
            BWTrackBar.Enabled = false;
            drawGistogramm(sender, e);
            PR.CalculateAverageRgb();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            PR.grayScale();
            drawImage();
            BWTrackBar.Enabled = false;
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            BWValueTB.Text = "0";
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            drawGistogramm(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PR.negative();
            drawImage();
            BWTrackBar.Enabled = false;
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            BWValueTB.Text = "0";
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            drawGistogramm(sender, e);
        }

        private void BWValueTB_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void BWTrackBar_Scroll(object sender, EventArgs e)
        {
            BWValueTB.Text = BWTrackBar.Value.ToString();
            try
            {
                int value = Convert.ToInt32(BWValueTB.Text);
                BWTrackBar.Value = value;
                PR.makeBlackWhiteImage(value);
                drawImage();
                drawGistogramm(sender, e);
            }
            catch
            {
                DialogResult rezult = MessageBox.Show("Так дела не делаются, не получилось преобразовать в ЧБ",
                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                brightnessTrackBar.Value = 0;
            }
        }

        private void MainPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            PR.returnStart();
            BWValueTB.Text = "0";
            contrastValueTB.Text = "0";
            brightnessValueTB.Text = "0";
            BWTrackBar.Value = 127;
            brightnessTrackBar.Value = 0;
            contrastTrackBar.Value = 0;
            drawImage();
            BWTrackBar.Enabled = false;
            drawGistogramm(sender, e);
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void MainPictureBox_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
