using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Services;

public class PaymentsService(RrsDbContext context) : IPaymentsService
{
    public async Task<decimal?> CalculateRemainingToPay(int contractId)
    {
        var contract = await context.Contracts.FindAsync(contractId);
        if (contract == null) return null;

        var payments = await context.ContractPayments
            .Where(p => p.ContractId == contractId)
            .ToListAsync();

        var totalPaid = payments.Sum(p => p.Value);
        var remainingToPay = contract.Price - totalPaid;

        return remainingToPay;
    }

    public async Task<(bool Success, IActionResult Response, ContractPayment Payment)> AddPayment(
        CreateContractPaymentDto paymentDto)
    {
        var contract = await context.Contracts.FindAsync(paymentDto.ContractId);
        if (contract == null) return (false, new NotFoundResult(), null);

        var client = await context.Clients.FindAsync(paymentDto.ClientId);
        if (client == null) return (false, new NotFoundResult(), null);

        var remaining = await CalculateRemainingToPay(paymentDto.ContractId);
        if (remaining < paymentDto.Value) return (false, new BadRequestResult(), null);

        var payment = new ContractPayment(
            paymentDto.ContractId,
            paymentDto.ClientId,
            paymentDto.Value,
            paymentDto.Date
        );

        context.ContractPayments.Add(payment);
        await context.SaveChangesAsync();

        if (remaining - paymentDto.Value == 0) await UpdateContractStatus(paymentDto.ContractId);

        return (true, null, payment);
    }

    public async Task<decimal> CalculateIncomeRecognized()
    {
        var contracts = await context.Contracts
            .Where(c => c.Status == Contract.ContractStatus.Active || c.Status == Contract.ContractStatus.Inactive)
            .ToListAsync();

        var incomeRecognized = contracts.Sum(c => c.Price);

        return incomeRecognized;
    }

    public async Task<decimal?> CalculateIncomeRecognized(int softwareId)
    {
        var contracts = await context.Contracts
            .Where(c => c.SoftwareId == softwareId)
            .Where(c => c.Status == Contract.ContractStatus.Active || c.Status == Contract.ContractStatus.Inactive)
            .ToListAsync();

        if (!contracts.Any()) return null;

        var incomeRecognized = contracts.Sum(c => c.Price);

        return incomeRecognized;
    }

    public async Task<(bool Success, IActionResult Response, decimal ConvertedIncome)> CalculateIncomeRecognized(
        string currency)
    {
        var incomeRecognized = await CalculateIncomeRecognized();
        currency = currency.ToUpperInvariant();

        if (currency == "PLN") return (true, null, incomeRecognized);

        using var httpClient = new HttpClient();
        var apiUrl = "https://api.exchangerate-api.com/v4/latest/PLN"; // Base currency is PLN

        try
        {
            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var exchangeData = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();

            if (exchangeData == null) return (false, new StatusCodeResult(503), 0);

            if (!exchangeData.Rates.TryGetValue(currency, out var exchangeRate))
                return (false, new NotFoundObjectResult($"Exchange rate for {currency} not found."), 0);

            var convertedIncome = incomeRecognized * exchangeRate;

            return (true, null, convertedIncome);
        }
        catch (HttpRequestException ex)
        {
            return (false, new StatusCodeResult(503), 0);
        }
    }

    public async Task<decimal> CalculateExpectedIncome()
    {
        var contracts = await context.Contracts
            .ToListAsync();

        var incomeExpected = contracts.Sum(c => c.Price);

        return incomeExpected;
    }

    private async Task UpdateContractStatus(int contractId)
    {
        var contract = await context.Contracts.FindAsync(contractId);
        if (contract == null) throw new ArgumentException();

        contract.Status = Contract.ContractStatus.Active;
        await context.SaveChangesAsync();
    }

    private class ExchangeRateResponse
    {
        [JsonProperty("rates")] public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}