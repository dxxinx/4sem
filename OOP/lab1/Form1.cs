using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab1
{
    public partial class Form1 : Form
    {
        private readonly Calculator _calculator;
        public Form1()
        {
            InitializeComponent();
            _calculator = new Calculator();
            textBox1.KeyPress += TextOnly_KeyPress;
            textBox3.KeyPress += TextOnly_KeyPress;
            textBox4.KeyPress += TextOnly_KeyPress;
            textBox5.KeyPress += IndexOnly_KeyPress; 
        }
        private void TextOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                !char.IsPunctuation(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void IndexOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Получить символ по индексу
        private void btn_getChar_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(textBox5.Text);

                textBox2.Text = _calculator.GetChar(textBox1.Text, index);

            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Введите корректный индекс (целое число)!",
                    "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Ошибка: Введенное число слишком большое или слишком маленькое!",
                    "Ошибка переполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Индекс вне диапазона",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine($"Попытка получения символа выполнена в {DateTime.Now}");
            }
        }

        // Замена подстроки
        private void btn_replace_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.Replace(
                textBox1.Text,
                textBox3.Text,
                textBox4.Text);
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"AAAAAA");
        }
        // Удаление подстроки
        private void btn_remove_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.Remove(
                textBox1.Text,
                textBox3.Text);
        }

        // Длина строки
        private void btn_length_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.GetLength(textBox1.Text);
        }

        // Количество гласных
        private void btn_Vowels_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.CountVowels(textBox1.Text);
        }

        // Количество согласных
        private void btn_Consonants_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.CountConsonants(textBox1.Text);
        }

        // Количество предложений
        private void btn_Sentences_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.CountSentences(textBox1.Text);
        }

        // Количество слов
        private void btn_Words_Click(object sender, EventArgs e)
        {
            textBox2.Text = _calculator.CountWords(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
