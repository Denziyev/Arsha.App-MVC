using Arsha.App.Context;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController:Controller
    {
        private readonly ArshaDbContext _context;

        public CategoryController(ArshaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        { 
            IEnumerable<Category> categories=await _context.Categories.Where(x=>!x.IsDeleted).ToListAsync();

            return View(categories);
        }

        [HttpGet] 
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            
            if(!ModelState.IsValid)
            {
                return View();
            }
            category.CreatedDate = DateTime.Now;
            await _context.Categories.AddAsync(category);
             await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Category? category=await _context.Categories.Where(x=>!x.IsDeleted &&  x.Id==id).FirstOrDefaultAsync();
            if(category==null)
            {
                return NotFound();
            }
           return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category postcategory,int id)
        {
            Category? category = await _context.Categories.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
           category.Name = postcategory.Name;
            category.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _context.Categories.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            category.IsDeleted =true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
    }
}
