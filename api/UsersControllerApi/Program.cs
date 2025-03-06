using BaseProjectApi.Services.ManualServices;
using BaseProjectApi.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Hosting;

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
                    webBuilder.ConfigureServices(services =>
                    {
                        // Register services for dependency injection
                        services.AddSingleton<IDBManualService, DBManualService>();
                        services.AddSingleton<IUserDBServices, UserDBServices>();

                        // MySQL connection configuration
                        services.AddScoped<MySqlConnection>(provider => 
                            new MySqlConnection("Server=localhost;Database=UserControllerDB;User=root;Password=yourpassword;"));
                    });

                    webBuilder.Configure(app =>
                    {
                        // Configure HTTP request pipeline (middleware)
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();  // Map API controllers
                        });
                    });
                });
    }
}
