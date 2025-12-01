using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Models;
using Services;

namespace Simulations;

public class ControlTower
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Kontrolltårn API test(HTTPclient + ekstern data) ===\n");

        using var httpClient = new HttpClient();
        var controlTower = new ControlTowerClient(httpClient);

        double latitude = 59.9139;
        double longitude = 10.7522;

        var weather = await controlTower.GetWeatherAsync(latitude, longitude);

        if (weather?.Current == null)
        {
            Console.WriteLine("\n💥 Feilet med å hente værdata. Går til default verdi. \n");
            await RunWithoutWeather();
            return;
        }

        int delayMultiplier = WeatherInterpreter.GetDelayMultiplier(weather.Current.WeatherCode);
        string condition = WeatherInterpreter.GetCondition(weather.Current.WeatherCode);

        Console.WriteLine($"Flytilstand {condition}");
        Console.WriteLine($"Forsinkelsesmultiplikator {delayMultiplier}");

        var drone1 = new DroneModel("India", 5, 200 * delayMultiplier);
        var drone2 = new DroneModel("Juliet", 5, 300 * delayMultiplier);
        var drone3 = new DroneModel("Kilo", 5, 250 * delayMultiplier);

        try
        {
            Console.WriteLine("🚁Aktiverer droner med justeringer for værdata🚁");

            await Task.WhenAll(
                FlyDroneAsync(drone1),
                FlyDroneAsync(drone2),
                FlyDroneAsync(drone3)
            );

            Console.WriteLine("\n Alle dronene fullførte rutene deres!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n Oppdrag feilet: {ex.Message}");
        }
    }

    private static async Task RunWithoutWeather()
    {
        var drone1 = new DroneModel("India", 5, 300);
        var drone2 = new DroneModel("Juliet", 5, 400);

        Console.WriteLine("Aktiverer droner med standard hastighet");

        await Task.WhenAll(
            FlyDroneAsync(drone1),
            FlyDroneAsync(drone2)
        );

        Console.WriteLine("\n Alle dronene har fullført");
    }

    private static async Task FlyDroneAsync(DroneModel drone)
    {
        Console.WriteLine($"[{drone.Name}] 🚁 Er aktivert!..");

        for (int checkpoint = 1; checkpoint <= drone.MaxCheckPoints; checkpoint++)
        {
            await Task.Delay(drone.DelayMs);
            Console.WriteLine($"[{drone.Name}] Checkpoint {checkpoint}/{drone.MaxCheckPoints}");
        }
        Console.WriteLine($"[{drone.Name}] Landet trygt!");
    }
}
