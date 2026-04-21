namespace WebAPI.Models;

public class OccurrenceObjectResult
{
    public string Name { get; set; } = null!;

    public string Variant { get; set; } = null!;

    public DateTime TimeStamp { get; set; }

    public int Count { get; set; }
}
