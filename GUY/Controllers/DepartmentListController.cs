using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using GUY.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GUY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public DepartmentListController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = "select Department.DepartmentName, UserInfo.Firstname, UserInfo.Lastname from EmployeeInfo, Department, UserInfo where UserInfo.ID = EmployeeInfo.ID and UserInfo.ID = Department.SupervisorID and EmployeeInfo.Department = Department.DepartmentID; ";
            DataTable table = new DataTable();
            string sqlDataSource = "Data Source=DESKTOP-2VQEA7I\\MSSQLSERVER01;Initial Catalog=GUY;Integrated Security=True;";
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
