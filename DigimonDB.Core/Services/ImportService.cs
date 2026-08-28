using System.Text.Json;
using DigimonDB.Core.Data;
using DigimonDB.Core.Import;
using DigimonDB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Services;

public class ImportService
{
    private readonly DigimonContext _context;

    public ImportService(DigimonContext context)
    {
        _context = context;
    }

    public async Task<ImportSummary> ImportFromFolderAsync(string dataRoot, CancellationToken cancellationToken = default)
    {
        var digimonDir = Path.Combine(dataRoot, "Digimons");
        var skillsDir = Path.Combine(dataRoot, "Skills");
        var itemsDir = Path.Combine(dataRoot, "Items");

        if (!Directory.Exists(digimonDir) || !Directory.Exists(skillsDir) || !Directory.Exists(itemsDir))
        {
            throw new DirectoryNotFoundException($"Expected Digimons/Skills/Items folders under: {dataRoot}");
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var digimonAdded = 0;
        var moveAdded = 0;
        var itemAdded = 0;

        foreach (var file in Directory.EnumerateFiles(digimonDir, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file, cancellationToken);
            var data = JsonSerializer.Deserialize<DataDto>(json, options);
            if (data?.Digimons is null)
            {
                continue;
            }

            foreach (var dto in data.Digimons)
            {
                var exists = await _context.Digimons.AnyAsync(d => d.Name == dto.Name, cancellationToken);
                if (exists)
                {
                    continue;
                }

                _context.Digimons.Add(new Digimon
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
                digimonAdded++;
            }
        }

        foreach (var file in Directory.EnumerateFiles(skillsDir, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file, cancellationToken);
            var skills = JsonSerializer.Deserialize<List<SkillDto>>(json, options);
            if (skills is null)
            {
                continue;
            }

            foreach (var skill in skills)
            {
                var exists = await _context.Moves.AnyAsync(m => m.Name == skill.Name, cancellationToken);
                if (exists)
                {
                    continue;
                }

                _context.Moves.Add(new Move
                {
                    Name = skill.Name,
                    Type = skill.Damage_Type,
                    Description = skill.Description
                });
                moveAdded++;
            }
        }

        foreach (var file in Directory.EnumerateFiles(itemsDir, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file, cancellationToken);
            var data = JsonSerializer.Deserialize<DataDto>(json, options);
            if (data?.Items is null)
            {
                continue;
            }

            foreach (var dto in data.Items)
            {
                var exists = await _context.Items.AnyAsync(i => i.Name == dto.Name, cancellationToken);
                if (exists)
                {
                    continue;
                }

                _context.Items.Add(new Item
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Description = string.Join(", ", dto.Effect),
                    EvolvesFrom = string.Join(", ", dto.EvolvesFrom),
                    EvolvesTo = string.Join(", ", dto.EvolvesTo)
                });
                itemAdded++;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new ImportSummary(digimonAdded, moveAdded, itemAdded);
    }
}
