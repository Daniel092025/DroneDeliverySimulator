// Services/JokeClient.cs
using System.Net.Http.Json;
using System.Text.Json;
using Models;

namespace Services;


public class JokeClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://official-joke-api.appspot.com";

    public JokeClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }


    public async Task<Joke?> GetRandomJokeAsync()
    {
        try
        {
            Console.WriteLine($"🎭 Requesting random joke...");

            var url = $"{BaseUrl}/random_joke";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var joke = await response.Content.ReadFromJsonAsync<Joke>();

            if (joke != null)
            {
                Console.WriteLine($"📻 Joke received! (ID: {joke.Id})");
            }

            return joke;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ Network error: {ex.Message}");
            return null;
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine($"❌ Request timeout");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"❌ Failed to parse joke: {ex.Message}");
            return null;
        }
    }


public async Task<List<Joke>?> GetJokesByTypeAsync(string type)
    {
        try
        {
            Console.WriteLine($"🎭 Requesting {type} jokes...");

            var url = $"{BaseUrl}/jokes/{type}/ten";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jokes = await response.Content.ReadFromJsonAsync<List<Joke>>();

            if (jokes != null)
            {
                Console.WriteLine($"📻 Received {jokes.Count} {type} jokes!");
            }

            return jokes;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ Network error: {ex.Message}");
            return null;
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine($"❌ Request timeout");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"❌ Failed to parse jokes: {ex.Message}");
            return null;
        }
    }

}
