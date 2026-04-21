namespace WebAPI.Models;

public class ObjectEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Variant { get; set; } = null!;

    // Navigation properties
    public List<Occurrence> Occurrences { get; set; } = null!;
}
