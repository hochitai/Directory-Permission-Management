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

namespace DirectoryPermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _itemService.GetById(id);

            if (result == null)
            {
                return BadRequest("Item was not existed!");
            }

            return Ok(result);
            
        }

        [HttpGet("{id}/file")]
        [CustomAuthorize]
        public async Task<IActionResult> GetFilesById([FromRoute] int id)
        {
            // Get user id
            var userId = (int)HttpContext.Items["userId"];

            var result = await _itemService.GetFilesById(id, userId);

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
            var result = await _itemService.Delete(id);
            if (!result)
            {
                return BadRequest("Delete item fail!");
            }
            return NoContent();

        }
    }
}
