using System;
using System.Linq;

namespace lab1
{
    /// <summary>
    /// Класс текстового калькулятора.
    /// Содержит методы для работы со строкой.
    /// </summary>
    public class Calculator
    {
        // Делегат для выполнения операций над строкой
        public delegate string Operation(string input);

        // Событие, возникающее при выполнении операции
        public event Operation OnOperationPerformed;

        /// <summary>
        /// Замена подстроки на другую подстроку
        /// </summary>
        public string Replace(string input, string oldValue, string newValue)
        {
            return input.Replace(oldValue, newValue);
        }

        /// <summary>
        /// Удаление подстроки
        /// </summary>
        public string Remove(string input, string value)
        {
            return input.Replace(value, string.Empty);
        }

        /// <summary>
        /// Получение символа по индексу
        /// </summary>
        public string GetChar(string input, int index)
        {
            if (index < 0 || index >= input.Length)
                return "Ошибка: индекс вне диапазона.";

            return input[index].ToString();
        }

        /// <summary>
        /// Получение длины строки
        /// </summary>
        public string GetLength(string input)
        {
            return input.Length.ToString();
        }

        /// <summary>
        /// Подсчёт количества гласных букв
        /// </summary>
        public string CountVowels(string input)
        {
            const string vowels = "аеёиоуыэюяAEIOUАЕЁИОУЫЭЮЯ";
            int count = input.Count(c => vowels.Contains(c));
            return count.ToString();
        }

        /// <summary>
        /// Подсчёт количества согласных букв
        /// </summary>
        public string CountConsonants(string input)
        {
            const string consonants = "бвгджзйклмнпрстфхцчшщBCDFGHJKLMNPQRSTVWXYZБВГДЖЗЙКЛМНПРСТФХЦЧШЩ";
            int count = input.Count(c => consonants.Contains(c));
            return count.ToString();
        }

        /// <summary>
        /// Подсчёт количества предложений
        /// </summary>
        public string CountSentences(string input)
        {
            int count = input.Count(c => c == '.' || c == '!' || c == '?');
            return count.ToString();
        }

        /// <summary>
        /// Подсчёт количества слов
        /// </summary>
        public string CountWords(string input)
        {
            var words = input.Split(
                new[] { ' ', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            return words.Length.ToString();
        }

        /// <summary>
        /// Выполнение операции через делегат
        /// </summary>
        public string Execute(Operation operation, string input)
        {
            return operation?.Invoke(input);
        }
    }
}
