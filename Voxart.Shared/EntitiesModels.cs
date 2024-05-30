namespace Voxart.Shared
{
    public class SharedClusterFile
    {
        public string ClusterId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public FileFormat FileFormat { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class SharedInviteCode
    {
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
    }
}
