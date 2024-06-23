using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RevenueRecognitionSystem.Models;

public class Client
{
    [Key] 
    public int Id { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public ClientType Type { get; set; }
    
    public ICollection<ClientContract> ClientContracts { get; set; } = new List<ClientContract>();
    
    // Attributes that are only applicable to individual clients
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Pesel { get; set; }
    
    public bool? Deleted { get; set; }

    /**
     * Constructor for individual clients
     */
    public Client(int id, string address, string email, string phoneNumber, ClientType type, string? firstName, string? lastName, string? pesel, bool deleted)
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

        Name = null;
        Krs = null;
    }


    // Attributes that are only applicable to company clients
    
    public string? Name { get; set; }
    
    public string? Krs { get; set; }

    /**
     * Constructor for company clients
     */
    public Client(int id, string address, string email, string phoneNumber, ClientType type, string? name, string? krs)
    {
        Id = id;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Type = type;
        Name = name;
        Krs = krs;
        
        FirstName = null;
        LastName = null;
        Pesel = null;
        Deleted = null;
    }


    public enum ClientType
    {
        Individual,
        Company
    }
}