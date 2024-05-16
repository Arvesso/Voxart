using Voxart.Lib.Interactions.Sber;

namespace Voxart.Server
{
    public class Startup
    {
        public static void Main()
        {
            var (builder, config) = StartupExtensions.CreateBuilder();

            builder.Services.AddControllers();

            builder.Services.AddDatabase(config.GetConnectionString("MySql")!);

            builder.Services.AddSingleton<ISaluteSpeechClient>(
                new SaluteSpeechClient(config["Auth:Sber:ApiKey"]!, config["Auth:Sber:ApiKeySecret"]!));

            //builder.WebHost.UseUrls(builder.Configuration["Listen"]!); Production!

            var app = builder.Build();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
