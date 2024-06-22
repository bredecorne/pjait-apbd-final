using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class SoftwareLicence(
    int id,
    string name,
    string description,
    string version,
    SoftwareLicence.SoftwareCategory category,
    bool subscriptionType)
{
    [Key]
    public int Id { get; set; } = id;

    [Required]
    public string Name { get; set; } = name;

    [Required]
    public string Description { get; set; } = description;

    [Required] 
    public string Version { get; set; } = version;

    [Required]
    public SoftwareCategory Category { get; set; } = category;

    [Required]
    public bool SubscriptionType { get; set; } = subscriptionType;

    public ICollection<SoftwareLicenceDiscount> SoftwareLicenceDiscounts { get; set; } = new List<SoftwareLicenceDiscount>();
    
    public ICollection<ContractSoftwareLicence> ContractSoftwareLicences { get; set; } = new List<ContractSoftwareLicence>();

    public enum SoftwareCategory
    {
        Finance,
        Healthcare,
        Education,
        Entertainment
    }
}