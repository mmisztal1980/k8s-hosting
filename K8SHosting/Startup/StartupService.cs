using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace K8SHosting.Startup
{
    public class StartupService : IHostedService
    {
        private readonly IMicroService service;
        private readonly IHostApplicationLifetime lifetime;
        private readonly ILogger<StartupService> logger;

        public StartupService(IMicroService service, IHostApplicationLifetime lifetime, ILogger<StartupService> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            lifetime.ApplicationStarted.Register(async () => await ExecuteHostedStartupServices().ConfigureAwait(false));

            return Task.FromResult(0);
        }

        private async Task ExecuteHostedStartupServices()
        {
            var svc = (MicroService)service;
            var svcs = svc.App.Services.GetServices<IHostedStartupService>();

            try
            {
                if (svcs != null)
                {
                    foreach (var s in svcs)
                    {
                        await s.StartAsync(default).ConfigureAwait(false);
                    }
                }

                logger.LogInformation("Service started successfully");
                
                svc.IsStarted = true;
                svc.IsReady = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Service failed to start");
                ((MicroServiceLifetime)svc.Lifetime).StartupFailedTokenSource.Cancel();

                Environment.ExitCode = -1;
                lifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}