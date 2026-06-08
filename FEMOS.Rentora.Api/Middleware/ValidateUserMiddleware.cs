using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FEMOS.Rentora.Api.Middleware
{
    public class ValidateUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _skipPaths;

        public ValidateUserMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _skipPaths = configuration.GetSection("UserValidation:SkipPaths").Get<List<string>>() ?? new List<string>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLowerInvariant();

            // Skip validation for anonymous/public routes (after sanitization above)
            if (_skipPaths.Any(p => path.StartsWith(p)))
            {
                await _next(context);
                return;
            }

            // Only apply for authenticated users
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // look up user id from a variety of possible claim types
                string? userFromToken = null;
                var claimTypesToCheck = new[] { "userPublicId", "userid", ClaimTypes.NameIdentifier, JwtRegisteredClaimNames.Sub, JwtRegisteredClaimNames.NameId };
                foreach (var ct in claimTypesToCheck)
                {
                    var c = context.User.FindFirst(ct);
                    if (c != null && !string.IsNullOrEmpty(c.Value))
                    {
                        userFromToken = c.Value;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(userFromToken))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("User ID not found in token.");
                    return;
                }

                var requestUserPublicId = await GetUserPublicIdFromRequest(context);

                if (!string.IsNullOrEmpty(requestUserPublicId) && requestUserPublicId != userFromToken)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("UserPublicId in request does not match token.");
                    return;
                }

                // Add UserPublicId to context for controller access
                context.Items["UserPublicId"] = userFromToken;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized user token.");
                return;
            }

            await _next(context);
        }

        public async Task<string?> GetUserPublicIdFromRequest(HttpContext context)
        {
            string? UserPublicId = null;

            // First try query (for GET, optional)
            var q = context.Request.Query;
            if (q.ContainsKey("UserPublicId") && !string.IsNullOrEmpty(q["UserPublicId"]))
                return q["UserPublicId"].ToString();

            if (q.ContainsKey("UserName") && !string.IsNullOrEmpty(q["UserName"]))
                return q["UserName"].ToString();

            // If request has a body (for POST, PUT)
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering(); // allows body to be read multiple times

                using var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true);

                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // reset position for downstream

                if (!string.IsNullOrEmpty(body))
                {
                    try
                    {
                        var json = JsonDocument.Parse(body);
                        if (json.RootElement.TryGetProperty("UserPublicId", out var uid))
                            return uid.GetString();

                        if (json.RootElement.TryGetProperty("UserName", out var uname))
                            return uname.GetString();
                    }
                    catch
                    {
                        // ignore parse errors — not a JSON body we can inspect
                    }
                }
            }

            return null;
        }
    }
}
