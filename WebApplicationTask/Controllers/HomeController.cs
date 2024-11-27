using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.DAL;
using WebApplicationTask.Models;

namespace WebApplicationTask.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _dbContext.Products.ToListAsync(); 

            return View(products);
        }
    }
}
