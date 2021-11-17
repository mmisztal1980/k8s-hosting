using K8SHosting.Shutdown;
using K8SHosting.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K8SHosting
{
    public class MicroService : IMicroService
    {
        private readonly ConcurrentDictionary<string, bool> microserviceFlags = new ConcurrentDictionary<string, bool>();
        public IMicroServiceLifetime Lifetime { get; } = new MicroServiceLifetime();

        public MicroService(string name)
        {
            Name = name;
            Id = System.Guid.NewGuid().ToString();
            IsReady = false;
            IsStarted = false;

            ConfigureActions.Add((svc) => {
                svc.AddSingleton<IMicroService>(this);
                svc.AddSingleton<IActiveRequestsService, ActiveRequestsService>();
                svc.AddHostedService<StartupService>();
                svc.AddHostedService<ShutdownService>();

                svc.Configure<HostOptions>(opts => opts.ShutdownTimeout = 60.Seconds());
            });            
        }

        public string Id { get; init; }

        public string Name { get; init; }

        /// <summary>
        /// Flag indicating whether the IMicroservice is ready to receive traffic
        /// </summary>
        public bool IsReady
        {
            get => IsStarted && this.microserviceFlags[nameof(IsReady)];

            set => this.microserviceFlags[nameof(IsReady)] = value;
        }

        /// <summary>
        /// Flag indicating whether the IMicroservice has completed it's startup cycle
        /// </summary>
        public bool IsStarted
        {
            get => this.microserviceFlags[nameof(IsStarted)];

            set
            {
                this.microserviceFlags[nameof(IsStarted)] = value;

                if (value == true) ((MicroServiceLifetime)this.Lifetime).ServiceStartedTokenSource.Cancel();
            }
        }

        public async Task RunAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            ConfigureActions.ForEach(action => action(builder.Services));

            App = builder.Build();

            if(ConfigurePipeline == null)
            {
                throw new InvalidOperationException("Unconfigured pipeline. Ensure a pipeline is configured before calling RunAsync");
            }

            ConfigurePipeline(App);

            await App.RunAsync();
        }


        public WebApplication App { get; internal set; }

        internal List<Action<IServiceCollection>> ConfigureActions { get; } = new List<Action<IServiceCollection>>();
        internal Action<WebApplication> ConfigurePipeline { get; set; } = null;

        public MicroServiceMode Mode { get; internal set; } = MicroServiceMode.NotSet;
    }
}
