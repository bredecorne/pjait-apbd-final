using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;

namespace RevenueRecognitionSystem.Services;

public class PaymentsService : IPaymentsService
{

    public async Task<decimal> CalculateRemainingToPay(int contractId, RrsDbContext context)
    {
        var contract = await context.Contracts.FindAsync(contractId);
        
        if (contract == null)
        {
            throw new ArgumentException();
        }
        
        var payments = await context.ContractPayments
            .Where(p => p.ContractId == contractId)
            .ToListAsync();
        
        var totalPaid = payments.Sum(p => p.Value);
        var remainingToPay = contract.Price - totalPaid;
        
        return remainingToPay;
    }
}