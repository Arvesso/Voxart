using Blazored.LocalStorage;

namespace Voxart.Client.Low.Processing
{
    public class BrowserStorage(ILocalStorageService storage)
    {
        public async Task SetKey(string key, string value)
        {
            await storage.SetItemAsStringAsync(key, value);
        }

        public async Task<string?> GetKey(string key)
        {
            if (!await storage.ContainKeyAsync(key))
                return null;
            else return await storage.GetItemAsStringAsync(key);
        }

        public static class Keys
        {
            public const string KeyClient = "client_id";
        }
    }
}
