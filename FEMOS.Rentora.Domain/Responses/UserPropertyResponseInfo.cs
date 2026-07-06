using FEMOS.Rentora.Domain.Entities;

namespace FEMOS.Rentora.Domain.Responses
{
    public class UserPropertyResponseInfo : DBResponseInfo
    {
        public long? PropertyId { get; set; }
        public UserPropertyInfo objUserPropertyInfo { get; set; }
    }
}
