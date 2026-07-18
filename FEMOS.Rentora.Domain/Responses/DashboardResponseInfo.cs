namespace FEMOS.Rentora.Domain.Responses
{
    public class DashboardResponseInfo : BaseResponseInfo
    {
        public List<DashboardWidgetResponseInfo> Widgets { get; set; } = new List<DashboardWidgetResponseInfo>();
    }
}
