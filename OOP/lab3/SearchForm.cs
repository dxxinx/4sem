using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class SearchForm : Form
    {
        private List<Course> allCourses;
        public List<Course> SearchResults { get; private set; }
        public SearchForm(List<Course> courses)
        {
            InitializeComponent();
            allCourses = courses;
            SearchResults = new List<Course>();
            chkName.Checked = false;
            chkAuthor.Checked = false;
            chkYear.Checked = false;
            chkDifficulty.Checked = false;
            chkRegex.Checked = false;

            txtName.Enabled = false;
            txtAuthor.Enabled = false;
            numYearFrom.Enabled = false;
            numYearTo.Enabled = false;
            numDiffFrom.Enabled = false;
            numDiffTo.Enabled = false;
            txtRegex.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numYearTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = chkName.Checked;
        }

        private void chkAuthor_CheckedChanged(object sender, EventArgs e)
        {
            txtAuthor.Enabled = chkAuthor.Checked;
        }

        private void chkYear_CheckedChanged(object sender, EventArgs e)
        {
            numYearFrom.Enabled = chkYear.Checked;
            numYearTo.Enabled = chkYear.Checked;
        }

        private void chkDifficulty_CheckedChanged(object sender, EventArgs e)
        {
            numDiffFrom.Enabled = chkDifficulty.Checked;
            numDiffTo.Enabled = chkDifficulty.Checked;
        }

        private void chkRegex_CheckedChanged(object sender, EventArgs e)
        {
            txtRegex.Enabled = chkRegex.Checked;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var query = allCourses.AsEnumerable();

                // Фильтр по названию
                if (chkName.Checked && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    query = query.Where(c => c.Name != null &&
                        c.Name.IndexOf(txtName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                // Фильтр по автору
                if (chkAuthor.Checked && !string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    query = query.Where(c => c.LiteratureList != null &&
                        c.LiteratureList.Any(l =>
                            l.Author != null && l.Author.IndexOf(txtAuthor.Text, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                // Фильтр по году
                if (chkYear.Checked)
                {
                    int from = (int)numYearFrom.Value;
                    int to = (int)numYearTo.Value;
                    query = query.Where(c => c.LiteratureList != null &&
                        c.LiteratureList.Any(l => l.Year >= from && l.Year <= to));
                }

                // Фильтр по сложности
                if (chkDifficulty.Checked)
                {
                    int from = (int)numDiffFrom.Value;
                    int to = (int)numDiffTo.Value;
                    query = query.Where(c => c.Difficulty >= from && c.Difficulty <= to);
                }

                // Регулярное выражение
                if (chkRegex.Checked && !string.IsNullOrWhiteSpace(txtRegex.Text))
                {
                    try
                    {
                        Regex regex = new Regex(txtRegex.Text, RegexOptions.IgnoreCase);
                        query = query.Where(c => regex.IsMatch(c.Name ?? ""));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка в регулярном выражении: " + ex.Message);
                        return;
                    }
                }

                SearchResults = query.ToList();

                lbResults.Items.Clear();
                foreach (var course in SearchResults)
                {
                    lbResults.Items.Add($"{course.Name} - {course.Teacher?.FullName} (бюджет: {course.Budget:F2})");
                }

                if (SearchResults.Count == 0)
                {
                    MessageBox.Show("Ничего не найдено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chkName.Checked = false;
            chkAuthor.Checked = false;
            chkYear.Checked = false;
            chkDifficulty.Checked = false;
            chkRegex.Checked = false;
            txtName.Text = "";
            txtAuthor.Text = "";
            txtRegex.Text = "";
            numYearFrom.Value = 2000;
            numYearTo.Value = 2025;
            numDiffFrom.Value = 1;
            numDiffTo.Value = 10;
            lbResults.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
