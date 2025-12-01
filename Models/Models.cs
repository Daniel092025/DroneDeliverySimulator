using System.Dynamic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Models;

public class DroneModel
{
    public string Name {get; set;}
    public int MaxCheckPoints {get; set;}
    public int DelayMs {get; set;}

    public DroneModel (string name, int maxCheckPoints, int delayMs)
    {
        Name = name;
        MaxCheckPoints = maxCheckPoints;
        DelayMs = delayMs;
    }
}

public class WeatherResponse
{
    public CurrentWeather? Current {get; set;}
}

public class CurrentWeather
{
    public double Temperature {get; set;}
    public double WindSpeed {get; set;}
    public int WeatherCode {get; set;}
}

public static class WeatherInterpreter
{
    public static string GetCondition(int code)
    {
        return code switch
        {
            0 => "clear",
            1 or 2 or 3  => "Partly cloudy",
            45 or 48 => "Foggy",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            71 or 73 or 75 => "Snow",
            95 or 96 or 99 => "Thunderstorm",
            _ => "Unknown"
        };

    }

    public static int GetDelayMultiplier(int code)
    {
        return code switch
        {
            0 => 1,
            1 or 2 or 3 => 1,
            45 or 48 => 2,
            51 or 53 or 55 => 2,
            61 or 63 or 65 => 3,
            71 or 73 or 75 => 4,
            95 or 96 or 99 => 5,
            _ => 2
        };
    }
}