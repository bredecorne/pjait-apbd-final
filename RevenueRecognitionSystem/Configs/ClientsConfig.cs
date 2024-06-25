using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Configs;

public class ClientsConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        var clients = new[]
        {
            new Client(1, "Grunwaldzka 4, Lublin", "anastazja.dzwon@onet.pl", "123456789",
                Client.ClientType.Individual, "Anastazja", "Dzwon", "123456789", false),
            new Client(2, "Południowa 1, Lipinki-Łużyckie", "prezes.zarzadu@krzakholdings.com", "987654321",
                Client.ClientType.Company, "Krzak Holdings", "123"),
            new Client(3, "ul. Kościuszki 15/3, Wrocław", "info@bytemasters.pl", "555888222",
                Client.ClientType.Company, "Byte Masters Sp. z o.o.", "456"),
            new Client(4, "Rynek Główny 22, Kraków", "kontakt@kawiarniapodbaranem.pl", "333666999",
                Client.ClientType.Company, "Kawiarnia Pod Baranem", "789"),
            new Client(5, "Aleje Jerozolimskie 55, Warszawa", "biuro@globalconsultinggroup.pl", "222444888",
                Client.ClientType.Company, "Global Consulting Group", "012"),
            new Client(6, "ul. Długa 33/12, Gdańsk", "jan.kowalski@gmail.com", "777111333",
                Client.ClientType.Individual, "Jan", "Kowalski", "777111333", false),
            new Client(7, "Plac Wolności 8, Poznań", "ewa.nowak@wp.pl", "999555111",
                Client.ClientType.Individual, "Ewa", "Nowak", "999555111", true)
        };

        builder.HasData(clients);
    }
}