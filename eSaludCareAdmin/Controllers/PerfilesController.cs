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
using CapaNegocio;


namespace eSaludCareAdmin.Controllers
{
    [RoutePrefix("Perfiles")]
    public class PerfilesController : Controller
    {
        private readonly string baseUrl = "https://localhost:44325/";

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var rol = Session["rol"]?.ToString();
            var idUsuario = Convert.ToInt32(Session["id_usuario"]);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios?rol={rol}&idUsuario={idUsuario}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo obtener la lista de usuarios.";
                    return View(new List<PerfilUsuarioDTO>());
                }

                var json = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("JSON recibido: " + json);

                var usuarios = JsonConvert.DeserializeObject<List<UsuarioApiDTO>>(json);

                var perfiles = usuarios
                    .Where(u => u != null && u.Id > 0)
                    .Select(u => new PerfilUsuarioDTO
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                        Correo = u.Correo,
                        Telefono = u.Telefono,
                        Rol = u.Rol
                    })
                    .ToList();

                foreach (var p in perfiles)
                {
                    System.Diagnostics.Debug.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Rol: {p.Rol}");
                }

                return View(perfiles);
            }


        }

        [Route("crear")]
        public ActionResult Crear()
        {
            return View(new PerfilUsuarioDTO());
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> Crear(PerfilUsuarioDTO perfil)
        {
            var token = await ObtenerTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var usuario = new Usuarios
                {
                    
                    nombre = perfil.Nombre.Split(' ')[0],
                    apellido = perfil.Nombre.Split(' ').Length > 1 ? perfil.Nombre.Split(' ')[1] : "",
                    correo = perfil.Correo,
                    telefono = perfil.Telefono,
                    rol = perfil.Rol,
                    contrasena = perfil.Contrasena,
                    fecha_registro = DateTime.Now,
                    activo = true,
                    fecha_actualizacion = DateTime.Now,
                    token = "",
                    especialidad = perfil.Rol == "medico" ? perfil.Especialidad : null,
                    numero_cedula = perfil.Rol == "medico" ? perfil.NumeroCedula : null
                };

                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/usuarios", content);
                var resultado = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Respuesta del API: " + resultado);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ViewBag.Error = "No se pudo crear el usuario.";
                return View(perfil);
            }
        }

        [Route("detalles/{id:int}")]
        public async Task<ActionResult> Detalles(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios/{id}");

                if (!response.IsSuccessStatusCode)
                    return HttpNotFound();

                var usuario = await response.Content.ReadAsAsync<Usuarios>();
                var dto = UsuarioMapper.APerfilDTO(usuario);
                return View(dto);
            }
        }

        [Route("Editar/{id:int}")]
        public async Task<ActionResult> Editar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios/{id}");

                if (!response.IsSuccessStatusCode)
                    return HttpNotFound();

                var usuario = await response.Content.ReadAsAsync<Usuarios>();
                var dto = UsuarioMapper.APerfilDTO(usuario);
                return View(dto);
            }
        }

        [HttpPost]
        [Route("Editar/{id:int}")]
        public async Task<ActionResult> Editar(PerfilUsuarioDTO perfil)
        {
            var token = await ObtenerTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var usuario = new Usuarios
                {
                    id_usuario = perfil.Id,
                    nombre = perfil.Nombre.Split(' ')[0],
                    apellido = perfil.Nombre.Split(' ').Length > 1 ? perfil.Nombre.Split(' ')[1] : "",
                    correo = perfil.Correo,
                    telefono = perfil.Telefono,
                    rol = perfil.Rol,
                    fecha_actualizacion = DateTime.Now,
                    
                    especialidad = perfil.Rol == "medico" ? perfil.Especialidad : null,
                    numero_cedula = perfil.Rol == "medico" ? perfil.NumeroCedula : null

                };

                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/usuarios/{perfil.Id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ViewBag.Error = "No se pudo actualizar el perfil.";
                return View(perfil);
            }
        }

        [Route("eliminar/{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo cargar el perfil.";
                    return View();
                }

                var usuario = await response.Content.ReadAsAsync<Usuarios>();
                var dto = UsuarioMapper.APerfilDTO(usuario);
                return View(dto);
            }
        }

        [HttpPost]
        [Route("eliminar-confirmado/{id:int}")]
        public async Task<ActionResult> EliminarConfirmado(int id)
        {
            var token = await ObtenerTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"api/usuarios/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ViewBag.Error = "No se pudo eliminar el perfil.";
                return RedirectToAction("Eliminar", new { id });
            }
        }

        [Route("cambiar-rol/{id:int}")]
        public async Task<ActionResult> CambiarRol(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios/{id}");

                if (!response.IsSuccessStatusCode)
                    return HttpNotFound();

                var usuario = await response.Content.ReadAsAsync<Usuarios>();
                var dto = UsuarioMapper.APerfilDTO(usuario);
                return View(dto);
            }
        }

        [HttpPost]
        [Route("cambiar-rol/{id:int}")]
        public async Task<ActionResult> CambiarRol(int id, string nuevoRol)
        {
            var token = await ObtenerTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var payload = new { rol = nuevoRol };
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/usuarios/cambiar-rol/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ViewBag.Error = "No se pudo cambiar el rol.";
                return RedirectToAction("CambiarRol", new { id });
            }
        }

        [Route("reset-password/{id:int}")]
        public async Task<ActionResult> ResetPassword(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"api/usuarios/{id}");

                if (!response.IsSuccessStatusCode)
                    return HttpNotFound();

                var usuario = await response.Content.ReadAsAsync<Usuarios>();
                var dto = UsuarioMapper.APerfilDTO(usuario);
                return View(dto);
            }
        }

        [HttpPost]
        [Route("reset-password/{id:int}")]
        public async Task<ActionResult> ResetPassword(int id, string nuevaContrasena)
        {
            var token = await ObtenerTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var payload = new { contrasena = nuevaContrasena };
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/usuarios/reset-password/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ViewBag.Error = "No se pudo resetear la contraseña.";
                return RedirectToAction("ResetPassword", new { id });
            }
        }

        private async Task<string> ObtenerTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var loginData = new
                {
                    correo = "sofia@admin.com",
                    contrasena = "admin123"
                };

                var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
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
