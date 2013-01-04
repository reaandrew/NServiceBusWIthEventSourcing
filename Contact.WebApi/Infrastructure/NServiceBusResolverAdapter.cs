using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using NServiceBus.ObjectBuilder;

namespace Contact.WebApi.Infrastructure
{
    /// <summary>
    /// From
    /// 
    /// http://tech.dir.groups.yahoo.com/group/nservicebus/message/16656
    /// </summary>
    public class NServiceBusResolverAdapter : IDependencyResolver
    {
        private readonly IBuilder builder;

        public NServiceBusResolverAdapter(IBuilder builder)
        {
            this.builder = builder;
        }

        public object GetService(Type serviceType)
        {
            if (typeof (IHttpController).IsAssignableFrom(serviceType))
            {
                return builder.Build(serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}