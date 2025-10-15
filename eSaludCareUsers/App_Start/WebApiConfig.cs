using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using System.Web.Http.Cors;
//instalar Microsoft.AspNet.WebApi.Cors en el nuget package manager
namespace eSaludCareUsers
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
