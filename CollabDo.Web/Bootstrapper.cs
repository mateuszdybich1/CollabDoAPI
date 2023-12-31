﻿using CollabDo.Application;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Services;
using CollabDo.Web;
using CollabDo.Web.Configuration;
using CollabDo.Web.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CollabDo.Web
{
    internal static class Bootstrapper
    {
        internal static void RegisterDependencies(this IServiceCollection services, AppConfiguration configuration)
        {
            SetConfiguration(services, configuration);
            RegisterServices(services, configuration);
            RegisterDatabase(services, configuration);
        }


        private static void RegisterServices(IServiceCollection services, AppConfiguration configuration)
        {
            services.AddScoped<IUserRepository>(p =>
            {
                IHttpClientFactory httpClientFactory = p.GetRequiredService<IHttpClientFactory>();

                HttpClient httpClient = httpClientFactory.CreateClient("KeycloakClient");
                return new UserRepository(httpClient, configuration.AuthTokenValidation);
            });

            services.AddScoped<IUserContext, HttpUserContext>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();


            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<ILeaderService, LeaderService>();

            services.AddScoped<IEmployeeRequestRepository, EmployeeRequestRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();



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
