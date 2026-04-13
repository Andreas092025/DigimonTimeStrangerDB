using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;
using System.Text.Json;

namespace DigimonDB.Features.Import;

public static class ImportManager
{
    public static void RunImport(DigimonContext context)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "sample-data.json");
        if (!File.Exists(filePath))
        {
            AnsiConsole.MarkupLine("[red]Sample data file not found.[/]");
            return;
        }

        var json = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<DataDto>(json, options);

        if (data?.Digimons != null)
        {
            foreach (var dto in data.Digimons)
            {
                if (!context.Digimons.Any(d => d.Name == dto.Name))
                {
                    context.Digimons.Add(new DigimonDB.Models.Digimon
                    {
                        Name = dto.Name,
                        Type = dto.Type,
                        Level = dto.Level,
                        HP = dto.HP,
                        ATK = dto.ATK,
                        DEF = dto.DEF,
                        SPD = dto.SPD,
                        INT = dto.INT,
                        Description = dto.Description
                    });
                }
            }
        }

        if (data?.Moves != null)
        {
            foreach (var dto in data.Moves)
            {
                if (!context.Moves.Any(m => m.Name == dto.Name))
                {
                    context.Moves.Add(new DigimonDB.Models.Move
                    {
                        Name = dto.Name,
                        Type = dto.Type,
                        Power = dto.Power,
                        Description = dto.Description
                    });
                }
            }
        }

        if (data?.Items != null)
        {
            foreach (var dto in data.Items)
            {
                if (!context.Items.Any(i => i.Name == dto.Name))
                {
                    context.Items.Add(new DigimonDB.Models.Item
                    {
                        Name = dto.Name,
                        Type = dto.Type,
                        Effect = dto.Effect
                    });
                }
            }
        }

        if (data?.Characters != null)
        {
            foreach (var dto in data.Characters)
            {
                if (!context.Characters.Any(c => c.Name == dto.Name))
                {
                    context.Characters.Add(new DigimonDB.Models.Character
                    {
                        Name = dto.Name,
                        Role = dto.Role,
                        Description = dto.Description
                    });
                }
            }
        }

        context.SaveChanges();
        AnsiConsole.MarkupLine("[green]Data imported successfully![/]");
    }
}
