using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set;  }

        public String Department { get; set; }

        public int Salary { get; set; }

        public DateTime Hired {  get; set; }
    }
}
