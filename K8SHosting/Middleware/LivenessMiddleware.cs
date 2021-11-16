using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace K8SHosting.Middleware
{
    public class LivenessMiddleware
    {
        public const string Endpoint = "/status/liveness";

        private readonly RequestDelegate next;
        private readonly LivenessResponse response;

        public LivenessMiddleware(IMicroService service, RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));

            response = new LivenessResponse(service);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET" && context.Request.Path == Endpoint)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(response, Serialization.JsonOptions.DefaultIndented);
                return;
            }

            await next.Invoke(context);
        }
    }
}
