namespace RevenueRecognitionSystem.Models;

public class LegalPerson(string name, string address, string email, string phoneNumber, string krs) 
    : Person(address, email, phoneNumber)
{
    public string Name { get; set; } = name;
    public string Krs { get; } = krs;
}