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
using DirectoryPermissionManagement.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
        /*
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
        public async Task<IActionResult> DeletePermission(Permission permission)
        {
            var result = await _permissionService.Delete(permission);
            if (!result)
            {
                return new JsonResult(BadRequest("Delete permission fail!"));
            }
            return new JsonResult(NoContent());

        } */

        [HttpPost("drive/{driveId}/role/{roleId}/user/{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareDrive([FromRoute] int driveId, [FromRoute] int roleId, [FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (! await _permissionService.HasPermission(userId, driveId, null, null, (int) RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantDrivePermission(id, driveId, roleId);
            return Ok();
        }

        [HttpPost("Folder/{folderId}/role/{roleId}/user/{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareFolder([FromRoute] int folderId, [FromRoute] int roleId, [FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, folderId, null, (int)RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantFolderPermission(id, folderId, roleId);
            return Ok();
        }

        [HttpPost("File/{fileId}/role/{roleId}/user/{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareFile([FromRoute] int fileId, [FromRoute] int roleId, [FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, null, fileId, (int)RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantFilePermission(id, fileId, roleId);
            return Ok();
        }

    }
}
