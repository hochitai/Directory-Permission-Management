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
using DirectoryPermissionManagement.Commons;
using DirectoryPermissionManagement.Filters;

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PermissionService _permissionService;

        public UserController(UserService userService, PermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            var result = await _userService.CreateUser(userRequest);

            if (result == null)
            {
                return BadRequest("Username was existed!");
            }

            return Created("", result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var result = await _userService.Login(userRequest);

            if (result == null)
            {
                return new JsonResult(BadRequest("Username or password is incorrect!"));
            }

            return new JsonResult(Ok(result));
        }

        [HttpPost("share/{uId}/drive/{driveId}/role/{roleId}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareDrive([FromRoute] int driveId, [FromRoute] int roleId, [FromRoute] int uId)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, driveId, null, null, (int)RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantDrivePermission(uId, driveId, roleId);
            return Ok();
        }

        [HttpPost("share/{uId}/folder/{folderId}/role/{roleId}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareFolder([FromRoute] int folderId, [FromRoute] int roleId, [FromRoute] int uId)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, folderId, null, (int)RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantFolderPermission(uId, folderId, roleId);
            return Ok();
        }

        [HttpPost("share/{uId}/file/{fileId}/role/{roleId}")]
        [CustomAuthorize]
        public async Task<IActionResult> ShareFile([FromRoute] int fileId, [FromRoute] int roleId, [FromRoute] int uId)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, null, fileId, (int)RoleEnum.Admin))
            {
                return Forbid();
            }

            await _permissionService.GrantFilePermission(uId, fileId, roleId);
            return Ok();
        }
    }
}
