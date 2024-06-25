using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueRecognitionSystem.Models;

[PrimaryKey("SoftwareId", "DiscountId")]
public class SoftwareDiscount
{
    public SoftwareDiscount(int softwareId, int discountId)
    {
        SoftwareId = softwareId;
        DiscountId = discountId;
    }

    public int SoftwareId { get; set; }

    public int DiscountId { get; set; }

    [ForeignKey("SoftwareId")] public virtual Software Software { get; set; }

    [ForeignKey("DiscountId")] public virtual Discount Discount { get; set; }
}