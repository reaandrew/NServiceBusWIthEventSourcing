using System.Web.Http;

namespace Contact.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "AccommodationLeads",
                routeTemplate: "api/accommodationleads/approved",
                defaults: new
                    {
                        id = RouteParameter.Optional,
                        controller = "AccommodationLeads"
                    }
                );
        }
    }
}