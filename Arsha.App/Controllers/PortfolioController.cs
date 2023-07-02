using Arsha.App.Context;
using Arsha.App.ViewModels;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Controllers
{
    public class PortfolioController:Controller
    {
        private readonly ArshaDbContext? _context;

        public PortfolioController(ArshaDbContext? context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync(),
                Products = await _context.Products.Where(x => !x.IsDeleted).ToListAsync()
            };

            return View(homeViewModel);
        }
    }
}
