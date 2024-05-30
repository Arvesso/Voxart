using Voxart.Shared;

namespace Voxart.Client.Static
{
    public static class Shared
    {
        public static readonly Dictionary<string, string> VoiceValues = new()
        {
            { Help.VoiceTextCode[VoiceCode.Nec_24000], "Женский" },
            { Help.VoiceTextCode[VoiceCode.Bys_24000], "Мужской" }
        };
    }
}
