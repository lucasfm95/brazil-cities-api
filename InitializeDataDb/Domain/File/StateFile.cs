using System.Text.Json.Serialization;

namespace InitializeDataDb.Domain.File;

public class StateFile
{
    [JsonPropertyName("sigla")]
    public string? StateAcronym { get; set; }
    [JsonPropertyName("nome")]
    public string? Name { get; set; }
    [JsonPropertyName("cidades")]
    public string[] Cities { get; set; }
}