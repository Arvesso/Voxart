namespace Voxart.Shared.WebInteractions
{
    public class RecognizeRequest : IWebRequest
    {
        required public string ClientId { get; set; }
        required public byte[] Audio { get; set; }
        required public AudioFormat AudioFormat { get; set; }
    }
}
