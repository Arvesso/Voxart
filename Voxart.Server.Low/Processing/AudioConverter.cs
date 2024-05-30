using NAudio.Lame;
using NAudio.Wave;

namespace Voxart.Server.Low.Processing
{
    public class AudioConverter
    {
        public static byte[] ConvertWavToMp3(byte[] wavBytes)
        {
            using (var wavStream = new MemoryStream(wavBytes))
            using (var mp3Stream = new MemoryStream())
            using (var reader = new WaveFileReader(wavStream))
            using (var writer = new LameMP3FileWriter(mp3Stream, reader.WaveFormat, LAMEPreset.VBR_90))
            {
                reader.CopyTo(writer);
                writer.Flush();
                return mp3Stream.ToArray();
            }
        }
    }
}
