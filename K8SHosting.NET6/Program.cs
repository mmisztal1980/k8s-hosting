using K8SHosting;
using K8SHosting.NET6.Startup;
using K8SHosting.Startup;

var service = new MicroService("k8s-hosting-net6")
    .ConfigureServices(services =>
    {
        services.AddHostedStartupService<PopulateCacheStartupService>();
    })
    .ConfigureWebApiPipeline();

await service.RunAsync(args);
