using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs.Contract;

namespace RevenueRecognitionSystem.Services;

public interface IContractsService
{
    bool CheckContractDates(CreateContractDto contractDto);
    
    bool CheckUpdatesToDate(CreateContractDto contractDto);
    
    Task<decimal> CalculatePrice(CreateContractDto contractDto, RrsDbContext context);
    
}