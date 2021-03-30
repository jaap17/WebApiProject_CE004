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
    public class AllocationController : ApiController
    {
        public List<Allocation> GetAllocations()
        {
            List<Allocation> allocations = new List<Allocation>();
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseAllocation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Select * from AllocatedCourses", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Allocation allocation = new Allocation();
                allocation.Subject1 = reader.GetString(0);
                allocation.Name = reader.GetString(1);
                allocation.Subject2 = reader.GetString(2);
                allocations.Add(allocation);
            }
            conn.Close();
            return allocations;
        }
    }
}
