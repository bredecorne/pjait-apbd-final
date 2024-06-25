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

    [Column(TypeName="money")] 
    public decimal Price { get; set; }

    [Required]
    public DateTime UpdatesTo { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int SoftwareId { get; set; }
    
    [Required]
    public int SoftwareVersionId { get; set; }
    
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; }

    [ForeignKey("SoftwareId")]
    public virtual Software Software { get; set; }
    
    [ForeignKey("SoftwareVersionId")]
    public virtual SoftwareVersion SoftwareVersion { get; set; }

    public enum ContractStatus
    {
        AwaitingPayment,
        Active,
        Inactive
    }

    public Contract(int id, DateTime dateFrom, DateTime dateTo, ContractStatus status, decimal price, DateTime updatesTo, int clientId, int softwareId, int softwareVersionId)
    {
        Id = id;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Price = price;
        UpdatesTo = updatesTo;
        ClientId = clientId;
        SoftwareId = softwareId;
        SoftwareVersionId = softwareVersionId;
    }

    public Contract(DateTime dateFrom, DateTime dateTo, ContractStatus status, decimal price, DateTime updatesTo, int clientId, int softwareId, int softwareVersionId)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Price = price;
        UpdatesTo = updatesTo;
        ClientId = clientId;
        SoftwareId = softwareId;
        SoftwareVersionId = softwareVersionId;
    }
}