using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Contact.WebApi.Infrastructure;
using NServiceBus;
using log4net;

namespace Contact.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ILog _logger = LogManager.GetLogger(typeof (WebApiApplication));

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Configure.With()
                     .DefaultBuilder()
                     .Log4Net()
                     .ForWebApi()
                     .XmlSerializer()
                     .MsmqTransport()
                     .UnicastBus()
                     .CreateBus()
                     .Start();
        }
    }
}