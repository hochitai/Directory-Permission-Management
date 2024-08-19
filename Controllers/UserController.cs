using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Text;
using DirectoryPermissionManagement.Helpers;
using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Services;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public JsonResult Register(UserRequest userRequest)
        {
            var result = _userService.AddUser(userRequest);

            if (result == null)
            {
                return new JsonResult(BadRequest("Username was existed!"));
            }

            return new JsonResult(Created("", result));
            
        }

        [HttpPost]
        public JsonResult Login(UserRequest userRequest)
        {

            var result = _userService.Login(userRequest);

            if (result == null)
            {
                return new JsonResult(BadRequest("Username or password is incorrect!"));
            }

            return new JsonResult(Ok(result));
        }
    }
}
