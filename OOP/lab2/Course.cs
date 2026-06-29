using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Course
    {
        public string Name { get; set; }
        public int AgeGroup { get; set; }
        public int Difficulty { get; set; }
        public int LecturesCount { get; set; }
        public int LabsCount { get; set; }
        public string ControlType { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public Teacher Teacher { get; set; }
        public List<Literature> LiteratureList { get; set; }
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

            // Используем обычный if вместо switch expression
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
    }
}
