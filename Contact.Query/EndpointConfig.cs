using Contact.Query.Contracts;
using Contact.Query.SqlServer;
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
            //Move to config so that it can be changed
            var container = new Container(expression => expression.For<IContactQueryRepository>()
                                                                  .Use<ContactQueryRepository>());
            SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
            Configure.With()
                     .StructureMapBuilder(container)
                     .RavenPersistence()
                     .EnablePerformanceCounters()
                     .Log4Net();
        }
    }
}