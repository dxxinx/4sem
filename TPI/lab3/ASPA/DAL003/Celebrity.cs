using System;

namespace DAL003
{
    public class Celebrity
    {
        public int Id { get; set; }
        public string? Firstname { get; set; } 
        public string? Surname { get; set; }  
        public string? PhotoPath { get; set; } 

        public override string ToString()
        {
            return $"Id: {Id}, Firstname: {Firstname}, Surname: {Surname}, PhotoPath: {PhotoPath}";
        }
    }
}