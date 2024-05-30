namespace Voxart.Shared.WebInteractions
{
    public class AvatarRequest : IWebRequest
    {
        required public string ClientId { get; set; }
        required public string Text { get; set; }
        required public VoiceCode VoiceCode { get; set; }
        required public byte[] Image { get; set; }
    }
}
