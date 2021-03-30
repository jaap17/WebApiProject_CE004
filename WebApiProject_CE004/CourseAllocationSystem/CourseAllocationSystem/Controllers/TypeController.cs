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
    public class TypeController : ApiController
    {
        public string getType()
        {
            string result = null;
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from Type", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                result = reader.GetString(0);
            }
            conn.Close();
            return result;
        }

        [HttpPost]
        public HttpResponseMessage storeType(SemType type)
        {
            string res = null;
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Insert into Type values(@id)", conn);
            conn.Open();
            SqlParameter p = new SqlParameter("@id",type.SemesterType);
            cmd.Parameters.Add(p);
            int ret = cmd.ExecuteNonQuery();
            if(ret == 1)
            {
                res = type.SemesterType;
                return Request.CreateResponse(HttpStatusCode.OK, type.SemesterType);
            }
            conn.Close();
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Semester type addition failed");
        }
    }
}
