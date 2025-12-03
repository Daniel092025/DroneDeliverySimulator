using Models;
using System.Diagnostics;

namespace Simulations;

public class ThreadRace 
{
    public static void Run()
    {
        Console.WriteLine("=== Thread Race === \n");

        var drone1 = new DroneModel("Alpha", 5, 500);
        var drone2 = new DroneModel("Beta" , 5, 300);
        var drone3 = new DroneModel("November", 3, 600);

       Thread thread1 = new Thread(() => FlyDrone (drone1));
       Thread thread2 = new Thread(() => FlyDrone (drone2));
       Thread thread3 = new Thread(() => FlyDrone (drone3));

       Console.WriteLine("Starter drone flyving");

       Stopwatch stopwatch = Stopwatch.StartNew();

       thread1.Start();
       thread2.Start();
       thread3.Start();

    
       thread1.Join();
       thread2.Join();
       thread3.Join();

       stopwatch.Stop();

       Console.WriteLine("Alle dronene har landet");
       Console.ForegroundColor = ConsoleColor.Green;
       Console.WriteLine($"Total tid: {stopwatch.ElapsedMilliseconds} ms({stopwatch.Elapsed.TotalSeconds:F2} sekunder)");
       Console.ResetColor();
    }

    private static void FlyDrone(DroneModel drone)
    {
        Console.WriteLine($"[{drone.Name}] tar av!");

        for (int checkpoint = 1; checkpoint <= drone.MaxCheckPoints; checkpoint++)
        {
            Thread.Sleep(drone.DelayMs);
            Console.WriteLine($"[{drone.Name}] Checkpoint {checkpoint}/{drone.MaxCheckPoints}");
        }

        Console.WriteLine($"{drone.Name} Landet trygt!");
    }
}
