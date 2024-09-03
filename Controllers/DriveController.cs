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
using Microsoft.AspNetCore.Authorization;
using DirectoryPermissionManagement.Filters;
using DirectoryPermissionManagement.Commons;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveController : ControllerBase
    {
        private readonly DriveService _driveService;
        private readonly PermissionService _permissionService;

        public DriveController(DriveService driveService, PermissionService permissionService)
        {
            _driveService = driveService;
            _permissionService = permissionService;
        }

        [HttpGet("{id}/folder")]
        [CustomAuthorize]
        public async Task<IActionResult> GetFoldersById([FromRoute] int id)
        {
            /// Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Admin) &&
                !await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _driveService.GetFoldersById(id);

            if (result == null)
            {
                return BadRequest("Drive was not existed!");
            }

            return Ok(result);   
        }

        [HttpGet("{id}/file")]
        [CustomAuthorize]
        public async Task<IActionResult> GetFilesById([FromRoute]  int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Admin) &&
                !await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _driveService.GetFilesById(id);

            if (result == null)
            {
                return BadRequest("Drive was not existed!");
            }

            return Ok(result);
        }

        [HttpGet]
        [CustomAuthorize]
        public async Task<IActionResult> GetByUserId()
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = await _driveService.GetByUserId(userId);

            if (result == null)
            {
                return BadRequest("Drive was not existed!");
            }

            return Ok(result);

        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> CreateDrive([FromBody] Drive drive)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (! await _driveService.HasNameAndUserId(drive.Name, userId))
            {
                BadRequest("Drive was existed, please change name!");
            }

            var result = await _driveService.Insert(drive, userId);

            return Created("", result);
        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateDrive([FromRoute] int id, [FromBody] Drive drive)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Admin) &&
                !await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Contributor)) 
            {
                return Forbid();
            }

            var result = await _driveService.Update(id, drive, userId);

            if (result == null)
            {
                BadRequest("Update drive fail!");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> DeleteDrive([FromRoute] int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Admin) &&
                !await _permissionService.HasPermission(userId, id, null, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            await _driveService.Delete(id);
            return NoContent();
        }
    }
}
