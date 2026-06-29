using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Teacher
    {
        public string Department { get; set; }
        public string FullName { get; set; }
        public int RoomNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
