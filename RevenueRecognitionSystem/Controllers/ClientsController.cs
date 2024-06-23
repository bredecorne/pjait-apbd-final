using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(RrsDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await context.Clients.ToListAsync();
        
        var individualClients = clients
            .FindAll(c => c.Type == Client.ClientType.Individual)
            .Select(c => new IndividualClientDto(
                c.Id,
                c.Address,
                c.Email,
                c.PhoneNumber,
                c.FirstName,
                c.LastName,
                c.Pesel,
                c.Deleted
                ));

        var companyClients = clients
            .FindAll(c => c.Type == Client.ClientType.Company)
            .Select(c => new CompanyClientDto(c.Id,
                c.Address,
                c.Email,
                c.PhoneNumber,
                c.Name,
                c.Krs
                ));
        
        return Ok(new { individualClients, companyClients });
    }
    
    [HttpPatch("individual/{id:int}")]
    public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] JsonPatchDocument<IndividualClientDto> patchDoc)
    {
        var client = await context.Clients.FindAsync(id);
        if (client is not { Type: Client.ClientType.Individual })
        {
            return NotFound();
        }

        var clientToPatch = new IndividualClientDto(
            client.Id,
            client.Address,
            client.Email,
            client.PhoneNumber,
            client.FirstName,
            client.LastName,
            client.Pesel,
            client.Deleted
        );
        
        patchDoc.ApplyTo(clientToPatch); 

        if (!TryValidateModel(clientToPatch))
        {
            return ValidationProblem(ModelState);
        }

        client.Address = clientToPatch.Address;
        client.Email = clientToPatch.Email;
        client.PhoneNumber = clientToPatch.PhoneNumber;
        client.FirstName = clientToPatch.FirstName;
        client.LastName = clientToPatch.LastName;

        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("company/{id:int}")]
    public async Task<IActionResult> UpdateCompanyClient(int id, [FromBody] JsonPatchDocument<CompanyClientDto> patchDoc)
    {
        var client = await context.Clients.FindAsync(id);
        if (client is not { Type: Client.ClientType.Company })
        {
            return NotFound();
        }

        var clientToPatch = new CompanyClientDto(
            client.Id,
            client.Address,
            client.Email,
            client.PhoneNumber,
            client.Name,
            client.Krs
            );
        
        patchDoc.ApplyTo(clientToPatch);

        if (!TryValidateModel(clientToPatch))
        {
            return ValidationProblem(ModelState);
        }

        client.Address = clientToPatch.Address;
        client.Email = clientToPatch.Email;
        client.PhoneNumber = clientToPatch.PhoneNumber;
        client.Name = clientToPatch.Name;

        await context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await context.Clients.FindAsync(id);
        
        if (client is null)
        {
            return NotFound();
        }
        
        if (client is not { Type: Client.ClientType.Individual })
        {
            return BadRequest("Only individual clients can be deleted");
        }

        client.Deleted = true;
        
        await context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("individual")]
    public async Task<IActionResult> AddIndividualClient([FromBody] CreateIndividualClientDto clientDto)
    {
        if (!TryValidateModel(clientDto))
        {
            return ValidationProblem(ModelState);
        }

        var client = new Client(
            clientDto.Address,
            clientDto.Email,
            clientDto.PhoneNumber,
            Client.ClientType.Individual,
            clientDto.FirstName,
            clientDto.LastName,
            clientDto.Pesel,
            false
            );
        

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("company")]
    public async Task<IActionResult> AddCompanyClient([FromBody] CreateCompanyClientDto clientDto)
    {
        if (!TryValidateModel(clientDto))
        {
            return ValidationProblem(ModelState);
        }
        
        var client = new Client(
            clientDto.Address,
            clientDto.Email,
            clientDto.PhoneNumber,
            Client.ClientType.Company,
            clientDto.Name,
            clientDto.Krs
            );

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        return Ok();
    }
}