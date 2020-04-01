using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevEdu.Auth;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserStorage userStorage;
        public AccountController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            userStorage = new UserStorage(dbCon);
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(UserLoginPassInputModel currentUser)
        {
            var identity = await GetIdentity(currentUser);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.Now;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(UserLoginPassInputModel currentUser)
        {
            //ищем пользователя по логину
            var user = await userStorage.UserGetByLogin(UserPassLoginMapper.FromInputModel(currentUser));

            // сравниваем введенный пароль с хешем пароля в базе
            if (user != null && Hashing.ValidateUserPassword(currentUser.Password, user.Password))
                {
                var roles = userStorage.UserRolesSelectByUserId((int)user.Id).Result.ToList();

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    };
                    //добавляем все роли пользователя
                    foreach (Role role in roles)
                    {
                        claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
                    }
                    //формируе токен
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
                    return claimsIdentity;
                }
            }

            // если пользователя не найдено
            return null;
        }
    }
}