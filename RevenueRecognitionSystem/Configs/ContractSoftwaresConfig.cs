using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class ContractSoftwaresConfig : IEntityTypeConfiguration<ContractSoftware>
{
    public void Configure(EntityTypeBuilder<ContractSoftware> builder)
    {
        var contractSoftwares = new[]
        {
            new ContractSoftware(1, 1)
        };
        
        builder.HasData(contractSoftwares);
    }
}