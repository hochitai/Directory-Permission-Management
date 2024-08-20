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
    public class DriveController : ControllerBase
    {
        private readonly DriveService _driveService;

        public DriveController(DriveService driveService)
        {
            _driveService = driveService;
        }

        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            var result = _driveService.GetById(id);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpGet("user/{userId}")]
        public JsonResult GetByUserId(int userId)
        {
            var result = _driveService.GetByUserId(userId);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was not existed!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpPost]
        public JsonResult CreateDrive(Drive drive)
        {
            var result = _driveService.Insert(drive);

            if (result == null)
            {
                return new JsonResult(BadRequest("Drive was existed, please change name!"));
            }

            return new JsonResult(Created("", result));

        }

        [HttpPut("{id}")]
        public JsonResult UpdateDrive(int id, Drive drive)
        {
            var result = _driveService.Update(id, drive);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update drive fail!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpDelete("{id}")]
        public JsonResult DeleteDrive(int id)
        {
            _driveService.Delete(id);
            return new JsonResult(NoContent());

        }
    }
}
