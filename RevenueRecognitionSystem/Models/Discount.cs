using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Discount
{
    [Key]
    public int Id { get; set; }

    [Required]
    public decimal Value { get; set; }

    [Required]
    public DateTime DateFrom { get; set; }

    [Required]
    public DateTime DateTo { get; set; }
    
    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; }

    public Discount(int id, decimal value, DateTime dateFrom, DateTime dateTo)
    {
        Id = id;
        Value = value;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public Discount(decimal value, DateTime dateFrom, DateTime dateTo)
    {
        Value = value;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}