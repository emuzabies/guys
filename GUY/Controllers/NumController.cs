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
    public class NumController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public NumController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select Checkk.DepartmentName, COUNT(Checkk.ID) as total from(select userinfo.ID, Department.DepartmentName, CASE WHEN[Sum] % 2 = 0 then 0 ELSE 1 END AS[Status] from (select  ID, COUNT(UserCheck.ID) as [Sum] from UserCheck where UserCheck.date = CAST(GETDATE() AS DATE) group by ID ) as Summary, Department, UserInfo where DepartmentID = UserInfo.Department and Summary.ID = UserInfo.ID) as Checkk, UserInfo, Department, Status, Title where userinfo.ID = Checkk.ID and UserInfo.Title = Title.TitleID and Department.DepartmentID = UserInfo.Department and Checkk.DepartmentName = Department.DepartmentName and Checkk.Status = 1 and Checkk.Status = Status.StatusID group by Checkk.DepartmentName; ";
            DataTable table = new DataTable();
            string sqlDataSource = "Data Source=LAPTOP-3KQ0AE11\\MSSQLSERVER01;Initial Catalog=GUYs;Integrated Security=True;";
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
