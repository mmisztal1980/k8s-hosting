using K8SHosting;
using K8SHosting.NET6.GraphQL.Books;
using K8SHosting.NET6.GraphQL.Graph;
using K8SHosting.NET6.GraphQL.WeatherForecasting;

var service = new MicroService("k8s-hosting-net6-graphql")
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBooksService, BooksService>();
        services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    })
    .ConfigureGraphQLPipeline((schema) => {
        schema
            .AddQueryType<QueryType>();
    });

await service.RunAsync(args);