using System.Diagnostics.CodeAnalysis;
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
}