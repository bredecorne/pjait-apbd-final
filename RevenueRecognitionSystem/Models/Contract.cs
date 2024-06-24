using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class Contract
{
    [Key]
    public int Id { get; set; }

    public DateTime DateFrom { get; set; }
    
    public DateTime DateTo { get; set; }

    public ContractStatus Status { get; set; }

    [Column(TypeName="money")] 
    public decimal Price { get; set; }

    public DateTime UpdatesTo { get; set; }

    public ICollection<ClientContract> ClientContracts { get; set; }
    
    public ICollection<ContractSoftware> ContractSoftwares { get; set; }

    public Contract(int id, DateTime dateFrom, DateTime dateTo, ContractStatus status, decimal price, DateTime updatesTo)
    {
        Id = id;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Price = price;
        UpdatesTo = updatesTo;
    }

    public Contract(DateTime dateFrom, DateTime dateTo, ContractStatus status, decimal price, DateTime updatesTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Price = price;
        UpdatesTo = updatesTo;
    }

    public enum ContractStatus
    {
        AwaitingPayment,
        Planned,
        Active,
        Inactive
    }
}