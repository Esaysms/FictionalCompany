using Microsoft.AspNetCore.Mvc;

namespace FictionalCompany.Controllers
{
    public class FrontEndControllerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
