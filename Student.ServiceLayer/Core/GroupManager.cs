using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.DataLayer;
using Student.DataLayer.Data;
using Student.DataLayer.Filters;
using Student.DataLayer.Repositories;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Core
{
    public class GroupManager : IGroupManager
    {
        private readonly IGroupRepository groupRepository;
        private readonly IStudentRepository studentRepository;

        public GroupManager(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            this.groupRepository = groupRepository;
            this.studentRepository = studentRepository;
        }

        public Task<List<GroupGetResponse>> GetAllGroupsAsync(GroupRequest request)
        {
            var groups = groupRepository.GetQuery();
            if (request.FiltredBy == FilterGroup.GroupName && request.FiltredValue != null)
                groups = groups.Where(g => g.Name.StartsWith(request.FiltredValue));

            var result = groups.Select(x => new GroupGetResponse
            {
                Id = x.Id,
                GroupName = x.Name,
                StudentsCount = x.StudentGroups.Count()
            });

            if (request.Skip == 0 & request.Take == 0)
            {
                return result.ToListAsync();
            }

            return result.Skip(request.Skip).Take(request.Take).ToListAsync();
        }

        public GroupGetResponse GetGroup(int id)
        {
            var group = groupRepository.Get(id);
            if (group == null)
                return null;
            return new GroupGetResponse()
            {
                Id = group.Id,
                GroupName = group.Name,
                StudentsCount = group.StudentGroups.Count
            };
        }

        public ResponseResult CreateGroup(string name)
        {
            try
            {
                groupRepository.Create(new Group() {Name = name});
                groupRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception e)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult UpdateGroup(GroupUpdateRequest group)
        {
            try
            {
                if (!groupRepository.Exists(group.Id))
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };
                var gr = groupRepository.Get(group.Id);
                gr.Name = group.Name;
                groupRepository.Update(gr);
                groupRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult DeleteGroup(int id)
        {
            try
            {
                if (!groupRepository.Exists(id))
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };
                var gr = groupRepository.Get(id);
                groupRepository.Delete(gr);
                groupRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult AddStudentToGroup(int studentId, int groupId)
        {
            try
            {
                if (!studentRepository.Exists(studentId))
                    return new ResponseResult {IsSuccess = false, Message = "Student not found"};
                if (!groupRepository.Exists(groupId))
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };

                var group = groupRepository.Get(groupId);
                if (group.StudentGroups.Any(x => x.IdStudent == studentId))
                    return new ResponseResult { IsSuccess = false, Message = "Student already is in group" };

                groupRepository.AddStudentToGroup(studentId, groupId);
                groupRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult DeleteStudentFromGroup(int studentId, int groupId)
        {
            try
            {
                if (!studentRepository.Exists(studentId))
                    return new ResponseResult { IsSuccess = false, Message = "Student not found" };
                if (!groupRepository.Exists(groupId))
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };

                groupRepository.DeleteStudentFromGroup(studentId, groupId);
                groupRepository.Save();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
    }
}
