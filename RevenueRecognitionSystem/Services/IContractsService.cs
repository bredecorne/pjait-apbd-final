using RevenueRecognitionSystem.DTOs.Contract;

namespace RevenueRecognitionSystem.Services;

public interface IContractsService
{
    Task<bool> ValidateContractAsync(CreateContractDto contractDto);

    Task<decimal> CalculatePrice(CreateContractDto contractDto);
}