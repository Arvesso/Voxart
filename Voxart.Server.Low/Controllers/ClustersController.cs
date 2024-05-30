using Microsoft.AspNetCore.Mvc;
using Voxart.Lib.LowManage;

namespace Voxart.Server.Low.Controllers
{
    [Route("/clusters")]
    public class ClustersController : Controller
    {
        [HttpGet("audio/{clusterId}/{fileId}")]
        public ActionResult GetAudioFile(string clusterId, string fileId)
        {
            var file = ClustersControl.GetFile(clusterId, fileId);
            var content = System.IO.File.ReadAllBytes(file!.FullName);

            return File(content, "audio/wav");
        }

        [HttpGet("video/{clusterId}/{fileId}")]
        public ActionResult GetVideoFile(string clusterId, string fileId)
        {
            var file = ClustersControl.GetFile(clusterId, fileId);
            var content = System.IO.File.ReadAllBytes(file!.FullName);

            return File(content, "video/mp4");
        }

        [HttpGet("text/{clusterId}/{fileId}")]
        public ActionResult GetTextFile(string clusterId, string fileId)
        {
            var file = ClustersControl.GetFile(clusterId, fileId);
            var content = System.IO.File.ReadAllText(file!.FullName);

            return Ok(content);
        }

        [HttpGet("image/{clusterId}/{fileId}")]
        public ActionResult GetImageFile(string clusterId, string fileId)
        {
            var file = ClustersControl.GetFile(clusterId, fileId);
            var content = System.IO.File.ReadAllBytes(file!.FullName);

            return File(content, "image/jpeg");
        }
    }
}
