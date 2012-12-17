using System;
using Contact.Core;
using Contact.Infrastructure;
using NServiceBus;
using StructureMap;
using log4net.Config;

namespace Contact
{
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
            SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
            var container = new Container(x =>
                {
                    x.Scan(scan =>
                        {
                            scan.AssemblyContainingType<Main>();
                            scan.AddAllTypesOf(typeof (ISendCommand<>));
                        });
                    x.For<IEventPublisher>().Use<EventPublisher>();
                });
            Configure.With()
                     .Log4Net()
                     .StructureMapBuilder(container);

            Console.WriteLine("Initialized");
        }
    }
}