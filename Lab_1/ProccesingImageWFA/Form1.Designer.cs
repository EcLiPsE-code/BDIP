namespace ProccesingImageWFA
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИзображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.newPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.quantComboBox = new System.Windows.Forms.ComboBox();
            this.quantButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.resizeComboBox = new System.Windows.Forms.ComboBox();
            this.resizeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.contransButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.fragmentButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.precentTextBox = new System.Windows.Forms.TextBox();
            this.randomReplaceButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.blueTrackBar = new System.Windows.Forms.TrackBar();
            this.greenTrackBar = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.redTrackBar = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.thresholdButton = new System.Windows.Forms.Button();
            this.barGraphPictureBox = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGraphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьИзображениеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьИзображениеToolStripMenuItem
            // 
            this.открытьИзображениеToolStripMenuItem.Name = "открытьИзображениеToolStripMenuItem";
            this.открытьИзображениеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.открытьИзображениеToolStripMenuItem.Text = "Открыть изображение";
            this.открытьИзображениеToolStripMenuItem.Click += new System.EventHandler(this.openImage_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Location = new System.Drawing.Point(12, 53);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(279, 274);
            this.originalPictureBox.TabIndex = 1;
            this.originalPictureBox.TabStop = false;
            // 
            // newPictureBox
            // 
            this.newPictureBox.Location = new System.Drawing.Point(358, 53);
            this.newPictureBox.Name = "newPictureBox";
            this.newPictureBox.Size = new System.Drawing.Size(297, 274);
            this.newPictureBox.TabIndex = 1;
            this.newPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Исходное изображение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(354, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Преобразованное изображение";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(451, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Уменьшить частоту квантования изображения в";
            // 
            // quantComboBox
            // 
            this.quantComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quantComboBox.FormattingEnabled = true;
            this.quantComboBox.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.quantComboBox.Location = new System.Drawing.Point(470, 356);
            this.quantComboBox.Name = "quantComboBox";
            this.quantComboBox.Size = new System.Drawing.Size(70, 32);
            this.quantComboBox.TabIndex = 4;
            // 
            // quantButton
            // 
            this.quantButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quantButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quantButton.ForeColor = System.Drawing.Color.White;
            this.quantButton.Location = new System.Drawing.Point(577, 356);
            this.quantButton.Name = "quantButton";
            this.quantButton.Size = new System.Drawing.Size(122, 32);
            this.quantButton.TabIndex = 5;
            this.quantButton.Text = "Выполнить";
            this.quantButton.UseVisualStyleBackColor = true;
            this.quantButton.Click += new System.EventHandler(this.QuantButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(372, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Уменьшить разрешение изображения в";
            // 
            // resizeComboBox
            // 
            this.resizeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resizeComboBox.FormattingEnabled = true;
            this.resizeComboBox.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.resizeComboBox.Location = new System.Drawing.Point(470, 401);
            this.resizeComboBox.Name = "resizeComboBox";
            this.resizeComboBox.Size = new System.Drawing.Size(70, 32);
            this.resizeComboBox.TabIndex = 4;
            // 
            // resizeButton
            // 
            this.resizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resizeButton.ForeColor = System.Drawing.Color.White;
            this.resizeButton.Location = new System.Drawing.Point(577, 404);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(122, 32);
            this.resizeButton.TabIndex = 5;
            this.resizeButton.Text = "Выполнить";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.ResizeButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(13, 453);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(503, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "Линейное контрастирование изображения от 28 до 94";
            // 
            // contransButton
            // 
            this.contransButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.contransButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contransButton.ForeColor = System.Drawing.Color.White;
            this.contransButton.Location = new System.Drawing.Point(577, 449);
            this.contransButton.Name = "contransButton";
            this.contransButton.Size = new System.Drawing.Size(122, 32);
            this.contransButton.TabIndex = 5;
            this.contransButton.Text = "Выполнить";
            this.contransButton.UseVisualStyleBackColor = true;
            this.contransButton.Click += new System.EventHandler(this.ContransButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 502);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(546, 24);
            this.label6.TabIndex = 3;
            this.label6.Text = "Вырезать фрагмент изображения и увеличить его в 4 раза";
            // 
            // fragmentButton
            // 
            this.fragmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fragmentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fragmentButton.ForeColor = System.Drawing.Color.White;
            this.fragmentButton.Location = new System.Drawing.Point(577, 498);
            this.fragmentButton.Name = "fragmentButton";
            this.fragmentButton.Size = new System.Drawing.Size(122, 32);
            this.fragmentButton.TabIndex = 5;
            this.fragmentButton.Text = "Выполнить";
            this.fragmentButton.UseVisualStyleBackColor = true;
            this.fragmentButton.Click += new System.EventHandler(this.FragmentButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(13, 551);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(450, 48);
            this.label7.TabIndex = 3;
            this.label7.Text = "Случайным образом заменить значения яркости\r\nзаданного процента пикселей";
            // 
            // precentTextBox
            // 
            this.precentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.precentTextBox.Location = new System.Drawing.Point(470, 565);
            this.precentTextBox.Name = "precentTextBox";
            this.precentTextBox.Size = new System.Drawing.Size(100, 29);
            this.precentTextBox.TabIndex = 6;
            // 
            // randomReplaceButton
            // 
            this.randomReplaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.randomReplaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.randomReplaceButton.ForeColor = System.Drawing.Color.White;
            this.randomReplaceButton.Location = new System.Drawing.Point(577, 564);
            this.randomReplaceButton.Name = "randomReplaceButton";
            this.randomReplaceButton.Size = new System.Drawing.Size(122, 32);
            this.randomReplaceButton.TabIndex = 5;
            this.randomReplaceButton.Text = "Выполнить";
            this.randomReplaceButton.UseVisualStyleBackColor = true;
            this.randomReplaceButton.Click += new System.EventHandler(this.RandomReplaceButton_Click);
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "";
            this.panel1.AccessibleName = "";
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.blueTrackBar);
            this.panel1.Controls.Add(this.greenTrackBar);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.redTrackBar);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(730, 386);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 174);
            this.panel1.TabIndex = 7;
            // 
            // blueTrackBar
            // 
            this.blueTrackBar.Location = new System.Drawing.Point(116, 117);
            this.blueTrackBar.Maximum = 255;
            this.blueTrackBar.Name = "blueTrackBar";
            this.blueTrackBar.Size = new System.Drawing.Size(162, 45);
            this.blueTrackBar.TabIndex = 0;
            this.blueTrackBar.TickFrequency = 20;
            this.blueTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // greenTrackBar
            // 
            this.greenTrackBar.Location = new System.Drawing.Point(116, 65);
            this.greenTrackBar.Maximum = 255;
            this.greenTrackBar.Name = "greenTrackBar";
            this.greenTrackBar.Size = new System.Drawing.Size(162, 45);
            this.greenTrackBar.TabIndex = 0;
            this.greenTrackBar.TickFrequency = 20;
            this.greenTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(22, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 22);
            this.label11.TabIndex = 2;
            this.label11.Text = "Синий";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(22, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 22);
            this.label10.TabIndex = 2;
            this.label10.Text = "Зелёный";
            // 
            // redTrackBar
            // 
            this.redTrackBar.Location = new System.Drawing.Point(116, 15);
            this.redTrackBar.Maximum = 255;
            this.redTrackBar.Name = "redTrackBar";
            this.redTrackBar.Size = new System.Drawing.Size(162, 45);
            this.redTrackBar.TabIndex = 0;
            this.redTrackBar.TickFrequency = 20;
            this.redTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(22, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 22);
            this.label9.TabIndex = 2;
            this.label9.Text = "Красный";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(728, 362);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(206, 24);
            this.label12.TabIndex = 2;
            this.label12.Text = "Пороговая обработка";
            // 
            // thresholdButton
            // 
            this.thresholdButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.thresholdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.thresholdButton.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thresholdButton.ForeColor = System.Drawing.Color.White;
            this.thresholdButton.Location = new System.Drawing.Point(730, 581);
            this.thresholdButton.Name = "thresholdButton";
            this.thresholdButton.Size = new System.Drawing.Size(297, 32);
            this.thresholdButton.TabIndex = 5;
            this.thresholdButton.Text = "Выполнить";
            this.thresholdButton.UseVisualStyleBackColor = true;
            this.thresholdButton.Click += new System.EventHandler(this.ThresholdButton_Click);
            // 
            // barGraphPictureBox
            // 
            this.barGraphPictureBox.Location = new System.Drawing.Point(730, 53);
            this.barGraphPictureBox.Name = "barGraphPictureBox";
            this.barGraphPictureBox.Size = new System.Drawing.Size(297, 306);
            this.barGraphPictureBox.TabIndex = 1;
            this.barGraphPictureBox.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(702, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(351, 24);
            this.label14.TabIndex = 2;
            this.label14.Text = "Гистограмма распределения яркости";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 622);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(341, 24);
            this.label8.TabIndex = 8;
            this.label8.Text = "Получить черно-белое изображение";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(576, 618);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 32);
            this.button1.TabIndex = 9;
            this.button1.Text = "Выполнить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.ClientSize = new System.Drawing.Size(1065, 665);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.precentTextBox);
            this.Controls.Add(this.randomReplaceButton);
            this.Controls.Add(this.fragmentButton);
            this.Controls.Add(this.contransButton);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.thresholdButton);
            this.Controls.Add(this.quantButton);
            this.Controls.Add(this.resizeComboBox);
            this.Controls.Add(this.quantComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barGraphPictureBox);
            this.Controls.Add(this.newPictureBox);
            this.Controls.Add(this.originalPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGraphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьИзображениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.PictureBox newPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox quantComboBox;
        private System.Windows.Forms.Button quantButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox resizeComboBox;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button contransButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button fragmentButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox precentTextBox;
        private System.Windows.Forms.Button randomReplaceButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar blueTrackBar;
        private System.Windows.Forms.TrackBar greenTrackBar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar redTrackBar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button thresholdButton;
        private System.Windows.Forms.PictureBox barGraphPictureBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}

