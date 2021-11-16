using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace K8SHosting.Middleware
{
    public class ReadinessMiddleware
    {
        public const string Endpoint = "/status/readiness";

        private readonly RequestDelegate next;
        private readonly IMicroService service;

        public ReadinessMiddleware(IMicroService service, RequestDelegate next, ILogger<ReadinessMiddleware> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET" && context.Request.Path == Endpoint)
            {
                context.Response.StatusCode = service.IsReady ? 200 : 503;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new ReadinessResponse(service), Serialization.JsonOptions.DefaultIndented);
                return;
            }

            if (service.IsReady || context.Request.Path.StartsWithSegments("/dapr", StringComparison.OrdinalIgnoreCase))
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 503;
            }
        }
    }
}
