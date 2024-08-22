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
    public class ShareController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public ShareController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("{userId}")]
        public JsonResult GetByUserId(int userId)
        {
            var result = _permissionService.GetByUserId(userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("List permission was empty!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpPost]
        public JsonResult CreatePermission(Permission permission)
        {
            var result = _permissionService.Insert(permission);

            if (result == null)
            {
                return new JsonResult(BadRequest("Permission was existed, please change name!"));
            }

            return new JsonResult(Created("", result));

        }

        [HttpPut]
        public JsonResult UpdatePermission(Permission permission)
        {
            var result = _permissionService.Update(permission);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update permission fail!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpDelete]
        public JsonResult DeletePermission(Permission permission)
        {
            var result = _permissionService.Delete(permission);
            if (!result)
            {
                return new JsonResult(BadRequest("Delete permission fail!"));
            }
            return new JsonResult(NoContent());

        }
    }
}
