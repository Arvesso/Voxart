using Microsoft.EntityFrameworkCore;

namespace Voxart.Server.Low.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<ClusterFile> ClusterFiles { get; set; }
        public required DbSet<InviteCode> Invites { get; set; }
    }
}
