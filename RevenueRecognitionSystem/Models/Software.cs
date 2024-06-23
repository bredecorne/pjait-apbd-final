using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Software(
    int id,
    string name,
    string description,
    Software.SoftwareCategory category,
    bool subscriptionType)
{
    [Key]
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public SoftwareCategory Category { get; set; } = category;

    public bool SubscriptionType { get; set; } = subscriptionType;

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = new List<SoftwareDiscount>();
    
    public ICollection<ContractSoftware> ContractSoftwares { get; set; } = new List<ContractSoftware>();
    
    public ICollection<SoftwareVersion> SoftwareVersions { get; set; } = new List<SoftwareVersion>();

    public enum SoftwareCategory
    {
        Finance,
        Healthcare,
        Education,
        Entertainment
    }
}