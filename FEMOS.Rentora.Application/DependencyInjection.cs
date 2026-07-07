using FEMOS.Rentora.Application.Identity;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Application.Services;
using FEMOS.Rentora.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT token generator
            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            // Repositories
            services.AddDataRepositories(configuration);

            // Domain services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEncryptDecryptService, EncryptDecryptService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITermsService, TermsService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<ICashfreeService, CashfreeService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IUnitService, UnitService>();

            return services;
        }
    }
}
