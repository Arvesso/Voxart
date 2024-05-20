using Microsoft.AspNetCore.Mvc;

namespace Voxart.Controllers
{
    [Route("/clusters")]
    public class ClustersController : Controller
    {
        public string Index()
        {
            return "View()";
        }
    }
}
