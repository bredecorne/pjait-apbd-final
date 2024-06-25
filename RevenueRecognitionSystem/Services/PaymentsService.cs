using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Services;

public class PaymentsService(RrsDbContext context) : IPaymentsService
{

    public async Task<decimal> CalculateRemainingToPay(int contractId)
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

    public async void UpdateContractStatus(int contractId)
    {
        var contract = await context.Contracts.FindAsync(contractId);
        
        if (contract == null)
        {
            throw new ArgumentException();
        }

        contract.Status = Contract.ContractStatus.Active;
        
        await context.SaveChangesAsync();
    }

    public async Task<decimal> CalculateIncomeRecognized(int softwareId)
    {
        var contracts = await context.Contracts
            .Include(c => c.ContractSoftwares)
            .Where(c => c.ContractSoftwares.Any(cs => cs.SoftwareId == softwareId))
            .Where(c => c.Status == Contract.ContractStatus.Active || c.Status == Contract.ContractStatus.Inactive)
            .ToListAsync();
        
        var incomeRecognized = contracts.Sum(c => c.Price);
        
        return await Task.FromResult(incomeRecognized);
    }
    
    public async Task<decimal> CalculateIncomeRecognized()
    {
        var contracts = await context.Contracts
            .Where(c => c.Status == Contract.ContractStatus.Active || c.Status == Contract.ContractStatus.Inactive)
            .ToListAsync();
        
        var incomeRecognized = contracts.Sum(c => c.Price);
        
        return await Task.FromResult(incomeRecognized);
    }
}