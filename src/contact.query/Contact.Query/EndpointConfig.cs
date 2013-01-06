using System.Configuration;
using Contact.Query.Contracts;
using Contact.Query.Mongo;
using NServiceBus;
using StructureMap;
using log4net.Config;

namespace Contact.Query
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/

    public class EndpointConfig :
        IConfigureThisEndpoint,
        AsA_Server,
        IWantCustomInitialization
    {
        public void Init()
        {
            var mongoConnectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            var mongoDatabase = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
            //Move to config so that it can be changed
            //And Use a factory, this is DIRTY
            var container = new Container(expression => expression.For<IContactQueryRepository>()
                                                                  .Use<MongoContactQueryRepository>()
                                                                  .Ctor<string>("connectionString")
                                                                  .Is(mongoConnectionString)
                                                                  .Ctor<string>("databaseName")
                                                                  .Is(mongoDatabase));
            SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
            Configure.With()
                     .StructureMapBuilder(container)
                     .EnablePerformanceCounters()
                     .Log4Net();
        }
    }
}