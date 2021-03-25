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
    public class JoiningController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JoiningController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select JoinID, JoinName from Joining; ";
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
        public JsonResult Post(Joining jnn)
        {
            string query = @"
                    insert into dbo.Joining values
                    ('" + jnn.JoinID + @"',
                     '" + jnn.JoinName + @"')
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
        public JsonResult Put(Joining jnn)
        {
            string query = @"
                    update dbo.Joining set
                    JoinID = '" + jnn.JoinID + @"'
                    ,JoinName = '" + jnn.JoinName + @"'
                    where JoinID = " + jnn.JoinID + @"
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
                    delete from dbo.Joining
                    where JoinID = " + id + @"
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
