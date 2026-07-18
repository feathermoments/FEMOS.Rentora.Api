namespace FEMOS.Rentora.Domain.Entities
{
    public class ReportSummaryInfo
    {
        public string ReportName { get; set; }
        public DateTime GeneratedDate { get; set; }
        public int RecordCount { get; set; }
        public string ReportUrl { get; set; }
    }
}
