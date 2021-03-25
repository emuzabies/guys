using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Usercontact
    {
        public int ID { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Province { get; set; }
        public string Postnumber { get; set; }
    }
}
