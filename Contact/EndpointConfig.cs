using System.Configuration;
using Contact.DomainServices;
using Contact.Infrastructure;
using Contact.Infrastructure.InProc;
using Contact.Infrastructure.Mongo;
using Contact.Infrastructure.NServiceBus;
using Core;
using Core.DomainServices;
using Infrastructure.NServiceBus;
using NServiceBus;
using StructureMap;
using StructureMap.Pipeline;
using log4net;
using log4net.Config;

namespace Contact
{
    /*
        This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
        can be found here: http://nservicebus.com/GenericHost.aspx
    */

    [EndpointSLA("00:00:30")]
    public class EndpointConfig :
        IConfigureThisEndpoint,
        AsA_Server,
        AsA_Publisher,
        IWantCustomInitialization
    {
        /// <summary>
        /// http://support.nservicebus.com/customer/portal/articles/859362-using-ravendb-in-nservicebus-%E2%80%93-connecting
        /// </summary>
        public void Init()
        {
            SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
            //I know I could use default conventions in here, but just being explicit for the purposes of example.
            var connectionString = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
            var mongoConnectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            var mongoDatabase = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
            var container = new Container(x =>
                {
                    x.For<IEventPublisher>().Use<NServiceBusEventPublisher>();

                    x.For<IEventMappings>()
                     .LifecycleIs(new SingletonLifecycle())
                     .Use(() => new NServiceBusDomainEventMappingFactory().CreateMappingCollection());
                    /*
                   x.For<IEventPersistence>()
                    .LifecycleIs(new SingletonLifecycle())
                    .Use(new SqlEventPersistence(connectionString));
                    * * */
                    x.For<IEventPersistence>()
                     .LifecycleIs(new SingletonLifecycle())
                     .Use(new MongoEventPersistence(mongoConnectionString, mongoDatabase));
                    x.For<IEventStore>().Use<EventStore>();
                    x.For<IDomainRepository>().Use<DomainRepository>();
                    x.For<ISendEmails>().Use<BlackHoleEmailSender>();
                    x.For<IGeneratePassword>().Use<RandomNumberPasswordGenerator>();
                    x.For<IHash>().Use<SHA512Hasher>();
                    //Doing this as I cannot see a route to constructor injection with
                    //Sagas that work with the NServiceBus Test framework
                    x.FillAllPropertiesOfType<IDomainRepository>();
                });

            Configure.With()
                     .Log4Net()
                     .StructureMapBuilder(container)
                     .EnablePerformanceCounters()
                     .RavenPersistence();

            LogManager.GetLogger(this.GetType()).Info("Initialized");
        }
    }
}