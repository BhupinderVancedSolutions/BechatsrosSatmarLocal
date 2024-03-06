using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces.DataBase;
using Infrastructure.Implementation.DataBase;
using Infrastructure.Extensions;
using System.Reflection;
using Application.Common.Interfaces.Services;
using Infrastructure.Implementation.Services;
using Application.Common.Interfaces.Services.PaymentService;
using Infrastructure.Implementation.Services.PaymentService;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBechatsrosSatmar, BechatsrosSatmarContext>();
            services.AddScoped<IParameterManager, ParameterManager>();
            services.RegisterApplicationServices(Assembly.GetExecutingAssembly());

            //services.AddScoped<ICityChargeService, CityChargeService>();
        }
    }
}