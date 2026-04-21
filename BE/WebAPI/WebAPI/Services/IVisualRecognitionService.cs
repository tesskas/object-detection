using WebAPI.Models;

namespace WebAPI.Services;

public interface IVisualRecognitionService
{
    Task<List<OccurrenceObjectResult>> GetOccurencesAsync(string? objectName, string? variant, DateTime? from, DateTime? to);

    Task<OccurrenceStatsResult> GetCountAsync(string? objectName, string? variant, DateTime? from, DateTime? to);

    Task<List<ObjectEntityResult>> GetMostFrequentAsync(string? objectName, string? variant, DateTime? from, DateTime? to);
}
