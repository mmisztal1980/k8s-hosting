using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace K8SHosting.Shutdown
{
    public class ShutdownService : IHostedService
    {
        private readonly IActiveRequestsService service;
        private readonly IHostApplicationLifetime lifetime;
        private readonly ILogger<ShutdownService> logger;
        
        private const int DefaultTimeoutSeconds = 30;

        public ShutdownService(IActiveRequestsService service, IHostApplicationLifetime lifetime, ILogger<ShutdownService> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            lifetime.ApplicationStopping.Register(async () => await ExecuteGracefulShutdown(DefaultTimeoutSeconds).ConfigureAwait(false));

            return Task.FromResult(0);
        }

        private async Task ExecuteGracefulShutdown(int timeout)
        {            
            if(await TaskExtensions.TryWaitUntil(() => !service.HasActiveRequests, frequency: 25.Milliseconds(), timeout: 30.Seconds()).ConfigureAwait(false))
            {
                logger.LogInformation("Service drained successfully");
            }
            else
            {
                logger.LogError($"Failed to drain pending requests within {timeout} [s]");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}