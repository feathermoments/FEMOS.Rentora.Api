using FEMOS.Rentora.Infrastructure.Connections;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Infrastructure.Persistance;
using FEMOS.Rentora.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // DB connection factory
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlDbConnectionFactory(configuration));

            // DB helper
            services.AddScoped<IDBHelper, DBHelper>();

            // Domain services
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITermsRepository, TermsRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRentRepository, RentRepository>();
            return services;
        }
    }
}
