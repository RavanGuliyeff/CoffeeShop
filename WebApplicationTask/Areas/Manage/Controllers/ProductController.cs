using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.DAL;
using WebApplicationTask.Helpers.Extensions;
using WebApplicationTask.Models;

namespace WebApplicationTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        AppDbContext _dbContext;
		private readonly IWebHostEnvironment _env;

		public ProductController(AppDbContext dbContext, IWebHostEnvironment env)
		{
			_dbContext = dbContext;
			_env = env;
		}

		[Area("Manage")]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _dbContext.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }


        public IActionResult Create()
        {
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!product.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Wrong type");
                return View();
            }

            if(product.FormFile.Length > 2097152)
            {
                ModelState.AddModelError("File", "Image size must be under of 2gb");
                return View();
            }

            product.ImgUrl = product.FormFile.Upload(_env.WebRootPath, "Upload/Product");

            if (!ModelState.IsValid)
            {
			    ViewBag.Categories = _dbContext.Categories.ToList();
                return View();
            }


			_dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            Product? product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) { return NotFound(); }
            FileExtension.Delete(_env.WebRootPath, "Upload/Product", product.ImgUrl);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {
			ViewBag.Categories = _dbContext.Categories.ToList();
			if (id == null) return NotFound();
            Product? product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
		public IActionResult Update(Product product)
		{
			// Kategorileri ViewBag'e aktaralım
			ViewBag.Categories = _dbContext.Categories.ToList();

			// Eğer dosya yüklenmemişse hata verelim
			if (product.FormFile == null || product.FormFile.Length == 0)
			{
				ModelState.AddModelError("File", "Please upload a file.");
				return View(product);
			}

			// Dosyanın türünü kontrol edelim
			if (!product.FormFile.ContentType.Contains("image"))
			{
				ModelState.AddModelError("File", "Wrong file type. Please upload an image.");
				return View(product);
			}

			// Dosya boyutunun 2MB'yi geçmemesini sağlayalım
			if (product.FormFile.Length > 2097152)
			{
				ModelState.AddModelError("File", "Image size must be under 2MB.");
				return View(product);
			}

			// Dosyayı yükleyelim
			product.ImgUrl = product.FormFile.Upload(_env.WebRootPath, "Upload/Product");

			// ModelState geçerli değilse, hataları geri gönderelim
			if (!ModelState.IsValid)
			{
				return View(product);
			}

			// Ürünü veritabanından alalım
			Product? oldProduct = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
			if (oldProduct == null)
			{
				return NotFound();
			}

			// Ürün bilgilerinde güncelleme yapalım
			oldProduct.Name = product.Name;
			oldProduct.ImgUrl = product.ImgUrl;  // Eğer görsel de güncelleniyorsa

			// Değişiklikleri kaydedelim
			_dbContext.SaveChanges();

			// Kullanıcıyı listeleme sayfasına yönlendirelim
			return RedirectToAction("Index");
		}

	}
}
