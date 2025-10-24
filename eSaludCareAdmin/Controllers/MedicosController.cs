using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;


namespace eSaludCareAdmin.Controllers
{

    public class MedicosController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var token = Session["Token"]?.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/"); // Ajusta el puerto si es necesario
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/medicos");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo obtener la lista de médicos.";
                    return View(new List<MedicoDTO>());
                }

                var medicos = await response.Content.ReadAsAsync<List<MedicoDTO>>();
                return View(medicos);
            }
        }

    }
}
