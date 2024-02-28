using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces.ExternalAPI;
using ExternalAPI.Email;

namespace ExternalAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternalAPI(this IServiceCollection services)
        {
            services.AddScoped<ISendGridEmail, SendGridEmail>();

            return services;
        }
    }
}
