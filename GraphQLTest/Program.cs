using GraphQLTest.Books;
using GraphQLTest.Graph;
using GraphQLTest.WeatherForecasting;
using HotChocolate.AspNetCore.Voyager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBooksService, BooksService>();
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

builder.Services
    .AddGraphQLServer()
        .AddQueryType<QueryType>();


var app = builder.Build();
app.MapGraphQL("/graphql");
app.UseVoyager(new VoyagerOptions()
{
    Path = "/graphql-voyager",
    QueryPath = "/graphql"
});


app.Run();
