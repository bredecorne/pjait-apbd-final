using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class Discount(
    int id,
    decimal value,
    DateTime dateFrom,
    DateTime dateTo)
{
    [Key]
    public int Id { get; set; } = id;

    [Required]
    public decimal Value { get; set; } = value;

    [Required] 
    public DateTime DateFrom { get; set; } = dateFrom;

    [Required]
    public DateTime DateTo { get; set; } = dateTo;

    public ICollection<SoftwareLicenceDiscount> SoftwareLicenceDiscounts { get; set; } = new List<SoftwareLicenceDiscount>();
}