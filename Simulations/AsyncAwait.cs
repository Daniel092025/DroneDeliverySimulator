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

        Task flight1 = FlyDroneAsync(drone1);
        Task flight2 = FlyDroneAsync(drone2);
        Task flight3 = FlyDroneAsync(drone3, shouldFailAtCheckpoint: 3);

        try
        {
            Console.WriteLine("🚁 Aktiverer droner!\n");

            await Task.WhenAll(flight1, flight2, flight3);

            Console.WriteLine("\n ▶️ Alle droner har fullført!");

        }

        catch (Exception)
        {
            var tasks = new [] {flight1, flight2, flight3};

            Console.WriteLine("\nDronene som feilet");
            foreach (var task in tasks.Where(t => t.IsFaulted))
            {
                Console.WriteLine($"-{task.Exception?.InnerException?.Message}");
            }
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