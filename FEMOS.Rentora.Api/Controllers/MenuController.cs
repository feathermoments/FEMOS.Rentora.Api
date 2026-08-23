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

        [HttpGet("getUserMenu/{propertyPublicId}")]
        public async Task<IActionResult> GetUserMenu(Guid propertyPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var userMenus = await _menuService.GetUserMenuAsync(userPublicId, propertyPublicId);
            return Ok(userMenus);
        }

        [HttpGet("getUserMenuPermissions/{propertyPublicId}")]
        public async Task<IActionResult> GetUserMenuPermissions(Guid propertyPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var userMenuPermissions = await _menuService.GetUserMenuPermissionsAsync(userPublicId, propertyPublicId);
            return Ok(userMenuPermissions);
        }
    }
}
