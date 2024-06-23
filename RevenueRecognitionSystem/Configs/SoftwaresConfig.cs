using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class SoftwaresConfig : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        var softwares = new []
        {
            new Software(1, "PC Optimizer", "Cleans and does maintenance on your PC",
                Software.SoftwareCategory.Entertainment, false),
            new Software(2, "Ultimate Photo Operator", "Allows you to apply filters and effects to your photos",
                Software.SoftwareCategory.Finance, false),
        };
        
        builder.HasData(softwares);
    }
}