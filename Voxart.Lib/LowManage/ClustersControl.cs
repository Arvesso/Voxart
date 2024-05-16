using Voxart.Shared;

namespace Voxart.Lib.LowManage
{
    public class ClustersControl
    {
        private static readonly DirectoryInfo _clusters;

        static ClustersControl()
        {
            _clusters = LocalDirectory.AddDirectory("Clusters");
        }

        public static DirectoryInfo CreateCluster(string cluster)
        {
            return _clusters.CreateSubdirectory(cluster);
        }

        public static DirectoryInfo? GetCluster(string cluster)
        {
            if (ClusterExists(cluster))
            {
                return new DirectoryInfo(Path.Combine(_clusters.FullName, cluster));
            }
            else return null;
        }

        public static bool ClusterExists(string cluster)
        {
            return Directory.Exists(Path.Combine(_clusters.FullName, cluster));
        }

        public static FileInfo CreateFile(DirectoryInfo cluster, string name, FileFormat format, string data)
        {
            return LocalDirectory.CreateFile(cluster, name, format, data);
        }

        public static FileInfo CreateFile(DirectoryInfo cluster, string name, FileFormat format, byte[] data)
        {
            return LocalDirectory.CreateFile(cluster, name, format, data);
        }
    }
}
