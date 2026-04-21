using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class VisualRecognitionDbContext : DbContext
{
    public VisualRecognitionDbContext(DbContextOptions<VisualRecognitionDbContext> options)
        : base(options) { }

    public DbSet<ObjectEntity> Objects => Set<ObjectEntity>();
    public DbSet<Occurrence> Occurrences => Set<Occurrence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Occurrence>()
            .HasOne(o => o.Object)
            .WithMany(o => o.Occurrences)
            .HasForeignKey(o => o.ObjectId);
    }
}