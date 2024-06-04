using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using NexaShopsBackend.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace NexaShopsBackend.Web.Endpoints;

public class WeatherForecasts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetWeatherForecasts);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }

}
