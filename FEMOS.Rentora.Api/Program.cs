using FEMOS.Rentora.Api.Filters;
using FEMOS.Rentora.Api.Middleware;
using FEMOS.Rentora.Application;
using FEMOS.Rentora.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -- Services ------------------------------------------------------------------
builder.Services.AddControllers(options =>
{
    // Apply model validation filter globally to every controller action
    options.Filters.Add<ValidateModelFilter>();
});

// Load configuration
IConfigurationRoot config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton<IConfigurationRoot>(provider => config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config[AppSettingConstants.JwtIssuer],
            ValidAudience = config[AppSettingConstants.JwtAudience],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config[AppSettingConstants.JwtSecretKey]!)
            )
        };
    });

// Registers: Infrastructure DI, CORS, Swagger Bearer
builder.Services.AddApplicationServices(builder.Configuration);

//builder.WebHost.UseUrls("http://localhost:5044");

// Configure CORS
var corsUrls = config.GetValue<string>(AppSettingConstants.CorsAllowedUrls)?.Split(',');
if (builder.Environment.IsProduction() && (corsUrls == null || corsUrls.Length == 0))
{
    throw new Exception("CORS URLs must be set in production.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApiConstants.DefaultCorsPolicy, policy =>
    {
        policy.WithOrigins(corsUrls)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// -- Pipeline -------------------------------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rentora API v1"));
}

app.UseHttpsRedirection();

app.UseCors(ApiConstants.DefaultCorsPolicy);

// JWT must be before the custom middleware so context.User is populated
app.UseAuthentication();
app.UseAuthorization();

// Custom middleware: validates UserPublicId claim against request body/query
app.UseMiddleware<ValidateUserMiddleware>();

app.MapControllers();

app.MapGet("api/healthcheck", () =>
{
    return $"Connected: {config.GetSection("Environment").Value}";
})
.WithName("")
.WithOpenApi();

await app.RunAsync();
