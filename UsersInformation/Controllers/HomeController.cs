using Microsoft.AspNetCore.Mvc;

namespace UsersInformation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
