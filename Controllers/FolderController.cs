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
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;

        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet("{id}/subfolder")]
        [CustomAuthorize]
        public async Task<IActionResult> GetSubFoldersById(int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = await _folderService.GetSubFoldersById(id, userId);

            if (result == null)
            {
                return BadRequest("Folder was not existed!");
            }

            return Ok(result);  
        }

        [HttpGet("{id}/file")]
        [CustomAuthorize]
        public async Task<IActionResult> GetFilesById(int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = await _folderService.GetFilesById(id, userId);

            if (result == null)
            {
                return BadRequest("Folder was not existed!");
            }

            return Ok(result);
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> CreateFolder(Folder folder)
        {
            var result = await _folderService.Insert(folder);

            if (result == null)
            {
                return BadRequest("Folder name was existed, please change name!");
            }

            return Created("", result);

        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateFolder(int id, Folder folder)
        {
            var result = await _folderService.Update(id, folder);

            if (result == null)
            {
                return BadRequest("Update folder fail!");
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var result = await _folderService.Delete(id);
            if (!result)
            {
                return BadRequest("Delete folder fail!");
            }
            return NoContent();

        }
    }
}
