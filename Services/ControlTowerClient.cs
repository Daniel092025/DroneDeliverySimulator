using System.Net.Http.Json;
using System.Text.Json;
using Models;

namespace Services;

public class ControlTowerClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.open-meteo.com/v1/forecast";

    public ControlTowerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    /// <summary>
    /// Gets weather data for a location (affects drone delays)
    /// </summary>
    /// <param name="latitude">Latitude of flight zone</param>
    /// <param name="longitude">Longitude of flight zone</param>
    
    public async Task <WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
    {
        try
        {
            Console.WriteLine($"Kontakter Kontrolltårnet for værdata...");

            var url = $"{BaseUrl}?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m,weather_code";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();

            if(weatherData?.Current != null)
            {
                var condition = WeatherInterpreter.GetCondition(weatherData.Current.WeatherCode);
                Console.WriteLine($"Værmelding motatt: {condition}, {weatherData.Current.Temperature}°C, Wind: {weatherData.Current.WindSpeed} km/t");
            }
            return weatherData;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Nettverk Error: {ex.Message}");
            return null;
        }
        catch(TaskCanceledException)
        {
            Console.WriteLine($"Request Timeout: Kontrolltårnet svarer ikke!");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Feilet med å parse værdata: {ex.Message}");
            return null;
        }
    }
}
