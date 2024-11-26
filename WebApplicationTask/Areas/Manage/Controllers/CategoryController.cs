using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.DAL;
using WebApplicationTask.Models;

namespace WebApplicationTask.Areas.Manage.Controllers
{
    public class CategoryController : Controller
    {

        AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Area("Manage")]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _dbContext.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

		
		
	}
}
