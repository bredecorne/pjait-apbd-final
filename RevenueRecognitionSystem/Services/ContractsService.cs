using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs.Contract;

namespace RevenueRecognitionSystem.Services;

public class ContractsService(RrsDbContext context) : IContractsService
{
    private const int MinLength = 3;
    private const int MaxLength = 30;
    private const int MinUpdatesTo = 365;

    public async Task<bool> ValidateContractAsync(CreateContractDto contractDto)
    {
        return CheckContractDates(contractDto) && CheckUpdatesToDate(contractDto) &&
               await ValidateEntitiesAsync(contractDto);
    }

    public async Task<decimal> CalculatePrice(CreateContractDto contractDto)
    {
        var software = await context.Softwares.FindAsync(contractDto.SoftwareId);
        if (software == null) throw new ArgumentException("Invalid software ID");

        var price = software.Price;
        var discount = await context.Discounts
            .Where(d => d.SoftwareDiscounts.Any(sd => sd.SoftwareId == contractDto.SoftwareId) &&
                        d.DateFrom <= contractDto.DateFrom && d.DateTo >= contractDto.DateTo)
            .OrderByDescending(d => d.Value)
            .FirstOrDefaultAsync();

        return price * discount?.Value ?? price;
    }

    private static bool CheckContractDates(CreateContractDto contractDto)
    {
        var duration = (contractDto.DateTo - contractDto.DateFrom).Days;
        return duration is >= MinLength and <= MaxLength;
    }

    private static bool CheckUpdatesToDate(CreateContractDto contractDto)
    {
        return (contractDto.UpdatesTo - contractDto.DateFrom).Days >= MinUpdatesTo;
    }

    private async Task<bool> ValidateEntitiesAsync(CreateContractDto contractDto)
    {
        var software = await context.Softwares.FindAsync(contractDto.SoftwareId);
        var softwareVersion = await context.SoftwareVersions.FindAsync(contractDto.SoftwareVersionId);
        var client = await context.Clients.FindAsync(contractDto.ClientId);

        return software != null && softwareVersion != null && client != null;
    }
}