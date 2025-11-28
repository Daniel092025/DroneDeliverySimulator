using System;
using System.ComponentModel.DataAnnotations;
using Models;

namespace Simulations;

public class AsyncAwait
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Async orchestration Async // Await ===");

        var drone1 = new DroneModel("Foxtrot", 5 , 400);
        var drone2 = new DroneModel("Golf", 5, 300);
        var drone3 = new DroneModel("Hotel", 5 , 250);

        try
        {
            Console.WriteLine("🚁 Aktiverer droner!\n");

            Task flight1 = FlyDroneAsync(drone1);
            Task flight2 = FlyDroneAsync(drone2);
            Task flight3 = FlyDroneAsync(drone3, shouldFailAtCheckpoint: 3);

            await Task.WhenAll(flight1, flight2, flight3);

            Console.WriteLine("\n ▶️ Alle droner har fullført!");

        }

        catch (Exception ex)
        {
            Console.WriteLine($"\n Dronene feilet: {ex.Message}");
            Console.WriteLine("Andre droner kan også ha feilet, men vi ser bare første exception");
        }
    }

    private static async Task FlyDroneAsync(DroneModel drone, int? shouldFailAtCheckpoint = null)
    {
        Console.WriteLine($"{drone.Name} Tar av!");

        for(int checkpoint = 1; checkpoint <= drone.MaxCheckPoints; checkpoint++)
        {
            await Task.Delay(drone.DelayMs);

         if (shouldFailAtCheckpoint.HasValue && checkpoint == shouldFailAtCheckpoint.Value)
         {
            throw new InvalidOperationException($"[{drone.Name}] 💥 Motorfeil ved checkpoint {checkpoint}!");
         }
            Console.WriteLine($"[{drone.Name}] Checkpoint {checkpoint}/{drone.MaxCheckPoints}");
        }

        Console.WriteLine($"[{drone.Name}] Landet trygt");
    }
    
  
}