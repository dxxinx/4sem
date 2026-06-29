using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Literature
    {
        [Required(ErrorMessage = "Название литературы обязательно для заполнения")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z0-9\s\-\.\,\:]+$",
            ErrorMessage = "Название может содержать буквы, цифры, пробелы, дефисы, точки, запятые и двоеточия")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Автор обязателен для заполнения")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "ФИО автора должно быть от 3 до 100 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z\s\.\-]+$",
            ErrorMessage = "Имя автора должно содержать только буквы, пробелы, точки и дефисы")]
        public string Author { get; set; }

        [YearRange(1900, 2026, ErrorMessage = "Год издания должен быть от 1900 до 2026")]
        public int Year { get; set; }

        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }

    // Свой атрибут валидации для года
    public class YearRangeAttribute : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        public YearRangeAttribute(int minYear, int maxYear)
        {
            _minYear = minYear;
            _maxYear = maxYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is int year)
            {
                if (year < _minYear || year > _maxYear)
                {
                    return new ValidationResult(ErrorMessage ?? $"Год должен быть между {_minYear} и {_maxYear}");
                }
            }

            return ValidationResult.Success;
        }
    }
}