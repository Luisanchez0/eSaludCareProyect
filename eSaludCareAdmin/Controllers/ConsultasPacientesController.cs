using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace eSaludCareAdmin.Controllers
{
    public class ConsultasPacientesController : Controller
    {
        private readonly CN_ConsultasPacientes consultasPacientes;

        public ConsultasPacientesController()
        {
            string cadenaConexion = "Host=localhost;Port=5432;Database=clinica_db;Username=postgres;Password=102538";
            consultasPacientes = new CN_ConsultasPacientes(cadenaConexion);
        }

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            if (Session["Rol"]?.ToString() != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var pacientes = await consultasPacientes.ObtenerPacientes();
            return View(pacientes);
        }
    }
}