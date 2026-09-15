using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("entitlements")]
        public async Task<IActionResult> GetEntitlements()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _subscriptionService.GetEntitlementsAsync(userPublicId);

            return Ok(result);
        }
    }
}
