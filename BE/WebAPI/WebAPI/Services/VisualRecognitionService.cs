using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public class VisualRecognitionService : IVisualRecognitionService
{
    private readonly VisualRecognitionDbContext _context;

    public VisualRecognitionService(VisualRecognitionDbContext context)
    {
        _context = context;
    }

    public async Task<List<OccurrenceObjectResult>> GetOccurencesAsync(string? objectName, string? variant, DateTime? from, DateTime? to)
    {
        var occurrencesQuery = await GetOccurrencesAsync(objectName, variant, from, to);

        return occurrencesQuery
            .Select(o => new OccurrenceObjectResult
            {
                Name = o.Object.Name,
                Variant = o.Object.Variant,
                TimeStamp = o.TimeStamp,
                Count = o.Count
            })
            .ToList();
    }

    public async Task<OccurrenceStatsResult> GetCountAsync(string? objectName, string? variant, DateTime? from, DateTime? to)
    {
        var occurrencesQuery = await GetOccurrencesAsync(objectName, variant, from, to);

        return new OccurrenceStatsResult
        {
            Count = occurrencesQuery.Sum(o => (int?)o.Count) ?? 0,
            Occurrences = occurrencesQuery
                .Select(o => new OccurrenceObjectResult
                {
                    Name = o.Object.Name,
                    Variant = o.Object.Variant,
                    TimeStamp = o.TimeStamp,
                    Count = o.Count
                })
                .ToList()
        };
    }

    public async Task<List<ObjectEntityResult>> GetMostFrequentAsync(string? objectName, string? variant, DateTime? from, DateTime? to)
    {
        var occurrencesQuery = await GetOccurrencesAsync(objectName, variant, from, to);

        var goupedOccurrences = occurrencesQuery
            .GroupBy(o => new { o.Object.Name, o.Object.Variant })
            .Select(g => new
            {
                ObjectName = g.Key.Name,
                Variant = g.Key.Variant,
                Total = g.Sum(x => x.Count)
            });

        var maxOccurrence = goupedOccurrences.Max(x => x.Total);

        return goupedOccurrences
            .Where(x => x.Total == maxOccurrence)
            .Select(x => new ObjectEntityResult
            {
                Name = x.ObjectName,
                Variant = x.Variant,
                OccurrenceCount = maxOccurrence
            })
            .ToList();
    }

    private async Task<IQueryable<Occurrence>> GetOccurrencesAsync(string? objectName, string? variant, DateTime? from, DateTime? to)
    {
        var occurrencesQuery = _context.Occurrences
            .Include(o => o.Object)
            .AsQueryable();

        if (!string.IsNullOrEmpty(objectName))
        {
            occurrencesQuery = occurrencesQuery
                .Where(o => o.Object.Name == objectName);
        }

        if (!string.IsNullOrEmpty(variant))
        {
            occurrencesQuery = occurrencesQuery
                .Where(o => o.Object.Variant == variant);
        }

        if (from.HasValue)
        {
            occurrencesQuery = occurrencesQuery.Where(o => o.TimeStamp >= from.Value);
        }

        if (to.HasValue)
        {
            occurrencesQuery = occurrencesQuery.Where(o => o.TimeStamp <= to.Value);
        }

        return occurrencesQuery;
    }
}
