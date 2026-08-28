using System.Text.Json;
using Spectre.Console;
using DigimonDB.Data;
using DigimonDB.Models;
using DigimonEntity = DigimonDB.Models.Digimon; 
using MoveEntity = DigimonDB.Models.Move; 

namespace DigimonDB.Features.Import;

public static class ImportManager
{
    private record SkillDto(string Name, string Damage_Type, string Description);

    public static void RunImport(DigimonContext context)
    {
        var dataRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        var digimonDir = Path.Combine(dataRoot, "Digimons");
        var skillsDir = Path.Combine(dataRoot, "Skills");
        var itemsDir = Path.Combine(dataRoot, "Items");

        if (!Directory.Exists(digimonDir) || !Directory.Exists(skillsDir) || !Directory.Exists(itemsDir))
        {
            AnsiConsole.MarkupLine($"[red]Missing data folders in {dataRoot}[/]");
            return;
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // 1) Import digimons from all files in Data/Digimons
        foreach (var file in Directory.EnumerateFiles(digimonDir))
        {
            var json = File.ReadAllText(file);
            var data = JsonSerializer.Deserialize<DataDto>(json, options);
            if (data?.Digimons is null) continue;


            foreach (var dto in data.Digimons)
            {
                if (context.Digimons.Any(d => d.Name == dto.Name)) continue;

                context.Digimons.Add(new DigimonEntity
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

        // 2) Import skills as moves from all files in Data/Skills
        foreach (var file in Directory.EnumerateFiles(skillsDir))
        {
            var json = File.ReadAllText(file);
            var skills = JsonSerializer.Deserialize<List<SkillDto>>(json, options);
            if (skills is null) continue;

            foreach (var s in skills)
            {
                if (context.Moves.Any(m => m.Name == s.Name)) continue;

                context.Moves.Add(new MoveEntity
                {
                    Name = s.Name,
                    Type = s.Damage_Type,
                    Description = s.Description
                });
            }
        }

        // 3) Import items from all files in Data/Items
        foreach (var file in Directory.EnumerateFiles(itemsDir))
        {
            var json = File.ReadAllText(file);
            var data = JsonSerializer.Deserialize<DataDto>(json, options);
            if (data?.Items is null) continue;

            foreach (var dto in data.Items)
            {
                if (context.Items.Any(i => i.Name == dto.Name)) continue;
                context.Items.Add(new Models.Item
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Description = string.Join(", ", dto.Effect),
                    EvolvesFrom = string.Join(", ", dto.EvolvesFrom),
                    EvolvesTo = string.Join(", ", dto.EvolvesTo)
                });
            }
        }

        context.SaveChanges();
        AnsiConsole.MarkupLine("[green]Folder import completed.[/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}