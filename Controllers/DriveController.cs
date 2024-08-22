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

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveController : ControllerBase
    {
        private readonly DriveService _driveService;

        public DriveController(DriveService driveService)
        {
            _driveService = driveService;
        }

        [HttpGet("{id}/folder")]
        [CustomAuthorize]
        public async Task<IActionResult> GetFoldersById([FromRoute] int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = await _driveService.GetFoldersById(id, userId);

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

            var result = await _driveService.GetFilesById(id, userId);

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

            var result = await _driveService.Insert(drive, userId);

            if (result == null)
            {
                BadRequest("Drive was existed, please change name!");
            }

            return Created("", result);
        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateDrive([FromRoute] int id, [FromBody] Drive drive)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

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

            await _driveService.Delete(id, userId);
            return NoContent();
        }
    }
}
