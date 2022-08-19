using AirLines.Infrastructure.Data;
using AirLines.Infrastructure.Data.repository;
using AirLines.Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirLines.Infrastructure
{
    public static class Bootstrap
    {
        public static void AddScopedData(this IServiceCollection services)
        {
            services.AddScoped<DbContext, AirlinesContext>();
            services.AddScoped<IAirPortRepository, AirPortRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IPassagerRepository, PassagerRepository>();
        }

        public static void AddTransientData(this IServiceCollection services)
        {
           // services.AddScoped<DbContext, AirlinesContext>();
           // services.AddScoped<IAirPortRepository, AirPortRepository>();

            services.AddTransient<IAirPortService, AirPortService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IPassagerService, PassagerService>();
        }
    }
}
