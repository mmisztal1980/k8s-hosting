using Microsoft.Extensions.DependencyInjection;

namespace K8SHosting.Startup
{
    public static class HostedStartupServiceExtensions
    {
        public static IServiceCollection AddHostedStartupService<T>(this IServiceCollection services)
            where T : class, IHostedStartupService
        {
            return services.AddSingleton<IHostedStartupService, T>();
        }
    }
}
