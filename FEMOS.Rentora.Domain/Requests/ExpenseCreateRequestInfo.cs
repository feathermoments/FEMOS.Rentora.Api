using FEMOS.Rentora.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class ExpenseCreateRequestInfo : BaseRequestInfo
    {
        public Guid PropertyPublicId { get; set; }
        public Guid? UnitPublicId { get; set; }
        public Guid? TenantPublicId { get; set; }
        public Guid? MaintenanceRequestPublicId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseTitle { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string VendorName { get; set; }
        public string? InvoiceUrl { get; set; }
        public string Notes { get; set; }
    }
}
