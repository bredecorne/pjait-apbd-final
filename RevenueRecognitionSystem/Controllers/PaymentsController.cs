using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        var remainingToPay = await service.CalculateRemainingToPay(contractId);
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

        var remaining = await service.CalculateRemainingToPay(paymentDto.ContractId);

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
        
        if (remaining - paymentDto.Value == 0)
        {
            service.UpdateContractStatus(paymentDto.ContractId);
        }

        return Ok(payment);
    }
    
    [HttpGet("income/all")]
    public async Task<IActionResult> GetIncomeRecognized()
    {
        var incomeRecognized = await service.CalculateIncomeRecognized();
        return Ok(incomeRecognized);
    }
    

    [HttpGet("income/{softwareId:int}")]
    public async Task<IActionResult> GetIncomeRecognized(int softwareId)
    {
        var software = await context.Softwares.FindAsync(softwareId);
        if (software == null)
        {
            return NotFound();
        }

        var incomeRecognized = await service.CalculateIncomeRecognized(softwareId);

        return Ok(incomeRecognized);
    }

    [HttpGet("income/{currency}")]
    public async Task<IActionResult> GetIncomeRecognized(string currency)
    {
        var incomeRecognized = await service.CalculateIncomeRecognized();

        currency = currency.ToUpperInvariant(); 
        if (currency == "PLN") 
        {
            return Ok(incomeRecognized);
        }
    
        using var httpClient = new HttpClient();
        var apiUrl = $"https://api.exchangerate-api.com/v4/latest/PLN"; // Base currency is PLN
    
        try
        {
            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
        
            var exchangeData = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();
        
            if (!exchangeData.Rates.TryGetValue(currency, out var exchangeRate))
            {
                return NotFound($"Exchange rate for {currency} not found."); 
            }

            // 3. Conversion
            var convertedIncome = incomeRecognized * exchangeRate;

            return Ok(convertedIncome);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, $"Error fetching exchange rate data: {ex.Message}");
        }
    }

    public class ExchangeRateResponse
    {
        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}