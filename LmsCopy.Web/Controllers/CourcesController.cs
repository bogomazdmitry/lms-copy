using Microsoft.AspNetCore.Mvc;

namespace LmsCopy.Web.Controllers;

public class CourcesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
