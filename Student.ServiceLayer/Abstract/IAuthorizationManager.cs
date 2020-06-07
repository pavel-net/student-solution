using System;
using System.Collections.Generic;
using System.Text;
using Student.ServiceLayer.Models;

namespace Student.ServiceLayer.Abstract
{
    public interface IAuthorizationManager
    {
        PersonGetResponse GetAllPeron(string username, string password);
    }
}
