using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Data.SqlClient;

namespace pin_number_gen.functions
{
    public class Configure
    {
        public static EndpointConfiguration NServiceBus(IConfiguration config)
        {   
            var endpointConfiguration = new EndpointConfiguration("Pingen");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(config.GetValue<string>("NServiceBusConnection"));

            var connectionString = config.GetValue<string>("SqlConnection");
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(() => new SqlConnection(connectionString));

            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}