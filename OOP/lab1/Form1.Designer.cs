namespace lab1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_replace = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.btn_getChar = new System.Windows.Forms.Button();
            this.btn_length = new System.Windows.Forms.Button();
            this.btn_Vowels = new System.Windows.Forms.Button();
            this.btn_Consonants = new System.Windows.Forms.Button();
            this.btn_Sentences = new System.Windows.Forms.Button();
            this.btn_Words = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Исходная строка";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(169, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(637, 30);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_replace
            // 
            this.btn_replace.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_replace.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_replace.Location = new System.Drawing.Point(16, 259);
            this.btn_replace.Name = "btn_replace";
            this.btn_replace.Size = new System.Drawing.Size(170, 43);
            this.btn_replace.TabIndex = 2;
            this.btn_replace.Text = "заменить";
            this.btn_replace.UseVisualStyleBackColor = false;
            this.btn_replace.Click += new System.EventHandler(this.btn_replace_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove.Location = new System.Drawing.Point(215, 259);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(200, 43);
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Text = "window";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Результат";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(169, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(637, 30);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "Подстрока";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(386, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Новая подстрока";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(139, 123);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(195, 30);
            this.textBox3.TabIndex = 8;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(569, 123);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(237, 30);
            this.textBox4.TabIndex = 9;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "Индекс";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(109, 198);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(111, 30);
            this.textBox5.TabIndex = 11;
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // btn_getChar
            // 
            this.btn_getChar.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_getChar.Location = new System.Drawing.Point(437, 259);
            this.btn_getChar.Name = "btn_getChar";
            this.btn_getChar.Size = new System.Drawing.Size(170, 43);
            this.btn_getChar.TabIndex = 12;
            this.btn_getChar.Text = "получить символ";
            this.btn_getChar.UseVisualStyleBackColor = true;
            this.btn_getChar.Click += new System.EventHandler(this.btn_getChar_Click);
            // 
            // btn_length
            // 
            this.btn_length.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_length.Location = new System.Drawing.Point(646, 259);
            this.btn_length.Name = "btn_length";
            this.btn_length.Size = new System.Drawing.Size(170, 43);
            this.btn_length.TabIndex = 13;
            this.btn_length.Text = "длина строки";
            this.btn_length.UseVisualStyleBackColor = true;
            this.btn_length.Click += new System.EventHandler(this.btn_length_Click);
            // 
            // btn_Vowels
            // 
            this.btn_Vowels.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Vowels.Location = new System.Drawing.Point(16, 328);
            this.btn_Vowels.Name = "btn_Vowels";
            this.btn_Vowels.Size = new System.Drawing.Size(193, 43);
            this.btn_Vowels.TabIndex = 14;
            this.btn_Vowels.Text = "кол-во гласных";
            this.btn_Vowels.UseVisualStyleBackColor = true;
            this.btn_Vowels.Click += new System.EventHandler(this.btn_Vowels_Click);
            // 
            // btn_Consonants
            // 
            this.btn_Consonants.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Consonants.Location = new System.Drawing.Point(424, 328);
            this.btn_Consonants.Name = "btn_Consonants";
            this.btn_Consonants.Size = new System.Drawing.Size(193, 43);
            this.btn_Consonants.TabIndex = 15;
            this.btn_Consonants.Text = "кол-во согласных";
            this.btn_Consonants.UseVisualStyleBackColor = true;
            this.btn_Consonants.Click += new System.EventHandler(this.btn_Consonants_Click);
            // 
            // btn_Sentences
            // 
            this.btn_Sentences.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sentences.Location = new System.Drawing.Point(215, 328);
            this.btn_Sentences.Name = "btn_Sentences";
            this.btn_Sentences.Size = new System.Drawing.Size(203, 43);
            this.btn_Sentences.TabIndex = 16;
            this.btn_Sentences.Text = "кол-во предложений";
            this.btn_Sentences.UseVisualStyleBackColor = true;
            this.btn_Sentences.Click += new System.EventHandler(this.btn_Sentences_Click);
            // 
            // btn_Words
            // 
            this.btn_Words.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Words.Location = new System.Drawing.Point(623, 328);
            this.btn_Words.Name = "btn_Words";
            this.btn_Words.Size = new System.Drawing.Size(193, 43);
            this.btn_Words.TabIndex = 17;
            this.btn_Words.Text = "кол-во слов";
            this.btn_Words.UseVisualStyleBackColor = true;
            this.btn_Words.Click += new System.EventHandler(this.btn_Words_Click);
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(636, 403);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(193, 43);
            this.btn1.TabIndex = 17;
            this.btn1.Text = "Window";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(858, 614);
            this.Controls.Add(this.btn_Words);
            this.Controls.Add(this.btn_Sentences);
            this.Controls.Add(this.btn_Consonants);
            this.Controls.Add(this.btn_Vowels);
            this.Controls.Add(this.btn_length);
            this.Controls.Add(this.btn_getChar);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_replace);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_replace;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button btn_getChar;
        private System.Windows.Forms.Button btn_length;
        private System.Windows.Forms.Button btn_Vowels;
        private System.Windows.Forms.Button btn_Consonants;
        private System.Windows.Forms.Button btn_Sentences;
        private System.Windows.Forms.Button btn_Words;
        private System.Windows.Forms.Button btn1;
    }
}

