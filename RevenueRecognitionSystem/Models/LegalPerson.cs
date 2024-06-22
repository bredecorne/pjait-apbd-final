using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class LegalPerson(int id, string name, string address, string email, string phoneNumber, string krs)
    : Person(id, address, email, phoneNumber)
{
    [Required]
    public string Name { get; set; } = name;

    [Required]
    public string Krs { get; private set; } = krs;
}