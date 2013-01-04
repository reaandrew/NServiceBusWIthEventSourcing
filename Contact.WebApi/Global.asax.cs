using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Contact.Query.Contracts;
using Contact.Query.SqlServer;
using Contact.WebApi.Infrastructure;
using NServiceBus;
using NServiceBus.Installation.Environments;
using StructureMap;
using log4net;
using log4net.Config;

namespace Contact.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        private static ILog _logger = LogManager.GetLogger(typeof (WebApiApplication));

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new Container(expression
                                          => expression.For<IContactQueryRepository>()
                                                       .Use<ContactQueryRepository>());

            Configure.With()
                     .StructureMapBuilder(container)
                     .Log4Net()
                     .ForWebApi()
                     .EnablePerformanceCounters()
                     .XmlSerializer()
                     .MsmqTransport()
                     .UnicastBus()
                     .IsTransactional(true)
                     .CreateBus()
                     .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}