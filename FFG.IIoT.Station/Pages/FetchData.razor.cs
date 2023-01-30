namespace IIoT.Station.Pages;
public partial class FetchData
{
    WeatherForecast[]? forecasts;
    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
    }
}