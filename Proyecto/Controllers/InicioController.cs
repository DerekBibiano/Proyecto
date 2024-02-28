using Microsoft.AspNetCore.Mvc;

namespace Proyecto
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
