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
            .Include(c => c.ClientContracts)
            .ThenInclude(cc => cc.Client)
            .Include(c => c.ContractSoftwares)
            .ThenInclude(cc => cc.Software)
            .ToListAsync();
        
        return Ok(contracts);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContract(int id)
    {
        var contract = await context.Contracts
            .Include(c => c.ClientContracts)
            .ThenInclude(cc => cc.Client)
            .Include(c => c.ContractSoftwares)
            .ThenInclude(cc => cc.Software)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (contract == null)
        {
            return NotFound();
        }
        
        return Ok(contract);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto contractDto)
    {
        if (!service.CheckContractDates(contractDto)
            || !service.CheckUpdatesToDate(contractDto))
        {
            return BadRequest();
        }
        
        var software = await context.Softwares.FindAsync(contractDto.SoftwareId);
        
        if (software == null)
        {
            return BadRequest();
        }
        
        var softwareVersion = await context.SoftwareVersions.FindAsync(contractDto.SoftwareVersionId);
        
        if (softwareVersion == null)
        {
            return BadRequest();
        }
        
        var client = await context.Clients.FindAsync(contractDto.ClientId);
        
        if (client == null)
        {
            return BadRequest();
        }
        
        var contract = new Contract(
            contractDto.DateFrom,
            contractDto.DateTo,
            Contract.ContractStatus.AwaitingPayment,
            await service.CalculatePrice(contractDto, context),
            contractDto.UpdatesTo
            );
        
        await context.Contracts.AddAsync(contract);
        await context.SaveChangesAsync();
        
        var contractSoftware = new ContractSoftware(contract.Id, software.Id);
        
        await context.ContractSoftwares.AddAsync(contractSoftware);
        await context.SaveChangesAsync();
        
        var clientContract = new ClientContract(client.Id, contract.Id);
        
        await context.ClientContracts.AddAsync(clientContract);
        await context.SaveChangesAsync();

        return Ok();
    }
}