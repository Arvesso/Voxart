using Voxart.Shared;

namespace Voxart.Client.Low.Data
{
    public class VoiceValues
    {
        public static readonly Dictionary<string, string> Voices = new()
        {
            { Help.VoiceTextCode[VoiceCode.Nec_24000], "Женский" },
            { Help.VoiceTextCode[VoiceCode.Bys_24000], "Мужской" }
        };
    }
}
