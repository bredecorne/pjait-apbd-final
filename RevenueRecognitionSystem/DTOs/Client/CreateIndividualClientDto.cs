namespace RevenueRecognitionSystem.DTOs;

public class CreateIndividualClientDto(
    string address,
    string email,
    string phoneNumber,
    string? firstName,
    string? lastName,
    string? pesel)
{
    public string Address { get; set; } = address;

    public string Email { get; set; } = email;

    public string PhoneNumber { get; set; } = phoneNumber;

    public string? FirstName { get; set; } = firstName;

    public string? LastName { get; set; } = lastName;

    public string? Pesel { get; set; } = pesel;
}