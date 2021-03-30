using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseAllocationSystem.Models
{
    public class FacultyChoice
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Priority1 { get; set; }

        [Required]
        public string Priority2 { get; set; }

        [Required]
        public string Priority3 { get; set; }

        [Required]
        public string Priority4 { get; set; }

        [Required]
        public string Priority5 { get; set; }
    }
}