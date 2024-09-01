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

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        private readonly PermissionService _permissionService;

        public ItemController(ItemService itemService, PermissionService permissionService)
        {
            _itemService = itemService;
            _permissionService = permissionService;
        }
        
        [HttpGet("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Contributor) ||
                !await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Reader))
            {
                return Forbid();
            }

            var result = await _itemService.GetById(id);

            if (result == null)
            {
                return BadRequest("Item was not existed!");
            }

            return Ok(result);     
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, item.DriveId, null, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, item.DriveId, null, null, (int)RoleEnum.Contributor) ||
                !await _permissionService.HasPermission(userId, null, item.FolderId, null, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, item.FolderId, null, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _itemService.Insert(item);

            if (result == null)
            {
                return BadRequest("Item name was existed, please change name!");
            }

            return Created("", result);

        }

        [HttpPut("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] Item item)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }
            var result = await _itemService.Update(id, item);

            if (result == null)
            {
                return BadRequest("Update item fail!");
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var userId = (int)HttpContext.Items["userId"];

            if (!await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Admin) ||
                !await _permissionService.HasPermission(userId, null, null, id, (int)RoleEnum.Contributor))
            {
                return Forbid();
            }

            var result = await _itemService.Delete(id);
            if (!result)
            {
                return BadRequest("Delete item fail!");
            }
            return NoContent();

        }
    }
}
