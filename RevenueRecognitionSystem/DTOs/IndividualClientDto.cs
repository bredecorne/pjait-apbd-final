namespace RevenueRecognitionSystem.DTOs;

public class IndividualClientDto(
    int id,
    string address,
    string email,
    string phoneNumber,
    string? firstName,
    string? lastName,
    string? pesel,
    bool? deleted)
{
    public int Id { get; set; } = id;

    public string Address { get; set; } = address;

    public string Email { get; set; } = email;

    public string PhoneNumber { get; set; } = phoneNumber;

    public string? FirstName { get; set; } = firstName;

    public string? LastName { get; set; } = lastName;

    public string? Pesel { get; set; } = pesel;

    public bool? Deleted { get; set; } = deleted;
}