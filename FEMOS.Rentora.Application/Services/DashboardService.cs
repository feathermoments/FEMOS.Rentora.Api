using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Application.Services.Factories;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    /// <summary>
    /// Service for orchestrating dashboard operations.
    /// Responsible for fetching assigned widgets for a role and aggregating their data.
    /// </summary>
    internal class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly DashboardWidgetFactory _widgetFactory;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(
            IDashboardRepository dashboardRepository,
            DashboardWidgetFactory widgetFactory,
            IPropertyRepository propertyRepository,
            ILogger<DashboardService> logger)
        {
            _dashboardRepository = dashboardRepository;
            _widgetFactory = widgetFactory;
            _propertyRepository = propertyRepository;
            _logger = logger;
        }

        public async Task<DashboardResponseInfo> GetDashboardAsync(long propertyId, Guid userPublicId)
        {
            var response = new DashboardResponseInfo();
            response.Status = StatusConstants.Success;
            response.Message = "Dashboard retrieved successfully.";
            response.Widgets = new List<DashboardWidgetResponseInfo>();

            try
            {
                UserPropertyMemberInfo objUserPropertyMemberInfo = await _propertyRepository.GetUserPropertyRole(userPublicId, propertyId);

                _logger.LogInformation($"Fetching dashboard for PropertyId: {propertyId}, UserPublicId: {userPublicId}, RoleId: {objUserPropertyMemberInfo.RoleId}");

                // Step 1: Fetch dashboard widgets assigned to the user's role
                var assignedWidgets = await _dashboardRepository.GetDashboardWidgetsByRoleAsync(objUserPropertyMemberInfo.RoleId);

                if (assignedWidgets == null || assignedWidgets.Count == 0)
                {
                    _logger.LogWarning($"No widgets assigned to role: {objUserPropertyMemberInfo.RoleId}");
                    return response;
                }

                _logger.LogInformation($"Found {assignedWidgets.Count} widgets assigned to role: {objUserPropertyMemberInfo.RoleId}");
                // Step 2: Iterate through assigned widgets and execute each one
                foreach (var assignedWidget in assignedWidgets.OrderBy(w => w.DisplayOrder))
                {
                    try
                    {
                        _logger.LogInformation($"Processing widget: {assignedWidget.WidgetCode}");

                        // Step 3: Resolve widget implementation using the factory
                        var widgetImplementation = _widgetFactory.ResolveWidget(assignedWidget.WidgetCode);

                        if (widgetImplementation == null)
                        {
                            _logger.LogWarning($"Widget implementation not found for code: {assignedWidget.WidgetCode}");
                            continue;
                        }

                        // Step 4: Execute widget to fetch data
                        var widgetData = await widgetImplementation.GetDataAsync(propertyId, userPublicId);

                        // Step 5: Aggregate widget response
                        var widgetResponse = new DashboardWidgetResponseInfo
                        {
                            WidgetCode = assignedWidget.WidgetCode,
                            Title = assignedWidget.WidgetTitle,
                            DisplayOrder = assignedWidget.DisplayOrder,
                            Data = widgetData
                        };

                        response.Widgets.Add(widgetResponse);
                        _logger.LogInformation($"Successfully processed widget: {assignedWidget.WidgetCode}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing widget {assignedWidget.WidgetCode}: {ex.Message}", ex);
                        // Continue processing other widgets even if one fails
                    }
                }

                _logger.LogInformation($"Dashboard retrieved successfully with {response.Widgets.Count} widgets");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving dashboard: {ex.Message}", ex);
                response.Status = StatusConstants.Failure;
                response.Message = "An error occurred while retrieving the dashboard.";
            }

            return response;
        }
    }
}
