using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    //Estas clases se ejecutan antes del controlador, para poder validar antes de que entre a cualquier parte
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            /*
             Cuando el usuario inicia sesion se crea una Session dentro de la pagina, 
            la cual puede ser accesada de una manera muy facil, lo que nos hace poder 
            verificarla solo preguntando si es nula, en caso de serlo te envia a iniciar sesion,
            de otra manera te deja pasar.

             */
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }


    public class ValidarAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*
             Aprovechando la misma caracteristica de Session, 
            se crea otra llamada tipo para verificar que tipo de usuario es

            +-----------------------------------+
            |                                   |
            |                                   |
            |                                   |
            |       Por ahora esta en desuso    |
            |                                   |
            |                                   |
            |                                   |
            +------------------------------------
             */



            if (HttpContext.Current.Session["tipo"].ToString() == "2")
            {
                filterContext.Result = new RedirectResult("~/Acceso/Restringido");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}