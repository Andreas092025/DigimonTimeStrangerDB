using DigimonDB.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Services;

public class DashboardService
{
    private readonly DigimonContext _context;

    public DashboardService(DigimonContext context)
    {
        _context = context;
    }

    public async Task<(int Digimon, int Moves, int Items, int Evolutions)> GetCountsAsync(CancellationToken cancellationToken = default)
    {
        var digimon = await _context.Digimons.CountAsync(cancellationToken);
        var moves = await _context.Moves.CountAsync(cancellationToken);
        var items = await _context.Items.CountAsync(cancellationToken);
        var evolutions = await _context.Evolutions.CountAsync(cancellationToken);

        return (digimon, moves, items, evolutions);
    }
}
