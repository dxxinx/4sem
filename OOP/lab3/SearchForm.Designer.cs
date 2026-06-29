namespace lab2
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkName = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkAuthor = new System.Windows.Forms.CheckBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.chkYear = new System.Windows.Forms.CheckBox();
            this.numYearFrom = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numYearTo = new System.Windows.Forms.NumericUpDown();
            this.chkDifficulty = new System.Windows.Forms.CheckBox();
            this.numDiffTo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numDiffFrom = new System.Windows.Forms.NumericUpDown();
            this.chkRegex = new System.Windows.Forms.CheckBox();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbResults = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numYearFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiffTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiffFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Location = new System.Drawing.Point(20, 20);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(116, 20);
            this.chkName.TabIndex = 0;
            this.chkName.Text = "По названию";
            this.chkName.UseVisualStyleBackColor = true;
            this.chkName.CheckedChanged += new System.EventHandler(this.chkName_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(163, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(225, 22);
            this.txtName.TabIndex = 1;
            // 
            // chkAuthor
            // 
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Location = new System.Drawing.Point(20, 55);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new System.Drawing.Size(97, 20);
            this.chkAuthor.TabIndex = 2;
            this.chkAuthor.Text = "По автору";
            this.chkAuthor.UseVisualStyleBackColor = true;
            this.chkAuthor.CheckedChanged += new System.EventHandler(this.chkAuthor_CheckedChanged);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(163, 55);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(225, 22);
            this.txtAuthor.TabIndex = 3;
            // 
            // chkYear
            // 
            this.chkYear.AutoSize = true;
            this.chkYear.Location = new System.Drawing.Point(20, 90);
            this.chkYear.Name = "chkYear";
            this.chkYear.Size = new System.Drawing.Size(80, 20);
            this.chkYear.TabIndex = 4;
            this.chkYear.Text = "По году";
            this.chkYear.UseVisualStyleBackColor = true;
            this.chkYear.CheckedChanged += new System.EventHandler(this.chkYear_CheckedChanged);
            // 
            // numYearFrom
            // 
            this.numYearFrom.Location = new System.Drawing.Point(207, 89);
            this.numYearFrom.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.numYearFrom.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numYearFrom.Name = "numYearFrom";
            this.numYearFrom.Size = new System.Drawing.Size(120, 22);
            this.numYearFrom.TabIndex = 5;
            this.numYearFrom.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "От";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "До";
            // 
            // numYearTo
            // 
            this.numYearTo.Location = new System.Drawing.Point(387, 89);
            this.numYearTo.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.numYearTo.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numYearTo.Name = "numYearTo";
            this.numYearTo.Size = new System.Drawing.Size(120, 22);
            this.numYearTo.TabIndex = 8;
            this.numYearTo.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.numYearTo.ValueChanged += new System.EventHandler(this.numYearTo_ValueChanged);
            // 
            // chkDifficulty
            // 
            this.chkDifficulty.AutoSize = true;
            this.chkDifficulty.Location = new System.Drawing.Point(20, 125);
            this.chkDifficulty.Name = "chkDifficulty";
            this.chkDifficulty.Size = new System.Drawing.Size(120, 20);
            this.chkDifficulty.TabIndex = 9;
            this.chkDifficulty.Text = "По сложности";
            this.chkDifficulty.UseVisualStyleBackColor = true;
            this.chkDifficulty.CheckedChanged += new System.EventHandler(this.chkDifficulty_CheckedChanged);
            // 
            // numDiffTo
            // 
            this.numDiffTo.Location = new System.Drawing.Point(332, 125);
            this.numDiffTo.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDiffTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDiffTo.Name = "numDiffTo";
            this.numDiffTo.Size = new System.Drawing.Size(67, 22);
            this.numDiffTo.TabIndex = 13;
            this.numDiffTo.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "До";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "От";
            // 
            // numDiffFrom
            // 
            this.numDiffFrom.Location = new System.Drawing.Point(207, 125);
            this.numDiffFrom.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDiffFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDiffFrom.Name = "numDiffFrom";
            this.numDiffFrom.Size = new System.Drawing.Size(67, 22);
            this.numDiffFrom.TabIndex = 10;
            this.numDiffFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkRegex
            // 
            this.chkRegex.AutoSize = true;
            this.chkRegex.Location = new System.Drawing.Point(20, 160);
            this.chkRegex.Name = "chkRegex";
            this.chkRegex.Size = new System.Drawing.Size(184, 20);
            this.chkRegex.TabIndex = 14;
            this.chkRegex.Text = "Регулярное выражение";
            this.chkRegex.UseVisualStyleBackColor = true;
            this.chkRegex.CheckedChanged += new System.EventHandler(this.chkRegex_CheckedChanged);
            // 
            // txtRegex
            // 
            this.txtRegex.Location = new System.Drawing.Point(221, 160);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.Size = new System.Drawing.Size(178, 22);
            this.txtRegex.TabIndex = 15;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(150, 200);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(240, 200);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(95, 23);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(351, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbResults
            // 
            this.lbResults.FormattingEnabled = true;
            this.lbResults.ItemHeight = 16;
            this.lbResults.Location = new System.Drawing.Point(20, 250);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(755, 164);
            this.lbResults.TabIndex = 19;
            this.lbResults.SelectedIndexChanged += new System.EventHandler(this.lbResults_SelectedIndexChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtRegex);
            this.Controls.Add(this.chkRegex);
            this.Controls.Add(this.numDiffTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numDiffFrom);
            this.Controls.Add(this.chkDifficulty);
            this.Controls.Add(this.numYearTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numYearFrom);
            this.Controls.Add(this.chkYear);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.chkAuthor);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkName);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.numYearFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiffTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiffFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.CheckBox chkYear;
        private System.Windows.Forms.NumericUpDown numYearFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numYearTo;
        private System.Windows.Forms.CheckBox chkDifficulty;
        private System.Windows.Forms.NumericUpDown numDiffTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDiffFrom;
        private System.Windows.Forms.CheckBox chkRegex;
        private System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbResults;
    }
}