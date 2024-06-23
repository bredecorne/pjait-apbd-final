using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class ClientContractsConfig : IEntityTypeConfiguration<ClientContract>
{
    public void Configure(EntityTypeBuilder<ClientContract> builder)
    {
        var clientContracts = new[]
        {
            new ClientContract(1, 1),
            new ClientContract(2, 2)
        };
        
        builder.HasData(clientContracts);
    }
}