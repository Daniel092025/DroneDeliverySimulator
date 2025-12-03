# Intermidiate oppgave 3, Drone Dash

- Jeg valgte å blaste på med å lage oppgaven rundt drone dash. Som du tipset / anbefalte i oppgaven.

### Delte dette opp litt som du også foreslo

1. ThreadRace
- I denne så laget jeg 3 droner (startet med 2, så added jeg en til etterhvert bare for).
- Og hver drone har en delay i MS. Som gjør at de "starter" med forskjellig forsinkelse.
- Hva skjer om man fjerner Join? Første man merker er at beskjeded "Alle droner har landet" kommer rett etter at det starter å fly. Før checkpoints blir fullført. Legger du den til lander alle etter de har fullført checkpointsene før denne beskjeden kommer.
- La til en stopwatch på ThreadRace, for gøy.

2. TaskCompletion
- I denne laget jeg også 3 droner, der en av dem har en negativ forsinkelse. 
- Kan sette opp tasks sammen. 
- Gir mer respons tilbake til bruker.
- Vil si at "komplesiteten" er større. Mer kode, men har mye mer kontroll.
- La til en ekstra feil etter sikker feil til "Delta" på checkpoint 3. En 50% sjanse til å feile etter checkpoint 2

3. AsyncAwait
- Er mindre kode enn oppgave 2 (B). Jeg syns det er lettere å mer oversiktlig å lese koden. 
- Er mer automatikk i exception handlingen.
- Jeg tror vedlikehold av koden blir mye lettere, siden det generelt mindre kode og den ser cleanere / mer oversiktlig ut.
- Har lagt inn en feil aka "shouldFailAtCheckpoint.

4. Control Tower API
- 
- Løste timeouts / feil med 3 metoder. 
- Satte Timeout til 10 sekunder, så den ikke bruker for lang tid eller for kort tid. Så den timer ut.
- Noe av utfordringene med konsumere APIer, er at du trenger forskjellig timeouts. Avhenging av side og hva du skal hente. 
    - For kort, falske feil
    - For lenge, kan oppleves negativt (vente for lenge)
- Jeg bruke ganske "simpel" side til HTTP kall. 
    - Men med større / andre sider kan det komme masse forskjellige errorer. Så å catche alle disse kan bli masse og mye kode. 
    samt å finne ut hva man skal catche. Blir egen feilsøking bare det.
    - Hvordan skal man logge dette også, dette kan bli mye logg og komplisert. Kanskje noe sensitivt kan komme i logg?

<br>

<details>
<summary><i> Kode: Klikk for å ekspandere </summary>

<br>
public async Task <WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
    {
        try
        {
            Console.WriteLine($"Kontakter Kontrolltårnet for værdata...");

            var url = $"{BaseUrl}?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m,weather_code";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();

            if(weatherData?.Current != null)
            {
                var condition = WeatherInterpreter.GetCondition(weatherData.Current.WeatherCode);
                Console.WriteLine($"Værmelding motatt: {condition}, {weatherData.Current.Temperature}°C, Wind: {weatherData.Current.WindSpeed} km/t");
            }
            return weatherData;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Nettverk Error: {ex.Message}");
            return null;
        }
        catch(TaskCanceledException)
        {
            Console.WriteLine($"Request Timeout: Kontrolltårnet svarer ikke!");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Feilet med å parse værdata: {ex.Message}");
            return null;
        }
    }



</details>

<br><br>

*De 3 metodene er Nettverks feil og timeout (koder sånn som 4** og 5**) og dårlig JSON.*



## ReadMe

Ganske enkelt oppsett på å teste hver del. 
Er bare å kjøre kommando:
Dotnet run.
Så er hvert valg forklart enkelt fra A-D.
- Via switch case.
- While loop, så kjører til du stopper selv.
- La til en stopwatch på ThreadRace, for å teste.
- Kontrolltårnet henter værdata fra en lokasjon. Så dette kan endres i koden med, koordinater. Har lagt disse i kommentar ved koden.
- Er lagt med en liten Joke API også. Hvis du tørr.

På spørsmålet om hvor man eventuelt ville ha brukt Task/TCS over Async/Await. Så vil jeg si i eldre miljøer som kanskje har dette.
Og bygge videre på dette. Eller hvis du vil kjøre tasks manuelt.






