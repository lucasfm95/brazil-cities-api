using System.Net.Http.Json;
using System.Text.Json;
using InitializeDataDb.Domain.File;
using InitializeDataDb.Domain.Requests;

Console.WriteLine("Process started");

const string brazilCitiesApiUrl = "http://localhost:5155";

var httpClient = new HttpClient
{
    BaseAddress = new Uri(brazilCitiesApiUrl)
};

Console.WriteLine("Stating to read states and cities file");

var statesCitiesFile = JsonSerializer.Deserialize<StatesCitiesFile>(File.ReadAllText("./estados-cidades.json"));

foreach (var state in statesCitiesFile.States)
{
    var stateRequest = new StateRequest
    {
        StateAcronym = state.StateAcronym,
        Name = state.Name
    };

    Console.WriteLine($"Creating state {stateRequest.Name}");

    var stateResponse = await httpClient.PostAsJsonAsync("api/states", stateRequest);

    stateResponse.EnsureSuccessStatusCode();

    foreach (var city in state.Cities)
    {
        var cityRequest = new CityRequest
        {
            Name = city,
            StateAcronym = state.StateAcronym
        };
        
        var cityResponse = await httpClient.PostAsJsonAsync("api/cities", cityRequest);

        cityResponse.EnsureSuccessStatusCode();
    }
    
    Console.WriteLine($"State {stateRequest.Name} created");
}

Console.WriteLine("Process finished"); 