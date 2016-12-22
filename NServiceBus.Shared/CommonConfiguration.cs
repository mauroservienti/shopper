using NServiceBus.Persistence;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
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

        public static void UseRavenPersistence(this EndpointConfiguration endpointConfiguration, IDocumentStore store)
        {
            endpointConfiguration.UsePersistence<RavenDBPersistence>()
                .SetDefaultDocumentStore(store);
        }

        public static IDocumentStore CreateEmbeddableDocumentStore(string defaultDatabaseName, Action<IDocumentSession> seedCallback = null)
        {
            var store = new EmbeddableDocumentStore()
            {
                DataDirectory = ConfigurationManager.AppSettings["RavenDB/DataDirectory"],
                DefaultDatabase = defaultDatabaseName
            }.Initialize();

            var index = store.DatabaseCommands.GetIndex("Raven/DocumentsByEntityName");
            if (index == null)
            {
                new RavenDocumentsByEntityName().Execute(store);
            }

            if (seedCallback != null)
            {
                using (var session = store.OpenSession())
                {
                    dynamic seed = session.Load<dynamic>("Data/Seed");
                    if (seed == null)
                    {
                        session.Store(new { }, "Data/Seed");
                        seedCallback(session);

                        session.SaveChanges();
                    }
                }
            }

            return store;
        }

        //public static void UseSqlitePersistence(this EndpointConfiguration endpointConfiguration)
        //{
        //    endpointConfiguration.UsePersistence<NHibernatePersistence>()
        //        .UseConfiguration(new global::NHibernate.Cfg.Configuration
        //        {
        //            Properties =
        //            {
        //                ["connection.driver_class"] = "NHibernate.Driver.SQLite20Driver",
        //                ["dialect"] = "NHibernate.Dialect.SQLiteDialect",
        //                ["query.substitutions"] = "true=1;false=0",
        //                ["show_sql"]= "true",
        //                ["connection.connection_string"] = ConfigurationManager.ConnectionStrings["NServiceBus/Persistence"].ConnectionString
        //            }
        //        });
        //}
    }
}
