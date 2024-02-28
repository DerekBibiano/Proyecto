using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;


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

        public ActionResult Index()
        {
            //Se registra un registro de tipo warn y se registrara con la descipcion que esta entre comillas
            _logger.Warn("Un usuario entro a la aplicacion");
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}