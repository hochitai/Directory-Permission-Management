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
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            var result = _itemService.GetById(id);

            if (result == null)
            {
                return new JsonResult(BadRequest("Item was not existed!"));
            }

            return new JsonResult(Ok(result));
            
        }

        [HttpPost]
        public JsonResult CreateItem(Item item)
        {
            var result = _itemService.Insert(item);

            if (result == null)
            {
                return new JsonResult(BadRequest("Item name was existed, please change name!"));
            }

            return new JsonResult(Created("", result));

        }

        [HttpPut("{id}")]
        public JsonResult UpdateItem(int id, Item item)
        {
            var result = _itemService.Update(id, item);

            if (result == null)
            {
                return new JsonResult(BadRequest("Update item fail!"));
            }

            return new JsonResult(Ok(result));

        }

        [HttpDelete("{id}")]
        public JsonResult DeleteItem(int id)
        {
            var result = _itemService.Delete(id);
            if (!result)
            {
                return new JsonResult(BadRequest("Delete item fail!"));
            }
            return new JsonResult(NoContent());

        }
    }
}
