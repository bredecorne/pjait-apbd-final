using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class ContractPayment
{
    public ContractPayment(int id, int contractId, int clientId, decimal value, DateTime date)
    {
        Id = id;
        ContractId = contractId;
        ClientId = clientId;
        Value = value;
        Date = date;
    }

    public ContractPayment(int contractId, int clientId, decimal value, DateTime date)
    {
        ContractId = contractId;
        ClientId = clientId;
        Value = value;
        Date = date;
    }

    [Key] public int Id { get; set; }

    [Required] public int ContractId { get; set; }

    [Required] public int ClientId { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Value { get; set; }

    [Required] public DateTime Date { get; set; }

    [ForeignKey("ClientId")] public virtual Client Client { get; set; }

    [ForeignKey("ContractId")] public virtual Contract Contract { get; set; }
}