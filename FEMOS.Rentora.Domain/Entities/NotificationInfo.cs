using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class NotificationInfo
    {
        public long NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public Guid SenderUserPublicId { get; set; }
        public Guid ReceiverUserPublicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string NotificationMessage { get; set; } = string.Empty;
        public string NavigationLink { get; set; } = string.Empty;
        public string ReceivedOn { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string ReadOn { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
    }
}
