using System.Net.Http.Json;
using Voxart.Shared;
using Voxart.Shared.WebInteractions;

namespace Voxart.Client
{
    public class ApiInteraction()
    {
        //public async Task<SynthesizeResponse?> Synthesize(string text, VoiceCode voiceCode)
        //{
        //    var request = new SynthesizeRequest() { Text = text, VoiceCode = voiceCode };
        //    return await ProcessRequest<SynthesizeResponse, SynthesizeRequest>(Endpoints.Synthesize, request);
        //}

        //public async Task<TValue?> ProcessRequest<TValue>(string endpoint)
        //{
        //    try
        //    {
        //        var result = await client.GetFromJsonAsync<TValue>(endpoint);
        //        return result;
        //    }
        //    catch
        //    {
        //        return default;
        //    }
        //}

        //public async Task<TKValue?> ProcessRequest<TKValue, TValue>(string endpoint, TValue value)
        //{
        //    try
        //    {
        //        var result = await client.PostAsJsonAsync(endpoint, value);

        //        if (result.IsSuccessStatusCode)
        //        {
        //            return await result.Content.ReadFromJsonAsync<TKValue>();
        //        }

        //        return default;
        //    }
        //    catch
        //    {
        //        return default;
        //    }
        //}
    }
}
