using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Client
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public ClientType Type { get; set; }
    
    
    // Attributes that are only applicable to individual clients
    
    [Required] 
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Pesel { get; private set; }

    [DefaultValue(false)] 
    public bool Deleted { get; set; }

    /**
     * Constructor for individual clients
     */
    public Client(int id, string address, string email, string phoneNumber, ClientType type, string firstName, string lastName, string pesel, bool deleted)
    {
        Id = id;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Type = type;
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
        Deleted = deleted;
    }


    // Attributes that are only applicable to company clients

    [Required] 
    public string Name { get; set; }

    [Required]
    public string Krs { get; private set; }

    /**
     * Constructor for company clients
     */
    public Client(int id, string address, string email, string phoneNumber, ClientType type, string name, string krs)
    {
        Id = id;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Type = type;
        Name = name;
        Krs = krs;
    }


    public enum ClientType
    {
        Individual,
        Company
    }
}