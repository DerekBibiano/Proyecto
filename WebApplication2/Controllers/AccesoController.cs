using System;
using System.Web.Mvc;
using System.Text;

using System.Security.Cryptography;

using WebApplication2.Models;

using System.Data.SqlClient;
using System.Data;
using NLog;



namespace WebApplication2.Controllers
{

    public class AccesoController : Controller
    {
        private readonly ILogger _logger;
        static string cadena = @"Data Source=Pc\SQLEXPRESS;Initial Catalog=CursosServitec; Integrated Security=true";

        public AccesoController()
        {
            //Se inicializa una variable logger de tipo ILogger que permite utilizar los logs. 
            _logger = LogManager.GetCurrentClassLogger();
        }

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Restringido()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Contrasena == oUsuario.ConfirmarContrasena)
            {
                oUsuario.Contrasena = Convertirsha256(oUsuario.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "Las Contrasenas no coinciden amor";
                return View();
            }


            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(usuario oUsuario)
        {

            oUsuario.Contrasena = Convertirsha256(oUsuario.Contrasena);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                // Crear el comando SqlCommand
                SqlCommand ValidarUsuario = new SqlCommand("sp_validarUsuario", cn);
                ValidarUsuario.CommandType = CommandType.StoredProcedure; // Especificar que se trata de un procedimiento almacenado

                // Agregar parámetros
                ValidarUsuario.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                ValidarUsuario.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);

                // Abrir la conexión
                cn.Open();

                // Ejecutar el comando y obtener el resultado
                //object result = ValidarUsuario.ExecuteScalar();

                SqlDataReader reader = ValidarUsuario.ExecuteReader();

                int idUsuario = 0;

                if (reader.Read())
                {
                    // Obtener los valores de las columnas
                    idUsuario = reader.GetInt32(0);
                }
                reader.Close(); // Cerrar el lector

                // Cerrar la conexión
                cn.Close();




                if (idUsuario != 0)
                {
                    // Asignar los valores a las sesiones
                    Session["usuario"] = idUsuario;
                    _logger.Warn($"El usuario {oUsuario.Correo} accesó a la pagina.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "Usuario no encontrado";
                    return View();
                }

            }

        }

        public static string Convertirsha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }


    }
}