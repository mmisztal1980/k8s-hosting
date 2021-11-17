using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using K8SHosting.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace K8SHosting
{
    public static partial class MicroServiceExtensions
    {
        public static IMicroService ConfigureGraphQLPipeline(this IMicroService service, Action<IRequestExecutorBuilder> schemaBuilder)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (schemaBuilder == null) throw new ArgumentNullException(nameof(schemaBuilder));

            var svc = (service as MicroService);

            if (svc.Mode != MicroServiceMode.NotSet)
            {
                throw new InvalidOperationException($"MicroService {nameof(svc.Mode)} is already set");
            }

            svc.ConfigureActions.Add((svc) => {
                svc.AddAuthorization();
            });

            svc.ConfigureActions.Add((svc) => {
                var server = svc.AddGraphQLServer();
                schemaBuilder(server);

                server.ModifyRequestOptions(opt => opt.IncludeExceptionDetails = service.App.Environment.IsDevelopment());
            });

            svc.ConfigurePipeline = (app) =>
            {
                app.UseMiddleware<LivenessMiddleware>();

                if (app.Environment.IsDevelopment())
                {
                    
                }

                app.UseMiddleware<StartupMiddleware>();
                app.UseMiddleware<ReadinessMiddleware>();
                app.UseMiddleware<ActiveRequestsMiddleware>();

                app.UseAuthorization();

                app.MapGraphQL("/graphql");
                app.UseVoyager(new VoyagerOptions()
                {
                    Path = "/graphql-voyager",
                    QueryPath = "/graphql"
                });

            };

            svc.Mode = MicroServiceMode.WebApi;

            return service;
        }
    }
}
