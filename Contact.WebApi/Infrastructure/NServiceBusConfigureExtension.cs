using System;
using System.Linq;
using System.Web.Http;
using NServiceBus;

namespace Contact.WebApi.Infrastructure
{
    public static class NServiceBusConfigureExtension
    {
        public static Configure ForWebApi(this Configure configure)
        {
            // Register our http controller activator with NSB

            configure.Configurer.RegisterSingleton(typeof(System.Web.Mvc.IControllerActivator), new NServiceBusControllerActivator());

            // Find every http controller class so that we can register it
            var controllers = Configure.TypesToScan
                                       .Where(t => typeof(ApiController).IsAssignableFrom(t));

            // Register each http controller class with the NServiceBus container
            foreach (Type type in controllers)
            {
                configure.Configurer.ConfigureComponent(type,
                                                        DependencyLifecycle.InstancePerCall);
            }

            // Set the WebApi dependency resolver to use our resolver
            GlobalConfiguration.Configuration.DependencyResolver =
                new NServiceBusResolverAdapter(configure.Builder);

            return configure;
        }
    }
}