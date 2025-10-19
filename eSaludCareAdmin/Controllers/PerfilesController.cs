using CapaEntidad;
using eSaludCareAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace eSaludCareAdmin.Controllers
{
    public class PerfilesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var token = await ObtenerTokenAsync();

            if (token == null)
            {
                ViewBag.Error = "No se pudo obtener el token.";
                return View(new List<PerfilesUser>());
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/usuarios");

                if (response.IsSuccessStatusCode)
                {
                    var perfiles = await response.Content.ReadAsAsync<List<PerfilesUser>>();
                    return View(perfiles);
                }

                ViewBag.Error = "No se pudo cargar los perfiles.";
                return View(new List<PerfilesUser>());
            }
        }

        private async Task<string> ObtenerTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/");
                var loginData = new
                {
                    correo = "sofia@admin.com",
                    contrasena = "admin123"
                };

                var content = new StringContent(
                    JsonConvert.SerializeObject(loginData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/usuarios/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsAsync<dynamic>();
                    return resultado.token;
                }

                return null;
            }
        }

    }
}
