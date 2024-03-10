using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data;
using System.Data.SqlClient;
=======
>>>>>>> refs/remotes/origin/master
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
<<<<<<< HEAD
using WebApplication2.Models;
=======

>>>>>>> refs/remotes/origin/master

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController()
        {
            //Se inicializa una variable logger de tipo ILogger que permite utilizar los logs. 
            _logger = LogManager.GetCurrentClassLogger();
        }

<<<<<<< HEAD
        [ValidarSesion]
        public ActionResult Index()
        {

            using (SqlConnection cn = new SqlConnection(Utilities.getString()))
            {
                // Crear el comando SqlCommand
                SqlCommand ValidarUsuario = new SqlCommand("sp_getUsuario", cn);
                ValidarUsuario.CommandType = CommandType.StoredProcedure; // Especificar que se trata de un procedimiento almacenado

                // Agregar parámetros
                ValidarUsuario.Parameters.AddWithValue("@IdUsuario", Session["usuario"]);

                // Abrir la conexión
                cn.Open();

                // Ejecutar el comando y obtener el resultado
                //object result = ValidarUsuario.ExecuteScalar();

                SqlDataReader reader = ValidarUsuario.ExecuteReader();


                if (reader.Read())
                {
                    // Obtener los valores de las columnas

                    ViewData["Nombre"] = reader.GetString(1);
                    ViewData["Correo"] = reader.GetString(2);
                    ViewData["Rfc"] = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                    {
                        // El valor no es nulo, podemos acceder a él de manera segura.
                        ViewData["DescripcionTrabajo"] = reader.GetString(5);
                        // Hacer algo con el valor aquí...
                    }
                    else
                    {
                        // El valor es nulo, podemos manejar este caso de manera adecuada.
                        ViewData["DescripcionTrabajo"] = "No existe información";
                        // Hacer algo con el valor nulo aquí...
                    }
     
                    ViewData["TipoUsuario"] = reader.GetString(6);
                    
                  
                }
                reader.Close(); // Cerrar el lector

                // Cerrar la conexión
                cn.Close();


                //Se registra un registro de tipo warn y se registrara con la descipcion que esta entre comillas
                _logger.Warn("Un usuario entro a la aplicacion");
                return View();
            }
        }

        [ValidarSesion]
        public ActionResult EditarPerfil(usuario oUsuario)
        {
=======
        public ActionResult Index()
        {
            //Se registra un registro de tipo warn y se registrara con la descipcion que esta entre comillas
            _logger.Warn("Un usuario entro a la aplicacion");
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
>>>>>>> refs/remotes/origin/master

            return View();
        }

<<<<<<< HEAD

        public ActionResult CerrarSesion()
        {
            // Vuelve nula la sesion, lo que le quita el acceso
            Session["usuario"] = null;
            return RedirectToAction("login", "Acceso");
=======
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
>>>>>>> refs/remotes/origin/master
        }

    }
}