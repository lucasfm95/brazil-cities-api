using System.Text.Json.Serialization;

namespace InitializeDataDb.Domain.File;

public class StatesCitiesFile
{
    [JsonPropertyName("estados")]
    public StateFile[] States { get; set; }
}