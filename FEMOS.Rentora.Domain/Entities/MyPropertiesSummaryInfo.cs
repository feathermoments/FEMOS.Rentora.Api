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
        public int PropertyCount { get; set; }

        public int TotalUnits { get; set; }

        public int OccupiedUnits { get; set; }

        public int VacantUnits { get; set; }

        public decimal Earnings { get; set; }

        public decimal Expenses { get; set; }

        public decimal NetIncome { get; set; }

        public int CollectionPercentage { get; set; }

        public int RentDueCount { get; set; }

        public decimal RentDueAmount { get; set; }

        public decimal TotalOutstandingAmount { get; set; }

        public decimal RentBilled { get; set; }
        public decimal RentCollected { get; set; }
        public decimal TotalRentCollectedTillDate { get; set; }
        public decimal TotalExpensesTillDate { get; set; }
        public decimal TotalNetIncomeTillDate { get; set; }
    }

    /// <summary>
    /// Contains summary information relevant to tenants.
    /// Includes rental status, payment information, and outstanding balances.
    /// </summary>
    public class TenantSummaryInfo
    {

        public int ActiveRentalCount { get; set; }
        public decimal RentPaid { get; set; }
        public int RentDueCount { get; set; }
        public decimal RentDueAmount { get; set; }
        public decimal TotalOutstandingAmount { get; set; }
        public decimal TotalRentPaidTillDate { get; set; }
        public int TotalFamilyMembers { get; set; }
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
