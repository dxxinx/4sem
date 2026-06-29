using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Course
    {
        [Required(ErrorMessage = "Название курса обязательно для заполнения")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название курса должно быть от 3 до 100 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z0-9\s\-]+$", ErrorMessage = "Название может содержать только буквы, цифры, пробелы и дефисы")]
        public string Name { get; set; }

        [Range(1, 4, ErrorMessage = "Возрастная группа должна быть от 1 до 4")]
        public int AgeGroup { get; set; }

        [Range(1, 10, ErrorMessage = "Сложность курса должна быть от 1 до 10")]
        public int Difficulty { get; set; }

        [Range(1, 100, ErrorMessage = "Количество лекций должно быть от 1 до 100")]
        public int LecturesCount { get; set; }

        [Range(1, 100, ErrorMessage = "Количество лабораторных должно быть от 1 до 100")]
        public int LabsCount { get; set; }

        [Required(ErrorMessage = "Вид контроля обязателен для заполнения")]
        [RegularExpression(@"^(Экзамен|Зачет)$", ErrorMessage = "Вид контроля должен быть 'Экзамен' или 'Зачет'")]
        public string ControlType { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(Course), nameof(ValidateStartDate))]
        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }

        public Teacher Teacher { get; set; }

        public List<Literature> LiteratureList { get; set; }

        [Range(0, 1000000, ErrorMessage = "Бюджет должен быть от 0 до 1 000 000")]
        public decimal Budget { get; set; }

        public Course()
        {
            LiteratureList = new List<Literature>();
            Teacher = new Teacher();
        }

        public decimal CalculateBudget()
        {
            // Формула расчета бюджета для возрастной группы
            decimal baseRate = 1000;
            decimal ageMultiplier = 1.0m;

            if (AgeGroup == 1)
            {
                ageMultiplier = 0.8m; // дети
            }
            else if (AgeGroup == 2)
            {
                ageMultiplier = 1.2m; // подростки
            }
            else if (AgeGroup == 3)
            {
                ageMultiplier = 1.5m; // взрослые
            }
            else if (AgeGroup == 4)
            {
                ageMultiplier = 1.0m; // пенсионеры
            }
            else
            {
                ageMultiplier = 1.0m;
            }

            decimal complexityMultiplier = 0.8m + (Difficulty * 0.2m);
            Budget = baseRate * ageMultiplier * complexityMultiplier * (LecturesCount + LabsCount);
            return Budget;
        }

        public static ValidationResult ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate < DateTime.Today)
            {
                return new ValidationResult("Дата начала не может быть в прошлом");
            }

            if (startDate > DateTime.Today.AddYears(1))
            {
                return new ValidationResult("Дата начала не может быть более чем через год");
            }

            return ValidationResult.Success;
        }

        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }
}