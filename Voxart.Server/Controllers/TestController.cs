using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voxart.Lib.Interactions.Sber;

namespace Voxart.Server.Controllers
{
    [Route("/")]
    public class TestController(ISaluteSpeechClient saluteSpeech) : Controller
    {
        public async Task<ActionResult> Index()
        {
            var c = await saluteSpeech.Synthesize("Меня зовут");

            System.IO.File.WriteAllBytes("hehe.wav", c);

            return Ok("ok");
        }
    }
}
