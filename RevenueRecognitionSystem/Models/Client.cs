using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Client
{
    public enum ClientType
    {
        Individual,
        Company
    }

    /**
     * Constructor for individual clients
     */
    public Client(int id, string address, string email, string phoneNumber, ClientType type, string? firstName,
        string? lastName, string? pesel, bool deleted)
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

    public Client(string address, string email, string phoneNumber, ClientType type, string? firstName,
        string? lastName, string? pesel, bool deleted)
    {
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

    public Client(string address, string email, string phoneNumber, ClientType type, string? name, string? krs)
    {
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

    [Key] public int Id { get; set; }

    [Required] public string Address { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public ClientType Type { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; }

    // Attributes that are only applicable to individual clients

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Pesel { get; set; }

    public bool? Deleted { get; set; }


    // Attributes that are only applicable to company clients

    public string? Name { get; set; }

    public string? Krs { get; set; }
}