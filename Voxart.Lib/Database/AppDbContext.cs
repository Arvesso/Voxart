using Microsoft.EntityFrameworkCore;
using Voxart.Shared;

namespace Voxart.Lib.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<AppDbUserEntity> Users { get; set; }
        public required DbSet<AppDbFileEntity> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // TODO: Test.
            {
                Database.EnsureCreated();
                Console.WriteLine("Configured DB");
            }
        }

        public UserControl AddUser(string email, string hash, string cluster)
        {
            var user = Users.Add(new()
            {
                Email = email,
                Hash = hash,
                Cluster = cluster
            });

            SaveChanges();

            return new(this, user.Entity);
        }

        public UserControl? GetUser(int id)
        {
            var user = Users.Find(id);
            return user != null ? new(this, user) : null;
        }

        public UserControl? GetUser(string raw)
        {
            var user = Users.FirstOrDefault(x => x.Email == raw || x.Cluster == raw || x.Token == raw);
            return user != null ? new(this, user) : null;
        }

        public void AddFile(string cluster, string file, DateTime create, FileFormat format)
        {
            Files.Add(new()
            {
                ClusterId = cluster,
                FileName = file,
                CreateTime = create,
                FileFormat = format
            });
            SaveChanges();
        }
    }
}
