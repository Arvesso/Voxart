using System.Net.Http.Json;
using Voxart.Shared.WebInteractions;
using Voxart.Shared;

namespace Voxart.Client.Low.Processing
{
    public class ApiInterop(HttpClient client)
    {
        public async Task<string?> CreateClient()
        {
            return await ProcessRequest<string>(Endpoints.CreateClient);
        } 

        public async Task<SynthesizeResponse?> Synthesize(string client, string text, VoiceCode voiceCode)
        {
            var request = new SynthesizeRequest() { ClientId = client, Text = text, VoiceCode = voiceCode };
            return await ProcessRequest<SynthesizeResponse, SynthesizeRequest>(Endpoints.Synthesize, request);
        }

        public async Task<RecognizeResponse?> Recognize(string client, byte[] audio, AudioFormat audioFormat)
        {
            var request = new RecognizeRequest() { ClientId = client, Audio = audio,  AudioFormat = audioFormat };
            return await ProcessRequest<RecognizeResponse, RecognizeRequest>(Endpoints.Recognize, request);
        }

        public async Task<AvatarResponse?> Avatar(string client, string text, VoiceCode voiceCode, byte[] image)
        {
            var request = new AvatarRequest()
            {
                ClientId = client,
                Text = text,
                VoiceCode = voiceCode,
                Image = image
            };
            return await ProcessRequest<AvatarResponse, AvatarRequest>(Endpoints.Avatar, request);
        }

        private async Task<TValue?> ProcessRequest<TValue>(string endpoint)
        {
            try
            {
                var result = await client.GetFromJsonAsync<TValue>(endpoint);
                return result;
            }
            catch
            {
                return default;
            }
        }

        private async Task<TKValue?> ProcessRequest<TKValue, TValue>(string endpoint, TValue value)
        {
            try
            {
                var result = await client.PostAsJsonAsync(endpoint, value);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<TKValue>();
                }

                return default;
            }
            catch
            {
                return default;
            }
        }

        private static class Endpoints
        {
            public const string Synthesize = "/client/synthesize";
            public const string Recognize = "/client/recognize";
            public const string Avatar = "/client/avatar";
            public const string CreateClient = "/client/create";
        }
    }
}
