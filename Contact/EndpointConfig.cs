using System;
using Contact.Core;
using Contact.DomainServices;
using Contact.Infrastructure;
using Contact.Infrastructure.InProc;
using Contact.Infrastructure.NServiceBus;
using Core.DomainServices;
using NServiceBus;
using StructureMap;
using StructureMap.Pipeline;
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
            //I know I could use default conventions in here, but just being explicit for the purposes of example.
            var container = new Container(x =>
                {
                    x.For<IEventPublisher>().Use<NServiceBusEventPublisher>();
                    x.For<IEventMappings>()
                     .LifecycleIs(new SingletonLifecycle())
                     .Use(() => new NServiceBusDomainEventMappingFactory()
                                    .CreateMappingCollection());
                    x.For<IEventPersistence>()
                     .LifecycleIs(new SingletonLifecycle())
                     .Use<InProcEventPersistence>();
                    x.For<IEventStore>().Use<IEventStore>();
                    x.For<IDomainRepository>().Use<DomainRepository>();
                    x.For<ISendEmails>().Use<BlackHoleEmailSender>();
                });
            Configure.With()
                     .Log4Net()
                     .StructureMapBuilder(container);

            Console.WriteLine("Initialized");
        }
    }
}