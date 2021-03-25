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
    public class UserInfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public UserInfoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //เรียกข้อมูลจากDatabaseสำหรับ api/userinfo
        [HttpGet]
        public JsonResult Get()
        {
            //query สำหรับเรียกข้อมูลจากDatabaseสำหรับ api/userinfo
            //emu479p1 :guysplatformapi/userinfo/get
            string query = "Select ID, TitleName, Firstname, Lastname, GenderName, Identification, Birthdate, Age, EducationName, Photo from UserInfo, Title, Gender, Education Where UserInfo.Title = Title.TitleID and UserInfo.Gender = Gender.GenderID and UserInfo.Education = Education.EducationID;";
            DataTable table = new DataTable();
            //เชื่อมต่อกับฐานข้อมูล
            //emu479p1 :guysplatformapi/userinfo/get
            string sqlDataSource = "Data Source=DESKTOP-2VQEA7I\\MSSQLSERVER01;Initial Catalog=GUY;Integrated Security=True;";
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
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

        //เพิ่มข้อมูลจากDatabaseสำหรับ api/userinfo
        [HttpPost]
        public JsonResult Post(Userinfo userinfo)
        {
            //query สำหรับสร้างหรือเก็บข้อมูลจากDatabaseสำหรับ api/userinfo
            //emu479p1 :guysplatformapi/userinfo/post
            string query = @"
                    insert into dbo.UserInfo values
                    ('" + userinfo.ID + @"',
                     '" + userinfo.Title + @"',
                     '" + userinfo.Firstname + @"',
                     '" + userinfo.Lastname + @"',
                     '" + userinfo.Gender + @"',
                     '" + userinfo.Identification + @"',
                     '" + userinfo.Birthdate + @"',
                     '" + userinfo.Age + @"',
                     '" + userinfo.Education + @"',
                     '" + userinfo.Photo + @"')
                       ";
            DataTable table = new DataTable();
            //เชื่อมต่อกับฐานข้อมูล
            //emu479p1 :guysplatformapi/userinfo/post
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

        //แก้ไขข้อมูลจากDatabaseสำหรับ api/userinfo
        [HttpPut]
        public JsonResult Put(Userinfo userinfo)
        {
            //query สำหรับแก้ไข้ข้อมูลจากDatabaseสำหรับ api/userinfo
            //emu479p1 :guysplatformapi/userinfo/put
            string query = @"
                    update dbo.UserInfo set
                    ID = '" + userinfo.ID + @"'
                    ,Title = '" + userinfo.Title + @"'
                    ,Firstname = '" + userinfo.Firstname + @"'
                    ,Lastname = '" + userinfo.Lastname + @"'
                    ,Gender = '" + userinfo.Gender + @"'
                    ,Identification = '" + userinfo.Identification + @"'
                    ,Birthdate = '" + userinfo.Birthdate+ @"'
                    ,Age = '" + userinfo.Age + @"'
                    ,Education = '" + userinfo.Education + @"'
                    ,Photo = '" + userinfo.Photo + @"'
                    where ID = " + userinfo.ID + @"
                    ";
            DataTable table = new DataTable();
            //เชื่อมต่อกับฐานข้อมูล
            //emu479p1 :guysplatformapi/userinfo/put
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

        //ลบข้อมูลจากDatabaseสำหรับ api/userinfo
        [HttpDelete("{id}")]
        public JsonResult Delete(int id) 
        {
            //query สำหรับลบข้อมูลจากDatabaseสำหรับ api/userinfo
            //emu479p1 :guysplatformapi/userinfo/put
            string query = @"
                    delete from dbo.UserInfo
                    where ID = " +id + @"
                    ";
            DataTable table = new DataTable();
            //เชื่อมต่อกับฐานข้อมูล
            //emu479p1 :guysplatformapi/userinfo/delete
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

        //apiสำหรับเก็บรูป api/userinfo
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            //apiสำหรับเก็บรูป api/userinfo
            //emu479p1 :guysplatformapi/userinfo/savefile
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
