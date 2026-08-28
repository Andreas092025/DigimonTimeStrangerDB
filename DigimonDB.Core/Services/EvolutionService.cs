using DigimonDB.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.Core.Services;

public class EvolutionService
{
    private readonly DigimonContext _context;

    public EvolutionService(DigimonContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetDigimonNamesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Digimons
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => d.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EvolutionLink>> GetDirectEvolutionsFromAsync(string fromName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fromName))
        {
            return [];
        }

        return await _context.Evolutions
            .AsNoTracking()
            .Include(e => e.FromDigimon)
            .Include(e => e.ToDigimon)
            .Where(e => e.FromDigimon.Name == fromName)
            .OrderBy(e => e.ToDigimon.Name)
            .Select(e => new EvolutionLink(
                e.FromDigimon.Name,
                e.ToDigimon.Name,
                e.Condition))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EvolutionLink>> FindPathAsync(string fromName, string toName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fromName) || string.IsNullOrWhiteSpace(toName))
        {
            return [];
        }

        if (string.Equals(fromName, toName, StringComparison.OrdinalIgnoreCase))
        {
            return [];
        }

        var edges = await _context.Evolutions
            .AsNoTracking()
            .Include(e => e.FromDigimon)
            .Include(e => e.ToDigimon)
            .Select(e => new EvolutionLink(
                e.FromDigimon.Name,
                e.ToDigimon.Name,
                e.Condition))
            .ToListAsync(cancellationToken);

        if (edges.Count == 0)
        {
            return [];
        }

        var adjacency = edges
            .GroupBy(e => e.FromName, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

        var queue = new Queue<string>();
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var previous = new Dictionary<string, EvolutionLink>(StringComparer.OrdinalIgnoreCase);

        queue.Enqueue(fromName);
        visited.Add(fromName);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (string.Equals(current, toName, StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (adjacency.TryGetValue(current, out var neighbors) == false)
            {
                continue;
            }

            foreach (var edge in neighbors)
            {
                if (visited.Contains(edge.ToName))
                {
                    continue;
                }

                visited.Add(edge.ToName);
                previous[edge.ToName] = edge;
                queue.Enqueue(edge.ToName);
            }
        }

        if (previous.ContainsKey(toName) == false)
        {
            return [];
        }

        var path = new List<EvolutionLink>();
        var step = toName;

        while (string.Equals(step, fromName, StringComparison.OrdinalIgnoreCase) == false)
        {
            var edge = previous[step];
            path.Add(edge);
            step = edge.FromName;
        }

        path.Reverse();
        return path;
    }
}

public record EvolutionLink(string FromName, string ToName, string Condition);
