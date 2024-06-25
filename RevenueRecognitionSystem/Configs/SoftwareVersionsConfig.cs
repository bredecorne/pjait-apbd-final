using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class SoftwareVersionsConfig : IEntityTypeConfiguration<SoftwareVersion>
{
    public void Configure(EntityTypeBuilder<SoftwareVersion> builder)
    {
        var softwareVersions = new[]
        {
            new SoftwareVersion(1, 1, "1.0.0", DateTime.Parse("2024-01-10")),
            new SoftwareVersion(2, 1, "1.0.1", DateTime.Parse("2024-01-20")),
            new SoftwareVersion(3, 2, "1.0.1", DateTime.Parse("2024-02-01")),
            new SoftwareVersion(4, 1, "1.0.3", DateTime.Parse("2024-02-15"))
        };

        builder.HasData(softwareVersions);
    }
}