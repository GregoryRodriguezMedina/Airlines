﻿using AirLines.Infrastructure.Data;
using AirLines.Infrastructure.Data.repository;
using AirLines.Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Infrastructure
{
    public static class Bootstrap
    {
        public static void AddScopedData(this IServiceCollection services)
        {
            services.AddScoped<DbContext, AirlinesContext>();
            services.AddScoped<IAirPortRepository, AirPortRepository>();

           //services.AddTransient<IAirPortService, AirPortService>();
        }

        public static void AddTransientData(this IServiceCollection services)
        {
           // services.AddScoped<DbContext, AirlinesContext>();
           // services.AddScoped<IAirPortRepository, AirPortRepository>();

            services.AddTransient<IAirPortService, AirPortService>();
        }
    }
}