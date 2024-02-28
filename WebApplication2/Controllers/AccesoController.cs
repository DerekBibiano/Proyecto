/*
 IMPORTACION DE LAS LIBRERIAS
 */

//librerias del sistema
using System;
using System.Web.Mvc;
using System.Text;

//libreria para cifrar las contraseñas
using System.Security.Cryptography;

//importacion del modelo del usuario
using WebApplication2.Models;

//librerias para sql server
using System.Data.SqlClient;
using System.Data;

//libreria para los logs
using NLog;



namespace WebApplication2.Controllers
{

    public class AccesoController : Controller
    {
        private readonly ILogger _logger;

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
        public ActionResult Registrar(usuario oUsuario) // Se usa el modelo de usuario para obtener los datos de la vista
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Contrasena == oUsuario.ConfirmarContrasena)
            {
                oUsuario.Contrasena = Utilities.Convertirsha256(oUsuario.Contrasena); // Manda llamar la funcion de utilities para cifrar la contraseña
            }
            else
            {
                ViewData["Mensaje"] = "Las Contrasenas no coinciden amor";
                return View();
            }

            // Utilities.getString() Es la cadena de conexion que se encuentra en la clase Utilities para poder hacerla accesible para cualquier parte del proyecto
            using (SqlConnection cn = new SqlConnection(Utilities.getString()))
            {
                // sp_RegistrarUsuario es un procedimiento almacenado de SQL server
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                //Se abre la conexion  y se ejecuta el query
                cn.Open();

                cmd.ExecuteNonQuery();

                // Tomamos los valores que regresa el procedimiento almacenado y se guardan en una varibale
                registrado = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }


            if (registrado)
            {
                // En caso de haber regustrado el usuario correctamente lo redirige a la pagina de inicio de sesión
                return RedirectToAction("Login", "Acceso");
            }
            else
            {

                //Se pone el mensaje en la pantalla
                ViewData["Mensaje"] = mensaje;
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(usuario oUsuario)
        {

            oUsuario.Contrasena = Utilities.Convertirsha256(oUsuario.Contrasena);// Manda llamar la funcion de utilities para cifrar la contraseña

            using (SqlConnection cn = new SqlConnection(Utilities.getString()))
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
                int tipoUsuario = 0;

                if (reader.Read())
                {
                    // Obtener los valores de las columnas
                    idUsuario = reader.GetInt32(0);
                    tipoUsuario = reader.GetInt32(1);
                }
                reader.Close(); // Cerrar el lector

                // Cerrar la conexión
                cn.Close();




                if (idUsuario != 0)
                {
                    // Asignar los valores a las sesiones
                    Session["usuario"] = idUsuario;
                    Session["tipo"] = tipoUsuario;
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

        // Funcion para cifrar la contraseña
        


    }
}