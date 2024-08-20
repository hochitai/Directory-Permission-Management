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
using Microsoft.AspNetCore.Http.HttpResults;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            var result = _roleService.GetById(id);

            if (result == null)
            {
                return new JsonResult(BadRequest("Role was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpPost]
        public JsonResult CreateRole(Role role)
        {
            var result = _roleService.Insert(role);

            if (result == null)
            {
                return new JsonResult(BadRequest("Role name was existed, please change name!"));
            }

            return new JsonResult(Created("", result));

        }

        [HttpPut("{id}")]
        public JsonResult UpdateRole(int id, Role role)
        {
            var result = _roleService.Update(id, role);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update role fail!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpDelete("{id}")]
        public JsonResult DeleteRole(int id)
        {
            var result = _roleService.Delete(id);
            if (!result)
            {
                return new JsonResult(BadRequest("Delete role fail!"));
            }
            return new JsonResult(NoContent());

        }
    }
}
