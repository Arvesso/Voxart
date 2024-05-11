using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voxart.Shared;

namespace Voxart.Lib.Database
{
    [Table("Users")]
    public class AppDbUserEntity
    {
        [Key] public int Id { get; set; }
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Hash { get; set; } = string.Empty;
        [Required] public string Cluster { get; set; } = string.Empty;
        public string? Token { get; set; }
    }

    [Table("Files"), Index(nameof(ClusterId), nameof(FileFormat), nameof(CreateTime))]
    public class AppDbFileEntity
    {
        [Key] public int Id { get; set; }
        [Required] public string ClusterId { get; set; } = string.Empty;
        [Required] public string FileName { get; set; } = string.Empty;
        [Required] public DateTime CreateTime { get; set; }
        [Required] public FileFormat FileFormat { get; set; }
    }
}
