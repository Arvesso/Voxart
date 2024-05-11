using RestSharp;
using System.Text.Json.Serialization;

namespace Voxart.Lib.Interactions.Sber
{
    public enum AudioFormat
    {
        wav16, 
    }

    public enum VoiceCode
    {
        Nec_24000,
    }

    public class SaluteSpeechClient : ISaluteSpeechClient, IDisposable
    {
        readonly RestClient _client;

        public SaluteSpeechClient(string apiKey, string apiKeySecret)
        {
            var options = new RestClientOptions()
            {
                Authenticator = new SaluteSpeechAuthenticator(ApiLinks.SberSaluteSpeech.Auth, apiKey, apiKeySecret),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            _client = new RestClient(options);
        }

        public async Task<byte[]> Synthesize(string text, AudioFormat audio = default, VoiceCode voice = default)
        {
            var request = new RestRequest(ApiLinks.SberSaluteSpeech.Synthesize, Method.Post);

            request.AddHeader("Content-Type", "application/text");
            request.AddParameter("application/text", text, ParameterType.RequestBody);
            request.AddQueryParameter("format", audio.ToString());
            request.AddQueryParameter("voice", voice.ToString());

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return response.RawBytes ?? throw new Exception("Null bytes in response");
            }
            else
            {
                throw new Exception("Response is bad status");
            }
        }

        public async Task<string[]> Recognize(byte[] audio)
        {
            var request = new RestRequest(ApiLinks.SberSaluteSpeech.Recognize, Method.Post);

            request.AddHeader("Content-Type", "audio/mpeg");
            request.AddParameter("audio/mpeg", audio, ParameterType.RequestBody);

            var response = await _client.ExecuteAsync<RecognizeResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data?.Results ?? throw new Exception("Null bytes in response");
            }
            else
            {
                throw new Exception("Response is bad status");
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    file record RecognizeResponse
    {
        [JsonPropertyName("result")]
        public string[] Results { get; init; }

        [JsonPropertyName("emotions")]
        public List<Dictionary<string, float>> Emotions { get; init; }

        [JsonPropertyName("status")]
        public int Status { get; init; }
    }
}
