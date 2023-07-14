using Arsha.App.Context;
using Arsha.App.Extentions;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ArshaDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(ArshaDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products=_context.Products.Include(x=>x.Category).Where(x => !x.IsDeleted).ToList();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            
            product.Image = product.FormFile.createimage(_env.WebRootPath, "assets/img/");
            product.CreatedDate= DateTime.Now;
           
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Product? product = await _context.Products.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product postproduct, int id)
        {
            Product? product = await _context.Products.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            product.Title = postproduct.Title;
            product.Description = postproduct.Description;
            product.UpdatedDate = DateTime.Now;
            product.ImageHeight= postproduct.ImageHeight;
            product.ImageWidth= postproduct.ImageWidth;
            product.Image = postproduct.FormFile?.createimage(_env.WebRootPath, "assets/img/");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Products.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}
