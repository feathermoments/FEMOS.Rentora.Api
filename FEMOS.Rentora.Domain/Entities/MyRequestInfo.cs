namespace FEMOS.Rentora.Domain.Entities
{
    public class MyRequestInfo
    {
        public MyRequestSummaryInfo objMyRequestSummaryInfo { get; set; }
        public List<MyRequestDetailInfo> objMyRequestDetails { get; set; }
    }
    public class MyRequestSummaryInfo
    {
        public int TotalRequests { get; set; }
        public int OpenRequests { get; set; }
        public int InProgressRequests { get; set; }
        public int ResolvedRequests { get; set; }
        public int ClosedRequests { get; set; }
        public int HighPriorityRequests { get; set; }
        public double AverageResolutionHours { get; set; }
    }
    public class MyRequestDetailInfo
    {
        public long RequestId { get; set; }
        public string ServiceTypeName { get; set; }
        public string Priority { get; set; }
        public string RequestStatus { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public double ResolutionHours { get; set; }
    }
}
