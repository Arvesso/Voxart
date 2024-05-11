using Voxart.Shared;

namespace Voxart.Lib.Database
{
    public class UserControl(AppDbContext db, AppDbUserEntity user)
    {
        public string Email 
        {
            get => user.Email;
            set
            {
                user.Email = value;
                db.SaveChanges();
            }
        }

        public string Hash
        {
            get => user.Hash;
            set
            {
                user.Hash = value;
                db.SaveChanges();
            }
        }

        public string? Token
        {
            get => user.Token;
            set
            {
                user.Token = value;
                db.SaveChanges();
            }
        }

        public string Cluster { get; } = user.Cluster;

        public void AddFile(string file, DateTime create, FileFormat format)
        {
            db.AddFile(Cluster, file, create, format);
        }

        public IEnumerable<AppDbFileEntity> GetFiles()
        {
            return db.Files.Where(f => f.ClusterId == Cluster);
        }

        public IEnumerable<AppDbFileEntity> GetOrderedFiles(FileFormat format, int limit = 8)
        {
            return db.Files.Where(f => f.ClusterId == Cluster && f.FileFormat == format).OrderByDescending(file => file.CreateTime).Take(limit);
        }
    }
}
