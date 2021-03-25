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

namespace GUY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select WorkID, Topic, ResponsibleID, CreatedID, ParticipantID, DueDate, Instruction, TaskStatusName from Task, TaskStatus where Task.Status = TaskStatus.TaskStatusID; ";
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
        public JsonResult Post(Tasks tk)
        {
            string query = @"
                    insert into dbo.Task values
                    ('" + tk.WorkID + @"',
                     '" + tk.Topic + @"',
                     '" + tk.ResponsibleID + @"',
                     '" + tk.CreatedID + @"',
                     '" + tk.ParticipantID + @"',
                     '" + tk.DueDate + @"',
                     '" + tk.Instruction + @"',
                     '" + tk.Status + @"')
                       ";
            DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("GUYAppCon");
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
        public JsonResult Put(Tasks tk)
        {
            string query = @"
                    update dbo.Task set
                    WorkID = '" + tk.WorkID + @"'
                    ,Topic = '" + tk.Topic + @"'
                    ,ResponsibleID = '" + tk.ResponsibleID + @"'
                    ,CreatedID = '" + tk.CreatedID + @"'
                    ,ParticipantID = '" + tk.ParticipantID + @"'
                    ,DueDate = '" + tk.DueDate + @"'
                    ,Instruction = '" + tk.Instruction + @"'
                    ,Status = '" + tk.Status + @"'
                    where WorkID = " + tk.WorkID + @"
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
                    delete from dbo.Task
                    where WorkID = " + id + @"
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
