
using UserController;

namespace BaseProjectApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("https://uc.paytequtils.com:5116") // Force your custom domain & port
                        .ConfigureKestrel((context, options) =>
                        {
                            var kestrelSection = context.Configuration.GetSection("Kestrel");
                            options.Configure(kestrelSection);
                        });
                });
    }
}
