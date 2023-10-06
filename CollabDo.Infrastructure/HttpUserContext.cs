using CollabDo.Application;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace CollabDo.Infrastructure
{
    public class HttpUserContext : IUserContext
    {

        private readonly ClaimsPrincipal _user;

        public Guid CurrentUserId
        {
            get
            {
                string id = GetIdFromClaims();
                return Guid.Parse(id);
            }
        }


        public HttpUserContext(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext != null)
            {
                _user = httpContext.HttpContext.User;
            }

        }

        public string GetIdFromClaims()
        {
            return _user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}