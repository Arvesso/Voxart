namespace Voxart.Lib.Interactions
{
    public class ApiLinks
    {
        public static class SberSaluteSpeech
        {
            private const string VoiceApi = "https://smartspeech.sber.ru/rest/v1/";

            public const string Auth = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
            public const string Synthesize = VoiceApi + "text:synthesize";
            public const string Recognize = VoiceApi + "speech:recognize";
        }

        public static class Did
        {
            public const string Talks = "https://api.d-id.com/talks";
        }
    }
}
