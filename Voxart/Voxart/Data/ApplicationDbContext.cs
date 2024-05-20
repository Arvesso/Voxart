using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voxart.Shared.Models;

namespace Voxart.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public required DbSet<ClusterFile> ClusterFiles { get; set; }
        public required DbSet<InviteCode> Invites { get; set; }
    }
}
