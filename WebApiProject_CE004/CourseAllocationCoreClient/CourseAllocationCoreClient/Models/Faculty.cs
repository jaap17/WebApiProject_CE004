using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Models
{
    public class Faculty
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public int? Experience { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
