namespace WebAPI.Models;

public class Occurrence
{
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public DateTime TimeStamp { get; set; }

    public int Count { get; set; }

    // Navigation properties
    public ObjectEntity Object { get; set; } = null!;
}
