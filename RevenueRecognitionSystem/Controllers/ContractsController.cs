using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;

namespace RevenueRecognitionSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(RrsDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await context.Contracts
            .ToListAsync();
        return Ok(contracts);
    }
}