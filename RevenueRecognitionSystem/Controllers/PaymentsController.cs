using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.DTOs.Contract;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IPaymentsService service) : ControllerBase
    {
        [HttpGet("remaining/{contractId:int}")]
        public async Task<IActionResult> GetRemainingToPay(int contractId)
        {
            var remainingToPay = await service.CalculateRemainingToPay(contractId);
            if (remainingToPay == null)
            {
                return NotFound();
            }

            return Ok(remainingToPay);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(CreateContractPaymentDto paymentDto)
        {
            var result = await service.AddPayment(paymentDto);
            if (!result.Success)
            {
                return result.Response;
            }

            return Ok(result.Payment);
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
            var incomeRecognized = await service.CalculateIncomeRecognized(softwareId);
            if (incomeRecognized == null)
            {
                return NotFound();
            }

            return Ok(incomeRecognized);
        }

        [HttpGet("income/{currency}")]
        public async Task<IActionResult> GetIncomeRecognized(string currency)
        {
            var result = await service.CalculateIncomeRecognized(currency);
            if (!result.Success)
            {
                return result.Response;
            }

            return Ok(result.ConvertedIncome);
        }
    }
}
