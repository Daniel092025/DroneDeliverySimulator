using System;
using System.Runtime.CompilerServices;
using Models;

namespace Simulations;

public class TaskCompletion
{
    public static void Run()
    {
        Console.WriteLine(" === Tower Says Go! === ");

        var drone1 = new DroneModel ("Charlie", 5, 400);
        var drone2 = new DroneModel ("Delta", 5, 300);
        var drone3 = new DroneModel ("Echo", 5, -100);

        Task task1 = FlyDroneWithTCS (drone1);
        Task task2 = FlyDroneWithTCS (drone2);
        Task task3 = FlyDroneWithTCS (drone3);

        Console.WriteLine("🚁 Alle dronene har lettet! Venter på fullføring... \n");
    
        try
        {
            Task.WaitAll(task1, task2, task3);
            Console.WriteLine("\n Alle dronene har fullført");
        }
        catch (AggregateException ex)
        {
            Console.WriteLine("\n Noen droner feilet");
            foreach (var innerEx in ex.InnerExceptions)
            {
                Console.WriteLine($"   Error: {innerEx.Message}");
            }
        }
    }

    private static Task FlyDroneWithTCS (DroneModel drone)
    {
        var tcs = new TaskCompletionSource<bool>();

        Task.Run(() =>
        {
            try
            {
                if (drone.DelayMs < 0)
                {
                    throw new ArgumentException ($"[{drone.Name}] Ugyldig forsinkelse: {drone.DelayMs}");
                }
                Console.WriteLine($"[{drone.Name}] Tar av aka letter!");

                for (int checkpoint = 1; checkpoint <= drone.MaxCheckPoints; checkpoint++)
                {
                    if (drone.Name == "Delta" && checkpoint == 3)
                    {
                        throw new InvalidOperationException ($"[{drone.Name}] Motor feil ved checkpoint {checkpoint}");
                    }
                    Thread.Sleep(drone.DelayMs);
                    Console.WriteLine($"[{drone.Name}] Fullført Checkpoint {checkpoint}/{drone.MaxCheckPoints}");

                    //La til en ekstra failure//
                   
                    if (checkpoint == 2 && Random.Shared.Next(0, 2) == 0)
                    {
                        throw new Exception($"[{drone.Name}] 💥 Tilfeldig Turbulens!");
                    }
                }
                Console.WriteLine($"[{drone.Name}] har landet trygt!");

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{drone.Name}] BOOM? {ex.Message}");

                tcs.SetException(ex);
            }

        });

        return tcs.Task;
    }
}
