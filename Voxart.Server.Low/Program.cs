using Voxart.Lib.Interactions.D_ID;
using Voxart.Lib.Interactions.Sber;

namespace Voxart.Server.Low
{
    public class Program
    {
        public static string WorkDomain { get; private set; } = string.Empty;

        public static async Task Main(string[] args)
        {
            var (builder, config) = StartupExtensions.CreateBuilder();

            WorkDomain = config["Domain"]!;

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            builder.Services.AddDatabase(config.GetConnectionString("MySql")!);

            builder.Services.AddSingleton<ISaluteSpeechClient>(
                new SaluteSpeechClient(config["Auth:Sber:ApiKey"]!, config["Auth:Sber:ApiKeySecret"]!));

            builder.Services.AddSingleton<IDidClient>(
                new DidClient(config["Auth:D-ID:Basic"]!));

            builder.WebHost.UseUrls(config["Listen"]!); // Prod!

            var app = builder.Build();

            app.UseExceptionHandler("/Error");

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            await app.RunAsync();
        }
    }
}
