using Microsoft.AspNetCore.Builder;

namespace IdentityServer.Middlewares
{
    public static class InstallerExtensions
    {
        public static IApplicationBuilder UseInstaller(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstallerMiddleware>();
        }
    }
}
