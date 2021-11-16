namespace GraphQLTest.WeatherForecasting
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}
