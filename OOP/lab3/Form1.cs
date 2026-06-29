using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace lab2
{
    public partial class Form1 : Form
    {
        private Course currentCourse;
        private List<Course> courses;
        private List<Course> searchResults;
        private string dataFilePath = "courses.json";
        private int currentIndex = -1;
        private string lastAction = "";

        public Form1()
        {
            InitializeComponent();
            courses = new List<Course>();
            currentCourse = new Course();
            LoadCourses();
            lastAction = "Запуск программы";
            UpdateStatusBar();
            timerDateTime.Start();
            InitializeMenuHandlers();
            InitializeToolStripHandlers();
        }
        private string GetFieldName(string field)
        {
            switch (field)
            {
                case "Name": return "name";
                case "Author": return "author";
                case "Year": return "year";
                default: return field;
            }
        }

        private void DisplayCourse(int index)
        {
            if (index >= 0 && index < courses.Count)
            {
                var course = courses[index];
                txtCourseName.Text = course.Name;
                cmbAgeGroup.SelectedIndex = course.AgeGroup - 1;
                trackDifficulty.Value = course.Difficulty;
                numLectures.Value = course.LecturesCount;
                numLabs.Value = course.LabsCount;

                if (course.ControlType == "Экзамен")
                    rbExam.Checked = true;
                else
                    rbTest.Checked = true;

                dtpStartDate.Value = course.StartDate;
                chkIsActive.Checked = course.IsActive;

                if (course.Teacher != null)
                {
                    txtTeacherName.Text = course.Teacher.FullName;
                    txtTeacherDepartment.Text = course.Teacher.Department;
                    numTeacherRoom.Value = course.Teacher.RoomNumber;
                    txtTeacherEmail.Text = course.Teacher.Email;
                    mtbTeacherPhone.Text = course.Teacher.Phone;
                }

                currentCourse.LiteratureList = course.LiteratureList ?? new List<Literature>();
                UpdateLiteratureList();
                lblBudget.Text = $"Бюджет: {course.Budget:F2} руб.";
            }
        }
        #region Инициализация обработчиков

        private void InitializeMenuHandlers()
        {
            // Файл
            сохранитьРезультатыToolStripMenuItem.Click += SaveResults_Click;

            // Поиск
            поНазваниюToolStripMenuItem.Click += (s, e) => SearchByField("Name");
            поАвторуToolStripMenuItem.Click += (s, e) => SearchByField("Author");
            поГодуToolStripMenuItem.Click += (s, e) => SearchByYear();
            расширенныйПоискToolStripMenuItem.Click += AdvancedSearch_Click;

            // Сортировка
            поНазваниюToolStripMenuItem1.Click += (s, e) => Sort("Name");
            поСложностиToolStripMenuItem.Click += (s, e) => Sort("Difficulty");
            поБюджетуToolStripMenuItem.Click += (s, e) => Sort("Budget");

            // Справка
            оПрограммеToolStripMenuItem.Click += About_Click;
        }

        private void InitializeToolStripHandlers()
        {
            tsbSearch.Click += (s, e) => расширенныйПоискToolStripMenuItem.PerformClick();
            tsbSort.Click += (s, e) => поНазваниюToolStripMenuItem1.PerformClick();
            tsbClear.Click += TsbClear_Click;
            tsbDelete.Click += TsbDelete_Click;
            tsbBack.Click += TsbBack_Click;
            tsbForward.Click += TsbForward_Click;
        }

        #endregion

        #region Поиск

        private void SearchByField(string fieldName)
        {
            try
            {
                string searchTerm = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter text to search by {GetFieldName(fieldName)}:",
                    "Search", "");

                if (string.IsNullOrEmpty(searchTerm)) return;

                var results = courses.Where(c =>
                {
                    switch (fieldName)
                    {
                        case "Name":
                            return c.Name?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;
                        case "Author":
                            return c.LiteratureList.Any(l =>
                                l.Author?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
                        default:
                            return false;
                    }
                }).ToList();

                ShowSearchResults(results, $"Found: {results.Count}");
                lastAction = $"Search by {fieldName}: {searchTerm}";
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void SearchByYear()
        {
            try
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter year or range (e.g., 2020 or 2010-2020):",
                    "Search by year", "");

                if (string.IsNullOrEmpty(input)) return;

                List<Course> results = new List<Course>();

                if (input.Contains("-"))
                {
                    var parts = input.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int yearFrom) &&
                        int.TryParse(parts[1], out int yearTo))
                    {
                        results = courses.Where(c =>
                            c.LiteratureList.Any(l => l.Year >= yearFrom && l.Year <= yearTo)).ToList();
                    }
                }
                else if (Regex.IsMatch(input, @"^\d{4}$"))
                {
                    int year = int.Parse(input);
                    results = courses.Where(c =>
                        c.LiteratureList.Any(l => l.Year == year)).ToList();
                }
                else
                {
                    MessageBox.Show("Invalid format. Use YYYY or YYYY-YYYY");
                    return;
                }

                ShowSearchResults(results, $"Found: {results.Count}");
                lastAction = $"Search by year: {input}";
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void AdvancedSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(courses);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                searchResults = searchForm.SearchResults;
                ShowSearchResults(searchResults, $"Advanced search results: {searchResults.Count}");
                lastAction = "Advanced search";
                UpdateStatusBar();
            }
        }

        private void ShowSearchResults(List<Course> results, string title)
        {
            if (results.Count == 0)
            {
                MessageBox.Show("Nothing found!");
                return;
            }

            Form resultForm = new Form();
            resultForm.Text = title;
            resultForm.Size = new Size(800, 400);
            resultForm.StartPosition = FormStartPosition.CenterParent;

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;

            dgv.Columns.Add("Name", "Name");
            dgv.Columns.Add("AgeGroup", "Age Group");
            dgv.Columns.Add("Difficulty", "Difficulty");
            dgv.Columns.Add("Teacher", "Teacher");
            dgv.Columns.Add("Budget", "Budget");

            foreach (var course in results)
            {
                string ageGroup = course.AgeGroup == 1 ? "Children" :
                                 course.AgeGroup == 2 ? "Teenagers" :
                                 course.AgeGroup == 3 ? "Adults" : "Seniors";

                dgv.Rows.Add(
                    course.Name,
                    ageGroup,
                    course.Difficulty,
                    course.Teacher?.FullName,
                    course.Budget.ToString("F2") + " rub."
                );
            }

            resultForm.Controls.Add(dgv);
            resultForm.ShowDialog();
        }

        #endregion

        #region Сортировка

        private void Sort(string criteria)
        {
            try
            {
                switch (criteria)
                {
                    case "Name":
                        courses = courses.OrderBy(c => c.Name).ToList();
                        lastAction = "Sort by name";
                        break;
                    case "Difficulty":
                        courses = courses.OrderBy(c => c.Difficulty).ToList();
                        lastAction = "Sort by difficulty";
                        break;
                    case "Budget":
                        courses = courses.OrderBy(c => c.Budget).ToList();
                        lastAction = "Sort by budget";
                        break;
                }

                UpdateDataGridView();
                UpdateStatusBar();
                MessageBox.Show($"Sort by {criteria} completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        #endregion

        #region Сохранение результатов

        private void SaveResults_Click(object sender, EventArgs e)
        {
            try
            {
                if (searchResults == null || searchResults.Count == 0)
                {
                    MessageBox.Show("No results to save!");
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml";
                saveDialog.DefaultExt = "json";
                saveDialog.FileName = $"search_results_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string json = JsonConvert.SerializeObject(searchResults, Formatting.Indented);
                    File.WriteAllText(saveDialog.FileName, json);

                    lastAction = $"Saved search results to {saveDialog.FileName}";
                    UpdateStatusBar();
                    MessageBox.Show("Results saved!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        #endregion

        #region Toolbar

        private void TsbClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lastAction = "Clear form";
            UpdateStatusBar();
        }

        private void TsbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentIndex >= 0 && currentIndex < courses.Count)
                {
                    var result = MessageBox.Show("Delete current course?", "Confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        courses.RemoveAt(currentIndex);
                        SaveCourses();
                        UpdateDataGridView();

                        if (courses.Count > 0)
                        {
                            currentIndex = Math.Min(currentIndex, courses.Count - 1);
                        }
                        else
                        {
                            currentIndex = -1;
                        }

                        lastAction = "Delete course";
                        UpdateStatusBar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void TsbBack_Click(object sender, EventArgs e)
        {
            if (courses.Count > 0 && currentIndex > 0)
            {
                currentIndex--;
                DisplayCourse(currentIndex);
                lastAction = "Navigate back";
                UpdateStatusBar();
            }
        }

        private void TsbForward_Click(object sender, EventArgs e)
        {
            if (courses.Count > 0 && currentIndex < courses.Count - 1)
            {
                currentIndex++;
                DisplayCourse(currentIndex);
                lastAction = "Navigate forward";
                UpdateStatusBar();
            }
        }

        #endregion

        #region Status bar

        private void UpdateStatusBar()
        {
            tsslTotalCount.Text = $"Всего курсов: {courses.Count}";
            tsslLastAction.Text = $"Последнее действие: {lastAction}";
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            tsslDateTime.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
        }

        #endregion

        #region About

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Education Department - Programming Courses\n" +
                "Version 2.0\n" +
                "Developer: Student Name\n" +
                "Group: ...\n" +
                "© 2026",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        #endregion

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
                lastAction = $"Сохранен курс: {currentCourse.Name}";
                UpdateStatusBar();
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
                lastAction = "Загрузка данных";
                UpdateStatusBar();
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
                lastAction = "Расчет бюджета";
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
            lastAction = "Показаны все курсы";
                UpdateStatusBar();

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

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }

    
}
