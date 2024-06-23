using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class SoftwareDiscountsConfig : IEntityTypeConfiguration<SoftwareDiscount>
{
    public void Configure(EntityTypeBuilder<SoftwareDiscount> builder)
    {
        var softwareDiscounts = new []
        {
            new SoftwareDiscount(1, 1),
            new SoftwareDiscount(1, 2),
        };
        
        builder.HasData(softwareDiscounts);
    }
}