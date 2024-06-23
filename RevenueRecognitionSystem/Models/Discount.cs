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

    public decimal Value { get; set; } = value;

    public DateTime DateFrom { get; set; } = dateFrom;

    public DateTime DateTo { get; set; } = dateTo;

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = new List<SoftwareDiscount>();
}