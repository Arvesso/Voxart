namespace Voxart.Lib.Interactions.D_ID
{
    public interface IDidClient
    {
        Task<byte[]> CreateTalk(string audioLink, string imageLink);
    }
}
