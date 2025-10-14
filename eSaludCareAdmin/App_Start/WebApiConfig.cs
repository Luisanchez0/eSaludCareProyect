using System.Web.Http;

namespace eSaludCareAdmin.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Habilita las rutas con atributos
            config.MapHttpAttributeRoutes();

            // Ruta por defecto opcional (no la necesitas si usas [RoutePrefix])
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);
        }
    }
}
