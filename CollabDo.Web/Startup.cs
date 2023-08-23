using CollabDo.Infrastructure;
using CollabDo.Infrastructure.Configuration;
using CollabDo.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Web
{
    public class Startup
    {
        private AppConfiguration _appConfiguration;
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;

            _appConfiguration = new AppConfiguration();
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCustomSwagger();
            services.AddHttpClient("KeycloakClient", client =>
            {
                client.BaseAddress = new Uri($"{_appConfiguration.AuthTokenValidation.ServerAddress}/auth/admin/realms/{_appConfiguration.AuthTokenValidation.Realm}");
            });
            services.AddHttpContextAccessor();
            services.Configure<AppConfiguration>(Configuration);
            services.RegisterDependencies(_appConfiguration);
            services.AddMemoryCache();
            services.AddAuthorization(_appConfiguration.AuthTokenValidation, CurrentEnvironment);
            services.ConfigureRouteOptions();
            services.ConfigureApiControllers();
            services.AddMvc(options =>
            {
                options.Conventions.Add(new ControllerRouteExtension());
            });
            

        }
        

        public void Configure(IApplicationBuilder app, AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
