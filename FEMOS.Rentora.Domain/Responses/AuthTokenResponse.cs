using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class UserInfoDto
    {
        public Guid UserId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; }
    }

    public class AuthTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserInfoDto User { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}

