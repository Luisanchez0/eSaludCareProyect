using CapaEntidad;
using eSaludCareUsers.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eSaludCareUsers.Controllers
{
    public class PerfilPacienteController : Controller
    {
        private readonly string apiUrl = "https://localhost:44301/api/v1/paciente/perfil";


        public ActionResult Index()
        {
            return View();
        }

        /*
        public async Task<ActionResult> Index()
        {
            // 🔒 Verificar si hay token en sesión
            var token = Session["token"]?.ToString();

            if (string.IsNullOrEmpty(token))
            {
                // Si no hay token, redirige al login
                return RedirectToAction("Index", "Login");
            }

            // Si hay token, mostrar la vista del perfil
            return View();
        }

        /*
        [HttpPost]
        public async Task<ActionResult> Actualizar(PerfilPaciente model)
        {
            var token = Session["token"]?.ToString();
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(model),
                                                System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                    TempData["Mensaje"] = "Perfil actualizado correctamente.";
                else
                    TempData["Mensaje"] = "No se pudo actualizar el perfil.";
            }

            return RedirectToAction("Index");
        }
        */
    }
}