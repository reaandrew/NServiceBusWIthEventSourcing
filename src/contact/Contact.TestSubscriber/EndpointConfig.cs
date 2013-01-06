using NServiceBus;
using log4net.Config;

namespace Contact.TestSubscriber
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server,
                                  IWantCustomInitialization
    {
        public void Init()
        {
            SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
            Configure.With()
                     .Log4Net()
                     .StructureMapBuilder()
                     .EnablePerformanceCounters();
        }
    }
}