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
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;

        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            var result = _folderService.GetById(id);

            if (result == null)
            {
                return new JsonResult(BadRequest("Folder was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpPost]
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
