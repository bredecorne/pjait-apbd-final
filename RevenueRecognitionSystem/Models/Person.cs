namespace RevenueRecognitionSystem.Models;

public abstract class Person(string address, string email, string phoneNumber)
{
    public string Address { get; set; } = address;
    public string Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
}