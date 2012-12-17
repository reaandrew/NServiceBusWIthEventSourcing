using System;
using Contact.Contracts;
using Contact.Senders;
using StructureMap;
using log4net;

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
            Console.WriteLine("Setting Up logging");
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
            Console.WriteLine("Logging Setup");
            LogManager.GetLogger("Name").Debug("Something interesting happened.");
            Console.WriteLine("Waiting");
            Console.ReadLine();
            var container = new Container(x => x.For<ICreateUserSender>()
                                                .Use<CreateUserSender>());
            Configure.With()
                .Log4Net()
                .StructureMapBuilder(container);

            Console.WriteLine("Initialized");
        }
    }
}