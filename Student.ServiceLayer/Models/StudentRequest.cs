using System;
using System.Collections.Generic;
using System.Text;
using Student.DataLayer.Filters;
using SortStudentState = Student.DataLayer.Data.SortStudentState;

namespace Student.ServiceLayer.Models
{
    public class StudentRequest
    {
        public FilterStudent FiltredSettings { get; set; }
        public SortStudentState SortStudentState { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
