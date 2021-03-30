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
    public class OddSubjectController : ApiController
    {
        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from Subjects where Semester=3 or Semester=5 or Semester=7", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
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
    }
}
