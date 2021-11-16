using HotChocolate.Types;
using K8SHosting.NET6.GraphQL.Books;
using K8SHosting.NET6.GraphQL.WeatherForecasting;

namespace K8SHosting.NET6.GraphQL.Graph
{
    public class Query
    {
    }

    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field("books")
                .Type<ListType<BookType>>()
                .Resolve(ctx => ctx.Service<IBooksService>().GetBooks());

            descriptor
                .Field("weatherForecast")
                .Type<ListType<WeatherForecastType>>()
                .Resolve(ctx => ctx.Service<IWeatherForecastService>().GetWeatherForecast());
        }
    }
}
