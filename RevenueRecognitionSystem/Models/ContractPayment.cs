using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class ContractPayment
{
    [Key]
    public int Id { get; set; }

    public int ContractId { get; set; }
    
    public int ClientId { get; set; }
    
    [Column(TypeName="money")] 
    public decimal Value { get; set; }
    
    public DateTime Date { get; set; }
    
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; }
    
    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }

    public ContractPayment(int id, int contractId, int clientId, decimal value, DateTime date)
    {
        Id = id;
        ContractId = contractId;
        ClientId = clientId;
        Value = value;
        Date = date;
    }
}