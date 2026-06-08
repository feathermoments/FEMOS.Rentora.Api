using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class AppSettingConstants
    {
        public const string DbDefaultConnectionString = "ConnectionStrings:DefaultConnection";
        public const string jwtAuthenticationEnabled = "AuthConfig:jwtAuthenticationEnabled";
        public const string BypassAuthentication = "AuthConfig:BypassAuthentication";
        public const string JwtSecretKey = "JwtConfig:SecretKey";
        public const string JwtExpiryTime = "JwtConfig:ExpiryTime";
        public const string JwtIssuer = "JwtConfig:Issuer";
        public const string JwtAudience = "JwtConfig:Audience";
        public const string CorsAllowedUrls = "CorsAllowedUrls";
        public const string CorsAllowedOrigins = "Cors:AllowedOrigins";
        public const string AppBaseUrl = "AppBaseUrl";
        public const string FallbackBaseUrl = "https://votera.feathermoments.com/workspace";
    }
}
