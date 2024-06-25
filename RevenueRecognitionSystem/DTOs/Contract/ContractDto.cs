namespace RevenueRecognitionSystem.DTOs.Contract;

public class ContractDto(
    DateTime dateFrom,
    DateTime dateTo,
    decimal price,
    DateTime updatesTo,
    string? clientFirstName,
    string? clientLastName,
    string? name,
    string softwareName,
    string softwareVersionNumber)
{
    public DateTime DateFrom { get; set; } = dateFrom;

    public DateTime DateTo { get; set; } = dateTo;

    public decimal Price { get; set; } = price;

    public DateTime UpdatesTo { get; set; } = updatesTo;

    public string? ClientFirstName { get; set; } = clientFirstName;

    public string? ClientLastName { get; set; } = clientLastName;

    public string? Name { get; set; } = name;

    public string SoftwareName { get; set; } = softwareName;

    public string SoftwareVersionNumber { get; set; } = softwareVersionNumber;
}