using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORMTest.Entity
{

    [Table("Employees")]

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }

        public Employee()
        {
            Orders = new List<Order>();
        }
    }
}