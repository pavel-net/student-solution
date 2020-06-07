using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Student.ServiceLayer.Models
{
    public class StudentUpdateRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string MiddleName { get; set; }

        [MinLength(6), MaxLength(16)]
        public string Nickname { get; set; }
    }
}
