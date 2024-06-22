using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RevenueRecognitionSystem.Models;

public class NaturalPerson(int id, string firstName, string lastName, string address, string email, string phoneNumber, 
    string pesel, bool active) : Person(id, address, email, phoneNumber)
{
    [Required] 
    public string FirstName { get; set; } = firstName;
    
    [Required]
    public string LastName { get; set; } = lastName;
    
    [Required]
    public string Pesel { get; private set; } = pesel;

    [DefaultValue(true)] 
    public bool Active { get; set; } = active;
}