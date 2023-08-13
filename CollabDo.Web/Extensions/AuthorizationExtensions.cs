using CollabDo.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;

namespace CollabDo.Web.Extensions
{
    internal static class AuthorizationExtensions
    {
        public static void AddAuthorization(this IServiceCollection services, AuthConfiguration auth, IWebHostEnvironment env)
        {
            bool requireHttpsMetadata = !env.IsDevelopment();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.Audience = auth.ClientId;
                     options.Authority = $"{auth.ServerAddress}/auth/realms/{auth.Realm}";
                     options.RequireHttpsMetadata = requireHttpsMetadata;
                 });
        }

        private static bool ApiForwardSelector(HttpContext context, out string scheme)
        {
            if (context.IsApiRequest())
            {
                scheme = JwtBearerDefaults.AuthenticationScheme;
                return true;
            }
            scheme = null;
            return false;
        }

        private static bool IsApiRequest(this HttpContext context) => context.Request.IsApiRequest();
        private static bool IsApiRequest(this HttpRequest request) => request.Path.StartsWithSegments("/api");

        

        private static void MapResourceAccessClaim(this ClaimActionCollection collection)
        {
            const string propertyName = "resource_access";
            collection.MapCustomJson(propertyName, json =>
            {
                if (json.TryGetProperty(propertyName, out JsonElement property))
                {
                    return property.GetRawText();

                }
                return string.Empty;
            });
        }
    }
}

