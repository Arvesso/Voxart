namespace Voxart.Shared
{
    public static class Help
    {
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
