using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class DiscountsConfig : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        var discounts = new Discount[]
        {
            new Discount(1, 9.5m, DateTime.Parse("2024-01-01"), DateTime.Parse("2024-12-31")),
            new Discount(2, 15.0m, DateTime.Parse("2024-01-01"), DateTime.Parse("2024-12-31"))
        };
        
        builder.HasData(discounts);
    }
}