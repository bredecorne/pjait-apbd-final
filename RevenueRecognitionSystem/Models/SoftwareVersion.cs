using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RevenueRecognitionSystem.Models;

public class SoftwareVersion
{
    [Key]
    public int Id { get; set; }

    public int SoftwareId { get; set; }

    public string VersionNumber { get; set; }

    public DateTime ReleaseDate { get; set; }

    [ForeignKey("SoftwareId")]
    public virtual Software Software { get; set; }

    public SoftwareVersion(int id, int softwareId, string versionNumber, DateTime releaseDate)
    {
        Id = id;
        SoftwareId = softwareId;
        VersionNumber = versionNumber;
        ReleaseDate = releaseDate;
    }
}