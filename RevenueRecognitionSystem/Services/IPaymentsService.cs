using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;

namespace RevenueRecognitionSystem.Services;

public interface IPaymentsService
{
    Task<decimal> CalculateRemainingToPay(int contractId, RrsDbContext context);
}