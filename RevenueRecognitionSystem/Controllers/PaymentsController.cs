using Microsoft.AspNetCore.Mvc;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.DTOs.Contract;
using RevenueRecognitionSystem.Models;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(RrsDbContext context, IPaymentsService service) : ControllerBase
{
    [HttpGet("remaining/{contractId:int}")]
    public async Task<IActionResult> GetRemainingToPay(int contractId, RrsDbContext rrsDbContext)
    {
        var contract = await context.Contracts.FindAsync(contractId);
        if (contract == null)
        {
            return NotFound();
        }

        var remainingToPay = await service.CalculateRemainingToPay(contractId, context);
        return Ok(remainingToPay);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPayment(CreateContractPaymentDto paymentDto)
    {
        var contract = await context.Contracts.FindAsync(paymentDto.ContractId);
        if (contract == null)
        {
            return NotFound();
        }
        
        var client = await context.Clients.FindAsync(paymentDto.ClientId);
        if (client == null)
        {
            return NotFound();
        }

        var remaining = await service.CalculateRemainingToPay(paymentDto.ContractId, context);

        if (remaining < paymentDto.Value)
        {
            return BadRequest();
        }
        
        var payment = new ContractPayment(
            paymentDto.ContractId,
            paymentDto.ClientId,
            paymentDto.Value,
            paymentDto.Date
        );

        context.ContractPayments.Add(payment);
        await context.SaveChangesAsync();

        return Ok(payment);
    }
}