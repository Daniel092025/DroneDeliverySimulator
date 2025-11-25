using Models;

namespace Simulations;

public class ThreadRace 
{
    public static void Run()
    {
        Console.WriteLine("=== Thread Race === \n");

        var drone1 = new DroneModel("Alpha", 5, 500);
    }
}
