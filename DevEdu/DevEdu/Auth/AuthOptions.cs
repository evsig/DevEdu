using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu
{
    public class AuthOptions
    {
        public const string ISSUER = "DevEduServer"; // издатель токена
        public const string AUDIENCE = "DevEduClient"; // потребитель токена
        const string KEY = "DSa34d12a@*@&$@sD1234";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
