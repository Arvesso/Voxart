namespace Voxart.Shared.WebInteractions
{
    public class ClusterFilesResponse : IWebResponse
    {
        required public bool IsSuccess { get; set; }
        required public ErrorCode ErrorCode { get; set; }
        required public List<SharedClusterFile> ClusterFiles { get; set; }
    }
}
