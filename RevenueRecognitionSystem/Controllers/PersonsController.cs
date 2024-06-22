using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.DTOs;

namespace RevenueRecognitionSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController(RrsDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        var naturalPersons = await context.NaturalPersons.ToListAsync();
        var legalPersons = await context.LegalPersons.ToListAsync();
        
        var persons = new List<PersonDto>();

        persons.AddRange(naturalPersons.Select(np => new PersonDto(
            np.Id,
            np.FirstName,
            np.LastName,
            np.Pesel,
            np.Active,
            np.Address,
            np.Email,
            np.PhoneNumber,
            "NaturalPerson"
        )));
        
        persons.AddRange(legalPersons.Select(lp => new PersonDto(
            lp.Id,
            lp.Name,
            lp.Krs,
            lp.Address,
            lp.Email,
            lp.PhoneNumber,
            "LegalPerson"
        )));

        return Ok(persons);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, PersonDto personDto)
    {
        switch (personDto.PersonType)
        {
            case "NaturalPerson":
            {
                var naturalPerson = await context.NaturalPersons.FindAsync(id);
                
                if (naturalPerson == null)
                {
                    return NotFound();
                }

                naturalPerson.FirstName = personDto.FirstName;
                naturalPerson.LastName = personDto.LastName;
                naturalPerson.Active = personDto.Active;
                naturalPerson.Address = personDto.Address;
                naturalPerson.Email = personDto.Email;
                naturalPerson.PhoneNumber = personDto.PhoneNumber;
                
                break;
            }
            case "LegalPerson":
            {
                var legalPerson = await context.LegalPersons.FindAsync(id);
                
                if (legalPerson == null)
                {
                    return NotFound();
                }

                legalPerson.Name = personDto.Name;
                legalPerson.Address = personDto.Address;
                legalPerson.Email = personDto.Email;
                legalPerson.PhoneNumber = personDto.PhoneNumber;
                
                break;
            }
        }

        await context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePerson(int id, string personType)
    {
        switch (personType)
        {
            case "NaturalPerson":
                var naturalPerson = await context.NaturalPersons.FindAsync(id);
                
                if (naturalPerson == null)
                {
                    return NotFound();
                }
                
                naturalPerson.Active = false;
                
                break;
            case "LegalPerson":
                return BadRequest("Legal persons cannot be deleted.");
        }

        await context.SaveChangesAsync();
        return Ok();
    }
}