using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class ContractSoftwareLicence
{
    [Key]
    public int ContractId { get; set; }
    
    [Key]
    public int SoftwareLicenceId { get; set; }

    public Contract Contract { get; set; }
    
    public SoftwareLicence SoftwareLicence { get; set; }
}