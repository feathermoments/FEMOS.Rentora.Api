namespace FEMOS.Rentora.Domain.Entities
{
    public class StaffSummaryInfo
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
