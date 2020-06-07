using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Abstract
{
    public interface IGroupManager
    {
        Task<List<GroupGetResponse>> GetAllGroupsAsync(GroupRequest request);
        GroupGetResponse GetGroup(int id);
        ResponseResult CreateGroup(string name);
        ResponseResult UpdateGroup(GroupUpdateRequest group);
        ResponseResult DeleteGroup(int id);
        ResponseResult AddStudentToGroup(int studentId, int groupId);
        ResponseResult DeleteStudentFromGroup(int studentId, int groupId);
    }
}
