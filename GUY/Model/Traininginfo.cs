using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Traininginfo
    {
        public int TrainingID { get; set; }
        public string Topic { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
    }
}
