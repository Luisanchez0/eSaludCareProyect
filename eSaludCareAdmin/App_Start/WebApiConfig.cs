using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eSaludCareAdmin.App_Start
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Habilitar el uso de atributos de ruta
            config.MapHttpAttributeRoutes();

            // Ruta por defecto para api/v1/{controller}/{id}
            config.Routes.MapHttpRoute(
                name: "ApiV1",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Ruta específica para login
            config.Routes.MapHttpRoute(
                name: "LoginRoute",
                routeTemplate: "api/v1/login",
                defaults: new { controller = "Login" }
            );
        }
    }
}
