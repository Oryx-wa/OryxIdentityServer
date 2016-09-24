using IdentityServer.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Middlewares
{
    public class InstallerMiddleware
    {
        private readonly RequestDelegate _next;

        public InstallerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var dbContext = context.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            if (dbContext.Users.Count() == 0)
            {
                if (!context.Request.Path.ToString().Contains("/Install"))
                {
                    context.Request.Path = "/Install";
                }
            }
            else
            {
                if (context.Request.Path.ToString().Contains("/Install"))
                {
                    context.Request.Path = "/";
                }
            }

            return _next(context);
        }
    }
}
