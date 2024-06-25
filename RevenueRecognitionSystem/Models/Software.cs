using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Software
{
    public enum SoftwareCategory
    {
        Finance,
        Healthcare,
        Education,
        Entertainment
    }

    public Software(int id, string name, string description, SoftwareCategory category, bool subscriptionType,
        decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        SubscriptionType = subscriptionType;
        Price = price;
    }

    public Software(string name, string description, SoftwareCategory category, bool subscriptionType, decimal price)
    {
        Name = name;
        Description = description;
        Category = category;
        SubscriptionType = subscriptionType;
        Price = price;
    }

    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public SoftwareCategory Category { get; set; }

    [Required] public bool SubscriptionType { get; set; }

    [Required] public decimal Price { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; }

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }

    public ICollection<SoftwareVersion> SoftwareVersions { get; set; }
}