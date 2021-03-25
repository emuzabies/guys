using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Tasks
    {
        public int WorkID { get; set; }
        public string Topic { get; set; }
        public int ResponsibleID { get; set; }
        public int CreatedID { get; set; }
        public int ParticipantID { get; set; }
        public string DueDate { get; set; }
        public string Instruction { get; set; }
        public int Status { get; set; }
    }
}
