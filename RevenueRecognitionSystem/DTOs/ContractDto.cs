using RevenueRecognitionSystem.Models;

namespace RevenueRecognitionSystem.DTOs;

public class ContractDto
{
    public int Id { get; set; }
    
    public DateTime DateFrom { get; set; }
    
    public DateTime DateTo { get; set; }
    
    public Contract.ContractStatus Status { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime UpdatesTo { get; set; }
    
    public ICollection<ClientContract> ClientContracts { get; set; }
    
    public ICollection<ContractSoftware> ContractSoftwares { get; set; }

    public ContractDto(int id, DateTime dateFrom, DateTime dateTo, Contract.ContractStatus status, decimal price, DateTime updatesTo, ICollection<ClientContract> clientContracts, ICollection<ContractSoftware> contractSoftwares)
    {
        Id = id;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
        Price = price;
        UpdatesTo = updatesTo;
        ClientContracts = clientContracts;
        ContractSoftwares = contractSoftwares;
    }
}