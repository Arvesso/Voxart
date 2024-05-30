using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Voxart.Shared;

namespace Voxart.Server.Low.Database
{
    [Table("ClusterFiles")]
    public class ClusterFile
    {
        [Key] public int Id { get; set; }
        [Required] public string ClusterId { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public FileFormat FileFormat { get; set; }
        [Required] public DateTime CreationTime { get; set; }
    }

    [Table("Invites")]
    public class InviteCode
    {
        [Key] public int Id { get; set; }
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public bool IsUsed { get; set; } = false;
    }
}
