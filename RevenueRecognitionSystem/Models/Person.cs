using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public abstract class Person(int id, string address, string email, string phoneNumber)
{
    [Key] 
    public int Id { get; set; } = id;
    
    [Required]
    public string Address { get; set; } = address;
    
    [Required]
    public string Email { get; set; } = email;
    
    [Required]
    public string PhoneNumber { get; set; } = phoneNumber;
    
    
}