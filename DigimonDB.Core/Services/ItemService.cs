using DigimonDB.Core.Data;
using DigimonDB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Services;

public class ItemService
{
    private readonly DigimonContext _context;

    public ItemService(DigimonContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Items
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .ToListAsync(cancellationToken);
    }
}
