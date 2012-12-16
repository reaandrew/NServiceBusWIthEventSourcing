using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

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
            //Configure.With().CastleWindsorBuilder(new WindsorContainer()
            //    .Register(Classes.FromThisAssembly()
            //        .InSameNamespaceAs<Main>()
            //        .LifestyleTransient()));
            Console.WriteLine("Initialized");
        }
    }
}