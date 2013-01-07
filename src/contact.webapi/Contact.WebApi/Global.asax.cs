using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Contact.Query.Auditing.DataAccess;
using Contact.Query.Auditing.Infrastructure;
using Contact.Query.Contracts;
using Contact.Query.Mongo;
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

            var mongoConnectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            var mongoDatabase = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
            var container = new Container(expression
                                          =>
                {
                    expression.For<IContactQueryRepository>()
                              .Use<MongoContactQueryRepository>()
                              .Ctor<string>("connectionString")
                              .Is(mongoConnectionString)
                              .Ctor<string>("databaseName")
                              .Is(mongoDatabase);
                    expression.For<IAuditInformationRepository>()
                              .Use<MongoAuditInformationRepository>()
                              .Ctor<string>("connectionString")
                              .Is(ConfigurationManager.AppSettings["MongoAuditConnectionString"])
                              .Ctor<string>("databaseName")
                              .Is(ConfigurationManager.AppSettings["MongoAuditDatabaseName"]);
                });

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