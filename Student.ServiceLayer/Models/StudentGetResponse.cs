using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Student.ServiceLayer.Models
{
    public class StudentGetResponse
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string GroupNames { get; set; }
    }
}
