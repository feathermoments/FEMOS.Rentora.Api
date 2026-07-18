using FEMOS.Rentora.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("getUserMenu/{propertyId}")]
        public async Task<IActionResult> GetUserMenu(long propertyId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var userMenus = await _menuService.GetUserMenuAsync(userPublicId, propertyId);
            return Ok(userMenus);
        }

        [HttpGet("getUserMenuPermissions/{propertyId}")]
        public async Task<IActionResult> GetUserMenuPermissions(long propertyId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var userMenuPermissions = await _menuService.GetUserMenuPermissionsAsync(userPublicId, propertyId);
            return Ok(userMenuPermissions);
        }
    }
}
