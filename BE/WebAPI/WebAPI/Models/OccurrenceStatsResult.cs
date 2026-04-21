namespace WebAPI.Models;

public class OccurrenceStatsResult
{
    public int Count { get; set; }

    public List<OccurrenceObjectResult> Occurrences { get; set; } = new List<OccurrenceObjectResult>();
}
