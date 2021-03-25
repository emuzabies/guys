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
    public class DashboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public DashboardController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select Userinfo.ID, Title.TitleName, Userinfo.Firstname, Userinfo.Lastname, Department.DepartmentName, convert(varchar(10), UserCheck.Date, 103), UserCheck.Time, Userinfo.Photo from UserInfo, Department, Title, UserCheck where Department.DepartmentID = UserInfo.Department and Title.TitleID = UserInfo.Title and UserInfo.ID = UserCheck.ID order by date, Time";
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
