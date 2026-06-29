using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DAL003
{
    public class Repository : IRepository
    {
        public static string JSONFileName { get; set; } = string.Empty;
        public static string BasePath { get; set; } = string.Empty;

        private List<Celebrity> _celebrities;

        private Repository(string path)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            BasePath = Path.Combine(currentDir, path);
            JSONFileName = Path.Combine(BasePath, "Celebrities.json");

            LoadFromJson();
        }

        public static Repository Create(string path)
        {
            return new Repository(path);
        }

        private void LoadFromJson()
        {
            try
            {
                if (!File.Exists(JSONFileName))
                {
                    throw new FileNotFoundException($"JSON файл не найден: {JSONFileName}");
                }

                string jsonString = File.ReadAllText(JSONFileName);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                _celebrities = JsonSerializer.Deserialize<List<Celebrity>>(jsonString, options);

                if (_celebrities == null)
                {
                    throw new InvalidOperationException("Не удалось десериализовать JSON");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка при загрузке данных: {ex.Message}");
                throw; 
            }
        }

        public List<Celebrity> getAllCelebrities()
        {
            return new List<Celebrity>(_celebrities);
        }

        public Celebrity? GetCelebrityById(int id)
        {
            return _celebrities.FirstOrDefault(c => c.Id == id);
        }

        public List<Celebrity> GetCelebritiesBySurname(string surname)
        {
            return _celebrities
                .Where(c => c.Surname != null &&
                       c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public string? getPhotoPathById(int id)
        {
            var celebrity = GetCelebrityById(id);
            return celebrity?.PhotoPath;
        }

        public void Dispose()
        {
            // Освобождение ресурсов
        }
    }
}