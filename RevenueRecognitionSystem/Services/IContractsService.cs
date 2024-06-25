using RevenueRecognitionSystem.DTOs.Contract;
using System.Threading.Tasks;

namespace RevenueRecognitionSystem.Services;

public interface IContractsService
{
    Task<bool> ValidateContractAsync(CreateContractDto contractDto);
    
    Task<decimal> CalculatePrice(CreateContractDto contractDto);
}