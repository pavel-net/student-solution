using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Student.ServiceLayer.Models
{
    public class GroupGetResponse
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int StudentsCount { get; set; }
    }
}
