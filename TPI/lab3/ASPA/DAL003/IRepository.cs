using System;
using System.Collections.Generic;

namespace DAL003
{
    public interface IRepository : IDisposable
    {
        public static string JSONFileName { get; set; }
        public static string BasePath { get; set; }

        List<Celebrity> getAllCelebrities();        
        Celebrity? GetCelebrityById(int id);        
        List<Celebrity> GetCelebritiesBySurname(string surname);  
        string? getPhotoPathById(int id);          
    }
}