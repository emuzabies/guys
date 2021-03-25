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
    public class EmployeeInfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeInfoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select Firstname, Lastname, JobName, DepartmentName, [Work Place_Name], Compensation, BankAccount, HiredDate, VacationLeft, SickLeft, StatusName from UserInfo, EmployeeInfo, JobTitle, Department, WorkPlace, Status where UserInfo.ID = EmployeeInfo.ID and EmployeeInfo.JobTitle = JobTitle.JobID and EmployeeInfo.Department = Department.DepartmentID and EmployeeInfo.WorkPlace = WorkPlace.[Work PlaceID] and EmployeeInfo.Status = Status.StatusID; ";
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

        [HttpPost]
        public JsonResult Post(Employeeinfo empinfo)
        {
            string query = @"
                    insert into dbo.EmployeeInfo values
                    ('" + empinfo.ID + @"',
                     '" + empinfo.JobTitle + @"',
                     '" + empinfo.Department + @"',
                     '" + empinfo.WorkPlace + @"',
                     '" + empinfo.Compensation + @"',
                     '" + empinfo.BankAccount + @"',
                     '" + empinfo.HiredDate + @"',
                     '" + empinfo.VacationLeft + @"',
                     '" + empinfo.SickLeft + @"',
                     '" + empinfo.Status + @"',
                     '" + empinfo.SupervisorID + @"',)
                       ";
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
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employeeinfo empinfo)
        {
            string query = @"
                    update dbo.EmployeeInfo set
                    ID = '" + empinfo.ID + @"'
                    ,JobTitle = '" + empinfo.JobTitle + @"'
                    ,Department = '" + empinfo.Department + @"'
                    ,WorkPlace = '" + empinfo.WorkPlace + @"'
                    ,Compensation = '" + empinfo.Compensation + @"'
                    ,BankAccount = '" + empinfo.BankAccount + @"'
                    ,HireDate = '" + empinfo.HiredDate + @"'
                    ,VacationLeft = '" + empinfo.VacationLeft + @"'
                    ,SickLest = '" + empinfo.SickLeft + @"'
                    ,Status = '" + empinfo.Status + @"'
                    ,SupervisorID = '" + empinfo.SupervisorID + @"'
                    where ID = " + empinfo.ID + @"
                    ";
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
            return new JsonResult("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.EmployeeInfo
                    where ID = " + id + @"
                    ";
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
            return new JsonResult("Deleted Successfully");
        }

        
    }
}
