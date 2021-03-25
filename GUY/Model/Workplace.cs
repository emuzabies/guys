using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Workplace
    {
        public int WorkPlaceID { get; set; }
        public string WorkPlaceName { get; set; }
        public string Address { get; set; }
        public int Province { get; set; }
        public string Postnumber { get; set; }
        public int SupervisorID { get; set; }
    }
}
