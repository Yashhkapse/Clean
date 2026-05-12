using Microsoft.AspNetCore.Mvc;

namespace ProCleanArchitecture.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet("/Home")]
    [HttpGet("/Home/Index")]
    public IActionResult Index()
    {
        return RedirectToAction("Index", "AdminDashboard");
    }

    [HttpGet("/Home/Privacy")]
    public IActionResult Privacy()
    {
        return RedirectToAction("Index", "AdminDashboard");
    }
}
