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
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;
        private readonly PermissionService _permissionService;

        public FolderController(FolderService folderService, PermissionService permissionService)
        {
            _folderService = folderService;
            _permissionService = permissionService;
        }

        [HttpGet("{folderId}/subfolder")]
        [CustomAuthorize]
        public async Task<IActionResult> GetSubFoldersById([FromRoute] int folderId)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, folderId, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, folderId, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _folderService.GetSubFoldersById(folderId, userId);

            if (result == null)
            {
                return BadRequest("Folder was not existed!");
            }

            return Ok(result);  
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> CreateFolder([FromBody] Folder folder)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, folder.DriveId, null, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, folder.DriveId, null, null, (int)RoleEnum.Contributor) ||
                !await _permissionService.HasPermission(userId, null, folder.ParrentFolderId, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, folder.ParrentFolderId, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _folderService.Insert(folder);

            if (result == null)
            {
                return BadRequest("Folder name was existed, please change name!");
            }

            return Created("", result);

        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateFolder([FromRoute] int id, [FromBody] Folder folder)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, id, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, id, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _folderService.Update(id, folder);

            if (result == null)
            {
                return BadRequest("Update folder fail!");
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> DeleteFolder([FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, id, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, id, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _folderService.Delete(id);
            if (!result)
            {
                return BadRequest("Delete folder fail!");
            }
            return NoContent();

        }
    }
}
