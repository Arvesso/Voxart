namespace Voxart.Shared.WebInteractions
{
    public class AvatarResponse : IWebResponse
    {
        required public bool IsSuccess { get; set; }
        required public ErrorCode ErrorCode { get; set; }
        public string? ResultLink { get; set; }
    }
}
