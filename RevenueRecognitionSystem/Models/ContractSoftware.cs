using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Models;

[PrimaryKey("ContractId", "SoftwareId")]
public class ContractSoftware
{
    public int ContractId { get; set; }
    
    public int SoftwareId { get; set; }

    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }
    
    [ForeignKey("SoftwareId")]
    public Software Software { get; set; }

    public ContractSoftware(int contractId, int softwareId)
    {
        ContractId = contractId;
        SoftwareId = softwareId;
    }
}