using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Models
{
    public class Subject
    {
        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Semester { get; set; }
    }
}
