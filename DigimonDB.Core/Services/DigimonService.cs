using DigimonDB.Core.Data;
using DigimonDB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Services;

public class DigimonService
{
    private readonly DigimonContext _context;

    public DigimonService(DigimonContext context)
    {
        _context = context;
    }

    public async Task<List<Digimon>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Digimons
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Digimon>> GetFilteredAsync(
        string? query,
        int? minLevel = null,
        int? maxLevel = null,
        DigimonSortBy sortBy = DigimonSortBy.NameAsc,
        CancellationToken cancellationToken = default)
    {
        var digimonQuery = _context.Digimons.AsNoTracking().AsQueryable();

        if (string.IsNullOrWhiteSpace(query) == false)
        {
            var search = query.Trim();
            digimonQuery = digimonQuery.Where(d =>
                d.Name.Contains(search) ||
                d.Type.Contains(search));
        }

        if (minLevel.HasValue)
        {
            digimonQuery = digimonQuery.Where(d => d.Level >= minLevel.Value);
        }

        if (maxLevel.HasValue)
        {
            digimonQuery = digimonQuery.Where(d => d.Level <= maxLevel.Value);
        }

        digimonQuery = sortBy switch
        {
            DigimonSortBy.IdAsc => digimonQuery.OrderBy(d => d.Id),
            DigimonSortBy.IdDesc => digimonQuery.OrderByDescending(d => d.Id),
            DigimonSortBy.NameDesc => digimonQuery.OrderByDescending(d => d.Name),
            _ => digimonQuery.OrderBy(d => d.Name)
        };

        return await digimonQuery.ToListAsync(cancellationToken);
    }
}

public enum DigimonSortBy
{
    NameAsc,
    NameDesc,
    IdAsc,
    IdDesc
}
