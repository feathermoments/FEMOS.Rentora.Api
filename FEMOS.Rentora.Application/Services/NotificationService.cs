using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<BaseResponseInfo> SaveUserToken(UserTokenRequestInfo objRequestInfo)
        {
            try
            {
                DBResponseInfo dbResponse = await _notificationRepository.SaveUserToken(objRequestInfo);
                return new BaseResponseInfo { Status = dbResponse.Status, Message = dbResponse.Message };
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<NotificationInfo>> GetUserNotificationsAsync(Guid userPublicId)
        {
            return await _notificationRepository.GetUserNotificationsAsync(userPublicId);
        }

        public async Task<BaseResponseInfo> SaveUserNotificationReadFlagAsync(Guid userPublicId, long notificationId)
        {
            DBResponseInfo dbResponse = await _notificationRepository.SaveUserNotificationReadFlagAsync(userPublicId, notificationId);
            return new BaseResponseInfo { Status = dbResponse.Status, Message = dbResponse.Message };
        }
    }
}
