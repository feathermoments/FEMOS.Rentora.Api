namespace FEMOS.Rentora.Domain.Entities
{
    public class DepositTransactionInfo
    {
        public long DepositTransactionId { get; set; }
        public Guid UniqueId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long DepositTransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public string TransactionCode { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
