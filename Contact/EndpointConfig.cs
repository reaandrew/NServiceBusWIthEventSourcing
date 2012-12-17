using System;
using Contact.Core;
using StructureMap;

namespace Contact
{
    using NServiceBus;

    /*
        This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
        can be found here: http://nservicebus.com/GenericHost.aspx
    */
    public class EndpointConfig :
        IConfigureThisEndpoint,
        AsA_Server,
        AsA_Publisher,
        IWantCustomInitialization
    {
        public void Init()
        {
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
            var container = new Container(x =>
                {
                    x.Scan(scan =>
                        {
                            scan.AssemblyContainingType<Main>();
                            scan.AddAllTypesOf(typeof (ISendCommand<>));
                        });
                    x.For<IPublishEvent>().Use<EventPublisher>();
                });
            Configure.With()
                .Log4Net()
                .StructureMapBuilder(container);

            Console.WriteLine("Initialized");
        }
    }
}