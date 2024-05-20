namespace Voxart.Shared
{
    public static class Help
    {
        public static readonly Dictionary<VoiceCode, string> VoiceTextCode = new()
        {
            { VoiceCode.Nec_24000, "Nec_24000" },
            { VoiceCode.TEMP, "TEMP" }
        };

        public static string GetTextFormat(FileFormat format)
        {
            return format switch
            {
                FileFormat.Wav16 => ".wav",
                FileFormat.MP3 => ".mp3",
                FileFormat.MP4 => ".mp4",
                _ => ".unk"
            };
        }
    }
}
