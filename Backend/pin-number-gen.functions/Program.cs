using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using pin_number_gen.application;
using pin_number_gen.infrastructure;
using SFA.DAS.Configuration.AzureTableStorage;

namespace pin_number_gen.functions
{
    public class Program
    {
        public static void Main()
        {
            IConfiguration config;

            new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((hostContext, services) =>
                {
                    var provider = services.BuildServiceProvider();
                    var configuration = provider.GetService<IConfiguration>();

                    var configBuilder = new ConfigurationBuilder()
                        .AddConfiguration(configuration)
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddEnvironmentVariables()
#if DEBUG
                        .AddJsonFile("local.settings.json", optional: true)
#endif
                        .AddAzureTableStorage(options =>
                        {
                            options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                            options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                            options.EnvironmentName = configuration["EnvironmentName"];
                            options.PreFixConfigurationKeys = false;
                        });

                    config = configBuilder.Build();
                    hostContext.Configuration = config;

                    services.AddOptions()
                        .AddLogging(c => c.AddConsole())
                        .AddCache(config)
                        .Configure<PingenConfig>(config.GetSection("service"))
                        .AddScoped<IPinRepository, PinSqlRepository>()
                        .AddTransient<IPinGeneratorService, PinGeneratorService>()
                        .AddScoped(p => new PinDbContext(new DbContextOptionsBuilder<PinDbContext>()
                            .UseSqlServer(config.GetValue<string>("SqlConnection")).Options))
                        .AddTransient<PinNumberGen.Function.PinNumberGen>(ctx => new PinNumberGen.Function.PinNumberGen(ctx.GetRequiredService<IPinGeneratorService>()))
                    ;

                })
                .UseNServiceBus(c => Configure.NServiceBus(c.Configuration))
                .Build()
                .Run();
        }
    }
}