using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ORMTest.Entity
{

    [Table("Orders")]
    public class Order
    {

        public int OrderID { set; get; }

        public int EmployeeID { set; get; }

        public string ShipName { set; get; }

        [JsonIgnore]
        public Employee Employee { set; get; }

    }
}