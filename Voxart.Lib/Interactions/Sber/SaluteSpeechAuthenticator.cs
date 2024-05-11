using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;

namespace Voxart.Lib.Interactions.Sber
{
    public class SaluteSpeechAuthenticator(string baseUrl, string clientId, string clientSecret) : AuthenticatorBase("")
    {
        readonly string _baseUrl = baseUrl;
        readonly string _clientId = clientId;
        readonly string _clientSecret = clientSecret;

        private DateTime _tokenLifeTime;

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            if (string.IsNullOrEmpty(Token) || DateTime.UtcNow.AddMinutes(2) > _tokenLifeTime)
            {
                Token = await GetToken();
            }
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            using var client = new RestClient(options);

            var request = new RestRequest()
                .AddHeader("RqUID", Guid.NewGuid().ToString())
                .AddParameter("scope", "SALUTE_SPEECH_PERS", ParameterType.GetOrPost);

            var response = await client.PostAsync<TokenResponse>(request);

            _tokenLifeTime = DateTimeOffset.FromUnixTimeMilliseconds(response!.ExpiresAt).UtcDateTime;

            return "Bearer " + response!.AccessToken;
        }
    }

    file record TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }

        [JsonPropertyName("expires_at")]
        public long ExpiresAt { get; init; }
    }
}
