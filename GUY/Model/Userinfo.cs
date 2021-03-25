using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Userinfo
    {
        public int ID { get; set; }
        public int Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Gender { get; set; }
        public string Identification { get; set; }
        public string Birthdate { get; set; }
        public int Age { get; set; }
        public int Education { get; set; }
        public string Photo { get; set; }
    }
}
