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
    public class EmployeeListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeListController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = "select UserInfo.ID, Title.TitleName,UserInfo.Firstname, UserInfo.Lastname, Department.DepartmentName, UserContact.Tel, UserContact.Email,Status.StatusName from UserInfo, EmployeeInfo, Department, Status, Title, UserContact where UserInfo.ID = EmployeeInfo.ID and UserInfo.ID = UserContact.ID and EmployeeInfo.Department = Department.DepartmentID and EmployeeInfo.Status = Status.StatusID and UserInfo.Title = Title.TitleID; ";
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
