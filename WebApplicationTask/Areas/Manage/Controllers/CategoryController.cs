using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.DAL;
using WebApplicationTask.Models;

namespace WebApplicationTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {

        AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _dbContext.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) { return NotFound(); }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category oldCategory = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (oldCategory == null) return NotFound();

            oldCategory.Name = category.Name;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
