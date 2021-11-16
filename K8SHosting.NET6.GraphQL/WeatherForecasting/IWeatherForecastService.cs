namespace K8SHosting.NET6.GraphQL.WeatherForecasting
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}
