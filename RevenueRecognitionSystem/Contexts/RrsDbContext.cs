using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Configs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Contexts;

public class RrsDbContext : DbContext
{
    protected RrsDbContext()
    {
    }

    public RrsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractPayment> ContractPayments { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<SoftwareDiscount> SoftwareDiscounts { get; set; }
    public DbSet<SoftwareVersion> SoftwareVersions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientsConfig());
        modelBuilder.ApplyConfiguration(new DiscountsConfig());
        modelBuilder.ApplyConfiguration(new SoftwaresConfig());

        modelBuilder.ApplyConfiguration(new SoftwareDiscountsConfig());
        modelBuilder.ApplyConfiguration(new SoftwareVersionsConfig());

        modelBuilder.ApplyConfiguration(new ContractsConfig());

        modelBuilder.ApplyConfiguration(new ContractPaymentsConfig());
    }
}