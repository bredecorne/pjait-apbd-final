using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Configs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Contexts;

public class RrsDbContext : DbContext
{
    protected RrsDbContext() {}

    public RrsDbContext(DbContextOptions options) : base(options) {}

    public virtual DbSet<LegalPerson> LegalPersons { get; set; }
    
    public virtual DbSet<NaturalPerson> NaturalPersons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new LegalPersonsConfig());
        modelBuilder.ApplyConfiguration(new NaturalPersonsConfig());
    }
}