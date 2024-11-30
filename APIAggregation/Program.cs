
using APIAggregation.Data;
using APIAggregation.Interfaces;
using APIAggregation.Repository;
using APIAggregation.Services;
using NewsAPI.Interfaces;
using NewsAPI.Services;
using Serilog;
using SpotifyAPI.Services;
using WeatherAPI.Interfaces;
using WeatherAPI.Services;

namespace APIAggregation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog from appsettings.json
            builder.Host.UseSerilog((context, services, configuration) =>
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services));


            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDBSettings"));

            // Register MongoDB context
            builder.Services.AddSingleton<MongoDBContext>();
            builder.Services.AddScoped<AggregationRepository>();

            builder.Services.AddHttpClient("NewsAPI", client =>
           {
               client.BaseAddress = new Uri(builder.Configuration["NewsApiSettings:BaseAddress"]);
               client.DefaultRequestHeaders.Add("User-Agent", "NewsAPI/1.0");
           });

            builder.Services.AddHttpClient("OpenWeatherMap", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["WeatherApiSettings:BaseAddress"]);
            });

            builder.Services.AddHttpClient();
           
            // Add services to the container.
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            builder.Services.AddScoped<SpotifyAuthService>();
            builder.Services.AddScoped<SpotifyApiService>();

            builder.Services.AddScoped<IBaseService, BaseService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
