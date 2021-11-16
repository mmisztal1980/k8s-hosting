using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace K8SHosting.Middleware
{
    public class StartupMiddleware
    {
        public const string Endpoint = "/status/startup";

        private readonly RequestDelegate next;
        private readonly IMicroService service;

        public StartupMiddleware(IMicroService service, RequestDelegate next)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET" && context.Request.Path == Endpoint)
            {
                context.Response.StatusCode = service.IsStarted ? 200 : 503;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new StartupResponse(service),
                    Serialization.JsonOptions.DefaultIndented);

                return;
            }

            await next.Invoke(context);
        }
    }
}
