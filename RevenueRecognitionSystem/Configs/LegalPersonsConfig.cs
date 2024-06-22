using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class LegalPersonsConfig : IEntityTypeConfiguration<LegalPerson>
{
    public void Configure(EntityTypeBuilder<LegalPerson> builder)
    {
        var legalPersons = new List<LegalPerson>();
                
        var legalPerson1 = new LegalPerson(
            1,
            "Firma Testowa",
            "ul. Testowa 2, Hrubieszów",
            "prezes@krzak.org",
            "+48 987 654 321",
            "9876543210");
        legalPersons.Add(legalPerson1);
        
        var legalPerson2 = new LegalPerson(
            2,
            "Firma Testowa 2",
            "ul. Testowa 2, Hrubieszów",
            "prezeszarzadu@krzakholdings.ltd",
            "+48 987 654 321",
            "9876543211");
        legalPersons.Add(legalPerson2);
        
        builder.HasData(legalPersons);
    }
}