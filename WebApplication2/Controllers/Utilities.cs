using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication2.Controllers
{
    public class Utilities
    {

        /*
         Esta clase se creó con el fin de hacer accesible funciones que se usan en diversas partes del proyecto
        para no hacernos pelotas cuando algo salga mal o alguien mas quiera usar el proyecto
         */
        //usa la libreria Cryptography para generar un texto en sha256, practicamente sera para las contraseñas
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
        public static string getString()
        {
            // En la seccion "Source" se cambia por el servidor que se va a usar para correr el proyecto
            return @"Data Source=ARATT\SQLEXPRESS;Initial Catalog=CursosServitec; Integrated Security=true"; 
        }
    }


}