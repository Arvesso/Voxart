using Voxart.Lib.Interactions.Sber;

namespace Voxart.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSingleton<ISaluteSpeechClient>(
                new SaluteSpeechClient(builder.Configuration["Auth:Sber:ApiKey"]!, builder.Configuration["Auth:Sber:ApiKeySecret"]!));

            //builder.WebHost.UseUrls(builder.Configuration["Listen"]!);

            var app = builder.Build();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
