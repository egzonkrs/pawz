using Microsoft.AspNetCore.Mvc;

namespace Pawz.Web.Controllers;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult NotFound()
    {
        return View();
    }
}
