# Developer Guide - Dynamic Dashboard System

## Table of Contents
1. [Quick Start](#quick-start)
2. [Architecture Overview](#architecture-overview)
3. [Adding New Widgets](#adding-new-widgets)
4. [Widget Development](#widget-development)
5. [Testing Widgets](#testing-widgets)
6. [Troubleshooting](#troubleshooting)
7. [Best Practices](#best-practices)

## Quick Start

### Prerequisites
- .NET 8
- SQL Server
- Rentora solution configured and running

### Running the Dashboard

```bash
# 1. Ensure database is set up (see DATABASE_SETUP.sql)
# 2. Build the solution
dotnet build

# 3. Run the API
dotnet run --project FEMOS.Rentora.Api

# 4. Test the endpoint
GET http://localhost:5000/api/dashboard?propertyId=1
Authorization: Bearer <your_jwt_token>
```

## Architecture Overview

### Component Relationship

```
Request ? DashboardController
        ?
        ? Extract UserId, RoleId from context
        ?
        ? DashboardService.GetDashboardAsync()
        ?
        ? DashboardRepository.GetDashboardWidgetsByRoleAsync()
        ?
        ? For each widget:
           ?? DashboardWidgetFactory.ResolveWidget(widgetCode)
           ?? IDashboardWidget.GetDataAsync()
           ?? Aggregate response
        ?
Response ? DashboardResponseInfo
```

### Design Decisions

| Decision | Reason |
|----------|--------|
| Factory Pattern | Enables dynamic widget resolution without hardcoding |
| Strategy Pattern | Each widget is independently replaceable |
| Separate Repository Methods | One method = one stored procedure for clarity |
| Role-Based Widget Assignment | Database-driven flexibility |
| Service Orchestration | Business logic separated from widget logic |

## Adding New Widgets

### 7-Step Process

#### Step 1: Create Entity Class
```csharp
// FEMOS.Rentora.Domain/Entities/YourDataInfo.cs
namespace FEMOS.Rentora.Domain.Entities
{
    public class YourDataInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
```

#### Step 2: Add Repository Interface Method
```csharp
// FEMOS.Rentora.Infrastructure/Interfaces/IDashboardRepository.cs
public interface IDashboardRepository
{
    // ... existing methods ...

    /// <summary>
    /// Retrieves your data.
    /// </summary>
    Task<List<YourDataInfo>> GetYourDataAsync(long propertyId, long userId);
}
```

#### Step 3: Implement Repository Method
```csharp
// FEMOS.Rentora.Infrastructure/Repositories/DashboardRepository.cs
public async Task<List<YourDataInfo>> GetYourDataAsync(long propertyId, long userId)
{
    var cmd = new SqlCommand(DBConstants.USP_Dashboard_YourWidget);
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Parameters.AddWithValue("@PropertyId", propertyId);
    cmd.Parameters.AddWithValue("@UserId", userId);

    var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
    var data = _dbHelper.ConvertDataTable<YourDataInfo>(dt);

    return data;
}
```

#### Step 4: Create Widget Class
```csharp
// FEMOS.Rentora.Application/Services/Widgets/YourDataWidget.cs
using FEMOS.Rentora.Application.Interfaces.Dashboard;
using FEMOS.Rentora.Infrastructure.Interfaces;

namespace FEMOS.Rentora.Application.Services.Widgets
{
    public class YourDataWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public YourDataWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "YOUR_WIDGET_CODE";

        public async Task<object> GetDataAsync(long propertyId, long userId)
        {
            return await _dashboardRepository.GetYourDataAsync(propertyId, userId);
        }
    }
}
```

**Key Points:**
- `WidgetCode` must be unique and descriptive
- Use UPPERCASE for widget codes
- Widget should focus on data retrieval only

#### Step 5: Register Widget in DependencyInjection
```csharp
// FEMOS.Rentora.Application/DependencyInjection.cs
public static IServiceCollection AddApplicationServices(
    this IServiceCollection services, 
    IConfiguration configuration)
{
    // ... existing registrations ...

    // Add your widget
    services.AddScoped<IDashboardWidget, YourDataWidget>();

    // ... rest of code ...
}
```

#### Step 6: Create Stored Procedure
```sql
-- Database
IF OBJECT_ID('dbo.USP_Dashboard_YourWidget', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_YourWidget;
GO

CREATE PROCEDURE dbo.USP_Dashboard_YourWidget
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Value,
        CreatedDate
    FROM YourDataTable
    WHERE PropertyId = @PropertyId
        AND UserId = @UserId
    ORDER BY CreatedDate DESC;
END;
GO
```

#### Step 7: Add Database Constants and Configure
```csharp
// FEMOS.Rentora.Domain/Constants/DBConstants.cs
public static class DBConstants
{
    // ... existing constants ...

    // Dashboard - Add your procedure
    public const string USP_Dashboard_YourWidget = "dbo.USP_Dashboard_YourWidget";
}
```

Then update the database:
```sql
-- Insert widget into master table
INSERT INTO Mst_DashboardWidgets (WidgetCode, WidgetTitle, Description, IsActive)
VALUES ('YOUR_WIDGET_CODE', 'Your Widget Title', 'Description', 1);

-- Assign to roles
INSERT INTO Mst_RoleDashboardWidgets (RoleId, WidgetId, DisplayOrder, IsActive)
VALUES (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'YOUR_WIDGET_CODE'), 8, 1);
```

**Done!** No other code needs to change.

## Widget Development

### Best Practices for Widgets

#### 1. Single Responsibility
```csharp
// ? GOOD - One job: get property summary
public class PropertySummaryWidget : IDashboardWidget
{
    public string WidgetCode => "PROPERTY_SUMMARY";

    public async Task<object> GetDataAsync(long propertyId, long userId)
    {
        return await _dashboardRepository.GetPropertySummaryAsync(propertyId, userId);
    }
}

// ? BAD - Too many responsibilities
public class MegaWidget : IDashboardWidget
{
    public async Task<object> GetDataAsync(long propertyId, long userId)
    {
        // Fetches multiple data sources, transforms, validates, caches, etc.
    }
}
```

#### 2. Minimal Dependencies
```csharp
// ? GOOD - Only needs what it uses
public class MyWidget : IDashboardWidget
{
    private readonly IDashboardRepository _dashboardRepository;

    public MyWidget(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }
}

// ? BAD - Injecting unnecessary dependencies
public class MyWidget : IDashboardWidget
{
    public MyWidget(
        IPropertyService propertyService,
        IRentService rentService,
        ITenantService tenantService,
        // ... 10 more services
    )
    {
    }
}
```

#### 3. Return Appropriate Data Types
```csharp
// ? GOOD - Clear data structures
public async Task<object> GetDataAsync(long propertyId, long userId)
{
    var data = await _dashboardRepository.GetPropertySummaryAsync(propertyId, userId);
    return data; // Returns List<PropertySummaryInfo>
}

// ? AVOID - Returning raw strings or unstructured data
public async Task<object> GetDataAsync(long propertyId, long userId)
{
    return "Some string data"; // Not useful for UI
}
```

#### 4. Handle Errors Gracefully
```csharp
// ? GOOD - Let service handle errors
public async Task<object> GetDataAsync(long propertyId, long userId)
{
    // Errors are caught by DashboardService
    // This widget continues to be logged without crashing dashboard
    return await _dashboardRepository.GetDataAsync(propertyId, userId);
}

// ? BAD - Catching and swallowing errors
public async Task<object> GetDataAsync(long propertyId, long userId)
{
    try
    {
        return await _dashboardRepository.GetDataAsync(propertyId, userId);
    }
    catch
    {
        return null; // Silent failure
    }
}
```

### Widget Code Naming Convention

```
PROPERTY_SUMMARY    ? Clear, descriptive
RENT_SUMMARY        ? Clear, descriptive
RECENT_PAYMENTS     ? Specific
MY_HOME             ? User perspective
XYZ                 ? Not descriptive
WIDGET_1            ? Not descriptive
```

## Testing Widgets

### Unit Testing Example

```csharp
using Xunit;
using Moq;
using FEMOS.Rentora.Application.Services.Widgets;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Domain.Entities;

public class PropertySummaryWidgetTests
{
    [Fact]
    public async Task GetDataAsync_WithValidInputs_ReturnsPropertySummary()
    {
        // Arrange
        var mockRepository = new Mock<IDashboardRepository>();
        var testData = new List<PropertySummaryInfo>
        {
            new PropertySummaryInfo
            {
                PropertyId = 1,
                PropertyName = "Test Property",
                PropertyType = "Apartment",
                City = "New York",
                State = "NY",
                TotalUnits = 10,
                OccupiedUnits = 8,
                VacantUnits = 2
            }
        };

        mockRepository
            .Setup(r => r.GetPropertySummaryAsync(1, 1))
            .ReturnsAsync(testData);

        var widget = new PropertySummaryWidget(mockRepository.Object);

        // Act
        var result = await widget.GetDataAsync(1, 1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<PropertySummaryInfo>>(result);
        var data = (List<PropertySummaryInfo>)result;
        Assert.Single(data);
        Assert.Equal("Test Property", data[0].PropertyName);
    }

    [Fact]
    public void WidgetCode_ReturnsCorrectCode()
    {
        // Arrange
        var mockRepository = new Mock<IDashboardRepository>();
        var widget = new PropertySummaryWidget(mockRepository.Object);

        // Act
        var code = widget.WidgetCode;

        // Assert
        Assert.Equal("PROPERTY_SUMMARY", code);
    }
}
```

### Integration Testing

```csharp
public class DashboardServiceIntegrationTests
{
    [Fact]
    public async Task GetDashboardAsync_WithValidRole_ReturnsDashboardWithWidgets()
    {
        // Arrange
        var dashboardService = new DashboardService(
            dashboardRepository,
            widgetFactory,
            logger);

        // Act
        var result = await dashboardService.GetDashboardAsync(
            propertyId: 1,
            userId: 1,
            roleId: 1);

        // Assert
        Assert.Equal("Success", result.Status);
        Assert.NotEmpty(result.Widgets);
        Assert.All(result.Widgets, w =>
        {
            Assert.NotNull(w.WidgetCode);
            Assert.NotNull(w.Title);
            Assert.NotNull(w.Data);
        });
    }
}
```

## Troubleshooting

### Widget Not Appearing in Dashboard

**Problem:** Widget is registered but doesn't show in dashboard response.

**Solutions:**
1. Check database: Is the widget in `Mst_DashboardWidgets`?
2. Check assignment: Is it assigned to the role in `Mst_RoleDashboardWidgets`?
3. Check stored procedure: Does `USP_Dashboard_GetWidgetsByRole` return it?
4. Check logs: Are there errors during widget resolution?

```sql
-- Verify widget exists
SELECT * FROM Mst_DashboardWidgets WHERE WidgetCode = 'YOUR_WIDGET_CODE';

-- Verify role assignment
SELECT * FROM Mst_RoleDashboardWidgets 
WHERE RoleId = 1 
AND WidgetId = (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'YOUR_WIDGET_CODE');

-- Test stored procedure
EXEC dbo.USP_Dashboard_GetWidgetsByRole @RoleId = 1;
```

### Widget Returns Empty Data

**Problem:** Widget appears but with no data.

**Solutions:**
1. Check stored procedure parameters
2. Verify data exists in source table
3. Check user/property permissions
4. Review stored procedure query

```sql
-- Test the widget's stored procedure directly
EXEC dbo.USP_Dashboard_YourWidget @PropertyId = 1, @UserId = 1;
```

### Widget Execution Error

**Problem:** Dashboard returns successfully but widget shows error in logs.

**Solutions:**
1. Check widget implementation for null references
2. Verify repository method implementation
3. Check stored procedure syntax
4. Verify parameters are being passed correctly

```csharp
// Enable detailed logging
logger.LogError($"Widget {widgetCode} failed: {exception}");
```

### Incorrect Display Order

**Problem:** Widgets appear in wrong order.

**Solutions:**
1. Check `DisplayOrder` in `Mst_RoleDashboardWidgets`
2. Update order as needed
3. Clear any caches

```sql
-- Update display order
UPDATE Mst_RoleDashboardWidgets
SET DisplayOrder = 2
WHERE RoleId = 1 
AND WidgetId = (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'YOUR_WIDGET_CODE');
```

## Best Practices

### Code Organization

```
FEMOS.Rentora.Application/
??? Interfaces/
?   ??? Dashboard/
?   ?   ??? IDashboardWidget.cs
?   ??? IDashboardService.cs
??? Services/
?   ??? DashboardService.cs
?   ??? Factories/
?   ?   ??? DashboardWidgetFactory.cs
?   ??? Widgets/
?       ??? PropertySummaryWidget.cs
?       ??? RentSummaryWidget.cs
?       ??? ... other widgets
```

### Naming Conventions

| Item | Convention | Example |
|------|-----------|---------|
| Widget Class | `{DataType}Widget` | `PropertySummaryWidget` |
| Widget Code | `UPPERCASE_SNAKE_CASE` | `PROPERTY_SUMMARY` |
| Entity Class | `{DataType}Info` | `PropertySummaryInfo` |
| Repository Method | `Get{DataType}Async` | `GetPropertySummaryAsync` |
| Stored Procedure | `USP_Dashboard_{DataType}` | `USP_Dashboard_PropertySummary` |

### Performance Tips

1. **Optimize Stored Procedures**
   ```sql
   -- Index frequently filtered columns
   CREATE INDEX IX_Properties_PropertyId ON Properties(PropertyId);
   ```

2. **Consider Caching**
   ```csharp
   // Future enhancement
   services.AddDistributedMemoryCache();
   ```

3. **Pagination for Large Datasets**
   ```csharp
   // In stored procedure
   ORDER BY Id
   OFFSET (@PageNumber - 1) * @PageSize ROWS
   FETCH NEXT @PageSize ROWS ONLY;
   ```

### Security Considerations

1. **Validate propertyId**
   - Ensure user has access to property

2. **Use userId from context**
   - Never trust user input for userId

3. **Filter by role**
   - Only show widgets assigned to user's role

4. **Parameterize queries**
   - Always use SqlParameters (already done)

### Documentation

```csharp
/// <summary>
/// Widget for displaying property summary information.
/// Shows property details, unit counts, and occupancy status.
/// </summary>
/// <remarks>
/// This widget is typically shown to property owners and managers.
/// It requires access to property and unit information.
/// </remarks>
public class PropertySummaryWidget : IDashboardWidget
{
    // ...
}
```

## Resources

- [DASHBOARD_ARCHITECTURE.md](./DASHBOARD_ARCHITECTURE.md) - Full architecture documentation
- [DATABASE_SETUP.sql](./DATABASE_SETUP.sql) - Database setup scripts
- [IMPLEMENTATION_SUMMARY.md](./IMPLEMENTATION_SUMMARY.md) - Implementation details
