using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CourseAllocationSystem.Models;

namespace CourseAllocationSystem.Controllers
{
    public class FacultyController : ApiController
    {
        public List<Faculty> GetFaculties()
        {
            List<Faculty> faculties = new List<Faculty>();
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from Faculties",conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Faculty faculty = new Faculty();
                faculty.Username = reader.GetString(0);
                faculty.Password = reader.GetString(1);
                faculty.FirstName = reader.GetString(2);
                faculty.LastName = reader.GetString(3);
                faculty.Experience = reader.GetInt32(4);
                faculties.Add(faculty);
            }
            conn.Close();
            return faculties;
        }

        [HttpPost]
        public HttpResponseMessage Add(Faculty faculty)
        {
            
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand sqlCommand = new SqlCommand("Insert into Faculties values(@uname,@pass,@fname,@lname,@experience)", conn);
            conn.Open();
            SqlParameter p = new SqlParameter("@uname", faculty.Username);
            SqlParameter u = new SqlParameter("@pass", faculty.Password);
            SqlParameter q = new SqlParameter("@fname", faculty.FirstName);
            SqlParameter s = new SqlParameter("@lname", faculty.LastName);
            SqlParameter r = new SqlParameter("@experience", faculty.Experience);
            
            sqlCommand.Parameters.Add(p);
            sqlCommand.Parameters.Add(u);
            sqlCommand.Parameters.Add(q);
            sqlCommand.Parameters.Add(s);
            sqlCommand.Parameters.Add(r);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret == 1)
            {
                Console.WriteLine("Faculty inserted successfully");
                return Request.CreateResponse(HttpStatusCode.OK, faculty);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The faculty object not added to the database");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string username)
        {
            int ret = 0;
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand sqlCommand = new SqlCommand("Delete from Faculties where username=@id", conn);
            SqlParameter p = new SqlParameter("@id", username);
            sqlCommand.Parameters.Add(p);
            conn.Open();
            ret = sqlCommand.ExecuteNonQuery();

            conn.Close();
            if (ret == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Faculty object not removed from the database");
        }

    }
}
