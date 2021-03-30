using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Models
{
    public class Allocation
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject1 { get; set; }

        [Required]
        public string Subject2 { get; set; }
    }
}
