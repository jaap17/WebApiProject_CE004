using CourseAllocationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CourseAllocationSystem.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public Faculty GetFaculty(Login login)
        {
            Faculty faculty = null;
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from Faculties where username=@uname and password=@pass", conn);
            SqlParameter p = new SqlParameter("@uname", login.Username);
            SqlParameter q = new SqlParameter("@pass", login.Password);
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(q);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            
            
            while (reader.Read())
            {
                faculty = new Faculty();
                faculty.Username = reader.GetString(0);
                faculty.Password = reader.GetString(1);
                faculty.FirstName = reader.GetString(2);
                faculty.LastName = reader.GetString(3);
                faculty.Experience = reader.GetInt32(4);
            }
            conn.Close();
            return faculty;
        }
    }
}
