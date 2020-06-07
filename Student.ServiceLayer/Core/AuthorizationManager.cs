using System;
using System.Collections.Generic;
using System.Text;
using Student.DataLayer.Repositories;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Core
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IPersonRepository personRepository;

        public AuthorizationManager(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        public PersonGetResponse GetAllPeron(string username, string password)
        {
            var person = personRepository.FirstOrDefault(f => f.Login == username && f.Password == password);
            if (person == null)
            {
                return new PersonGetResponse { IsSuccess = false, Message = "Invalid username or password" };
            }

            var result = new PersonGetResponse
            {
                Login = person.Login,
                Password = person.Password,
                Role = person.Role,
                IsSuccess = true
            };
            return result;
        }
    }
}
