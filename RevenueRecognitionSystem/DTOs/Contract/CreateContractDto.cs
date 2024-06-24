using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.DTOs.Contract;

public class CreateContractDto(
    DateTime dateFrom,
    DateTime dateTo,
    DateTime updatesTo,
    int clientId,
    int softwareId,
    int softwareVersionId)
{
    public DateTime DateFrom { get; set; } = dateFrom;

    public DateTime DateTo { get; set; } = dateTo;
    
    public DateTime UpdatesTo { get; set; } = updatesTo;

    public int ClientId { get; set; } = clientId;

    public int SoftwareId { get; set; } = softwareId;

    public int SoftwareVersionId { get; set; } = softwareVersionId;
}