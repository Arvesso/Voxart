using Microsoft.AspNetCore.Mvc;

namespace Voxart.Server.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
