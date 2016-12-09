using NServiceBus.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus
{
    public static class CommonConfiguration
    {
        public static void ApplyCommonConfiguration(this EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UseSerialization<JsonSerializer>();
            //endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));
            endpointConfiguration.UseTransport<MsmqTransport>()
                .ConnectionString("deadLetter=false;journal=false");

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Commands") && t.Name.EndsWith("Command"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events") && t.Name.EndsWith("Event"));

            endpointConfiguration.EnableInstallers();
        }

        public static void UseSqlitePersistence(this EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<NHibernatePersistence>()
                .UseConfiguration(new global::NHibernate.Cfg.Configuration
                {
                    Properties =
                    {
                        ["connection.driver_class"] = "NHibernate.Driver.SQLite20Driver",
                        ["dialect"] = "NHibernate.Dialect.SQLiteDialect",
                        ["query.substitutions"] = "true=1;false=0",
                        ["show_sql"]= "true",
                        ["connection.connection_string"] = ConfigurationManager.ConnectionStrings["NServiceBus/Persistence"].ConnectionString
                    }
                });
        }
    }
}
