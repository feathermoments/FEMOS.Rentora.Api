using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents the comprehensive summary of a user's properties,
    /// including owner and tenant summaries, as well as monthly trends.
    /// </summary>
    public class MyPropertiesSummaryInfo
    {
        /// <summary>
        /// Summary information for property owners
        /// </summary>
        public OwnerSummaryInfo objOwnerSummary { get; set; }

        /// <summary>
        /// Summary information for tenants
        /// </summary>
        public TenantSummaryInfo objTenantSummary { get; set; }

        /// <summary>
        /// Monthly billing and financial trends
        /// </summary>
        public List<MonthlyTrendInfo> objMonthlyTrends { get; set; } = new List<MonthlyTrendInfo>();
    }

    /// <summary>
    /// Represents a user's role (Owner or Tenant)
    /// Used to determine which summary data to return
    /// </summary>
    public class RoleSummaryInfo
    {
        /// <summary>
        /// The role ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// The role name (e.g., "OWNER", "TENANT")
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        public string RoleCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// Contains aggregated financial and operational data for property owners.
    /// Includes property counts, occupancy rates, earnings, expenses, and collection metrics.
    /// </summary>
    public class OwnerSummaryInfo
    {
        /// <summary>
        /// Total number of properties owned
        /// </summary>
        public int PropertyCount { get; set; }

        /// <summary>
        /// Total number of units across all properties
        /// </summary>
        public int TotalUnits { get; set; }

        /// <summary>
        /// Number of currently occupied units
        /// </summary>
        public int OccupiedUnits { get; set; }

        /// <summary>
        /// Number of vacant units
        /// </summary>
        public int VacantUnits { get; set; }

        /// <summary>
        /// Total earnings (typically monthly)
        /// </summary>
        public decimal Earnings { get; set; }

        /// <summary>
        /// Total expenses (typically monthly)
        /// </summary>
        public decimal Expenses { get; set; }

        /// <summary>
        /// Net income (Earnings - Expenses)
        /// </summary>
        public decimal NetIncome { get; set; }

        /// <summary>
        /// Total amount billed in the current month
        /// </summary>
        public decimal CurrentMonthBilled { get; set; }

        /// <summary>
        /// Total amount collected in the current month
        /// </summary>
        public decimal CurrentMonthCollected { get; set; }

        /// <summary>
        /// Percentage of current month billed amount that has been collected (0-100)
        /// </summary>
        public int CollectionPercentage { get; set; }

        /// <summary>
        /// Number of rental payments that are due
        /// </summary>
        public int RentDueCount { get; set; }

        /// <summary>
        /// Total amount of rent that is due
        /// </summary>
        public decimal RentDueAmount { get; set; }

        /// <summary>
        /// Total outstanding amount across all invoices
        /// </summary>
        public decimal TotalOutstandingAmount { get; set; }
    }

    /// <summary>
    /// Contains summary information relevant to tenants.
    /// Includes rental status, payment information, and outstanding balances.
    /// </summary>
    public class TenantSummaryInfo
    {
        /// <summary>
        /// Number of active rental agreements/assignments
        /// </summary>
        public int ActiveRentalCount { get; set; }

        /// <summary>
        /// Total rent paid by the tenant
        /// </summary>
        public decimal RentPaid { get; set; }

        /// <summary>
        /// Number of rental payments that are due
        /// </summary>
        public int RentDueCount { get; set; }

        /// <summary>
        /// Total amount of rent that is due
        /// </summary>
        public decimal RentDueAmount { get; set; }

        /// <summary>
        /// Total outstanding amount owed by the tenant
        /// </summary>
        public decimal TotalOutstandingAmount { get; set; }
    }

    /// <summary>
    /// Represents financial trends for a specific billing month.
    /// Used for tracking earnings, expenses, and net income over time.
    /// </summary>
    public class MonthlyTrendInfo
    {
        /// <summary>
        /// The billing year
        /// </summary>
        public int BillingYear { get; set; }

        /// <summary>
        /// The billing month (1-12)
        /// </summary>
        public int BillingMonth { get; set; }

        /// <summary>
        /// The start date of the billing month
        /// </summary>
        public DateTime MonthStart { get; set; }

        /// <summary>
        /// Total earnings for the month
        /// </summary>
        public decimal Earnings { get; set; }

        /// <summary>
        /// Total expenses for the month
        /// </summary>
        public decimal Expenses { get; set; }

        /// <summary>
        /// Net income for the month (Earnings - Expenses)
        /// </summary>
        public decimal NetIncome { get; set; }
    }
}
