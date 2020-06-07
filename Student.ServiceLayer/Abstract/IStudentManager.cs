using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Abstract
{
    public interface IStudentManager
    {
        Task<List<StudentGetResponse>> GetAllStudents(StudentRequest request);
        ResponseResult CreateStudent(StudentCreateRequest student);
        ResponseResult UpdateStudent(StudentUpdateRequest student);
        ResponseResult DeleteStudent(int id);
        StudentGetResponse GetStudent(int id);
    }
}
