namespace Voxart.Shared.WebInteractions
{
    public interface IWebResponse
    {
        bool IsSuccess { get; }
        ErrorCode ErrorCode { get; }
    }
}
