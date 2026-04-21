namespace WebAPI.Models;

public class ObjectEntityResult
{
    public string Name { get; set; } = null!;

    public string Variant { get; set; } = null!;

    public int OccurrenceCount { get; set; }
}
