using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Models;

public class SoftwareVersion
{
    public SoftwareVersion(int id, int softwareId, string versionNumber, DateTime releaseDate)
    {
        Id = id;
        SoftwareId = softwareId;
        VersionNumber = versionNumber;
        ReleaseDate = releaseDate;
    }

    [Key] public int Id { get; set; }

    [Required] public int SoftwareId { get; set; }

    [Required] public string VersionNumber { get; set; }

    [Required] public DateTime ReleaseDate { get; set; }

    [ForeignKey("SoftwareId")] public virtual Software Software { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; }
}