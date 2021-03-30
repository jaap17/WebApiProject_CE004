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
    public class SubjectController : ApiController
    {
       public List<Subject> GetSubjects()
       {
            List<Subject> subjects = new List<Subject>();
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from Subjects", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Subject subject = new Subject();
                subject.SubjectId = reader.GetString(0);
                subject.Name = reader.GetString(1);
                subject.Semester = reader.GetInt32(2);
                subjects.Add(subject);
            }
            conn.Close();
            return subjects;
       }

        [HttpPost]
        public HttpResponseMessage Add(Subject subject)
        { 
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand sqlCommand = new SqlCommand("Insert into Subjects values(@sid,@name,@semester)", conn);
            conn.Open();
            SqlParameter p = new SqlParameter("@sid", subject.SubjectId);
            SqlParameter u = new SqlParameter("@name", subject.Name);
            SqlParameter q = new SqlParameter("@semester", subject.Semester);
            sqlCommand.Parameters.Add(p);
            sqlCommand.Parameters.Add(u);
            sqlCommand.Parameters.Add(q);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret == 1)
            {
                Console.WriteLine("Subject inserted successfully");
                return Request.CreateResponse(HttpStatusCode.OK, subject);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Subject object not added to the database");
        }


        [HttpDelete]
        public HttpResponseMessage Delete(string SubjectId)
        {
            int ret = 0;
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand sqlCommand = new SqlCommand("Delete from Subjects where SubjectId=@id", conn);
            SqlParameter p = new SqlParameter("@id",SubjectId);
            sqlCommand.Parameters.Add(p);
            conn.Open();
            ret = sqlCommand.ExecuteNonQuery();
            
            conn.Close();
            if(ret == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Subject object not added to the database");
        }
    }
        
    
}
