using FEMOS.Rentora.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Controllers
{
    /// <summary>
    /// Controller for dashboard operations.
    /// Provides endpoints for retrieving user dashboards with widget-based content.
    /// </summary>
    [Route("api/dashboard")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// GET /api/dashboard
        /// Retrieves the complete dashboard for the authenticated user with all assigned widgets.
        /// </summary>
        /// <param name="propertyId">The property ID for which to retrieve the dashboard</param>
        /// <returns>Dashboard response containing all assigned widgets and their data</returns>
        /// <response code="200">Dashboard retrieved successfully</response>
        /// <response code="400">Invalid property ID</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        public async Task<IActionResult> GetDashboard(long propertyId)
        {
            if (propertyId <= 0)
                return BadRequest(new { status = "Failure", message = "Invalid property ID." });

            // Extract user information from context
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();

            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var dashboard = await _dashboardService.GetDashboardAsync(propertyId, userPublicId);

            if (dashboard.Status == "Failure")
                return BadRequest(dashboard);

            return Ok(dashboard);
        }
    }
}
