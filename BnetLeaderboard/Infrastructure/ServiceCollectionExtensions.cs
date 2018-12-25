using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BnetLeaderboard.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureSettings<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
        }
    }
}