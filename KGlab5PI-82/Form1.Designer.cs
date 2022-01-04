
namespace KGlab5PI_82
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.brightnessValueTB = new System.Windows.Forms.TextBox();
            this.contrastValueTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cancelChangesBrightness = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BWValueTB = new System.Windows.Forms.TextBox();
            this.BWTrackBar = new System.Windows.Forms.TrackBar();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BWTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MainPictureBox.Location = new System.Drawing.Point(0, 29);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(795, 596);
            this.MainPictureBox.TabIndex = 0;
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.Click += new System.EventHandler(this.MainPictureBox_Click);
            this.MainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseDown);
            this.MainPictureBox.MouseLeave += new System.EventHandler(this.MainPictureBox_MouseLeave);
            this.MainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1329, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 24);
            this.toolStripMenuItem1.Text = "Файл";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // brightnessTrackBar
            // 
            this.brightnessTrackBar.Location = new System.Drawing.Point(947, 340);
            this.brightnessTrackBar.Maximum = 255;
            this.brightnessTrackBar.Minimum = -255;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.Size = new System.Drawing.Size(193, 56);
            this.brightnessTrackBar.TabIndex = 0;
            this.brightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.brightnessTrackBar.Scroll += new System.EventHandler(this.brightnessTrackBar_Scroll);
            this.brightnessTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.brightnessTrackBar_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(861, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Яркость";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // brightnessValueTB
            // 
            this.brightnessValueTB.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.brightnessValueTB.Location = new System.Drawing.Point(1146, 340);
            this.brightnessValueTB.Name = "brightnessValueTB";
            this.brightnessValueTB.ReadOnly = true;
            this.brightnessValueTB.Size = new System.Drawing.Size(76, 22);
            this.brightnessValueTB.TabIndex = 7;
            this.brightnessValueTB.TabStop = false;
            this.brightnessValueTB.TextChanged += new System.EventHandler(this.brightnessValueTB_TextChanged);
            // 
            // contrastValueTB
            // 
            this.contrastValueTB.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.contrastValueTB.Location = new System.Drawing.Point(1146, 402);
            this.contrastValueTB.Name = "contrastValueTB";
            this.contrastValueTB.ReadOnly = true;
            this.contrastValueTB.Size = new System.Drawing.Size(76, 22);
            this.contrastValueTB.TabIndex = 10;
            this.contrastValueTB.TabStop = false;
            this.contrastValueTB.TextChanged += new System.EventHandler(this.contrastValueTB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(861, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Контраст";
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.Location = new System.Drawing.Point(947, 402);
            this.contrastTrackBar.Maximum = 100;
            this.contrastTrackBar.Minimum = -100;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.Size = new System.Drawing.Size(193, 56);
            this.contrastTrackBar.TabIndex = 8;
            this.contrastTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.contrastTrackBar.Value = 1;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.contrastTrackBar_Scroll);
            this.contrastTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.contrastTrackBar_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(814, 509);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 26);
            this.button2.TabIndex = 12;
            this.button2.Text = "Серый";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(814, 550);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 26);
            this.button3.TabIndex = 13;
            this.button3.Text = "Негатив";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cancelChangesBrightness
            // 
            this.cancelChangesBrightness.Location = new System.Drawing.Point(1052, 509);
            this.cancelChangesBrightness.Name = "cancelChangesBrightness";
            this.cancelChangesBrightness.Size = new System.Drawing.Size(88, 69);
            this.cancelChangesBrightness.TabIndex = 14;
            this.cancelChangesBrightness.Text = "Отменить слой";
            this.cancelChangesBrightness.UseVisualStyleBackColor = true;
            this.cancelChangesBrightness.Click += new System.EventHandler(this.button4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(953, 509);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 69);
            this.button4.TabIndex = 16;
            this.button4.Text = "Сохранить слой";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(814, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 26);
            this.button1.TabIndex = 11;
            this.button1.Text = "Бинаризация";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BWValueTB
            // 
            this.BWValueTB.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BWValueTB.Location = new System.Drawing.Point(1146, 467);
            this.BWValueTB.Name = "BWValueTB";
            this.BWValueTB.ReadOnly = true;
            this.BWValueTB.Size = new System.Drawing.Size(76, 22);
            this.BWValueTB.TabIndex = 15;
            this.BWValueTB.TabStop = false;
            this.BWValueTB.TextChanged += new System.EventHandler(this.BWValueTB_TextChanged);
            // 
            // BWTrackBar
            // 
            this.BWTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.BWTrackBar.Location = new System.Drawing.Point(947, 464);
            this.BWTrackBar.Maximum = 255;
            this.BWTrackBar.Name = "BWTrackBar";
            this.BWTrackBar.Size = new System.Drawing.Size(193, 56);
            this.BWTrackBar.TabIndex = 17;
            this.BWTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BWTrackBar.Value = 127;
            this.BWTrackBar.Scroll += new System.EventHandler(this.BWTrackBar_Scroll);
            this.BWTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BWTrackBar_MouseUp);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1146, 509);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 69);
            this.button5.TabIndex = 18;
            this.button5.Text = "Сбросить все слои";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(953, 592);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(281, 25);
            this.button6.TabIndex = 19;
            this.button6.Text = "Сбросить выделение области";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(813, 40);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(137, 51);
            this.button7.TabIndex = 20;
            this.button7.Text = "Шум";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(813, 105);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(137, 51);
            this.button8.TabIndex = 21;
            this.button8.Text = "Фильтр Гаусса";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(813, 175);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(137, 48);
            this.button9.TabIndex = 22;
            this.button9.Text = "Равномерный фильтр";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(987, 39);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(115, 52);
            this.button10.TabIndex = 23;
            this.button10.Text = "Резкость 3x3";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(987, 105);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(117, 51);
            this.button11.TabIndex = 24;
            this.button11.Text = "Резкость 5x5";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(987, 175);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(117, 48);
            this.button12.TabIndex = 25;
            this.button12.Text = "Стекло";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1146, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 22);
            this.textBox1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1108, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Значение в центре матрицы";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 627);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.cancelChangesBrightness);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.BWTrackBar);
            this.Controls.Add(this.BWValueTB);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.contrastValueTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.contrastTrackBar);
            this.Controls.Add(this.brightnessValueTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.brightnessTrackBar);
            this.Controls.Add(this.MainPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Лабораторная работа №5";
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BWTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox brightnessValueTB;
        private System.Windows.Forms.TextBox contrastValueTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button cancelChangesBrightness;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox BWValueTB;
        private System.Windows.Forms.TrackBar BWTrackBar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

