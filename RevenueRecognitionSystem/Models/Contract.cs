using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class Contract
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateFrom { get; set; }

    [Required]
    public DateTime DateTo { get; set; }
    
    [Required]
    public ContractStatus Status { get; set; }
    
    [Required, Column(TypeName="money")] 
    public decimal Price { get; set; }
    
    [Required] 
    public DateTime UpdatesTo { get; set; }
    
    public ICollection<ContractSoftwareLicence> ContractSoftwareLicences { get; set; } = new List<ContractSoftwareLicence>();
    
    public enum ContractStatus
    {
        Planned,
        Active,
        Inactive
    }
}