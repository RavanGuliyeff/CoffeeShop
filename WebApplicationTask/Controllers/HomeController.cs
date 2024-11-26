using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTask.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
