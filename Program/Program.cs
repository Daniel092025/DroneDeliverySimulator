using System.Drawing;
using Simulations;

bool fortsetteKommando = true;

Console.ForegroundColor = ConsoleColor.Yellow;

while (fortsetteKommando)
{
    Console.WriteLine("Drone Delivery Simulator \n");
    Console.WriteLine("Velg en simulering");
    Console.WriteLine("A. - Thread Race");
    Console.WriteLine("B. - Tower Says Go! (Task)");
    Console.WriteLine("C. - Async Orchestration");
    Console.WriteLine("D. - Control Tower API");
    Console.WriteLine("\n Joke API, brought to you by AI. Cause I ran out of time and wanted to test");
    Console.WriteLine("E. Please don't choose this");
    Console.WriteLine("F. No, just no");
    Console.WriteLine("G. - Exit");

    var choice = Console.ReadLine()?.ToUpper();

    switch (choice)
    {
        case "A":
        ThreadRace.Run();
        break;

        case "B":
        TaskCompletion.Run();
        break;

        case "C":
        await AsyncAwait.RunAsync();
        break;
        
        case "D":
        await ControlTower.RunAsync();
        break;

        case "E":
        await JokeApi.RunBasicJokeDemoAsync();  
        break;

        case "F":
        await JokeApi.RunProgrammingJokesDemoAsync();  
        break;

        case "G":
        Console.WriteLine("\n 🚁 Takk for at du brukte Drone Delivery Simulator!🚁");
        fortsetteKommando = false;
        break;

        default:
            Console.WriteLine("Ikke gyldig valg");
            break;
    }
    if (fortsetteKommando && choice != "E")
    {
        Console.WriteLine("\n\nTrykk en tast for å fortsette...");
        Console.ReadKey();
        Console.Clear();
    }
}
Console.ResetColor();
