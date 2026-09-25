using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class ExpenseInfo
    {
        public int TotalRecords { get; set; }
        public Guid ExpensePublicId { get; set; }
        public Guid PropertyPublicId { get; set; }
        public string PropertyName { get; set; }
        public Guid? UnitPublicId { get; set; }
        public string UnitName { get; set; }
        public Guid? TenantPublicId { get; set; }
        public string TenantName { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public Guid? MaintenanceRequestPublicId { get; set; }
        public string ExpenseTitle { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string VendorName { get; set; }
        public string InvoiceUrl { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
