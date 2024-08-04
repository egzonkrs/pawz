using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Web.Models;

namespace Pawz.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Shop()
    {
        return View();
    }

    public IActionResult Adopt()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
}
