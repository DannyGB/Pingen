using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace pin_number_gen.functions
{
    public static class CacheStartupExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(options => { options.Configuration = config.GetValue<string>("RedisConnectionString"); });

            return services;
        }
    }
}
