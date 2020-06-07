using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupManager _manager;
        public GroupController(IGroupManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get list of groups
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGroups([FromQuery] GroupRequest request)
        {
            var result = await _manager.GetAllGroupsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get group
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            var result = _manager.GetGroup(id);
            return Ok(result);
        }

        /// <summary>
        /// Create group
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateGroup([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Group name IsNullOrEmpty");
            }
            var result = _manager.CreateGroup(name);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Edit group
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateGroup([FromBody] GroupUpdateRequest group)
        {
            var result = _manager.UpdateGroup(group);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Delete group
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGroup([FromRoute] int id)
        {
            var result = _manager.DeleteGroup(id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Add student to group
        /// </summary>
        [HttpPost("{id}/students/{studentId}")]
        [Authorize(Roles = "admin")]
        public IActionResult AddStudentToGroup([FromRoute] int studentId, int id)
        {
            var result = _manager.AddStudentToGroup(studentId, id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Delete student from group
        /// </summary>
        [HttpDelete("{id}/students/{studentId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteStudentFromGroup([FromRoute] int studentId, int id)
        {
            var result = _manager.DeleteStudentFromGroup(studentId, id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
