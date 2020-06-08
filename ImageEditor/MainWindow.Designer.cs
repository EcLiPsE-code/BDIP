namespace ImageEditor
{
    partial class MainWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.BtnLoadImg = new System.Windows.Forms.Button();
            this.pictureBoxStock = new System.Windows.Forms.PictureBox();
            this.pictureBoxEdit = new System.Windows.Forms.PictureBox();
            this.btnDecreaseQuant = new System.Windows.Forms.Button();
            this.btnIncreaseSize = new System.Windows.Forms.Button();
            this.btnDecreaseSize = new System.Windows.Forms.Button();
            this.btnSetRandomBrght = new System.Windows.Forms.Button();
            this.myChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSetStockImg = new System.Windows.Forms.Button();
            this.btnDrc = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.textDRLeftRange = new System.Windows.Forms.TextBox();
            this.textDRRightRange = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textLogConvert = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelQuantLevel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelResolution = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelHistogram = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelHistMinValue = new System.Windows.Forms.Label();
            this.labelHistMaxValue = new System.Windows.Forms.Label();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.BtnCutImage = new System.Windows.Forms.Button();
            this.BtnEqHistogram = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.LabelFilePath = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.BtnSetBlackAndWhite = new System.Windows.Forms.Button();
            this.BtnLHFilter = new System.Windows.Forms.Button();
            this.BtnHFFilter = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.BtnSetFilter = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ComboBoxFilters = new System.Windows.Forms.ComboBox();
            this.BtnFft = new System.Windows.Forms.Button();
            this.BtnReverseFFT = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textContrastMin = new System.Windows.Forms.TextBox();
            this.textContrastMax = new System.Windows.Forms.TextBox();
            this.btnLinearContrast = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.contransButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnLoadImg
            // 
            this.BtnLoadImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.BtnLoadImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadImg.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnLoadImg.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnLoadImg.Location = new System.Drawing.Point(11, 43);
            this.BtnLoadImg.Name = "BtnLoadImg";
            this.BtnLoadImg.Size = new System.Drawing.Size(140, 48);
            this.BtnLoadImg.TabIndex = 0;
            this.BtnLoadImg.Text = "Загрузить изображение";
            this.BtnLoadImg.UseVisualStyleBackColor = false;
            this.BtnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // pictureBoxStock
            // 
            this.pictureBoxStock.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxStock.Location = new System.Drawing.Point(904, 47);
            this.pictureBoxStock.Name = "pictureBoxStock";
            this.pictureBoxStock.Size = new System.Drawing.Size(384, 279);
            this.pictureBoxStock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStock.TabIndex = 1;
            this.pictureBoxStock.TabStop = false;
            // 
            // pictureBoxEdit
            // 
            this.pictureBoxEdit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxEdit.Location = new System.Drawing.Point(904, 364);
            this.pictureBoxEdit.Name = "pictureBoxEdit";
            this.pictureBoxEdit.Size = new System.Drawing.Size(384, 279);
            this.pictureBoxEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxEdit.TabIndex = 2;
            this.pictureBoxEdit.TabStop = false;
            // 
            // btnDecreaseQuant
            // 
            this.btnDecreaseQuant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecreaseQuant.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDecreaseQuant.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDecreaseQuant.Location = new System.Drawing.Point(380, 43);
            this.btnDecreaseQuant.Name = "btnDecreaseQuant";
            this.btnDecreaseQuant.Size = new System.Drawing.Size(193, 30);
            this.btnDecreaseQuant.TabIndex = 3;
            this.btnDecreaseQuant.Text = "Уровень квантования (x2)";
            this.btnDecreaseQuant.UseVisualStyleBackColor = true;
            this.btnDecreaseQuant.Click += new System.EventHandler(this.btnDecreaseQuant_Click);
            // 
            // btnIncreaseSize
            // 
            this.btnIncreaseSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIncreaseSize.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnIncreaseSize.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnIncreaseSize.Location = new System.Drawing.Point(607, 79);
            this.btnIncreaseSize.Name = "btnIncreaseSize";
            this.btnIncreaseSize.Size = new System.Drawing.Size(173, 30);
            this.btnIncreaseSize.TabIndex = 5;
            this.btnIncreaseSize.Text = "Увеличить размер (x2)";
            this.btnIncreaseSize.UseVisualStyleBackColor = true;
            this.btnIncreaseSize.Click += new System.EventHandler(this.btnIncreaseSize_Click);
            // 
            // btnDecreaseSize
            // 
            this.btnDecreaseSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecreaseSize.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDecreaseSize.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDecreaseSize.Location = new System.Drawing.Point(380, 79);
            this.btnDecreaseSize.Name = "btnDecreaseSize";
            this.btnDecreaseSize.Size = new System.Drawing.Size(193, 30);
            this.btnDecreaseSize.TabIndex = 6;
            this.btnDecreaseSize.Text = "Уменьшить размер (x2)";
            this.btnDecreaseSize.UseVisualStyleBackColor = true;
            this.btnDecreaseSize.Click += new System.EventHandler(this.btnDecreaseSize_Click);
            // 
            // btnSetRandomBrght
            // 
            this.btnSetRandomBrght.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRandomBrght.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetRandomBrght.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSetRandomBrght.Location = new System.Drawing.Point(607, 43);
            this.btnSetRandomBrght.Name = "btnSetRandomBrght";
            this.btnSetRandomBrght.Size = new System.Drawing.Size(173, 30);
            this.btnSetRandomBrght.TabIndex = 7;
            this.btnSetRandomBrght.Text = "Добавить шумы";
            this.btnSetRandomBrght.UseVisualStyleBackColor = true;
            this.btnSetRandomBrght.Click += new System.EventHandler(this.btnSetRandomBrght_Click);
            // 
            // myChart
            // 
            this.myChart.BackColor = System.Drawing.Color.Transparent;
            this.myChart.BorderlineColor = System.Drawing.SystemColors.Control;
            chartArea4.Name = "ChartHistogram";
            this.myChart.ChartAreas.Add(chartArea4);
            this.myChart.Location = new System.Drawing.Point(282, 363);
            this.myChart.Name = "myChart";
            this.myChart.Size = new System.Drawing.Size(591, 315);
            this.myChart.TabIndex = 8;
            this.myChart.Text = "chart1";
            // 
            // btnSetStockImg
            // 
            this.btnSetStockImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetStockImg.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetStockImg.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSetStockImg.Location = new System.Drawing.Point(11, 142);
            this.btnSetStockImg.Name = "btnSetStockImg";
            this.btnSetStockImg.Size = new System.Drawing.Size(174, 51);
            this.btnSetStockImg.TabIndex = 9;
            this.btnSetStockImg.Text = "Установить начальное изображение";
            this.btnSetStockImg.UseVisualStyleBackColor = true;
            this.btnSetStockImg.Click += new System.EventHandler(this.btnSetStockImg_Click);
            // 
            // btnDrc
            // 
            this.btnDrc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDrc.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDrc.Location = new System.Drawing.Point(3, 3);
            this.btnDrc.Name = "btnDrc";
            this.btnDrc.Size = new System.Drawing.Size(140, 30);
            this.btnDrc.TabIndex = 10;
            this.btnDrc.Text = "DR";
            this.btnDrc.UseVisualStyleBackColor = true;
            this.btnDrc.Click += new System.EventHandler(this.btnDrc_Click);
            // 
            // btnLog
            // 
            this.btnLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLog.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLog.Location = new System.Drawing.Point(3, 3);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(205, 30);
            this.btnLog.TabIndex = 11;
            this.btnLog.Text = "Log преобразование";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // textDRLeftRange
            // 
            this.textDRLeftRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.textDRLeftRange.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textDRLeftRange.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textDRLeftRange.Location = new System.Drawing.Point(3, 24);
            this.textDRLeftRange.Name = "textDRLeftRange";
            this.textDRLeftRange.Size = new System.Drawing.Size(50, 23);
            this.textDRLeftRange.TabIndex = 12;
            this.textDRLeftRange.Text = "0";
            this.textDRLeftRange.TextChanged += new System.EventHandler(this.textDRLeftRange_TextChanged);
            // 
            // textDRRightRange
            // 
            this.textDRRightRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.textDRRightRange.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textDRRightRange.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textDRRightRange.Location = new System.Drawing.Point(87, 24);
            this.textDRRightRange.Name = "textDRRightRange";
            this.textDRRightRange.Size = new System.Drawing.Size(50, 23);
            this.textDRRightRange.TabIndex = 13;
            this.textDRRightRange.Text = "255";
            this.textDRRightRange.TextChanged += new System.EventHandler(this.textDRRightRange_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Лево";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(84, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Право";
            // 
            // textLogConvert
            // 
            this.textLogConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.textLogConvert.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLogConvert.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textLogConvert.Location = new System.Drawing.Point(94, 51);
            this.textLogConvert.Name = "textLogConvert";
            this.textLogConvert.Size = new System.Drawing.Size(50, 23);
            this.textLogConvert.TabIndex = 20;
            this.textLogConvert.Text = "0";
            this.textLogConvert.TextChanged += new System.EventHandler(this.textLogConvert_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Значение:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.textDRRightRange);
            this.panel1.Controls.Add(this.textDRLeftRange);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(195, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 55);
            this.panel1.TabIndex = 22;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.labelQuantLevel);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.labelResolution);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(11, 375);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(247, 50);
            this.panel6.TabIndex = 28;
            // 
            // labelQuantLevel
            // 
            this.labelQuantLevel.AutoSize = true;
            this.labelQuantLevel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQuantLevel.Location = new System.Drawing.Point(173, 31);
            this.labelQuantLevel.Name = "labelQuantLevel";
            this.labelQuantLevel.Size = new System.Drawing.Size(0, 17);
            this.labelQuantLevel.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(5, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Уровень квантования:";
            // 
            // labelResolution
            // 
            this.labelResolution.AutoSize = true;
            this.labelResolution.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResolution.Location = new System.Drawing.Point(173, 7);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(0, 17);
            this.labelResolution.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(4, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Размер изображения:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnDrc);
            this.panel3.Location = new System.Drawing.Point(191, 171);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(147, 45);
            this.panel3.TabIndex = 24;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btnLog);
            this.panel5.Controls.Add(this.textLogConvert);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(161, 41);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(213, 95);
            this.panel5.TabIndex = 26;
            // 
            // labelHistogram
            // 
            this.labelHistogram.AutoSize = true;
            this.labelHistogram.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHistogram.Location = new System.Drawing.Point(314, 345);
            this.labelHistogram.Name = "labelHistogram";
            this.labelHistogram.Size = new System.Drawing.Size(275, 23);
            this.labelHistogram.TabIndex = 27;
            this.labelHistogram.Text = "Гистограмма изображения";
            this.labelHistogram.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(301, 681);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Максимальное значение:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(692, 682);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 17);
            this.label9.TabIndex = 29;
            this.label9.Text = "Минимальное значение:";
            // 
            // labelHistMinValue
            // 
            this.labelHistMinValue.AutoSize = true;
            this.labelHistMinValue.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHistMinValue.Location = new System.Drawing.Point(873, 682);
            this.labelHistMinValue.Name = "labelHistMinValue";
            this.labelHistMinValue.Size = new System.Drawing.Size(0, 17);
            this.labelHistMinValue.TabIndex = 26;
            // 
            // labelHistMaxValue
            // 
            this.labelHistMaxValue.AutoSize = true;
            this.labelHistMaxValue.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHistMaxValue.Location = new System.Drawing.Point(492, 682);
            this.labelHistMaxValue.Name = "labelHistMaxValue";
            this.labelHistMaxValue.Size = new System.Drawing.Size(0, 17);
            this.labelHistMaxValue.TabIndex = 30;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveImage.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveImage.Location = new System.Drawing.Point(11, 200);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(174, 44);
            this.btnSaveImage.TabIndex = 31;
            this.btnSaveImage.Text = "Сохранить изображение";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // BtnCutImage
            // 
            this.BtnCutImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCutImage.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnCutImage.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnCutImage.Location = new System.Drawing.Point(11, 252);
            this.BtnCutImage.Name = "BtnCutImage";
            this.BtnCutImage.Size = new System.Drawing.Size(174, 30);
            this.BtnCutImage.TabIndex = 32;
            this.BtnCutImage.Text = "Вырезать изображение";
            this.BtnCutImage.UseVisualStyleBackColor = true;
            this.BtnCutImage.Click += new System.EventHandler(this.BtnCutImage_Click);
            // 
            // BtnEqHistogram
            // 
            this.BtnEqHistogram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEqHistogram.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnEqHistogram.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnEqHistogram.Location = new System.Drawing.Point(11, 291);
            this.BtnEqHistogram.Name = "BtnEqHistogram";
            this.BtnEqHistogram.Size = new System.Drawing.Size(327, 30);
            this.BtnEqHistogram.TabIndex = 33;
            this.BtnEqHistogram.Text = "Эквализация изображения";
            this.BtnEqHistogram.UseVisualStyleBackColor = true;
            this.BtnEqHistogram.Click += new System.EventHandler(this.BtnEqHistogram_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnClose.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnClose.Location = new System.Drawing.Point(1228, 7);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(60, 30);
            this.BtnClose.TabIndex = 34;
            this.BtnClose.Text = "Выход";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.LabelFilePath);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Location = new System.Drawing.Point(19, 7);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(582, 28);
            this.panel7.TabIndex = 29;
            // 
            // LabelFilePath
            // 
            this.LabelFilePath.AutoSize = true;
            this.LabelFilePath.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelFilePath.Location = new System.Drawing.Point(41, 4);
            this.LabelFilePath.Name = "LabelFilePath";
            this.LabelFilePath.Size = new System.Drawing.Size(0, 16);
            this.LabelFilePath.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 16);
            this.label13.TabIndex = 22;
            this.label13.Text = "File:";
            // 
            // BtnSetBlackAndWhite
            // 
            this.BtnSetBlackAndWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSetBlackAndWhite.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnSetBlackAndWhite.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnSetBlackAndWhite.Location = new System.Drawing.Point(11, 331);
            this.BtnSetBlackAndWhite.Name = "BtnSetBlackAndWhite";
            this.BtnSetBlackAndWhite.Size = new System.Drawing.Size(174, 30);
            this.BtnSetBlackAndWhite.TabIndex = 35;
            this.BtnSetBlackAndWhite.Text = "Черно-белое фото";
            this.BtnSetBlackAndWhite.UseVisualStyleBackColor = true;
            this.BtnSetBlackAndWhite.Click += new System.EventHandler(this.BtnSetBlackAndWhite_Click);
            // 
            // BtnLHFilter
            // 
            this.BtnLHFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLHFilter.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnLHFilter.Location = new System.Drawing.Point(640, 175);
            this.BtnLHFilter.Name = "BtnLHFilter";
            this.BtnLHFilter.Size = new System.Drawing.Size(140, 49);
            this.BtnLHFilter.TabIndex = 36;
            this.BtnLHFilter.Text = "Низкочастотный фильтр";
            this.BtnLHFilter.UseVisualStyleBackColor = true;
            this.BtnLHFilter.Click += new System.EventHandler(this.BtnLHFilter_Click);
            // 
            // BtnHFFilter
            // 
            this.BtnHFFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnHFFilter.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnHFFilter.Location = new System.Drawing.Point(494, 175);
            this.BtnHFFilter.Name = "BtnHFFilter";
            this.BtnHFFilter.Size = new System.Drawing.Size(140, 49);
            this.BtnHFFilter.TabIndex = 37;
            this.BtnHFFilter.Text = "Высокочастотный фильтр";
            this.BtnHFFilter.UseVisualStyleBackColor = true;
            this.BtnHFFilter.Click += new System.EventHandler(this.BtnHFFilter_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.BtnSetFilter);
            this.panel8.Location = new System.Drawing.Point(344, 171);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(148, 45);
            this.panel8.TabIndex = 25;
            // 
            // BtnSetFilter
            // 
            this.BtnSetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSetFilter.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnSetFilter.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnSetFilter.Location = new System.Drawing.Point(3, 3);
            this.BtnSetFilter.Name = "BtnSetFilter";
            this.BtnSetFilter.Size = new System.Drawing.Size(140, 30);
            this.BtnSetFilter.TabIndex = 10;
            this.BtnSetFilter.Text = "Применить";
            this.BtnSetFilter.UseVisualStyleBackColor = true;
            this.BtnSetFilter.Click += new System.EventHandler(this.BtnSetFilter_Click);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.ComboBoxFilters);
            this.panel9.Location = new System.Drawing.Point(344, 216);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(140, 55);
            this.panel9.TabIndex = 22;
            // 
            // ComboBoxFilters
            // 
            this.ComboBoxFilters.FormattingEnabled = true;
            this.ComboBoxFilters.Location = new System.Drawing.Point(4, 4);
            this.ComboBoxFilters.Name = "ComboBoxFilters";
            this.ComboBoxFilters.Size = new System.Drawing.Size(134, 25);
            this.ComboBoxFilters.TabIndex = 0;
            // 
            // BtnFft
            // 
            this.BtnFft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFft.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnFft.Location = new System.Drawing.Point(490, 236);
            this.BtnFft.Name = "BtnFft";
            this.BtnFft.Size = new System.Drawing.Size(140, 66);
            this.BtnFft.TabIndex = 38;
            this.BtnFft.Text = "Прямое преобразование Фурье";
            this.BtnFft.UseVisualStyleBackColor = true;
            this.BtnFft.Click += new System.EventHandler(this.BtnFft_Click);
            // 
            // BtnReverseFFT
            // 
            this.BtnReverseFFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReverseFFT.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnReverseFFT.Location = new System.Drawing.Point(640, 236);
            this.BtnReverseFFT.Name = "BtnReverseFFT";
            this.BtnReverseFFT.Size = new System.Drawing.Size(140, 66);
            this.BtnReverseFFT.TabIndex = 39;
            this.BtnReverseFFT.Text = "Обратное преобразование Фурье";
            this.BtnReverseFFT.UseVisualStyleBackColor = true;
            this.BtnReverseFFT.Click += new System.EventHandler(this.BtnReverseFFT_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textContrastMin);
            this.panel2.Controls.Add(this.textContrastMax);
            this.panel2.Location = new System.Drawing.Point(3, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(169, 54);
            this.panel2.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Min";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(132, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Max";
            // 
            // textContrastMin
            // 
            this.textContrastMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.textContrastMin.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textContrastMin.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textContrastMin.Location = new System.Drawing.Point(3, 23);
            this.textContrastMin.Name = "textContrastMin";
            this.textContrastMin.Size = new System.Drawing.Size(50, 23);
            this.textContrastMin.TabIndex = 16;
            this.textContrastMin.Text = "0";
            this.textContrastMin.TextChanged += new System.EventHandler(this.textContrastMin_TextChanged);
            // 
            // textContrastMax
            // 
            this.textContrastMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.textContrastMax.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textContrastMax.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textContrastMax.Location = new System.Drawing.Point(115, 22);
            this.textContrastMax.Name = "textContrastMax";
            this.textContrastMax.Size = new System.Drawing.Size(50, 23);
            this.textContrastMax.TabIndex = 17;
            this.textContrastMax.Text = "255";
            this.textContrastMax.TextChanged += new System.EventHandler(this.textContrastMax_TextChanged);
            // 
            // btnLinearContrast
            // 
            this.btnLinearContrast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.btnLinearContrast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLinearContrast.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLinearContrast.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLinearContrast.Location = new System.Drawing.Point(3, 0);
            this.btnLinearContrast.Name = "btnLinearContrast";
            this.btnLinearContrast.Size = new System.Drawing.Size(169, 30);
            this.btnLinearContrast.TabIndex = 4;
            this.btnLinearContrast.Text = "Границы гистограммы";
            this.btnLinearContrast.UseVisualStyleBackColor = false;
            this.btnLinearContrast.Click += new System.EventHandler(this.btnLinearContrast_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnLinearContrast);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(11, 431);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(177, 90);
            this.panel4.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(380, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(371, 17);
            this.label10.TabIndex = 40;
            this.label10.Text = "Линейное контрастирование изображения от 28 до 94";
            // 
            // contransButton
            // 
            this.contransButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.contransButton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contransButton.ForeColor = System.Drawing.Color.White;
            this.contransButton.Location = new System.Drawing.Point(764, 120);
            this.contransButton.Name = "contransButton";
            this.contransButton.Size = new System.Drawing.Size(122, 32);
            this.contransButton.TabIndex = 41;
            this.contransButton.Text = "Выполнить";
            this.contransButton.UseVisualStyleBackColor = true;
            this.contransButton.Click += new System.EventHandler(this.contransButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.contransButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.BtnReverseFFT);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.BtnFft);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.BtnHFFilter);
            this.Controls.Add(this.BtnLHFilter);
            this.Controls.Add(this.BtnSetBlackAndWhite);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnEqHistogram);
            this.Controls.Add(this.BtnCutImage);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.labelHistMaxValue);
            this.Controls.Add(this.labelHistMinValue);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelHistogram);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSetStockImg);
            this.Controls.Add(this.myChart);
            this.Controls.Add(this.btnSetRandomBrght);
            this.Controls.Add(this.btnDecreaseSize);
            this.Controls.Add(this.btnIncreaseSize);
            this.Controls.Add(this.btnDecreaseQuant);
            this.Controls.Add(this.pictureBoxEdit);
            this.Controls.Add(this.pictureBoxStock);
            this.Controls.Add(this.BtnLoadImg);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1300, 720);
            this.MinimumSize = new System.Drawing.Size(1024, 720);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnLoadImg;
        private System.Windows.Forms.PictureBox pictureBoxStock;
        private System.Windows.Forms.PictureBox pictureBoxEdit;
        private System.Windows.Forms.Button btnDecreaseQuant;
        private System.Windows.Forms.Button btnIncreaseSize;
        private System.Windows.Forms.Button btnDecreaseSize;
        private System.Windows.Forms.Button btnSetRandomBrght;
        private System.Windows.Forms.DataVisualization.Charting.Chart myChart;
        private System.Windows.Forms.Button btnSetStockImg;
        private System.Windows.Forms.Button btnDrc;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.TextBox textDRLeftRange;
        private System.Windows.Forms.TextBox textDRRightRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textLogConvert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelHistogram;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelQuantLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelHistMinValue;
        private System.Windows.Forms.Label labelHistMaxValue;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button BtnCutImage;
        private System.Windows.Forms.Button BtnEqHistogram;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label LabelFilePath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button BtnSetBlackAndWhite;
        private System.Windows.Forms.Button BtnLHFilter;
        private System.Windows.Forms.Button BtnHFFilter;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button BtnSetFilter;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox ComboBoxFilters;
        private System.Windows.Forms.Button BtnFft;
        private System.Windows.Forms.Button BtnReverseFFT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textContrastMin;
        private System.Windows.Forms.TextBox textContrastMax;
        private System.Windows.Forms.Button btnLinearContrast;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button contransButton;
    }
}

