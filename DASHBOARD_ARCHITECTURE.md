# Dynamic Dashboard Architecture

## Overview

The Rentora dashboard has been refactored into a **dynamic, widget-based architecture** following **Clean Architecture**, **SOLID Principles**, and the **Strategy Pattern**. This implementation enables:

- **Extensibility**: Adding new widgets requires no changes to existing code
- **Maintainability**: Each widget is independently testable and isolated
- **Flexibility**: Widgets can be dynamically assigned to roles via database configuration
- **Scalability**: New dashboard features can be added without affecting the core framework

## Architecture Flow

```
User Request
     ?
     ?
DashboardController
     ?
     ?
DashboardService
     ?? Fetch widgets assigned to user's role (IDashboardRepository)
     ?? For each widget:
         ?? Resolve widget using WidgetCode (DashboardWidgetFactory)
         ?? Execute widget to fetch data (IDashboardWidget)
         ?? Aggregate results
     ?
     ?
DashboardResponse (with all widget data)
```

## Project Structure

### Domain Layer (`FEMOS.Rentora.Domain`)

**Entities:**
- `DashboardWidgetInfo` - Dashboard widget metadata
- `PropertySummaryInfo` - Property summary data
- `RentSummaryInfo` - Rent summary data
- `RecentPaymentInfo` - Recent payment transactions
- `OpenRequestInfo` - Open maintenance/tenant requests
- `UpcomingRenewalInfo` - Upcoming agreement renewals
- `MyHomeInfo` - Tenant's home/unit information
- `AgreementInfo` - Rental agreement details
- `MyRequestInfo` - User's requests
- `StaffSummaryInfo` - Staff information
- `ReportSummaryInfo` - Report summaries

**Responses:**
- `DashboardWidgetResponseInfo` - Individual widget response structure
- `DashboardResponseInfo` - Complete dashboard response

**Constants:**
- `DBConstants` - Added dashboard-related stored procedure constants

### Infrastructure Layer (`FEMOS.Rentora.Infrastructure`)

**Interfaces:**
- `IDashboardRepository` - Contract for dashboard data access

**Repositories:**
- `DashboardRepository` - Implements data retrieval for all dashboard widgets

Each method in the repository corresponds to exactly one stored procedure, maintaining separation of concerns.

### Application Layer (`FEMOS.Rentora.Application`)

**Interfaces:**
- `IDashboardWidget` - Strategy interface for all dashboard widgets
- `IDashboardService` - Service contract for dashboard operations

**Services:**
- `DashboardService` - Orchestrates widget retrieval and data aggregation

**Factories:**
- `DashboardWidgetFactory` - Resolves widgets by code using dependency injection

**Widgets (Services/Widgets/):**
1. `PropertySummaryWidget` - Property overview
2. `RentSummaryWidget` - Rent collection metrics
3. `RecentPaymentsWidget` - Recent transaction history
4. `OpenRequestsWidget` - Pending requests
5. `UpcomingRenewalsWidget` - Agreement renewals
6. `MyHomeWidget` - Tenant's unit information
7. `AgreementWidget` - Agreement details
8. `MyRequestsWidget` - User's submitted requests
9. `StaffSummaryWidget` - Property staff
10. `ReportSummaryWidget` - Report metrics

### API Layer (`FEMOS.Rentora.Api`)

**Controllers:**
- `DashboardController` - Exposes the `/api/dashboard` endpoint

## API Endpoint

### Get Dashboard

```http
GET /api/dashboard?propertyId={propertyId}
Authorization: Bearer {token}
```

**Response:**
```json
{
    "status": "Success",
    "message": "Dashboard retrieved successfully.",
    "widgets": [
        {
            "widgetCode": "PROPERTY_SUMMARY",
            "title": "Property Summary",
            "displayOrder": 1,
            "data": [
                {
                    "propertyId": 1,
                    "propertyName": "Downtown Apartments",
                    "propertyType": "Apartment",
                    "city": "New York",
                    "state": "NY",
                    "totalUnits": 10,
                    "occupiedUnits": 8,
                    "vacantUnits": 2
                }
            ]
        },
        {
            "widgetCode": "RENT_SUMMARY",
            "title": "Rent Summary",
            "displayOrder": 2,
            "data": [
                {
                    "totalRentCollected": 15000,
                    "pendingRent": 2000,
                    "overdueRent": 1000,
                    "activeAgreements": 8
                }
            ]
        }
    ]
}
```

## Widget Implementation Pattern

Each widget follows this pattern:

```csharp
public class YourWidget : IDashboardWidget
{
    private readonly IDashboardRepository _dashboardRepository;

    public YourWidget(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public string WidgetCode => "YOUR_WIDGET_CODE";

    public async Task<object> GetDataAsync(long propertyId, long userId)
    {
        return await _dashboardRepository.GetYourDataAsync(propertyId, userId);
    }
}
```

## Database Configuration

### Tables

1. **Mst_DashboardWidgets** - Master table for all available widgets
   ```sql
   - WidgetId (PK)
   - WidgetCode (unique)
   - WidgetTitle
   - IsActive
   ```

2. **Mst_RoleDashboardWidgets** - Maps widgets to roles
   ```sql
   - RoleId (FK)
   - WidgetId (FK)
   - DisplayOrder
   ```

### Stored Procedures

Each widget has a corresponding stored procedure:

- `USP_Dashboard_GetWidgetsByRole` - Fetches widgets for a role
- `USP_Dashboard_PropertySummary` - Property summary data
- `USP_Dashboard_RentSummary` - Rent summary data
- `USP_Dashboard_RecentPayments` - Recent payments
- `USP_Dashboard_OpenRequests` - Open requests
- `USP_Dashboard_UpcomingRenewals` - Upcoming renewals
- `USP_Dashboard_MyHome` - Tenant's home
- `USP_Dashboard_Agreement` - Agreement details
- `USP_Dashboard_MyRequests` - User's requests
- `USP_Dashboard_StaffSummary` - Staff information
- `USP_Dashboard_ReportSummary` - Report summaries

## Dependency Injection

All widgets and services are registered in `DependencyInjection.cs`:

```csharp
// Dashboard widgets
services.AddScoped<IDashboardWidget, PropertySummaryWidget>();
services.AddScoped<IDashboardWidget, RentSummaryWidget>();
// ... other widgets

// Dashboard services
services.AddScoped<IDashboardService, DashboardService>();
services.AddScoped<DashboardWidgetFactory>();
services.AddScoped<IDashboardRepository, DashboardRepository>();
```

## Design Patterns Used

### 1. **Strategy Pattern**
- `IDashboardWidget` defines the strategy interface
- Each widget is a concrete strategy
- `DashboardWidgetFactory` selects the appropriate strategy at runtime

### 2. **Factory Pattern**
- `DashboardWidgetFactory` creates widgets dynamically
- No hardcoded switch statements or if-else chains
- Relies on dependency injection for all registrations

### 3. **Repository Pattern**
- `IDashboardRepository` abstracts data access
- Each method maps to one stored procedure
- Maintains data layer isolation

### 4. **Dependency Injection**
- All dependencies are constructor-injected
- Enables loose coupling and testability
- Automatic widget discovery through `IEnumerable<IDashboardWidget>`

## SOLID Principles

### Single Responsibility Principle
- `DashboardService` only orchestrates widgets
- Each widget focuses on one data source
- Repository methods handle one stored procedure

### Open/Closed Principle
- Open for extension: Add new widgets without modifying existing code
- Closed for modification: Core components remain unchanged

### Liskov Substitution Principle
- All widgets implement `IDashboardWidget` consistently
- Can be substituted without affecting the system

### Interface Segregation Principle
- `IDashboardWidget` is focused and minimal
- `IDashboardRepository` contains only necessary methods

### Dependency Inversion Principle
- Depends on abstractions (`IDashboardWidget`, `IDashboardRepository`)
- Not on concrete implementations

## Extensibility Guide

### Adding a New Widget

1. **Create Widget Entity** (if needed)
   ```csharp
   // Domain/Entities/YourDataInfo.cs
   public class YourDataInfo { ... }
   ```

2. **Add Repository Method**
   ```csharp
   // Infrastructure/Interfaces/IDashboardRepository.cs
   Task<List<YourDataInfo>> GetYourDataAsync(long propertyId, long userId);

   // Infrastructure/Repositories/DashboardRepository.cs
   public async Task<List<YourDataInfo>> GetYourDataAsync(long propertyId, long userId)
   {
       var cmd = new SqlCommand(DBConstants.USP_Dashboard_YourData);
       cmd.CommandType = CommandType.StoredProcedure;
       // ... add parameters
       return await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
   }
   ```

3. **Create Widget Class**
   ```csharp
   // Application/Services/Widgets/YourWidget.cs
   public class YourWidget : IDashboardWidget
   {
       private readonly IDashboardRepository _dashboardRepository;
       public string WidgetCode => "YOUR_WIDGET_CODE";

       public async Task<object> GetDataAsync(long propertyId, long userId)
       {
           return await _dashboardRepository.GetYourDataAsync(propertyId, userId);
       }
   }
   ```

4. **Register Widget**
   ```csharp
   // Application/DependencyInjection.cs
   services.AddScoped<IDashboardWidget, YourWidget>();
   ```

5. **Create Stored Procedure**
   ```sql
   CREATE PROCEDURE USP_Dashboard_YourData
       @PropertyId BIGINT,
       @UserId BIGINT
   AS
   BEGIN
       SELECT ... FROM ... WHERE ...
   END
   ```

6. **Add DB Constant**
   ```csharp
   // Domain/Constants/DBConstants.cs
   public const string USP_Dashboard_YourData = "dbo.USP_Dashboard_YourData";
   ```

7. **Configure in Database**
   - Insert row in `Mst_DashboardWidgets` with your widget code
   - Insert rows in `Mst_RoleDashboardWidgets` mapping widget to roles

That's it! No changes needed to `DashboardController`, `DashboardService`, or `DashboardWidgetFactory`.

## Error Handling

- Invalid property ID returns 400 Bad Request
- Missing authentication returns 401 Unauthorized
- Widget resolution failures are logged and skipped
- Individual widget failures don't stop dashboard retrieval
- All errors are logged with appropriate severity levels

## Logging

The implementation includes comprehensive logging:
- Widget registration in factory
- Widget resolution attempts
- Widget execution status
- Error tracking with stack traces

## Testing Opportunities

The architecture supports easy unit testing:

```csharp
// Mock repository
var mockRepository = new Mock<IDashboardRepository>();
var widget = new PropertySummaryWidget(mockRepository.Object);

// Test widget
var result = await widget.GetDataAsync(1, 1);

// Mock factory with widgets
var widgets = new List<IDashboardWidget> { widget };
var factory = new DashboardWidgetFactory(widgets, mockLogger);
var resolved = factory.ResolveWidget("PROPERTY_SUMMARY");
```

## Performance Considerations

1. **Parallel Widget Execution** (Future Enhancement)
   - Widgets can be executed in parallel using `Task.WhenAll()`
   - Each widget accesses only its required data

2. **Caching** (Future Enhancement)
   - Widget data can be cached based on role/property
   - Configurable cache duration

3. **Pagination** (Future Enhancement)
   - Widget data can be paginated for large datasets
   - Implement in stored procedures

## Security

- JWT authentication required for all endpoints
- Role-based widget assignment
- User ID and Role ID extracted from authentication context
- Property ID validated before processing
