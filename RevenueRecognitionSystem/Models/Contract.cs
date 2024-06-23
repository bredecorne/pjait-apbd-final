using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class Contract(
    int id,
    DateTime dateFrom,
    DateTime dateTo,
    Contract.ContractStatus status,
    decimal price,
    DateTime updatesTo)
{
    [Key]
    public int Id { get; set; } = id;

    public DateTime DateFrom { get; set; } = dateFrom;

    public DateTime DateTo { get; set; } = dateTo;

    public ContractStatus Status { get; set; } = status;

    [Column(TypeName="money")] 
    public decimal Price { get; set; } = price;

    public DateTime UpdatesTo { get; set; } = updatesTo;

    public ICollection<ClientContract> ClientContracts { get; set; } = new List<ClientContract>();
    
    public ICollection<ContractSoftware> ContractSoftwares { get; set; } = new List<ContractSoftware>();
    
    public enum ContractStatus
    {
        Planned,
        Active,
        Inactive
    }
}