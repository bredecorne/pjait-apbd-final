using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs.Contract;
using RevenueRecognitionSystem.Models;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(RrsDbContext context, IContractsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        
        var contracts = await context.Contracts
            .Include(c => c.Client)
            .Include(c => c.Software)
            .Include(c => c.SoftwareVersion)
            .Select(c => new ContractDto(
                c.DateFrom,
                c.DateTo,
                c.Price,
                c.UpdatesTo,
                c.Client.FirstName,
                c.Client.LastName,
                c.Client.Name,
                c.Software.Name,
                c.SoftwareVersion.VersionNumber))
            .ToListAsync();
        
        return Ok(contracts);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContracts(int id)
    {

        var contract = await context.Contracts
            .Include(c => c.Client)
            .Include(c => c.Software)
            .Include(c => c.SoftwareVersion)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contract == null)
        {
            return NotFound();
        }
        
        var contractDto = new ContractDto(
            contract.DateFrom,
            contract.DateTo,
            contract.Price,
            contract.UpdatesTo,
            contract.Client.FirstName,
            contract.Client.LastName,
            contract.Client.Name,
            contract.Software.Name,
            contract.SoftwareVersion.VersionNumber);
        
        return Ok(contractDto);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto contractDto)
    {
        
        var contract = new Contract(
            contractDto.DateFrom,
            contractDto.DateTo,
            Contract.ContractStatus.AwaitingPayment,
            await service.CalculatePrice(contractDto),
            contractDto.UpdatesTo,
            contractDto.ClientId,
            contractDto.SoftwareId,
            contractDto.SoftwareVersionId
        );
    
        await context.Contracts.AddAsync(contract);
        await context.SaveChangesAsync();
    
        return Ok();
    }
}