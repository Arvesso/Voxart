namespace Voxart.Shared
{
    public class SharedClusterFile
    {
        public string Name { get; set; } = string.Empty;
        public string Template { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
    }

    public class SharedInviteCode
    {
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
    }
}
