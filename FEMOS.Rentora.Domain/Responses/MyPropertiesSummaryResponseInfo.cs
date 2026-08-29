using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class MyPropertiesSummaryResponseInfo : BaseResponseInfo
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
}
