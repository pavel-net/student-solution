using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace StudentApi.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "byPahychCorporate";   // издатель токена
        public const string AUDIENCE = "StudentTest";       // потребитель токена
        const string KEY = "supersecret_secretkey!333";     // ключ для шифрования
        public const int LIFETIME = 1;                      // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
