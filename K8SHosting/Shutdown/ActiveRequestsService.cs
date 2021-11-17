using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace K8SHosting.Shutdown
{
    internal class ActiveRequestsService : IActiveRequestsService
    {
        private long counter = 0L;
        private readonly ILogger<ActiveRequestsService> logger;

        public long Counter 
        { 
            get 
            {
                return Interlocked.Read(ref counter);
            } 
        }

        public ActiveRequestsService(ILogger<ActiveRequestsService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool HasActiveRequests
        { 
            get
            {
                return Counter > 0;
            }
        }

        public void Decrement()
        {
            Interlocked.Decrement(ref counter);
            logger.LogInformation($"Active requests: {Interlocked.Read(ref counter)}");
        }

        public void Increment()
        {
            Interlocked.Increment(ref counter);
            logger.LogInformation($"Active requests: {Interlocked.Read(ref counter)}");
        }
    }
}
