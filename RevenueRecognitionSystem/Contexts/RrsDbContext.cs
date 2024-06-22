using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Configs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Contexts;

public class RrsDbContext : DbContext
{
    protected RrsDbContext() {}

    public RrsDbContext(DbContextOptions options) : base(options) {}

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PersonConfig());
    }
}