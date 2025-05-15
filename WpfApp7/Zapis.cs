using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    public class Zapis
    {
        public int ID { get; set; }
        public TimeOnly Nachalo { get; set; }
        public TimeOnly Konez { get; set; }
        public string Vrach { get; set; }
        public string Kabinet { get; set; }
        public DateTime Den { get; set; }
        public DateTime DataPriema { get; set; }
        public TimeOnly Pereriv { get; set; }
        public string Primechanie { get; set; }

    }
}
