using FEMOS.Rentora.Application.Interfaces.Dashboard;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services.Factories
{
    /// <summary>
    /// Factory for resolving dashboard widget implementations by widget code.
    /// Implements the Strategy Pattern to dynamically resolve widgets.
    /// No hardcoded logic or switch statements - relies on dependency injection.
    /// </summary>
    public class DashboardWidgetFactory
    {
        private readonly Dictionary<string, IDashboardWidget> _widgetRegistry;
        private readonly ILogger<DashboardWidgetFactory> _logger;

        public DashboardWidgetFactory(IEnumerable<IDashboardWidget> widgets, ILogger<DashboardWidgetFactory> logger)
        {
            _logger = logger;
            _widgetRegistry = new Dictionary<string, IDashboardWidget>(StringComparer.OrdinalIgnoreCase);

            // Automatically register all widget implementations
            foreach (var widget in widgets)
            {
                if (widget != null)
                {
                    _widgetRegistry[widget.WidgetCode] = widget;
                    _logger.LogInformation($"Registered dashboard widget: {widget.WidgetCode}");
                }
            }
        }

        /// <summary>
        /// Retrieves a widget implementation by its code.
        /// </summary>
        /// <param name="widgetCode">The widget code identifier</param>
        /// <returns>The widget implementation, or null if not found</returns>
        public IDashboardWidget ResolveWidget(string widgetCode)
        {
            if (string.IsNullOrWhiteSpace(widgetCode))
            {
                _logger.LogWarning("Widget code is null or empty.");
                return null;
            }

            if (_widgetRegistry.TryGetValue(widgetCode, out var widget))
            {
                _logger.LogInformation($"Resolved widget: {widgetCode}");
                return widget;
            }

            _logger.LogWarning($"Widget not found: {widgetCode}");
            return null;
        }

        /// <summary>
        /// Gets all registered widget codes.
        /// </summary>
        /// <returns>List of registered widget codes</returns>
        public List<string> GetRegisteredWidgets()
        {
            return _widgetRegistry.Keys.ToList();
        }
    }
}
