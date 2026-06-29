namespace lab2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCourseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAgeGroup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackDifficulty = new System.Windows.Forms.TrackBar();
            this.lblDifficultyValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numLectures = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numLabs = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTest = new System.Windows.Forms.RadioButton();
            this.rbExam = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTeacherName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTeacherDepartment = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numTeacherRoom = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTeacherEmail = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.mtbTeacherPhone = new System.Windows.Forms.MaskedTextBox();
            this.lbLiterature = new System.Windows.Forms.ListBox();
            this.btnAddLiterature = new System.Windows.Forms.Button();
            this.btnEditLiterature = new System.Windows.Forms.Button();
            this.btnRemoveLiterature = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblBudget = new System.Windows.Forms.Label();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvCourses = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeGroupCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DifficultyCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LecturesCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeacherCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BudgetCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьРезультатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поНазваниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поАвторуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поГодуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расширенныйПоискToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сортировкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поНазваниюToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.поСложностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поБюджетуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbSort = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbBack = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslTotalCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLastAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackDifficulty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLectures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabs)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTeacherRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название курса:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtCourseName
            // 
            this.txtCourseName.Location = new System.Drawing.Point(188, 23);
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(234, 27);
            this.txtCourseName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Возрастная аудитория";
            // 
            // cmbAgeGroup
            // 
            this.cmbAgeGroup.FormattingEnabled = true;
            this.cmbAgeGroup.Items.AddRange(new object[] {
            "Дети(7-12)",
            "Подростки(13-17)",
            "Взрослые(18-60)",
            "Пенсионеры(60+)"});
            this.cmbAgeGroup.Location = new System.Drawing.Point(234, 64);
            this.cmbAgeGroup.Name = "cmbAgeGroup";
            this.cmbAgeGroup.Size = new System.Drawing.Size(159, 28);
            this.cmbAgeGroup.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Сложность курса";
            // 
            // trackDifficulty
            // 
            this.trackDifficulty.Location = new System.Drawing.Point(188, 116);
            this.trackDifficulty.Minimum = 1;
            this.trackDifficulty.Name = "trackDifficulty";
            this.trackDifficulty.Size = new System.Drawing.Size(205, 56);
            this.trackDifficulty.TabIndex = 5;
            this.trackDifficulty.Value = 5;
            this.trackDifficulty.Scroll += new System.EventHandler(this.trackDifficulty_Scroll);
            // 
            // lblDifficultyValue
            // 
            this.lblDifficultyValue.AutoSize = true;
            this.lblDifficultyValue.Location = new System.Drawing.Point(420, 116);
            this.lblDifficultyValue.Name = "lblDifficultyValue";
            this.lblDifficultyValue.Size = new System.Drawing.Size(53, 20);
            this.lblDifficultyValue.TabIndex = 6;
            this.lblDifficultyValue.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Количество лекций";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // numLectures
            // 
            this.numLectures.Location = new System.Drawing.Point(262, 186);
            this.numLectures.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLectures.Name = "numLectures";
            this.numLectures.Size = new System.Drawing.Size(120, 27);
            this.numLectures.TabIndex = 8;
            this.numLectures.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Количество лабораторных";
            // 
            // numLabs
            // 
            this.numLabs.Location = new System.Drawing.Point(273, 234);
            this.numLabs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLabs.Name = "numLabs";
            this.numLabs.Size = new System.Drawing.Size(120, 27);
            this.numLabs.TabIndex = 10;
            this.numLabs.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTest);
            this.groupBox1.Controls.Add(this.rbExam);
            this.groupBox1.Location = new System.Drawing.Point(9, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 118);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вид контроля";
            // 
            // rbTest
            // 
            this.rbTest.AutoSize = true;
            this.rbTest.Location = new System.Drawing.Point(7, 74);
            this.rbTest.Name = "rbTest";
            this.rbTest.Size = new System.Drawing.Size(82, 24);
            this.rbTest.TabIndex = 1;
            this.rbTest.Text = "Зачёт";
            this.rbTest.UseVisualStyleBackColor = true;
            // 
            // rbExam
            // 
            this.rbExam.AutoSize = true;
            this.rbExam.Checked = true;
            this.rbExam.Location = new System.Drawing.Point(7, 31);
            this.rbExam.Name = "rbExam";
            this.rbExam.Size = new System.Drawing.Size(103, 24);
            this.rbExam.TabIndex = 0;
            this.rbExam.TabStop = true;
            this.rbExam.Text = "Экзамен";
            this.rbExam.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Дата начала";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(212, 308);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 27);
            this.dtpStartDate.TabIndex = 13;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Location = new System.Drawing.Point(354, 371);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(143, 24);
            this.chkIsActive.TabIndex = 14;
            this.chkIsActive.Text = "Курс активен";
            this.chkIsActive.UseVisualStyleBackColor = true;
            this.chkIsActive.CheckedChanged += new System.EventHandler(this.chkIsActive_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "ФИО преподавателя";
            // 
            // txtTeacherName
            // 
            this.txtTeacherName.Location = new System.Drawing.Point(234, 40);
            this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.Size = new System.Drawing.Size(252, 27);
            this.txtTeacherName.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Кафедра";
            // 
            // txtTeacherDepartment
            // 
            this.txtTeacherDepartment.Location = new System.Drawing.Point(234, 78);
            this.txtTeacherDepartment.Name = "txtTeacherDepartment";
            this.txtTeacherDepartment.Size = new System.Drawing.Size(252, 27);
            this.txtTeacherDepartment.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "Аудитория";
            // 
            // numTeacherRoom
            // 
            this.numTeacherRoom.Location = new System.Drawing.Point(234, 120);
            this.numTeacherRoom.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numTeacherRoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTeacherRoom.Name = "numTeacherRoom";
            this.numTeacherRoom.Size = new System.Drawing.Size(120, 27);
            this.numTeacherRoom.TabIndex = 20;
            this.numTeacherRoom.Value = new decimal(new int[] {
            101,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Email";
            // 
            // txtTeacherEmail
            // 
            this.txtTeacherEmail.Location = new System.Drawing.Point(234, 165);
            this.txtTeacherEmail.Name = "txtTeacherEmail";
            this.txtTeacherEmail.Size = new System.Drawing.Size(178, 27);
            this.txtTeacherEmail.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 215);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "Телефон";
            // 
            // mtbTeacherPhone
            // 
            this.mtbTeacherPhone.Location = new System.Drawing.Point(234, 212);
            this.mtbTeacherPhone.Mask = "+375(99)000-00-00";
            this.mtbTeacherPhone.Name = "mtbTeacherPhone";
            this.mtbTeacherPhone.Size = new System.Drawing.Size(159, 27);
            this.mtbTeacherPhone.TabIndex = 24;
            this.mtbTeacherPhone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mtbTeacherPhone_MaskInputRejected);
            // 
            // lbLiterature
            // 
            this.lbLiterature.FormattingEnabled = true;
            this.lbLiterature.ItemHeight = 20;
            this.lbLiterature.Location = new System.Drawing.Point(6, 35);
            this.lbLiterature.Name = "lbLiterature";
            this.lbLiterature.Size = new System.Drawing.Size(193, 144);
            this.lbLiterature.TabIndex = 25;
            // 
            // btnAddLiterature
            // 
            this.btnAddLiterature.Location = new System.Drawing.Point(253, 35);
            this.btnAddLiterature.Name = "btnAddLiterature";
            this.btnAddLiterature.Size = new System.Drawing.Size(110, 34);
            this.btnAddLiterature.TabIndex = 26;
            this.btnAddLiterature.Text = "Добавить";
            this.btnAddLiterature.UseVisualStyleBackColor = true;
            this.btnAddLiterature.Click += new System.EventHandler(this.btnAddLiterature_Click);
            // 
            // btnEditLiterature
            // 
            this.btnEditLiterature.Location = new System.Drawing.Point(253, 88);
            this.btnEditLiterature.Name = "btnEditLiterature";
            this.btnEditLiterature.Size = new System.Drawing.Size(110, 35);
            this.btnEditLiterature.TabIndex = 27;
            this.btnEditLiterature.Text = "Изменить";
            this.btnEditLiterature.UseVisualStyleBackColor = true;
            this.btnEditLiterature.Click += new System.EventHandler(this.btnEditLiterature_Click);
            // 
            // btnRemoveLiterature
            // 
            this.btnRemoveLiterature.Location = new System.Drawing.Point(253, 144);
            this.btnRemoveLiterature.Name = "btnRemoveLiterature";
            this.btnRemoveLiterature.Size = new System.Drawing.Size(110, 35);
            this.btnRemoveLiterature.TabIndex = 28;
            this.btnRemoveLiterature.Text = "Удалить";
            this.btnRemoveLiterature.UseVisualStyleBackColor = true;
            this.btnRemoveLiterature.Click += new System.EventHandler(this.btnRemoveLiterature_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSave.Location = new System.Drawing.Point(120, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(178, 31);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Сохранить курс";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoad.Location = new System.Drawing.Point(331, 527);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(175, 31);
            this.btnLoad.TabIndex = 31;
            this.btnLoad.Text = "Загрузить курсы";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(531, 527);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(212, 31);
            this.btnCalculate.TabIndex = 32;
            this.btnCalculate.Text = "Рассчитать бюджет";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblBudget
            // 
            this.lblBudget.AutoSize = true;
            this.lblBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBudget.Location = new System.Drawing.Point(556, 576);
            this.lblBudget.Name = "lblBudget";
            this.lblBudget.Size = new System.Drawing.Size(134, 20);
            this.lblBudget.TabIndex = 33;
            this.lblBudget.Text = "Бюджет: 0 руб.";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShowAll.Location = new System.Drawing.Point(768, 527);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(204, 31);
            this.btnShowAll.TabIndex = 34;
            this.btnShowAll.Text = "Показать все курсы";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCourseName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbAgeGroup);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.trackDifficulty);
            this.groupBox2.Controls.Add(this.lblDifficultyValue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numLectures);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numLabs);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dtpStartDate);
            this.groupBox2.Controls.Add(this.chkIsActive);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(38, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 402);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "О курсе";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtTeacherName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtTeacherDepartment);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.numTeacherRoom);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtTeacherEmail);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.mtbTeacherPhone);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(563, 258);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(496, 253);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "О преподавателе";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbLiterature);
            this.groupBox4.Controls.Add(this.btnAddLiterature);
            this.groupBox4.Controls.Add(this.btnEditLiterature);
            this.groupBox4.Controls.Add(this.btnRemoveLiterature);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(563, 58);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(480, 194);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Литература";
            // 
            // dgvCourses
            // 
            this.dgvCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCourses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameCol,
            this.AgeGroupCol,
            this.DifficultyCol,
            this.LecturesCol,
            this.LabsCol,
            this.ControlCol,
            this.TeacherCol,
            this.BudgetCol});
            this.dgvCourses.Location = new System.Drawing.Point(35, 602);
            this.dgvCourses.Name = "dgvCourses";
            this.dgvCourses.RowHeadersWidth = 51;
            this.dgvCourses.RowTemplate.Height = 24;
            this.dgvCourses.Size = new System.Drawing.Size(1021, 150);
            this.dgvCourses.TabIndex = 38;
            // 
            // NameCol
            // 
            this.NameCol.HeaderText = "Название курса";
            this.NameCol.MinimumWidth = 6;
            this.NameCol.Name = "NameCol";
            this.NameCol.Width = 125;
            // 
            // AgeGroupCol
            // 
            this.AgeGroupCol.HeaderText = "Возрастная группа";
            this.AgeGroupCol.MinimumWidth = 6;
            this.AgeGroupCol.Name = "AgeGroupCol";
            this.AgeGroupCol.Width = 125;
            // 
            // DifficultyCol
            // 
            this.DifficultyCol.HeaderText = "Сложность";
            this.DifficultyCol.MinimumWidth = 6;
            this.DifficultyCol.Name = "DifficultyCol";
            this.DifficultyCol.Width = 125;
            // 
            // LecturesCol
            // 
            this.LecturesCol.HeaderText = "Лекции";
            this.LecturesCol.MinimumWidth = 6;
            this.LecturesCol.Name = "LecturesCol";
            this.LecturesCol.Width = 125;
            // 
            // LabsCol
            // 
            this.LabsCol.HeaderText = "Лабораторные";
            this.LabsCol.MinimumWidth = 6;
            this.LabsCol.Name = "LabsCol";
            this.LabsCol.Width = 125;
            // 
            // ControlCol
            // 
            this.ControlCol.HeaderText = "Контроль";
            this.ControlCol.MinimumWidth = 6;
            this.ControlCol.Name = "ControlCol";
            this.ControlCol.Width = 125;
            // 
            // TeacherCol
            // 
            this.TeacherCol.HeaderText = "Преподаватель";
            this.TeacherCol.MinimumWidth = 6;
            this.TeacherCol.Name = "TeacherCol";
            this.TeacherCol.Width = 125;
            // 
            // BudgetCol
            // 
            this.BudgetCol.HeaderText = "Бюджет";
            this.BudgetCol.MinimumWidth = 6;
            this.BudgetCol.Name = "BudgetCol";
            this.BudgetCol.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.поискToolStripMenuItem,
            this.сортировкаToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1089, 28);
            this.menuStrip1.TabIndex = 39;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьРезультатыToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьРезультатыToolStripMenuItem
            // 
            this.сохранитьРезультатыToolStripMenuItem.Name = "сохранитьРезультатыToolStripMenuItem";
            this.сохранитьРезультатыToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.сохранитьРезультатыToolStripMenuItem.Text = "Сохранить результаты";
            // 
            // поискToolStripMenuItem
            // 
            this.поискToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поНазваниюToolStripMenuItem,
            this.поАвторуToolStripMenuItem,
            this.поГодуToolStripMenuItem,
            this.расширенныйПоискToolStripMenuItem});
            this.поискToolStripMenuItem.Name = "поискToolStripMenuItem";
            this.поискToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.поискToolStripMenuItem.Text = "Поиск";
            // 
            // поНазваниюToolStripMenuItem
            // 
            this.поНазваниюToolStripMenuItem.Name = "поНазваниюToolStripMenuItem";
            this.поНазваниюToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.поНазваниюToolStripMenuItem.Text = "По названию";
            // 
            // поАвторуToolStripMenuItem
            // 
            this.поАвторуToolStripMenuItem.Name = "поАвторуToolStripMenuItem";
            this.поАвторуToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.поАвторуToolStripMenuItem.Text = "По автору";
            // 
            // поГодуToolStripMenuItem
            // 
            this.поГодуToolStripMenuItem.Name = "поГодуToolStripMenuItem";
            this.поГодуToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.поГодуToolStripMenuItem.Text = "По году";
            // 
            // расширенныйПоискToolStripMenuItem
            // 
            this.расширенныйПоискToolStripMenuItem.Name = "расширенныйПоискToolStripMenuItem";
            this.расширенныйПоискToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.расширенныйПоискToolStripMenuItem.Text = "Расширенный поиск";
            // 
            // сортировкаToolStripMenuItem
            // 
            this.сортировкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поНазваниюToolStripMenuItem1,
            this.поСложностиToolStripMenuItem,
            this.поБюджетуToolStripMenuItem});
            this.сортировкаToolStripMenuItem.Name = "сортировкаToolStripMenuItem";
            this.сортировкаToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.сортировкаToolStripMenuItem.Text = "Сортировка";
            // 
            // поНазваниюToolStripMenuItem1
            // 
            this.поНазваниюToolStripMenuItem1.Name = "поНазваниюToolStripMenuItem1";
            this.поНазваниюToolStripMenuItem1.Size = new System.Drawing.Size(191, 26);
            this.поНазваниюToolStripMenuItem1.Text = "По названию";
            // 
            // поСложностиToolStripMenuItem
            // 
            this.поСложностиToolStripMenuItem.Name = "поСложностиToolStripMenuItem";
            this.поСложностиToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.поСложностиToolStripMenuItem.Text = "По сложности";
            // 
            // поБюджетуToolStripMenuItem
            // 
            this.поБюджетуToolStripMenuItem.Name = "поБюджетуToolStripMenuItem";
            this.поБюджетуToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.поБюджетуToolStripMenuItem.Text = "По бюджету";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSearch,
            this.tsbSort,
            this.tsbClear,
            this.tsbDelete,
            this.tsbBack,
            this.tsbForward});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1089, 27);
            this.toolStrip1.TabIndex = 40;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSearch
            // 
            this.tsbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbSearch.Image")));
            this.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new System.Drawing.Size(29, 24);
            this.tsbSearch.Text = "Поиск";
            // 
            // tsbSort
            // 
            this.tsbSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSort.Image = ((System.Drawing.Image)(resources.GetObject("tsbSort.Image")));
            this.tsbSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSort.Name = "tsbSort";
            this.tsbSort.Size = new System.Drawing.Size(29, 24);
            this.tsbSort.Text = "Сортировка";
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(29, 24);
            this.tsbClear.Text = "Очистить";
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(29, 24);
            this.tsbDelete.Text = "Удалить";
            // 
            // tsbBack
            // 
            this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
            this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBack.Name = "tsbBack";
            this.tsbBack.Size = new System.Drawing.Size(29, 24);
            this.tsbBack.Text = "Назад";
            // 
            // tsbForward
            // 
            this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbForward.Image")));
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(29, 24);
            this.tsbForward.Text = "Вперёд";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTotalCount,
            this.tsslLastAction,
            this.tsslDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 779);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1089, 26);
            this.statusStrip1.TabIndex = 41;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslTotalCount
            // 
            this.tsslTotalCount.Name = "tsslTotalCount";
            this.tsslTotalCount.Size = new System.Drawing.Size(114, 20);
            this.tsslTotalCount.Text = "Всего курсов: 0";
            // 
            // tsslLastAction
            // 
            this.tsslLastAction.Name = "tsslLastAction";
            this.tsslLastAction.Size = new System.Drawing.Size(159, 20);
            this.tsslLastAction.Text = "Последнее действие: ";
            this.tsslLastAction.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(137, 20);
            this.tsslDateTime.Text = "00:00:00 01.01.2026";
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 805);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvCourses);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.lblBudget);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackDifficulty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLectures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTeacherRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCourseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAgeGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackDifficulty;
        private System.Windows.Forms.Label lblDifficultyValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numLectures;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numLabs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTest;
        private System.Windows.Forms.RadioButton rbExam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTeacherName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTeacherDepartment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numTeacherRoom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTeacherEmail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox mtbTeacherPhone;
        private System.Windows.Forms.ListBox lbLiterature;
        private System.Windows.Forms.Button btnAddLiterature;
        private System.Windows.Forms.Button btnEditLiterature;
        private System.Windows.Forms.Button btnRemoveLiterature;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvCourses;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeGroupCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn DifficultyCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LecturesCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeacherCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn BudgetCol;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьРезультатыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поНазваниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поАвторуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поГодуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem расширенныйПоискToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сортировкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поНазваниюToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem поСложностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поБюджетуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbSearch;
        private System.Windows.Forms.ToolStripButton tsbSort;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotalCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslLastAction;
        private System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripButton tsbForward;
    }
}

