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
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager _manager;

        public StudentController(IStudentManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get list of students
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] StudentRequest request)
        {
            var result = await _manager.GetAllStudents(request);
            return Ok(result);
        }

        /// <summary>
        /// Get student
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var result = _manager.GetStudent(id);
            return Ok(result);
        }

        /// <summary>
        /// Create student
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateStudent([FromBody] StudentCreateRequest student)
        {
            var result = _manager.CreateStudent(student);
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
        /// Edit student
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateStudent([FromBody] StudentUpdateRequest student)
        {
            var result = _manager.UpdateStudent(student);
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
        /// Delete student
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _manager.DeleteStudent(id);
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
