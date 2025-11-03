using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Correos
    {
        private static readonly string remitente = "edi70512@gmail.com";
        private static readonly string claveApp = "xjnmfetzrhalcjvb"; 
        private static readonly string nombreEmisor = "eSaludCare Soporte";

        private static bool Enviar(string correoDestino, string asunto, string cuerpoHtml)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(remitente, nombreEmisor),
                    Subject = asunto,
                    Body = cuerpoHtml,
                    IsBodyHtml = true
                };
                mail.To.Add(correoDestino);

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(remitente, claveApp);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
                return false;
            }
        }

        public static bool EnviarBienvenida(string nombre, string correo)
        {
            string html = ObtenerPlantillaBase(
                titulo: "¡Bienvenido a eSaludCare!",
                contenido: $@"
                    <p>Hola <b>{nombre}</b>,</p>
                    <p>Gracias por registrarte en <b>eSaludCare</b>, tu plataforma inteligente para gestión médica.</p>
                    <p>Ya puedes iniciar sesión y disfrutar de todos nuestros servicios.</p>
                    <a href='https://eSaludCare.mx/Login' style='background-color:#007bff;color:white;padding:10px 20px;text-decoration:none;border-radius:6px;'>Iniciar Sesión</a>
                ",
                pie: "Tu salud, más conectada que nunca."
            );
            return Enviar(correo, "Bienvenido a eSaludCare", html);
        }

        public static bool EnviarCambioContrasena(string correo)
        {
            string html = ObtenerPlantillaBase(
                titulo: "Cambio de Contraseña Exitoso",
                contenido: @"
                    <p>Hola,</p>
                    <p>Tu contraseña ha sido actualizada correctamente.</p>
                    <p>Si no realizaste este cambio, contacta con nuestro equipo de soporte de inmediato.</p>
                ",
                pie: "Seguridad y confianza en cada inicio de sesión."
            );
            return Enviar(correo, "Tu contraseña fue cambiada - eSaludCare", html);
        }

        public static bool EnviarRecuperacion(string correo, string claveTemporal)
        {
            string html = ObtenerPlantillaBase(
                titulo: "Recuperación de Contraseña",
                contenido: $@"
                    <p>Has solicitado recuperar tu acceso a <b>eSaludCare</b>.</p>
                    <p>Tu nueva clave temporal es:</p>
                    <div style='font-size:20px;font-weight:bold;background:#f4f4f4;border-radius:8px;padding:10px;display:inline-block;margin:10px 0;'>{claveTemporal}</div>
                    <p>Por seguridad, cambia esta clave al iniciar sesión.</p>
                ",
                pie: "Protegemos tu cuenta con tecnología avanzada."
            );
            return Enviar(correo, "Recuperación de Clave - eSaludCare", html);
        }

        private static string ObtenerPlantillaBase(string titulo, string contenido, string pie)
        {
            return $@"
            <html>
            <body style='font-family:Arial,sans-serif;background:#f9f9f9;padding:20px;'>
                <div style='max-width:600px;margin:auto;background:white;padding:20px;border-radius:10px;box-shadow:0 2px 10px rgba(0,0,0,0.1);'>
                    <div style='text-align:center;border-bottom:2px solid #007bff;padding-bottom:10px;margin-bottom:20px;'>
                        <h2 style='color:#007bff;margin:0;'>eSaludCare</h2>
                        <h4 style='color:#555;margin-top:5px;'>{titulo}</h4>
                    </div>
                    {contenido}
                    <hr style='margin-top:30px;border:none;border-top:1px solid #eee;'>
                    <p style='font-size:12px;color:#777;text-align:center;'>{pie}<br>© {DateTime.Now.Year} eSaludCare</p>
                </div>
            </body>
            </html>";
        }
    }
}
