using CollabDo.Application;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Services;
using CollabDo.Infrastructure;
using CollabDo.Infrastructure.Configuration;
using CollabDo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Patient.Portal.Infrastructure;

namespace CollabDo.Web
{
    internal static class Bootstrapper
    {
        internal static void RegisterDependencies(this IServiceCollection services, AppConfiguration configuration)
        {
            SetConfiguration(services, configuration);
            RegisterServices(services);
            RegisterDatabase(services, configuration);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserContext, HttpUserContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        private static void RegisterDatabase(IServiceCollection services, AppConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(configuration.PostgresDataBase.ConnectionString), ServiceLifetime.Transient);
        }

        private static void SetConfiguration(IServiceCollection services, AppConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton(configuration.AuthTokenValidation);

        }
    }
}
