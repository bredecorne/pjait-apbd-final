using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class NaturalPersonsConfig : IEntityTypeConfiguration<NaturalPerson>
{
    public void Configure(EntityTypeBuilder<NaturalPerson> builder)
    {
        var naturalPersons = new List<NaturalPerson>();

        var naturalPerson1 = new NaturalPerson(
            1,
            "John",
            "Doe",
            "ul. Testowa 1, Hrubieszów",
            "john.doe@buziaczek.pl",
            "+48 123 456 789",
            "12345678901",
            true);
        naturalPersons.Add(naturalPerson1);
        
        var naturalPerson2 = new NaturalPerson(
            2,
            "Jane",
            "Doe",
            "ul. Testowa 1, Hrubieszów",
            "jane.doe@misiaczek.com",
            "+48 123 456 789",
            "12345678902",
            true);
        naturalPersons.Add(naturalPerson2);
        
        builder.HasData(naturalPersons);
    }
}