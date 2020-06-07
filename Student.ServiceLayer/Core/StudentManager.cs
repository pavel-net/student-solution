using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.DataLayer.Data;
using Student.DataLayer.Repositories;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Core
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepository studentRepository;

        public StudentManager(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public Task<List<StudentGetResponse>> GetAllStudents(StudentRequest request)
        {
            var students = studentRepository.GetQuery();

            // фильтрация
            if (request.FiltredSettings != null)
            {
                var filters = request.FiltredSettings;
                if (!string.IsNullOrEmpty(filters.Surname))
                    students = students.Where(s => s.Surname.StartsWith(filters.Surname));
                if (!string.IsNullOrEmpty(filters.Name))
                    students = students.Where(s => s.Name.StartsWith(filters.Name));
                if (!string.IsNullOrEmpty(filters.MiddleName))
                    students = students.Where(s => s.MiddleName.StartsWith(filters.MiddleName));
                if (!string.IsNullOrEmpty(filters.Nickname))
                    students = students.Where(s => s.Nickname.StartsWith(filters.Nickname));
                if (!string.IsNullOrEmpty(filters.Gender))
                    students = students.Where(s => s.Gender == filters.Gender);
                if (!string.IsNullOrEmpty(filters.GroupName))
                    students = students.Where(s =>
                        s.StudentGroups.Any(sg => sg.Group.Name.StartsWith(filters.GroupName)));
            }

            // сортировка
            students = request.SortStudentState
            switch
            {
                SortStudentState.NicknameAsc => students.OrderBy(s => s.Nickname),
                SortStudentState.NicknameDesc => students.OrderByDescending(s => s.Nickname),
                SortStudentState.SurnameAsc => students.OrderBy(s => s.Surname),
                SortStudentState.SurnameDesc => students.OrderByDescending(s => s.Surname),
                _ => students.OrderBy(s => s.Surname)
            };

            var result = students.Select(s => new StudentGetResponse
            {
                Id = s.Id,
                Gender = s.Gender,
                Name = s.Name,
                Surname = s.Surname,
                MiddleName = s.MiddleName,
                Nickname = s.Nickname,
                GroupNames = string.Join(",", s.StudentGroups.Select(x => x.Group.Name))
            });

            if (request.Skip == 0 & request.Take == 0)
            {
                return result.ToListAsync();
            }
            return result.Skip(request.Skip).Take(request.Take).ToListAsync();
        }

        public StudentGetResponse GetStudent(int id)
        {
            var student = studentRepository.Get(id);
            if (student == null)
                return null;
            return new StudentGetResponse()
            {
                Id = student.Id,
                Gender = student.Gender,
                Name = student.Name,
                Surname = student.Surname,
                MiddleName = student.MiddleName,
                Nickname = student.Nickname,
                GroupNames = string.Join(",", student.StudentGroups.Select(x => x.Group.Name))
            };
        }

        public ResponseResult CreateStudent(StudentCreateRequest student)
        {
            try
            {
                studentRepository.Create(new DataLayer.Data.Student()
                {
                    Gender = student.Gender,
                    Name = student.Name,
                    Surname = student.Surname,
                    MiddleName = student.MiddleName,
                    Nickname = student.Nickname
                });
                studentRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ResponseResult UpdateStudent(StudentUpdateRequest student)
        {
            try
            {
                if (!studentRepository.Exists(student.Id))
                    return new ResponseResult { IsSuccess = false, Message = "Student not found" };
                var stud = studentRepository.Get(student.Id);
                stud.Gender = student.Gender;
                stud.Name = student.Name;
                stud.Surname = student.Surname;
                stud.MiddleName = student.MiddleName;
                stud.Nickname = student.Nickname;
                studentRepository.Update(stud);
                studentRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception e)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult DeleteStudent(int id)
        {
            try
            {
                if (!studentRepository.Exists(id))
                    return new ResponseResult { IsSuccess = false, Message = "Student not found" };
                var stud = studentRepository.Get(id);
                studentRepository.Delete(stud);
                studentRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception e)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

    }
}
