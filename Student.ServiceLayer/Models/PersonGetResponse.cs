using System;
using System.Collections.Generic;
using System.Text;

namespace Student.ServiceLayer.Models
{
    public class PersonGetResponse
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
