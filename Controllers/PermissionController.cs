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
using DirectoryPermissionManagement.Filters;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("{userId}")]
        [CustomAuthorize]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _permissionService.GetByUserId(userId);

            if (result == null)
            {
                return BadRequest("List permission was empty!");
            }

            return Ok(result);
            
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> CreatePermission([FromBody] Permission permission)
        {
            var result = await _permissionService.Insert(permission);

            if (result == null)
            {
                return BadRequest("Create fail!");
            }

            return Created("", result);

        }

        [HttpPut]
        [CustomAuthorize]
        public async Task<IActionResult> UpdatePermission(Permission permission)
        {
            var result = await _permissionService.Update(permission);

            if (result == null)
            {
                return BadRequest("Update permission fail!");
            }

            return Ok(result);

        }

        [HttpDelete]
        [CustomAuthorize]
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
