namespace RevenueRecognitionSystem.DTOs;

public class CreateCompanyClientDto(string address, string email, string phoneNumber, string? name, string? krs)
{
    
    public string Address { get; set; } = address;

    public string Email { get; set; } = email;

    public string PhoneNumber { get; set; } = phoneNumber;

    public string? Name { get; set; } = name;

    public string? Krs { get; set; } = krs;
}