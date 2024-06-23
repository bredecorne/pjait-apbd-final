using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Models;

[PrimaryKey("ClientId", "ContractId")]
public class ClientContract
{
    public int ClientId { get; set; }

    public int ContractId { get; set; }

    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; }

    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }

    
    public ClientContract(int clientId, int contractId)
    {
        ClientId = clientId;
        ContractId = contractId;
    }
}