using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        var persons = new List<Person>();

        var naturalPerson1 = new NaturalPerson(
            "John",
            "Doe",
            "ul. Testowa 1, Hrubieszów",
            "john.doe@buziaczek.pl",
            "+48 123 456 789",
            "12345678901");
        persons.Add(naturalPerson1);
        
        var naturalPerson2 = new NaturalPerson(
            "Jane",
            "Doe",
            "ul. Testowa 1, Hrubieszów",
            "jane.doe@misiaczek.com",
            "+48 123 456 789",
            "12345678902");
        persons.Add(naturalPerson2);
        
        var legalPerson1 = new LegalPerson(
            "Firma Testowa",
            "ul. Testowa 2, Hrubieszów",
            "prezes@krzak.org",
            "+48 987 654 321",
            "9876543210");
        persons.Add(legalPerson1);
        
        var legalPerson2 = new LegalPerson(
            "Firma Testowa 2",
            "ul. Testowa 2, Hrubieszów",
            "prezeszarzadu@krzakholdings.ltd",
            "+48 987 654 321",
            "9876543211");
        persons.Add(legalPerson2);
        
        builder.HasData(persons);
    }
}