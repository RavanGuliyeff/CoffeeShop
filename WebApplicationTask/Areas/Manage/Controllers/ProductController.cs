using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.DAL;
using WebApplicationTask.Models;

namespace WebApplicationTask.Areas.Manage.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _dbContext;

		public ProductController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[Area("Manage")]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            return View(products);
        }

    }
}
