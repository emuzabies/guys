using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUY.Model
{
    public class Employeeinfo
    {
        public int ID { get; set; }
        public int JobTitle { get; set; }
        public int Department { get; set; }
        public int WorkPlace { get; set; }
        public float Compensation { get; set; }
        public string BankAccount { get; set; }
        public string HiredDate { get; set; }
        public int VacationLeft { get; set; }
        public int SickLeft { get; set; }
        public int Status { get; set; }
        public int SupervisorID { get; set; }
    }
}
