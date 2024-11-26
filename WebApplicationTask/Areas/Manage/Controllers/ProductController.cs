using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTask.Areas.Manage.Controllers
{
    public class ProductController : Controller
    {
        [Area("Manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
