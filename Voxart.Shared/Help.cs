namespace Voxart.Shared
{
    public static class Help
    {
        public static readonly Dictionary<VoiceCode, string> VoiceTextCode = new()
        {
            { VoiceCode.Nec_24000, "Nec_24000" },
            { VoiceCode.Bys_24000, "Bys_24000" }
        };

        public static VoiceCode GetVoiceCode(string text)
        {
            return VoiceTextCode.First(kv => kv.Value == text).Key;
        }

        public static string GetTextFormat(FileFormat format)
        {
            return format switch
            {
                FileFormat.Wav16 => ".wav",
                FileFormat.MP3 => ".mp3",
                FileFormat.MP4 => ".mp4",
                FileFormat.TXT => ".txt",
                FileFormat.JPG => ".jpg",
                _ => ".unk"
            };
        }
    }
}
