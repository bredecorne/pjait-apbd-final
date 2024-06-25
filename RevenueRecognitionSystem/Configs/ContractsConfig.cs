using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class ContractsConfig : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        var contracts = new[]
        {
            new Contract(1, DateTime.Parse("2024-01-01"), DateTime.Parse("2024-03-30"),
                Contract.ContractStatus.AwaitingPayment, 450.11m, DateTime.Parse("2025-01-01"), 1, 1, 1)
        };
        
        builder.HasData(contracts);
    }
}