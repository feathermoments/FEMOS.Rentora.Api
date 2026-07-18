-- =============================================
-- Dashboard Database Setup Scripts
-- =============================================

-- =============================================
-- 1. Master Table for Dashboard Widgets
-- =============================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Mst_DashboardWidgets')
BEGIN
    CREATE TABLE Mst_DashboardWidgets
    (
        WidgetId BIGINT PRIMARY KEY IDENTITY(1,1),
        WidgetCode NVARCHAR(100) NOT NULL UNIQUE,
        WidgetTitle NVARCHAR(200) NOT NULL,
        Description NVARCHAR(500),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME DEFAULT GETUTCDATE()
    );

    CREATE INDEX IX_Mst_DashboardWidgets_WidgetCode ON Mst_DashboardWidgets(WidgetCode);
    CREATE INDEX IX_Mst_DashboardWidgets_IsActive ON Mst_DashboardWidgets(IsActive);
END;

-- =============================================
-- 2. Role-to-Widget Mapping Table
-- =============================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Mst_RoleDashboardWidgets')
BEGIN
    CREATE TABLE Mst_RoleDashboardWidgets
    (
        RoleDashboardWidgetId BIGINT PRIMARY KEY IDENTITY(1,1),
        RoleId BIGINT NOT NULL,
        WidgetId BIGINT NOT NULL,
        DisplayOrder INT DEFAULT 1,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME DEFAULT GETUTCDATE(),

        FOREIGN KEY (WidgetId) REFERENCES Mst_DashboardWidgets(WidgetId),
        CONSTRAINT UC_RoleDashboardWidgets UNIQUE (RoleId, WidgetId)
    );

    CREATE INDEX IX_Mst_RoleDashboardWidgets_RoleId ON Mst_RoleDashboardWidgets(RoleId);
    CREATE INDEX IX_Mst_RoleDashboardWidgets_WidgetId ON Mst_RoleDashboardWidgets(WidgetId);
END;

-- =============================================
-- 3. Seed Dashboard Widgets
-- =============================================

IF NOT EXISTS (SELECT 1 FROM Mst_DashboardWidgets WHERE WidgetCode = 'PROPERTY_SUMMARY')
BEGIN
    INSERT INTO Mst_DashboardWidgets (WidgetCode, WidgetTitle, Description, IsActive)
    VALUES
        ('PROPERTY_SUMMARY', 'Property Summary', 'Displays property overview with unit counts', 1),
        ('RENT_SUMMARY', 'Rent Summary', 'Shows rent collection metrics', 1),
        ('RECENT_PAYMENTS', 'Recent Payments', 'Displays recent payment transactions', 1),
        ('OPEN_REQUESTS', 'Open Requests', 'Shows open maintenance and tenant requests', 1),
        ('UPCOMING_RENEWALS', 'Upcoming Renewals', 'Displays upcoming agreement renewals', 1),
        ('MY_HOME', 'My Home', 'Shows tenant home/unit information', 1),
        ('AGREEMENT', 'Agreements', 'Displays rental agreement details', 1),
        ('MY_REQUESTS', 'My Requests', 'Shows user submitted requests', 1),
        ('STAFF_SUMMARY', 'Staff Summary', 'Displays property staff information', 1),
        ('REPORT_SUMMARY', 'Report Summary', 'Shows report summaries and analytics', 1);
END;

-- =============================================
-- 4. Stored Procedure: Get Widgets by Role
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_GetWidgetsByRole', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_GetWidgetsByRole;
GO

CREATE PROCEDURE dbo.USP_Dashboard_GetWidgetsByRole
    @RoleId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        dw.WidgetCode,
        dw.WidgetTitle,
        rdw.DisplayOrder
    FROM Mst_DashboardWidgets dw
    INNER JOIN Mst_RoleDashboardWidgets rdw ON dw.WidgetId = rdw.WidgetId
    WHERE rdw.RoleId = @RoleId
        AND dw.IsActive = 1
        AND rdw.IsActive = 1
    ORDER BY rdw.DisplayOrder ASC;
END;
GO

-- =============================================
-- 5. Stored Procedure: Property Summary Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_PropertySummary', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_PropertySummary;
GO

CREATE PROCEDURE dbo.USP_Dashboard_PropertySummary
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema
    -- This is a template - adjust according to your actual tables

    SELECT TOP 1
        @PropertyId AS PropertyId,
        'Property Name' AS PropertyName,
        'Apartment' AS PropertyType,
        'New York' AS City,
        'NY' AS State,
        10 AS TotalUnits,
        8 AS OccupiedUnits,
        2 AS VacantUnits;
END;
GO

-- =============================================
-- 6. Stored Procedure: Rent Summary Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_RentSummary', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_RentSummary;
GO

CREATE PROCEDURE dbo.USP_Dashboard_RentSummary
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT
        15000.00 AS TotalRentCollected,
        2000.00 AS PendingRent,
        1000.00 AS OverdueRent,
        8 AS ActiveAgreements;
END;
GO

-- =============================================
-- 7. Stored Procedure: Recent Payments Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_RecentPayments', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_RecentPayments;
GO

CREATE PROCEDURE dbo.USP_Dashboard_RecentPayments
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 10
        1 AS PaymentId,
        1 AS TenantId,
        'John Doe' AS TenantName,
        1500.00 AS Amount,
        GETUTCDATE() AS PaymentDate,
        'Online' AS PaymentMethod,
        'Success' AS Status;
END;
GO

-- =============================================
-- 8. Stored Procedure: Open Requests Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_OpenRequests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_OpenRequests;
GO

CREATE PROCEDURE dbo.USP_Dashboard_OpenRequests
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 5
        1 AS RequestId,
        1 AS TenantId,
        'John Doe' AS TenantName,
        'Maintenance' AS RequestType,
        'Plumbing issue' AS Description,
        GETUTCDATE() AS CreatedDate,
        'Open' AS Status;
END;
GO

-- =============================================
-- 9. Stored Procedure: Upcoming Renewals Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_UpcomingRenewals', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_UpcomingRenewals;
GO

CREATE PROCEDURE dbo.USP_Dashboard_UpcomingRenewals
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 5
        1 AS AgreementId,
        1 AS TenantId,
        'John Doe' AS TenantName,
        101 AS UnitId,
        'Unit 101' AS UnitName,
        DATEADD(MONTH, 3, GETUTCDATE()) AS RenewalDate,
        90 AS DaysUntilRenewal;
END;
GO

-- =============================================
-- 10. Stored Procedure: My Home Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_MyHome', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_MyHome;
GO

CREATE PROCEDURE dbo.USP_Dashboard_MyHome
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT
        101 AS UnitId,
        'Unit 101' AS UnitName,
        'Downtown Apartments' AS PropertyName,
        'New York' AS City,
        'NY' AS State,
        '2023-01-15' AS OccupancyDate,
        'Active' AS AgreementStatus;
END;
GO

-- =============================================
-- 11. Stored Procedure: Agreement Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_Agreement', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_Agreement;
GO

CREATE PROCEDURE dbo.USP_Dashboard_Agreement
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 5
        1 AS AgreementId,
        'AGR-001' AS AgreementNumber,
        1 AS TenantId,
        'John Doe' AS TenantName,
        101 AS UnitId,
        'Unit 101' AS UnitName,
        '2023-01-15' AS StartDate,
        '2024-01-15' AS EndDate,
        1500.00 AS MonthlyRent,
        'Active' AS Status;
END;
GO

-- =============================================
-- 12. Stored Procedure: My Requests Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_MyRequests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_MyRequests;
GO

CREATE PROCEDURE dbo.USP_Dashboard_MyRequests
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 5
        1 AS RequestId,
        'Maintenance' AS RequestType,
        'Need plumbing repair' AS Description,
        GETUTCDATE() AS CreatedDate,
        'Open' AS Status,
        'Property Manager' AS RequestedTo;
END;
GO

-- =============================================
-- 13. Stored Procedure: Staff Summary Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_StaffSummary', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_StaffSummary;
GO

CREATE PROCEDURE dbo.USP_Dashboard_StaffSummary
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT
        1 AS StaffId,
        'Jane Smith' AS StaffName,
        'Property Manager' AS Role,
        'Active' AS Status,
        '2023-01-01' AS JoinDate;
END;
GO

-- =============================================
-- 14. Stored Procedure: Report Summary Widget
-- =============================================

IF OBJECT_ID('dbo.USP_Dashboard_ReportSummary', 'P') IS NOT NULL
    DROP PROCEDURE dbo.USP_Dashboard_ReportSummary;
GO

CREATE PROCEDURE dbo.USP_Dashboard_ReportSummary
    @PropertyId BIGINT,
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: Implement based on your schema

    SELECT TOP 5
        'Monthly Report' AS ReportName,
        GETUTCDATE() AS GeneratedDate,
        15 AS RecordCount,
        '/reports/monthly-' + FORMAT(GETUTCDATE(), 'yyyyMM') + '.pdf' AS ReportUrl;
END;
GO

-- =============================================
-- 15. Sample Role-Widget Mapping (Owner)
-- =============================================

-- Assuming RoleId = 1 is "Owner"
IF NOT EXISTS (SELECT 1 FROM Mst_RoleDashboardWidgets WHERE RoleId = 1)
BEGIN
    DECLARE @WidgetIds TABLE (WidgetCode NVARCHAR(100), WidgetId BIGINT);

    INSERT INTO @WidgetIds
    SELECT WidgetCode, WidgetId FROM Mst_DashboardWidgets;

    INSERT INTO Mst_RoleDashboardWidgets (RoleId, WidgetId, DisplayOrder, IsActive)
    VALUES
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'PROPERTY_SUMMARY'), 1, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'RENT_SUMMARY'), 2, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'RECENT_PAYMENTS'), 3, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'OPEN_REQUESTS'), 4, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'UPCOMING_RENEWALS'), 5, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'STAFF_SUMMARY'), 6, 1),
        (1, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'REPORT_SUMMARY'), 7, 1);
END;

-- =============================================
-- 16. Sample Role-Widget Mapping (Tenant)
-- =============================================

-- Assuming RoleId = 2 is "Tenant"
IF NOT EXISTS (SELECT 1 FROM Mst_RoleDashboardWidgets WHERE RoleId = 2)
BEGIN
    INSERT INTO Mst_RoleDashboardWidgets (RoleId, WidgetId, DisplayOrder, IsActive)
    VALUES
        (2, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'MY_HOME'), 1, 1),
        (2, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'AGREEMENT'), 2, 1),
        (2, (SELECT WidgetId FROM Mst_DashboardWidgets WHERE WidgetCode = 'MY_REQUESTS'), 3, 1);
END;

-- =============================================
-- 17. Query to Verify Setup
-- =============================================

-- View all widgets
SELECT 'All Widgets' AS Label;
SELECT * FROM Mst_DashboardWidgets;

-- View role assignments
SELECT 'Role Assignments' AS Label;
SELECT 
    rdw.RoleId,
    dw.WidgetCode,
    dw.WidgetTitle,
    rdw.DisplayOrder
FROM Mst_RoleDashboardWidgets rdw
INNER JOIN Mst_DashboardWidgets dw ON rdw.WidgetId = dw.WidgetId
ORDER BY rdw.RoleId, rdw.DisplayOrder;

-- Test stored procedure
EXEC dbo.USP_Dashboard_GetWidgetsByRole @RoleId = 1;
