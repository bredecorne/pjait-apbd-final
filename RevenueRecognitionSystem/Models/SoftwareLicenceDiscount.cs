using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class SoftwareLicenceDiscount(int softwareLicenceId, int discountId, SoftwareLicence softwareLicence,
    Discount discount)
{
    [Key]
    public int SoftwareLicenceId { get; set; } = softwareLicenceId;

    [Key]
    public int DiscountId { get; set; } = discountId;

    public SoftwareLicence SoftwareLicence { get; set; } = softwareLicence;

    public Discount Discount { get; set; } = discount;
}