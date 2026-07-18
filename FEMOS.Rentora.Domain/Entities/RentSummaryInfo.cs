namespace FEMOS.Rentora.Domain.Entities
{
    public class RentSummaryInfo
    {
        public decimal ExpectedRent { get; set; }
        public decimal CollectedRent { get; set; }
        public decimal PendingRent { get; set; }
        public decimal OverdueRent { get; set; }
        public int InvoiceCount { get; set; }
        public int PaidInvoices { get; set; }
        public int PendingInvoices { get; set; }
        public int OverdueInvoices { get; set; }
        public decimal CollectionPercentage { get; set; }
    }
}
