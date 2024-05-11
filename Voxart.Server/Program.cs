using Voxart.Lib.Interactions.Sber;
using Voxart.Lib.Extensions;

namespace Voxart.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

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
