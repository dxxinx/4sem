using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace lab2
{
    public partial class Form1 : Form
    {
        private Course currentCourse;
        private List<Course> courses;
        private string dataFilePath = "courses.json";
        public Form1()
        {
            InitializeComponent();
            courses = new List<Course>();
            currentCourse = new Course();
            LoadCourses();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                {
                    throw new Exception("Проверьте правильность введенных данных!");
                }

                // Заполняем курс
                if (txtCourseName.Text == "")
                {
                    throw new Exception("Введите название курса!");
                }
                currentCourse.Name = txtCourseName.Text;

                if (cmbAgeGroup.SelectedIndex == -1)
                {
                    throw new Exception("Выберите возрастную группу!");
                }
                currentCourse.AgeGroup = cmbAgeGroup.SelectedIndex + 1;

                currentCourse.Difficulty = trackDifficulty.Value;
                currentCourse.LecturesCount = (int)numLectures.Value;
                currentCourse.LabsCount = (int)numLabs.Value;

                if (rbExam.Checked)
                {
                    currentCourse.ControlType = "Экзамен";
                }
                else if (rbTest.Checked)
                {
                    currentCourse.ControlType = "Зачет";
                }
                else
                {
                    throw new Exception("Выберите вид контроля!");
                }

                currentCourse.StartDate = dtpStartDate.Value;
                currentCourse.IsActive = chkIsActive.Checked;

                // Заполняем преподавателя
                if (txtTeacherName.Text == "")
                {
                    throw new Exception("Введите ФИО преподавателя!");
                }
                currentCourse.Teacher.FullName = txtTeacherName.Text;

                if (!ContainsOnlyLetters(txtTeacherName.Text))
                {
                    throw new Exception("ФИО должно содержать только буквы и пробелы!");
                }

                currentCourse.Teacher.Department = txtTeacherDepartment.Text;
                currentCourse.Teacher.RoomNumber = (int)numTeacherRoom.Value;
                currentCourse.Teacher.Email = txtTeacherEmail.Text;

                if (!string.IsNullOrEmpty(txtTeacherEmail.Text) && !txtTeacherEmail.Text.Contains("@"))
                {
                    throw new Exception("Введите корректный email!");
                }

                currentCourse.Teacher.Phone = mtbTeacherPhone.Text;

                // Рассчитываем бюджет
                CalculateBudget();

                // Сохраняем
                courses.Add(currentCourse);
                SaveCourses();

                MessageBox.Show("Курс успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCourses();
                UpdateDataGridView();
                MessageBox.Show("Курсы загружены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                {
                    throw new Exception("Заполните основные данные для расчета!");
                }

                CalculateBudget();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();

        }

        private void btnAddLiterature_Click(object sender, EventArgs e)
        {
            try
            {
                LiteratureForm litForm = new LiteratureForm();
                if (litForm.ShowDialog() == DialogResult.OK)
                {
                    currentCourse.LiteratureList.Add(litForm.LiteratureData);
                    UpdateLiteratureList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditLiterature_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbLiterature.SelectedIndex == -1)
                {
                    throw new Exception("Выберите литературу для редактирования!");
                }

                LiteratureForm litForm = new LiteratureForm(currentCourse.LiteratureList[lbLiterature.SelectedIndex]);
                if (litForm.ShowDialog() == DialogResult.OK)
                {
                    currentCourse.LiteratureList[lbLiterature.SelectedIndex] = litForm.LiteratureData;
                    UpdateLiteratureList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveLiterature_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbLiterature.SelectedIndex == -1)
                {
                    throw new Exception("Выберите литературу для удаления!");
                }

                currentCourse.LiteratureList.RemoveAt(lbLiterature.SelectedIndex);
                UpdateLiteratureList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackDifficulty_Scroll(object sender, EventArgs e)
        {
            lblDifficultyValue.Text = trackDifficulty.Value.ToString();
        }
        private void CalculateBudget()
        {
            decimal baseRate = 1000;
            decimal ageMultiplier = 1.0m;
            decimal complexityMultiplier = 0.8m + (trackDifficulty.Value * 0.2m);

            if (cmbAgeGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Возрастная группа не выбрана!");
                return;
            }

            switch (cmbAgeGroup.SelectedIndex)
            {
                case 0: // Дети
                    ageMultiplier = 0.8m;
                    break;
                case 1: // Подростки
                    ageMultiplier = 1.2m;
                    break;
                case 2: // Взрослые
                    ageMultiplier = 1.5m;
                    break;
                case 3: // Пенсионеры
                    ageMultiplier = 1.0m;
                    break;
            }

            decimal budget = baseRate * ageMultiplier * complexityMultiplier * ((int)numLectures.Value + (int)numLabs.Value);
            currentCourse.Budget = budget;
            lblBudget.Text = $"Бюджет: {budget:F2} руб.";
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                errorProvider.SetError(txtCourseName, "Введите название курса");
                return false;
            }
            errorProvider.SetError(txtCourseName, "");

            if (cmbAgeGroup.SelectedIndex == -1)
            {
                errorProvider.SetError(cmbAgeGroup, "Выберите возрастную группу");
                return false;
            }
            errorProvider.SetError(cmbAgeGroup, "");

            if (string.IsNullOrWhiteSpace(txtTeacherName.Text))
            {
                errorProvider.SetError(txtTeacherName, "Введите ФИО преподавателя");
                return false;
            }
            errorProvider.SetError(txtTeacherName, "");

            if (!string.IsNullOrWhiteSpace(txtTeacherEmail.Text) && !txtTeacherEmail.Text.Contains("@"))
            {
                errorProvider.SetError(txtTeacherEmail, "Введите корректный email");
                return false;
            }
            errorProvider.SetError(txtTeacherEmail, "");

            return true;
        }

        public static bool ContainsOnlyLetters(string input)
        {
            foreach (char c in input)
            {
                if (!(char.IsLetter(c) || c == ' ' || c == '-'))
                {
                    return false;
                }
            }
            return true;
        }

        private void SaveCourses()
        {
            List<Course> existingCourses;

            if (File.Exists(dataFilePath))
            {
                string existingData = File.ReadAllText(dataFilePath);
                existingCourses = JsonConvert.DeserializeObject<List<Course>>(existingData) ?? new List<Course>();
            }
            else
            {
                existingCourses = new List<Course>();
            }

            if (!existingCourses.Contains(currentCourse))
            {
                existingCourses.Add(currentCourse);
            }

            string json = JsonConvert.SerializeObject(existingCourses, Formatting.Indented);
            File.WriteAllText(dataFilePath, json);

            courses = existingCourses;
        }

        private void LoadCourses()
        {
            if (File.Exists(dataFilePath))
            {
                string existingData = File.ReadAllText(dataFilePath);
                courses = JsonConvert.DeserializeObject<List<Course>>(existingData) ?? new List<Course>();
            }
            else
            {
                courses = new List<Course>();
            }
        }

        private void UpdateDataGridView()
        {
            dgvCourses.Rows.Clear();

            foreach (var course in courses)
            {
                string ageGroup = "";
                if (course.AgeGroup == 1)
                    ageGroup = "Дети";
                else if (course.AgeGroup == 2)
                    ageGroup = "Подростки";
                else if (course.AgeGroup == 3)
                    ageGroup = "Взрослые";
                else if (course.AgeGroup == 4)
                    ageGroup = "Пенсионеры";
                else
                    ageGroup = "Не указано";

                dgvCourses.Rows.Add(
                    course.Name,
                    ageGroup,
                    course.Difficulty,
                    course.LecturesCount,
                    course.LabsCount,
                    course.ControlType,
                    course.Teacher?.FullName,
                    course.Budget.ToString("F2") + " руб."
                );
            }
        }

        private void UpdateLiteratureList()
        {
            lbLiterature.Items.Clear();
            foreach (var lit in currentCourse.LiteratureList)
            {
                lbLiterature.Items.Add($"{lit.Title} - {lit.Author} ({lit.Year})");
            }
        }

        private void ClearForm()
        {
            txtCourseName.Clear();
            cmbAgeGroup.SelectedIndex = -1;
            trackDifficulty.Value = 5;
            lblDifficultyValue.Text = "5";
            numLectures.Value = 10;
            numLabs.Value = 5;
            rbExam.Checked = true;
            dtpStartDate.Value = DateTime.Now;
            chkIsActive.Checked = true;
            txtTeacherName.Clear();
            txtTeacherDepartment.Clear();
            numTeacherRoom.Value = 101;
            txtTeacherEmail.Clear();
            mtbTeacherPhone.Clear();
            currentCourse.LiteratureList.Clear();
            UpdateLiteratureList();
            lblBudget.Text = "Бюджет: 0 руб.";
            currentCourse = new Course();
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void mtbTeacherPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }

    
}
