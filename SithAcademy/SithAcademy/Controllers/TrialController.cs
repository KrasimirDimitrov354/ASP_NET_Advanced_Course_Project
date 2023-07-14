namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class TrialController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Display()
    {
        return View();
    }
}
