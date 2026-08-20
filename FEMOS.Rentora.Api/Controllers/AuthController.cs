using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IEncryptDecryptService _encryptDecryptService;

        public AuthController(
            IAuthService authService,
            ITokenService tokenService,
            IEncryptDecryptService encryptDecryptService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _encryptDecryptService = encryptDecryptService;
        }

        /// <summary>POST /api/auth/send-otp</summary>
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpInfo model)
        {
            var result = await _authService.SendOtpAsync(model);
            return Ok(result);
        }

        /// <summary>POST /api/auth/verify-otp</summary>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpInfo model)
        {
            var result = await _authService.VerifyOtpAsync(model);
            if (result.Status == "Failure")
                return Unauthorized(result);
            return Ok(result);
        }

        /// <summary>POST /api/auth/refresh</summary>
        /// <remarks>
        /// Refreshes the access token using a valid refresh token.
        /// Returns a new access token and refresh token.
        /// </remarks>
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request?.AccessToken) || string.IsNullOrEmpty(request?.RefreshToken))
                return BadRequest(new { Status = "Failure", Message = "Access token and refresh token are required." });

            var result = await _tokenService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

            if (result.Status == "Failure")
                return Unauthorized(result);

            return Ok(result);
        }

        /// <summary>POST /api/auth/logout</summary>
        /// <remarks>
        /// Logs out the user by revoking the current refresh token.
        /// Requires Bearer token authorization.
        /// </remarks>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            // Extract userPublicId from JWT claims
            var userPublicIdClaim = User.FindFirst("userPublicId");
            if (userPublicIdClaim == null || !Guid.TryParse(userPublicIdClaim.Value, out var userPublicId))
                return Unauthorized(new { Status = "Failure", Message = "Invalid user information." });

            // Revoke the specific refresh token
            if (!string.IsNullOrEmpty(request?.RefreshToken))
            {
                await _tokenService.LogoutAsync(userPublicId, request.RefreshToken);
            }

            return Ok(new { Status = "Success", Message = "Logged out successfully." });
        }

        /// <summary>POST /api/auth/logout-all</summary>
        /// <remarks>
        /// Logs out the user from all devices by revoking all refresh tokens.
        /// Requires Bearer token authorization.
        /// </remarks>
        [HttpPost("logout-all")]
        [Authorize]
        public async Task<IActionResult> LogoutAll()
        {
            // Extract userPublicId from JWT claims
            var userPublicIdClaim = User.FindFirst("userPublicId");
            if (userPublicIdClaim == null || !Guid.TryParse(userPublicIdClaim.Value, out var userPublicId))
                return Unauthorized(new { Status = "Failure", Message = "Invalid user information." });

            // Revoke all refresh tokens for the user
            await _tokenService.LogoutAllAsync(userPublicId);

            return Ok(new { Status = "Success", Message = "Logged out from all devices successfully." });
        }

        /// <summary>POST /api/auth/decrypt</summary>
        /// <remarks>
        /// This endpoint is intended for administrative use to decrypt sensitive information.
        /// It should be protected and not exposed to regular users. In a production environment, consider adding additional
        /// security measures such as an API key or admin role check.
        /// </remarks>
        //[HttpPost("decrypt")]
        //[Authorize]
        //public IActionResult Decrypt([FromBody] DecryptRequest model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.CipherText))
        //        return BadRequest(new { Status = "Failure", Message = "CipherText is required." });

        //    var plainText = _encryptDecryptService.Decrypt(model.CipherText);
        //    return Ok(new { PlainText = plainText });
        //}

        /// <summary>POST /api/auth/recomputeContactHashes</summary>
        /// <remarks>
        /// This endpoint is intended for administrative use to trigger a recomputation of contact hashes for all users.
        /// It should be protected and not exposed to regular users. In a production environment, consider adding additional
        /// security measures such as an API key or admin role check.
        /// </remarks>
        //[Route("recomputeContactHashes")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> RecomputeContactHashes()
        //{
        //    try
        //    {
        //        await _authService.RecomputeContactHashes();
        //        return Ok(new { Status = "Success", Message = "Contact hashes recomputed successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Status = "Failure", Message = ex.Message });
        //    }
        //}
    }
}

