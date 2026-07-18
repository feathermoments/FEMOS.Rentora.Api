namespace FEMOS.Rentora.Domain.Responses
{
    public class DashboardWidgetResponseInfo
    {
        public string WidgetCode { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public object Data { get; set; }
    }
}
