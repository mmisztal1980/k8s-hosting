using K8SHosting.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace K8SHosting
{
    public static partial class MicroServiceExtensions
    {
        public static IMicroService ConfigureServices(this IMicroService service, [NotNull]Action<IServiceCollection> action)
        {
            var svc = (service as MicroService);
            svc.ConfigureActions.Add(action);

            return service;
        }
        public static IMicroService ConfigureWebApiPipeline(this IMicroService service)
        {
            var svc = (service as MicroService);

            if (svc.Mode != MicroServiceMode.NotSet) 
            {
                throw new InvalidOperationException($"MicroService {nameof(svc.Mode)} is already set");
            }

            svc.ConfigureActions.Add((svc) => {
                svc.AddControllers();
                svc.AddEndpointsApiExplorer();
                svc.AddSwaggerGen();
                svc.AddAuthorization();
            });

            svc.ConfigurePipeline = (app) =>
            {
                app.UseMiddleware<LivenessMiddleware>();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseMiddleware<StartupMiddleware>();
                app.UseMiddleware<ReadinessMiddleware>();

                app.UseAuthorization();

                app.MapControllers();
            };

            svc.Mode = MicroServiceMode.WebApi;

            return service;
        }
    }
}
