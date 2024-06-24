using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs.Contract;

namespace RevenueRecognitionSystem.Services;

public class ContractsService : IContractsService
{
    private const int MinLength = 3;
    private const int MaxLength = 30;
    private const int MinUpdatesTo = 365;

    public bool CheckContractDates(CreateContractDto contractDto)
    {
        return (contractDto.DateTo - contractDto.DateFrom).Days >= MinLength 
               && (contractDto.DateTo - contractDto.DateFrom).Days <= MaxLength;
    }
    
    public bool CheckUpdatesToDate(CreateContractDto contractDto)
    {
        return (contractDto.UpdatesTo - contractDto.DateFrom).Days >= MinUpdatesTo;
    }

    public async Task<decimal> CalculatePrice(CreateContractDto contractDto, RrsDbContext context)
    {
        var software = await context.Softwares.FindAsync(contractDto.SoftwareId);
        
        if (software == null)
        {
            throw new ArgumentException();
        }
        
        var price = software.Price;
        
        var discount = await context.Discounts
            .OrderByDescending(d => d.Value)
            .Where(d => d.SoftwareDiscounts.Any(sd => sd.SoftwareId == contractDto.SoftwareId))
            .FirstOrDefaultAsync(d => d.DateFrom <= contractDto.DateFrom && d.DateTo >= contractDto.DateTo);
        
        if (discount == null)
        {
            return price;
        }
        
        return price * discount.Value;
    }
}