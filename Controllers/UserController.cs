using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Data;
using DirectoryPermissionManagement.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Text;
using DirectoryPermissionManagement.Helpers;
using DirectoryPermissionManagement.Configs;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly JWTConfig _jwtConfig;

        public UserController(ApplicationContext context, JWTConfig jwtConfig)
        {
            _context = context;
            _jwtConfig = jwtConfig;
        }

        [HttpPost]
        public JsonResult Register(UserRequest userRequest)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Username == userRequest.Username);

            if (userInDb != null)
            {
                return new JsonResult(BadRequest("Username was existed!"));
            }

            StringHelper stringHelper = new StringHelper();
            byte[] salt = stringHelper.CreateSalt();

            User user = new User();
            user.Username = userRequest.Username;
            user.Password = stringHelper.HashPassword(userRequest.Password, salt);
            user.Name = userRequest.Name;
            user.IsActived = true;
            user.Salt = Convert.ToBase64String(salt);

            _context.Users.Add(user);

            _context.SaveChanges();

            return new JsonResult(Created("", new UserResponse(user.Id, user.Name, "")));
        }

        [HttpPost]
        public JsonResult Login(UserRequest userRequest)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Username == userRequest.Username);

            if (userInDb == null)
            {
                return new JsonResult(BadRequest("Username or password is incorrect!"));
            }

            StringHelper stringHelper = new StringHelper();
            string hashedPassword = stringHelper.HashPassword(userRequest.Password, Convert.FromBase64String(userInDb.Salt));

            if (hashedPassword != userInDb.Password)
            {
                return new JsonResult(BadRequest("Username or password is incorrect!"));
            }

            var response = new UserResponse(userInDb.Id, userInDb.Name, "");

            TokenHelper tokenHelper = new TokenHelper();
            var token = tokenHelper.GenerateToken(response, _jwtConfig);

            response.Token = token;

            return new JsonResult(Ok(response));
        }
    }
}
