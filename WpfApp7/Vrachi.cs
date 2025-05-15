using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    public class Vrachi
    {
        public int ID {  get; set; }
        public string VFirstname { get; set; }
        public string VLastname { get; set; }
        public string VPatronymic { get; set; }
        public string Specialnost { get; set; }
        public int Dennedeli { get; set; }
        public TimeOnly Nachalopriema { get; set; }
        public string Primechanie { get; set; }

    }
}
