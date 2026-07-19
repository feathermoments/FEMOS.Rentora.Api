namespace FEMOS.Rentora.Domain.Entities
{
    public class MyHomeInfo
    {
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public long UnitId { get; set; }
        public string UnitNumber { get; set; }
        public string BHKType { get; set; }
        public int FloorNo { get; set; }
        public long RentAgreementId { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysRemaining { get; set; }
        public string AgreementHealth { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public string FullName { get; set; }
        public DateTime OccupancySince { get; set; }

        public PropertyMemberInfo objOwnerInfo { get; set; }
    }
}