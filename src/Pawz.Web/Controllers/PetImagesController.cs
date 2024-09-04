using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pawz.Web.Controllers
{
    [Route("[controller]")]
    public class PetImagesController : Controller
    {
        private readonly ILogger<PetImagesController> _logger;

        public PetImagesController(ILogger<PetImagesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
