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
    public class FacultyChoiceController : ApiController
    {
        // GET: api/FacultyChoice
        public List<FacultyChoice> Get()
        {
            List<FacultyChoice> facultyChoices = new List<FacultyChoice>();
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from FacultyChoice", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FacultyChoice fchoice = new FacultyChoice();
                fchoice.Name = reader.GetString(0);
                fchoice.Priority1 = reader.GetString(1);
                fchoice.Priority2 = reader.GetString(2);
                fchoice.Priority3 = reader.GetString(3);
                fchoice.Priority4 = reader.GetString(4);
                fchoice.Priority5 = reader.GetString(5);
                facultyChoices.Add(fchoice);
            }
            conn.Close();
            return facultyChoices;
            
        }

        [HttpPost]
        public HttpResponseMessage Add(FacultyChoice facultyChoice)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand sqlCommand = new SqlCommand("Insert into FacultyChoice values(@name,@priority1,@priority2,@priority3,@priority4,@priority5)", conn);
            conn.Open();
            SqlParameter p = new SqlParameter("@name", facultyChoice.Name);
            SqlParameter u = new SqlParameter("@priority1", facultyChoice.Priority1);
            SqlParameter q = new SqlParameter("@priority2", facultyChoice.Priority2);
            SqlParameter s = new SqlParameter("@priority3", facultyChoice.Priority3);
            SqlParameter r = new SqlParameter("@priority4", facultyChoice.Priority4);
            SqlParameter t = new SqlParameter("@priority5", facultyChoice.Priority5);

            sqlCommand.Parameters.Add(p);
            sqlCommand.Parameters.Add(u);
            sqlCommand.Parameters.Add(q);
            sqlCommand.Parameters.Add(s);
            sqlCommand.Parameters.Add(r);
            sqlCommand.Parameters.Add(t);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret == 1)
            {
                Console.WriteLine("Faculty Choice inserted successfully");
                return Request.CreateResponse(HttpStatusCode.OK, facultyChoice);
            }
            conn.Close();
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The faculty object not added to the database");
        }

    }
}
