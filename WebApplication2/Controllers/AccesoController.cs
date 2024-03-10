<<<<<<< HEAD
﻿/*
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
=======
﻿using System;
using System.Web.Mvc;
using System.Text;

using System.Security.Cryptography;

using WebApplication2.Models;

using System.Data.SqlClient;
using System.Data;
>>>>>>> refs/remotes/origin/master
using NLog;



namespace WebApplication2.Controllers
{

    public class AccesoController : Controller
    {
        private readonly ILogger _logger;
<<<<<<< HEAD
=======
        static string cadena = @"Data Source=Pc\SQLEXPRESS;Initial Catalog=CursosServitec; Integrated Security=true";
>>>>>>> refs/remotes/origin/master

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
<<<<<<< HEAD
        public ActionResult Registrar(usuario oUsuario) // Se usa el modelo de usuario para obtener los datos de la vista
=======
        public ActionResult Registrar(usuario oUsuario)
>>>>>>> refs/remotes/origin/master
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Contrasena == oUsuario.ConfirmarContrasena)
            {
<<<<<<< HEAD
                oUsuario.Contrasena = Utilities.Convertirsha256(oUsuario.Contrasena); // Manda llamar la funcion de utilities para cifrar la contraseña
=======
                oUsuario.Contrasena = Convertirsha256(oUsuario.Contrasena);
>>>>>>> refs/remotes/origin/master
            }
            else
            {
                ViewData["Mensaje"] = "Las Contrasenas no coinciden amor";
                return View();
            }

<<<<<<< HEAD
            // Utilities.getString() Es la cadena de conexion que se encuentra en la clase Utilities para poder hacerla accesible para cualquier parte del proyecto
            using (SqlConnection cn = new SqlConnection(Utilities.getString()))
            {
                // sp_RegistrarUsuario es un procedimiento almacenado de SQL server
=======

            using (SqlConnection cn = new SqlConnection(cadena))
            {
>>>>>>> refs/remotes/origin/master
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

<<<<<<< HEAD
                //Se abre la conexion  y se ejecuta el query
=======
>>>>>>> refs/remotes/origin/master
                cn.Open();

                cmd.ExecuteNonQuery();

<<<<<<< HEAD
                // Tomamos los valores que regresa el procedimiento almacenado y se guardan en una varibale
=======
>>>>>>> refs/remotes/origin/master
                registrado = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

<<<<<<< HEAD

            if (registrado)
            {
                // En caso de haber regustrado el usuario correctamente lo redirige a la pagina de inicio de sesión
=======
            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
>>>>>>> refs/remotes/origin/master
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
<<<<<<< HEAD

                //Se pone el mensaje en la pantalla
                ViewData["Mensaje"] = mensaje;
=======
>>>>>>> refs/remotes/origin/master
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(usuario oUsuario)
        {

<<<<<<< HEAD
            oUsuario.Contrasena = Utilities.Convertirsha256(oUsuario.Contrasena);// Manda llamar la funcion de utilities para cifrar la contraseña

            using (SqlConnection cn = new SqlConnection(Utilities.getString()))
=======
            oUsuario.Contrasena = Convertirsha256(oUsuario.Contrasena);

            using (SqlConnection cn = new SqlConnection(cadena))
>>>>>>> refs/remotes/origin/master
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
<<<<<<< HEAD
                int tipoUsuario = 0;
=======
>>>>>>> refs/remotes/origin/master

                if (reader.Read())
                {
                    // Obtener los valores de las columnas
                    idUsuario = reader.GetInt32(0);
<<<<<<< HEAD
                    tipoUsuario = reader.GetInt32(1);
=======
>>>>>>> refs/remotes/origin/master
                }
                reader.Close(); // Cerrar el lector

                // Cerrar la conexión
                cn.Close();




                if (idUsuario != 0)
                {
                    // Asignar los valores a las sesiones
                    Session["usuario"] = idUsuario;
<<<<<<< HEAD
                    Session["tipo"] = tipoUsuario;
=======
>>>>>>> refs/remotes/origin/master
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

<<<<<<< HEAD
        // Funcion para cifrar la contraseña
        
=======
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
>>>>>>> refs/remotes/origin/master


    }
}