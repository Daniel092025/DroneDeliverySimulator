using Simulations;

Console.WriteLine("Drone Delivery Simulator \n");
Console.WriteLine("Velg en simulering");
Console.WriteLine("A. - Thread Race");
Console.WriteLine("B. - Tower Says Go! (Task)");
Console.WriteLine("C. - Async Orchestration");
Console.WriteLine("D. - Control Tower API");

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

    default:
        Console.WriteLine("Ikke gyldig valg");
        break;
}

