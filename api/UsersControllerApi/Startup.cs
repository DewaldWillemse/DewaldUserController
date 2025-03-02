
using BaseProjectApi.Services.EncrytionService;
using BaseProjectApi.Services.ManualServices;
using BaseProjectApi.Services.UserService;
using BaseProjectApi.Services.UserServices;

namespace UserController
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Load configuration from appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            // Users
            services.AddSingleton<IUserServices, UserServices>();
            services.AddSingleton<IUserDBServices, UserDBServices>();
            services.AddSingleton<IDBUsersChecks, DBUsersChecks>();
            services.AddSingleton<IDBManualService, DBManualService>();
            services.AddSingleton<IEncryptionService, EncryptionService>();

            services.AddControllers(); // Add MVC controller services

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:4200",
                                     "http://192.168.101.121:4200",
                                     "https://ucweb.paytequtils.com",
                                     "https://ucweb.paytequtils.com:5116"
                                     )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRouting(); // Enable routing for HTTP requests

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map controller routes
            });
        }
    }
}
