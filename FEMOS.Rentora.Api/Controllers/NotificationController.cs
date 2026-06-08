using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/notification")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("register-token")]
        [HttpPatch]
        public async Task<IActionResult> SaveUserToken(UserTokenRequestInfo objRequestInfo)
        {
            try
            {
                var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
                if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                    return Unauthorized();

                objRequestInfo.UserPublicId = userPublicId;
                var objResponseInfo = await _notificationService.SaveUserToken(objRequestInfo);

                return Ok(objResponseInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>GET /api/notification/list</summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetUserNotifications()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _notificationService.GetUserNotificationsAsync(userPublicId);
            return Ok(result);
        }

        /// <summary>POST /api/notification/read</summary>
        [HttpPatch("read/{notificationId:long}")]
        public async Task<IActionResult> MarkAsRead(long notificationId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _notificationService.SaveUserNotificationReadFlagAsync(userPublicId, notificationId);
            if (result.Status == "Failure")
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>POST /api/notification/read-all</summary>
        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            // pass 0 to mark all as read
            var result = await _notificationService.SaveUserNotificationReadFlagAsync(userPublicId, 0);
            if (result.Status == "Failure")
                return BadRequest(result);
            return Ok(result);
        }

        //[HttpGet("access-token")]
        ////[AllowAnonymous]
        //public async Task<IActionResult> getAccessToken()
        //{
        //    const string jsonString = @"{
        //      'type': 'service_account',
        //      'project_id': 'votera-6ffda',
        //      'private_key_id': '78fd39e4285b9a0f28655d1c14cb430c18cca6b2',
        //      'private_key': '-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDXRBRlMTgx54pr\npPoTtCCrWPtd7t6QZFbxqjcGXK7sstAb57fuVyj2ClG16ZLG8GgsEJQqzOIkTE0r\nLuG2zV0INT5ulOmwRPntLvpjbHOoF9hRuI6gJrNj+OR4SKz7SBzytKGBV0vVtBk/\nqGqiUmyEO7RBxIKlRxWk4RWOSSBRVyNQUR+JmWbkInPwup8oW/BEgZ90j4a13LM0\n6+wM9nz4Sk0MCxP3gsCV6ErHoNSHwBGbVNz08Aco6xovHJUFwtbp7JEvVF8QYqdK\nQhmUnein/GAxDBD+JqPHY/JY8LBpuf0yLDUcGXAhoMZrRPeKeck/WlpjANyAgcau\njnkUBDsfAgMBAAECggEAN9omIItAtVIKDBvl9q7Juyt/LMQJYxVlqZYsaK4rhKZw\ncI0Sn/hlXFEZGkXcG1nM7YHvr5sxIZHag9XKIo/uD28hH7frWhzLa8rAlINTDs4B\nBlMITm08JjgxrzPMDfaL7D0JmnF3756m3mloIW2ZUkXbYyUFcLSc3Qa5fQ1kITYs\nyMExP6NnxW8V50w1hUb/biPfzwRL5R7rbVIAHsia1R0A57gUmPNy3C8qzYbfoPmW\nfdE61nhkdIpkJ5jaoUbkN0bbhWRMclXWFIYLW3gYkAvJ85n8gE/eMVnZAVn8xXSj\n8UlLZ0b46tKP3TYh6wvf0hkvaBpTmTrMLpRru8RSQQKBgQDxv/sG23rUgPuodBw9\nKjAoC52YyMheju5EPNdLHRnSknL6SrTHEqvXZXum1Lj2jPmhr7+OSli43+8gi4h3\nbpHXxG+GF9pCj3kItF52o3smwS5FTzoMvMeOWrrh4kwiVEXNymdGUCAvMppcdGFX\nmV0G2PZtVaTv2/+noHItNq32JwKBgQDj9HRMGLNyhIgAcgxVkJ75d0pP+hBXhlbo\nrB7IyqJoTzNPjXjNY+fGkUomIu/b7zHCXzZEvAGsJ+FJZuu0K+ondkJJHZ9M4ba5\n81m7XnICCPH3ezRbLhhYrFBDTVik+elavE0VFruGKTTdOyZc5eJcqCL8JfajDx+y\n4JMEf+7mSQKBgQDvvGGVEEyDthFaoSJr6Y1t+O7lV5+UXguc8fTS0V4NOxv4Z+NU\nU4jEByiTbfqqzWy00dOVqNbJJ5E85gKD5cuNfIjYIolYXm05m5zd7AsxiHt5UjMP\n9Jm06vbPEJpNpOLATIsT6FuyUm1PaD4sH7NoGfpuvHLfRn0F34+6lLH5GwKBgEE9\nANuWZiTsERAvk6AZV6YFodrpsiuaYt0MBXNpqrk3kXO/BUeiw/5sLlBjx0mmVxNN\ntHZPaBsg/rTHI8XYm5bXnXjXZXPXVTg6kP+ys+YpMTamqAFAW+9kwUPBqxDsBQDW\nyyix7NEkkeCOwRftIq9p3zlSlBduiJ/k7a5n/rMhAoGAWR4t9EFJ0SQX5JKWPEOn\nOumD98LFlZ6NVCmky1qiGB+1v1xNHfiNbhhhoUimDuXhI0ii275r+5h2xTePvxyj\n/WVm4VqC2HVhQZJqkErNypCUNE9FbKJKhWsBTkRDFEtFqgClWlGbqsJiYsqX48Gx\nTQa8P3CYg75PZj4+kknIbXY=\n-----END PRIVATE KEY-----\n',
        //      'client_email': 'firebase-adminsdk-fbsvc@votera-6ffda.iam.gserviceaccount.com',
        //      'client_id': '111587283841342867066',
        //      'auth_uri': 'https://accounts.google.com/o/oauth2/auth',
        //      'token_uri': 'https://oauth2.googleapis.com/token',
        //      'auth_provider_x509_cert_url': 'https://www.googleapis.com/oauth2/v1/certs',
        //      'client_x509_cert_url': 'https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-fbsvc%40votera-6ffda.iam.gserviceaccount.com',
        //      'universe_domain': 'googleapis.com'
        //    }";

        //    try
        //    {
        //        byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
        //        using (MemoryStream stream = new MemoryStream(byteArray))
        //        {
        //            GoogleCredential credential = GoogleCredential.FromStream(stream)
        //                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        //            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        //            return Ok(token);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
