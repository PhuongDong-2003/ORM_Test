using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ORMTest.DB;
using ORMTest.Entity;

namespace ORMTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DatabaseSetting databaseSetting;
        private readonly DatabaseContext _context;
        public EmployeeController(DatabaseSetting databaseSetting, DatabaseContext context)
        {
            this.databaseSetting = databaseSetting;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            using (var connection = new SqlConnection(databaseSetting.Connection))
            {
                var sql = @"select o.ShipName,  o.OrderID, e.lastname, e.employeeid EmployeeID from Employees e
                inner join Orders o on e.EmployeeID = o.EmployeeID 
                Where e.EmployeeID = @id ";

                var employees = new Dictionary<int, Employee>();
                var employee = await connection.QueryAsync<Order, Employee, Employee>(sql, (order, employee) =>
                {
                    if (employees.TryGetValue(employee.EmployeeID, out var existingEmployee))
                    {
                        order.EmployeeID= employee.EmployeeID;
                        existingEmployee.Orders.Add(order);
                        return existingEmployee;
                    }
                    else 
                    {
                        order.EmployeeID= employee.EmployeeID;
                        employees.Add(employee.EmployeeID, employee);
                        employee.Orders.Add(order);
                        return employee;
                    
                    }
                },
                splitOn: "lastname",
                param: new {id = id});

                return Ok(employees.Values.ToList());
            }
            
        }


        [HttpGet("get2")]
        public async Task<IActionResult> Get2(int id)
        {
            var parameters = new { id = id };
            SqlConnection connection = new SqlConnection(databaseSetting.Connection);
            connection.Open();
            string sqlQuery = "SELECT EmployeeID, lastname FROM Employees where EmployeeID=@id";
            var data = await connection.QueryFirstAsync<Employee>(sqlQuery, parameters);
            if (data is not null)
            {
                return Ok(data);
            }
            return NotFound();

        }

        [HttpGet("get3")]
        public async Task<IActionResult> Get3(int id)
        {
         
            // var employees = _context.Employees.Where(x => x.EmployeeID == id).Include(x => x.Orders).ToList();
                           
            // if (employees is not null)
            // {
            //     foreach (var employee in employees)
            //     {
            //         var order = dbContext.Orders.Where(x => x.EmployeeID == id && x.OrderID % 3== 0).ToList();  
            //         employee.Orders = order;
            //     }
     
            //     return Ok(employees);
            // }


            // return Ok(employees);


        }
    }
}