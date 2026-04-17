using System.Text.Json;
using System.Text.Json.Serialization;

namespace DigimonDB.Data;

public record DigimonDto(string Name, string Type, int Level, int HP, int ATK, int DEF, int SPD, int INT, string Description);
public record MoveDto(string Name, string Type, int Power, string Description);

public record ItemDto
{
    [JsonPropertyName("item")]
    public string Name { get; init; }
    
    public string Type { get; init; }
    
    [JsonPropertyName("materials")]
    public string[] Effect { get; init; }
}

public record CharacterDto(string Name, string Role, string Description);

public record DataDto
{
    [JsonPropertyName("digimons")]
    public DigimonDto[] Digimons { get; init; }
    
    [JsonPropertyName("moves")]
    public MoveDto[] Moves { get; init; }
    
    [JsonPropertyName("items")]
    public ItemDto[] Items { get; init; }
    
    [JsonPropertyName("characters")]
    public CharacterDto[] Characters { get; init; }
}