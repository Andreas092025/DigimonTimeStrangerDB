using System.Text.Json;

namespace DigimonDB.Data;

public record DigimonDto(string Name, string Type, int Level, int HP, int ATK, int DEF, int SPD, int INT, string Description);
public record MoveDto(string Name, string Type, int Power, string Description);
public record ItemDto(string Name, string Type, string Effect);
public record CharacterDto(string Name, string Role, string Description);

public record DataDto(DigimonDto[] Digimons, MoveDto[] Moves, ItemDto[] Items, CharacterDto[] Characters);