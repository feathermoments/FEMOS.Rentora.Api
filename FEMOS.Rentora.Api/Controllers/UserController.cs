using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// GET /api/user/profile
        /// Response: { userPublicId, name, email (masked), mobile (masked), workspaces[], stats }
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var profile = await _userService.GetUserProfileAsync(userPublicId);
            if (profile is null)
                return NotFound(new { Status = "Failure", Message = "User not found" });

            return Ok(profile);
        }

        /// <summary>
        /// PATCH /api/user/update-profile
        /// All body fields are optional — only supplied fields are updated.
        /// Request:  { "name": "Rahul", "email": "r@r.com", "mobileNumber": "9876543210", "ProfilePhoto": "url" }
        /// Response: { "status": "Success", "message": "Profile updated successfully" }
        /// </summary>
        [HttpPatch("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileInfo model)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            if (model.Name is null &&
                model.Email is null &&
                model.MobileNumber is null &&
                model.ProfilePhoto is null)
            {
                return BadRequest(new
                {
                    Status = "Failure",
                    Message = "At least one field (name, email, mobileNumber, ProfilePhoto) must be provided."
                });
            }

            // UserPublicId always comes from the verified JWT — never trust the request body
            model.UserPublicId = userPublicId;

            var result = await _userService.UpdateUserProfileAsync(model);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// DELETE /api/user/account
        /// Permanently deletes the authenticated user's account and all associated data.
        /// </summary>
        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _userService.DeleteUserAccountAsync(userPublicId);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }
    }
}
