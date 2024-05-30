using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Voxart.Client.Low.Processing;

namespace Voxart.Client.Low
{
    public class Program
    {
        public static string BaseAddress { get; private set; } = string.Empty;

        public static async Task Main()
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices(o =>
            {
                o.SnackbarConfiguration.SnackbarVariant = Variant.Text;
                o.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
            });

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<BrowserStorage>();
            builder.Services.AddScoped<ApiInterop>();

            BaseAddress = builder.HostEnvironment.BaseAddress;

            await builder.Build().RunAsync();
        }
    }
}
