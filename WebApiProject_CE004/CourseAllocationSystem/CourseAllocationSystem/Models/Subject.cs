using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseAllocationSystem.Models
{
    public class Subject
    {
       
        public string SubjectId { get; set; }

       
        public string Name { get; set; }

        
        public int Semester { get; set; }
    }
}