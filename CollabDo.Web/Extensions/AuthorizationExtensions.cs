using CollabDo.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;

namespace CollabDo.Web.Extensions
{
    internal static class AuthorizationExtensions
    {
        public static void AddAuthorization(this IServiceCollection services, AuthConfiguration auth)
        { 
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.Audience = auth.ClientId;
                     options.Authority = $"{auth.ServerAddress}/auth/realms/{auth.Realm}";
                     options.RequireHttpsMetadata = false;
                 });
        }

      
    }
}

