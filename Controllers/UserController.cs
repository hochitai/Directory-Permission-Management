using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Data;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }


        [HttpPost]
        public JsonResult Register()
        {
            return new JsonResult("Login");
        }

        [HttpPost]
        public JsonResult Login()
        {
            return new JsonResult("Login");
        }
    }
}
