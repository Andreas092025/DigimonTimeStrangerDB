using System.Text.Json;
using System.Text.Json.Serialization;

namespace DigimonDB.Data;

public record DigimonDto(string Name, string Type, int Level, int HP, int ATK, int DEF, int SPD, int INT, string Description);
public record MoveDto(string Name, string Type, string Description);

public record ItemDto
{
    [JsonPropertyName("item")]
    public string Name { get; init; } = string.Empty;
    
    public string Type { get; init; } = string.Empty;
    
    [JsonPropertyName("evolves_from")]
    public string[] EvolvesFrom { get; init; } = [];

    [JsonPropertyName("evolves_to")]
    public string[] EvolvesTo { get; init; } = [];
    
    [JsonPropertyName("materials")]
    public string[] Effect { get; init; } = [];
}

public record DataDto
{
    [JsonPropertyName("digimons")]
    public DigimonDto[] Digimons { get; init; } = [];
    
    [JsonPropertyName("moves")]
    public MoveDto[] Moves { get; init; } = [];
    
    [JsonPropertyName("items")]
    public ItemDto[] Items { get; init; } = [];
}