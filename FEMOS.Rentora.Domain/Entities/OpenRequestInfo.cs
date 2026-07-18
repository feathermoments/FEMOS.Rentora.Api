namespace FEMOS.Rentora.Domain.Entities
{
    public class OpenRequestInfo
    {
        public int TotalRequests { get; set; }
        public int OpenRequests { get; set; }
        public int InProgressRequests { get; set; }
        public int HighPriority { get; set; }
        public int MediumPriority { get; set; }
        public int LowPriority { get; set; }
        public int OverdueRequests { get; set; }
        public int TodayRequests { get; set; }
        public int CompletedThisMonth { get; set; }
    }
}