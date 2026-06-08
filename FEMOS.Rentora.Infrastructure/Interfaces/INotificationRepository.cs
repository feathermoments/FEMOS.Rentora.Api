using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface INotificationRepository
    {
        Task<DBResponseInfo> SaveUserToken(UserTokenRequestInfo objRequestInfo);
        Task<List<NotificationInfo>> GetUserNotificationsAsync(Guid userPublicId);
        Task<DBResponseInfo> SaveUserNotificationReadFlagAsync(Guid userPublicId, long notificationId);
    }
}
