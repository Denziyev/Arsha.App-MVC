using Arsha.App.Context;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialNetworkController : Controller
    {
        private readonly ArshaDbContext _context;

        public SocialNetworkController(ArshaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SocialNetwork> socialNetworks= await _context.SocialNetworks.Include(x=>x.Employee).Where(x=>!x.IsDeleted).ToListAsync();
            return View(socialNetworks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialNetwork socialNetwork)
        {
            ViewBag.Employees = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            socialNetwork.CreatedDate = DateTime.Now;
          await  _context.SocialNetworks.AddAsync(socialNetwork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            SocialNetwork? socialNetwork=await _context.SocialNetworks.Where(x=>!x.IsDeleted &&  x.Id == id).FirstOrDefaultAsync();
            ViewBag.Employees = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
            if (socialNetwork==null)
            {
                return NotFound();
            }
           return View(socialNetwork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SocialNetwork updatesocialNetwork)
        {
            ViewBag.Employees = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
            SocialNetwork? socialNetwork = await _context.SocialNetworks.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if(socialNetwork == null)
            {
                return NotFound();
            }

            socialNetwork.Icon=updatesocialNetwork.Icon;
            socialNetwork.Link=updatesocialNetwork.Link;
            socialNetwork.UpdatedDate = DateTime.Now;
    
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            SocialNetwork socialNetwork=await _context.SocialNetworks.Where(x=>!x.IsDeleted && x.Id==id).FirstOrDefaultAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if (socialNetwork == null)
            {
                return NotFound();
            }

            socialNetwork.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
