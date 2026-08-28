using System.Text.Json.Serialization;

namespace DigimonDB.Core.Import;

public record DigimonDto(string Name, string Type, int Level, int HP, int ATK, int DEF, int SPD, int INT, string Description);

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

    [JsonPropertyName("items")]
    public ItemDto[] Items { get; init; } = [];
}

public record SkillDto(string Name, string Damage_Type, string Description);

public record ImportSummary(int DigimonAdded, int MovesAdded, int ItemsAdded);
