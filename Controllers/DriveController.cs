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

        [HttpGet("{id}")]
        [CustomAuthorize]
        public JsonResult GetById(int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = _driveService.GetById(id);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpGet("user")]
        [CustomAuthorize]
        public JsonResult GetByUserId()
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = _driveService.GetByUserId(userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was not existed!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult CreateDrive(Drive drive)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = _driveService.Insert(drive, userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was existed, please change name!"));
            }

            return new JsonResult(Created("", result));
        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public JsonResult UpdateDrive(int id, Drive drive)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = _driveService.Update(id, drive, userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update drive fail!"));
            }

            return new JsonResult(Ok(result));
        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public JsonResult DeleteDrive(int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            _driveService.Delete(id, userId);
            return new JsonResult(NoContent());

        }
    }
}
