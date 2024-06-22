using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Contexts;

public class RrsDbContext : DbContext
{
    protected RrsDbContext() {}

    public RrsDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}