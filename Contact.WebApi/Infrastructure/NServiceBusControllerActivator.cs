using System;
using System.Web.Mvc;

namespace Contact.WebApi.Infrastructure
{
    public class NServiceBusControllerActivator : IControllerActivator
    {

        public IController Create(System.Web.Routing.RequestContext
                                      requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as
                   IController;

        }
    }
}