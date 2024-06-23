using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class ContractPaymentsConfig : IEntityTypeConfiguration<ContractPayment>
{
    public void Configure(EntityTypeBuilder<ContractPayment> builder)
    {
        var contractPayments = new[]
        {
            new ContractPayment(1, 1, 1, 1000m, DateTime.Parse("2024-01-01"))
        };
        
        builder.HasData(contractPayments);
    }
}