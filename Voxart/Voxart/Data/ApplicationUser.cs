using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Voxart.Models;

namespace Voxart.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required] public string Cluster { get; set; } = string.Empty;
        [Required] public DateTime LastActivity { get; set; }
        [Required] public Limit UseLimitType { get; set; }
        [Required] public int UsedAvatarsGeneration { get; set; }
        [Required] public int UsedVoiceGeneration { get; set; }
        [Required] public int UsedVoiceRecognition { get; set; }
    }
}
