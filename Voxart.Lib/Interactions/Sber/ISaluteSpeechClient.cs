namespace Voxart.Lib.Interactions.Sber
{
    public interface ISaluteSpeechClient
    {
        Task<byte[]> Synthesize(string text, AudioFormat audio = default, VoiceCode voice = default);
        Task<string[]> Recognize(byte[] audio);
    }
}
