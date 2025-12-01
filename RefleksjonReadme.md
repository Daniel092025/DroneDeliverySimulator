# Intermidiate oppgave 3, Drone Dash

- Jeg valgte å blaste på med å lage oppgaven rundt drone dash. Som du tipset / anbefalte i oppgaven.

### Delte dette opp litt som du også foreslo

1. ThreadRace
- I denne så laget jeg 3 droner (startet med 2, så added jeg en til etterhvert bare for).
- Og hver drone har en delay i MS. Som gjør at de "starter" med forskjellig forsinkelse.
- Hva skjer om man fjerner Join? Første man merker er at beskjeded "Alle droner har landet" kommer rett etter at det starter å fly. Før checkpoints blir fullført. Legger du den til lander alle etter de har fullført checkpointsene før denne beskjeden kommer.

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


## ReadMe

Ganske enkelt oppsett på å teste hver del. 
Er bare å kjøre kommando:
Dotnet run.
Så er hvert valg forklart enkelt fra A-C.
- Via switch case.




<span> Hello <span>
<span> hi <span>



