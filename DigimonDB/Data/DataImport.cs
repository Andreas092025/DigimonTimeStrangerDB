using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace DigimonDB.Data;

public record DigimonDto(string Name, string Type, int Level, int HP, int ATK, int DEF, int SPD, int INT, string Description);
public record MoveDto(string Name, string Type, int Power, string Description);

public record ItemDto
{
    [JsonPropertyName("item")]
    public string Name { get; init; } = string.Empty;
    
    public string Type { get; init; } = string.Empty;
    
    [JsonPropertyName("materials")]
    public string[] Effect { get; init; } = Array.Empty<string>();
}

public record CharacterDto(string Name, string Role, string Description);

public record DataDto
{
    [JsonPropertyName("digimons")]
    public DigimonDto[] Digimons { get; init; } = Array.Empty<DigimonDto>();
    
    [JsonPropertyName("moves")]
    public MoveDto[] Moves { get; init; } = Array.Empty<MoveDto>();
    
    [JsonPropertyName("items")]
    public ItemDto[] Items { get; init; } = Array.Empty<ItemDto>();
    
    
    [JsonPropertyName("characters")]
    public CharacterDto[] Characters { get; init; } = Array.Empty<CharacterDto>();
}