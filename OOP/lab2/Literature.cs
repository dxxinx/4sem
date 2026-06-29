using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    public class Literature
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
