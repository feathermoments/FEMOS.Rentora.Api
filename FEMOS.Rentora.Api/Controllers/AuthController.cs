using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEncryptDecryptService _encryptDecryptService;

        public AuthController(IAuthService authService, IEncryptDecryptService encryptDecryptService)
        {
            _authService = authService;
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
