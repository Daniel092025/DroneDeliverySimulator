using Models;
using Services;

namespace Simulations;

public class JokeApi
{

    public static async Task RunBasicJokeDemoAsync()
    {
        Console.WriteLine("=== PART E: Random Jokes Demo ===\n");

        using var httpClient = new HttpClient();
        var jokeClient = new JokeClient(httpClient);

        Console.WriteLine("Fetching 3 random jokes...\n");

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"--- Joke {i} ---");

            var joke = await jokeClient.GetRandomJokeAsync();

            if (joke != null)
            {
                Console.WriteLine($"Setup: {joke.Setup}");
                await Task.Delay(1500);
                Console.WriteLine($"Punchline: {joke.Punchline}");
                Console.WriteLine($"(Type: {joke.Type})\n");
            }
            else
            {
                Console.WriteLine("⚠️ Failed to get joke.\n");
            }

            if (i < 3)
            {
                await Task.Delay(800);
            }
        }

        Console.WriteLine("✅ Done!\n");
    }

 
    public static async Task RunProgrammingJokesDemoAsync()
    {
        Console.WriteLine("=== PART E: Programming Jokes Demo ===\n");

        using var httpClient = new HttpClient();
        var jokeClient = new JokeClient(httpClient);

        Console.WriteLine("Fetching 10 programming jokes...\n");

        var jokes = await jokeClient.GetJokesByTypeAsync("programming");

        if (jokes != null && jokes.Any())
        {
            Console.WriteLine($"📻 Received {jokes.Count} jokes!\n");

           
            for (int i = 0; i < jokes.Count; i++)
            {
                var joke = jokes[i];
                Console.WriteLine($"{i + 1}. {joke.Setup}");
                await Task.Delay(1200);
                Console.WriteLine($"   → {joke.Punchline}\n");
                await Task.Delay(500);
            }

            Console.WriteLine($"✅ All {jokes.Count} jokes displayed!\n");
        }
        else
        {
            Console.WriteLine("⚠️ Failed to get jokes.\n");
        }
    }
}