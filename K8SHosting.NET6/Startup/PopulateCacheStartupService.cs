using K8SHosting.Startup;

namespace K8SHosting.NET6.Startup
{
    public class PopulateCacheStartupService : HostedStartupService<PopulateCacheStartupService>
    {
        public PopulateCacheStartupService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {

        }
        protected override async Task OnStartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Populating cache...");
            await Task.Delay(10000);
            logger.LogInformation("Done!");
        }
    }
}
