using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;

namespace RevenueRecognitionSystem.Services;

public interface IPaymentsService
{
    Task<decimal> CalculateRemainingToPay(int contractId);
    
    void UpdateContractStatus(int contractId);
    
    Task<decimal> CalculateIncomeRecognized(int softwareId);
    
    Task<decimal> CalculateIncomeRecognized();
}