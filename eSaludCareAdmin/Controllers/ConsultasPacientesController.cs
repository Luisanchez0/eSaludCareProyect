using CapaEntidad;
using eSaludCareUsers.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


public class ConsultasPacientesController : Controller
{
    private readonly CN_ConsultasPacientes consultasPacientes;

    public ConsultasPacientesController()
    {
        string cadenaConexion = "Host=localhost;Port=5432;Database=clinica_db;Username=postgres;Password=102538;";
        consultasPacientes = new CN_ConsultasPacientes(cadenaConexion);
    }

    public async Task<ActionResult> Index()
    {
        List<ConsultaPacientesDTO> pacientes = await consultasPacientes.ObtenerPacientes();
        return View(pacientes);
    }
}