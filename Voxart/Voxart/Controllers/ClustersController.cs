using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxart.Lib.LowManage;

namespace Voxart.Controllers
{
    [Route("/clusters")]
    [Authorize]
    public class ClustersController : Controller
    {
        [HttpGet("{clusterId}/{fileId}")]
        public ActionResult GetFile(string clusterId, string fileId)
        {
            var file = ClustersControl.GetFile(clusterId, fileId);
            var content = System.IO.File.ReadAllBytes(file!.FullName);

            return File(content, "audio/wav");
        }
    }
}
