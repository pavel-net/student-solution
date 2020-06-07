using System;
using System.Collections.Generic;
using System.Text;
using Student.DataLayer.Filters;

namespace Student.ServiceLayer.Models
{
    public class GroupRequest
    {
        public FilterGroup FiltredBy { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string FiltredValue { get; set; }
    }

}
