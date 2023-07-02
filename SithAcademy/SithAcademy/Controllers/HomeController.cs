namespace SithAcademy.Controllers;

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels;

public class HomeController : Controller
{
    public HomeController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Acknowledgement()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}