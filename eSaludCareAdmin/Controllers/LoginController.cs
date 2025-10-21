using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eSaludCareAdmin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string correo, string contrasena)

        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/"); // Asegúrate del puerto correcto

                var loginData = new { correo, contrasena };
                var content = new System.Net.Http.StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(loginData),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/v1/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsAsync<dynamic>();
                    Session["Token"] = resultado.token;
                    Session["Rol"] = resultado.role;
                    Session["Correo"] = correo;
                    Session["Nombre"] = resultado.nombre;
                    Session["id_usuario"] = resultado.id_usuario;

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Borra todo
            return RedirectToAction("Index", "Login"); // Vuelve al login
        }



    }
}
