

using WeatherAPI.Data;
using WeatherAPI.Interfaces;
using WeatherAPI.Repository;
using WeatherAPI.Services;

namespace WeatherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Configure MongoDB settings
            //builder.Services.Configure<MongoDBSettings>(
            //    builder.Configuration.GetSection("MongoDBSettings"));

            //// Register MongoDB context
            //builder.Services.AddSingleton<MongoDBContext>();
            //builder.Services.AddScoped<WeatherRepository>();

            builder.Services.AddHttpClient("OpenWeatherMap", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["WeatherApiSettings:BaseAddress"]);
            });

            builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            

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
