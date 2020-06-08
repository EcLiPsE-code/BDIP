namespace WindowsForms
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonHighPass = new System.Windows.Forms.Button();
            this.buttonLowPass = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonSetStart = new System.Windows.Forms.Button();
            this.buttonTrebleBoost = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(26, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(26, 285);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(164, 23);
            this.buttonLoadImage.TabIndex = 1;
            this.buttonLoadImage.Text = "Загрузить изображение";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
            // 
            // buttonHighPass
            // 
            this.buttonHighPass.Location = new System.Drawing.Point(196, 314);
            this.buttonHighPass.Name = "buttonHighPass";
            this.buttonHighPass.Size = new System.Drawing.Size(164, 36);
            this.buttonHighPass.TabIndex = 3;
            this.buttonHighPass.Text = "Высокочастотный фильтр (Оператор собеля)";
            this.buttonHighPass.UseVisualStyleBackColor = true;
            this.buttonHighPass.Click += new System.EventHandler(this.buttonHighPass_Click);
            // 
            // buttonLowPass
            // 
            this.buttonLowPass.Location = new System.Drawing.Point(196, 285);
            this.buttonLowPass.Name = "buttonLowPass";
            this.buttonLowPass.Size = new System.Drawing.Size(164, 23);
            this.buttonLowPass.TabIndex = 4;
            this.buttonLowPass.Text = "Низкочастотный фильтр";
            this.buttonLowPass.UseVisualStyleBackColor = true;
            this.buttonLowPass.Click += new System.EventHandler(this.buttonLowPass_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(591, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // buttonSetStart
            // 
            this.buttonSetStart.Location = new System.Drawing.Point(26, 314);
            this.buttonSetStart.Name = "buttonSetStart";
            this.buttonSetStart.Size = new System.Drawing.Size(164, 23);
            this.buttonSetStart.TabIndex = 26;
            this.buttonSetStart.Text = "Сбросить изменения";
            this.buttonSetStart.UseVisualStyleBackColor = true;
            this.buttonSetStart.Click += new System.EventHandler(this.buttonSetStart_Click);
            // 
            // buttonTrebleBoost
            // 
            this.buttonTrebleBoost.Location = new System.Drawing.Point(196, 356);
            this.buttonTrebleBoost.Name = "buttonTrebleBoost";
            this.buttonTrebleBoost.Size = new System.Drawing.Size(164, 23);
            this.buttonTrebleBoost.TabIndex = 27;
            this.buttonTrebleBoost.Text = "Подъём высоких частот";
            this.buttonTrebleBoost.UseVisualStyleBackColor = true;
            this.buttonTrebleBoost.Click += new System.EventHandler(this.buttonTrebleBoost_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(307, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 28;
            this.pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 534);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.buttonTrebleBoost);
            this.Controls.Add(this.buttonSetStart);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonLowPass);
            this.Controls.Add(this.buttonHighPass);
            this.Controls.Add(this.buttonLoadImage);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonHighPass;
        private System.Windows.Forms.Button buttonLowPass;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonSetStart;
        private System.Windows.Forms.Button buttonTrebleBoost;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

