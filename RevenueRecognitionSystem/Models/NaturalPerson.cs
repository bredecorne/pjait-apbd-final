namespace RevenueRecognitionSystem.Models;

public class NaturalPerson(string firstName, string lastName, string address, string email, string phoneNumber, 
    string pesel) : Person(address, email, phoneNumber)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Pesel { get; } = pesel;
}