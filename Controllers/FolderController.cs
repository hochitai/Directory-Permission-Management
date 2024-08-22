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

        [HttpGet("{id}")]
        [CustomAuthorize]
        public JsonResult GetByIdOfUser(int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = _folderService.GetById(id, userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("Folder was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult CreateFolder(Folder folder)
        {
            var result = _folderService.Insert(folder);

            if (result == null)
            {
                return new JsonResult(BadRequest("Folder name was existed, please change name!"));
            }

            return new JsonResult(Created("", result));

        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public JsonResult UpdateFolder(int id, Folder folder)
        {
            var result = _folderService.Update(id, folder);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update folder fail!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public JsonResult DeleteFolder(int id)
        {
            var result = _folderService.Delete(id);
            if (!result)
            {
                return new JsonResult(BadRequest("Delete folder fail!"));
            }
            return new JsonResult(NoContent());

        }
    }
}
