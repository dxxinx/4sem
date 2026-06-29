using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Teacher
    {
        [StringLength(50, ErrorMessage = "Название кафедры не должно превышать 50 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z\s\-]+$", ErrorMessage = "Название кафедры должно содержать только буквы, пробелы и дефисы")]
        public string Department { get; set; }

        [Required(ErrorMessage = "ФИО преподавателя обязательно для заполнения")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "ФИО должно быть от 5 до 100 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z\s\-]+$", ErrorMessage = "ФИО должно содержать только буквы, пробелы и дефисы")]
        public string FullName { get; set; }

        [Range(1, 500, ErrorMessage = "Номер аудитории должен быть от 1 до 500")]
        public int RoomNumber { get; set; }

        [EmailAddress(ErrorMessage = "Введите корректный email адрес")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email должен быть в формате user@example.com")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        [RegularExpression(@"^\+375\(\d{2}\)\d{3}-\d{2}-\d{2}$", ErrorMessage = "Телефон должен быть в формате +375(XX)XXX-XX-XX")]
        public string Phone { get; set; }

        // Метод для валидации объекта преподавателя
        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }
}