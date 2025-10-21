using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace eSaludCareUsers.Views.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /*
        [HttpPost]
        public async Task<ActionResult> Login(string correo, string contraseña)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new { correo, contraseña });
            }
        }
        */


        
    }
}
