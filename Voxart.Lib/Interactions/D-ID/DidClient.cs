using Newtonsoft.Json;
using RestSharp;

namespace Voxart.Lib.Interactions.D_ID
{
    public class DidClient : IDidClient, IDisposable
    {
        readonly RestClient _client;

        public DidClient(string basic)
        {
            var options = new RestClientOptions()
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            _client = new RestClient(options);

            _client.AddDefaultHeader("accept", "application/json");
            _client.AddDefaultHeader("authorization", $"Basic {basic}");
        }

        public async Task<byte[]> CreateTalk(string audioLink, string imageLink)
        {
            var request = new RestRequest(ApiLinks.Did.Talks, Method.Post);

            request.AddJsonBody("" +
                "{\"script\":" +
                    "{\"type\":\"audio\"," +
                    "\"subtitles\":\"false\"," +
                     "\"provider\":" +
                        "{\"type\":\"microsoft\"," +
                        "\"voice_id\":\"en-US-JennyNeural\"}," +
                        $"\"audio_url\":\"{audioLink}\"}}," +
                        "\"config\":" +
                            "{\"fluent\":\"false\"," +
                            "\"pad_audio\":\"0.0\"}," +
                        $"\"source_url\":\"{imageLink}\"}}", false);

            var response = await _client.ExecuteAsync<CreateTalkResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Response is bad status");
            }

            var link = await GetTalk(response.Data!.Id);

            using var client = new RestClient();

            var resultRequest = new RestRequest(link, Method.Get);

            var result = await client.ExecuteAsync(resultRequest);

            if (result.IsSuccessful)
            {
                return result.RawBytes!;
            }
            else throw new Exception("Response is bad status");
        }

        public async Task<string> GetTalk(string id)
        {
            var request = new RestRequest(ApiLinks.Did.Talks + $"/{id}", Method.Get);

            while (true)
            {
                await Task.Delay(500);

                var response = await _client.ExecuteAsync<GetTalkResponse>(request);

                if (!response.IsSuccessful)
                {
                    throw new Exception("Response is bad status");
                }

                var data = JsonConvert.DeserializeObject<GetTalkResponse>(response.Content!)!;

                if (data.Status == "error")
                {
                    throw new Exception("Generation error");
                }
                else if (data.Status == "done")
                {
                    return data.ResultUrl;
                }
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    file record CreateTalkResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    file record GetTalkResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("result_url")]
        public string ResultUrl { get; set; }
    }
}
