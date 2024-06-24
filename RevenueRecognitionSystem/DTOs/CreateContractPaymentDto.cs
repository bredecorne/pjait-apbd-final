namespace RevenueRecognitionSystem.DTOs;

public class CreateContractPaymentDto(int contractId, int clientId, decimal value, DateTime date)
{
    public int ContractId { get; set; } = contractId;

    public int ClientId { get; set; } = clientId;

    public decimal Value { get; set; } = value;

    public DateTime Date { get; set; } = date;
}