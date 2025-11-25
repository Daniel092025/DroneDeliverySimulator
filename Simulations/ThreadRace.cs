using Models;

namespace Simulations;

public class ThreadRace 
{
    public static void Run()
    {
        Console.WriteLine("=== Thread Race === \n");

        var drone1 = new DroneModel("Alpha", 5, 500);
        var drone2 = new DroneModel("Beta" , 5, 300);

       Thread thread1 = new Thread(() => FlyDrone (drone1));
       Thread thread2 = new Thread(() => FlyDrone (drone2));

       Console.WriteLine("Starter drone flyving");

       thread1.Start();
       thread2.Start();

       thread1.Join();
       thread2.Join();

       Console.WriteLine("Alle dronene har landet");
    }

    private static void FlyDrone(DroneModel drone)
    {
        Console.WriteLine($"{drone.Name} tar av!");
    }
}
