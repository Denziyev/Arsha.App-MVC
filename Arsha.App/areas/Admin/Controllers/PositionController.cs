﻿using Arsha.App.Context;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly ArshaDbContext _context;

        public PositionController(ArshaDbContext context)
        {
            _context = context;
        }

  
        public async Task<IActionResult> Index()
        {
            IEnumerable<Position> positions= await _context.Positions.Where(x=>!x.IsDeleted).ToListAsync();
            return View(positions);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            position.CreatedDate=DateTime.Now;
          await  _context.Positions.AddAsync(position);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Position? Position = await _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (Position == null)
            {
                return NotFound();
            }
            return View(Position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Position postPosition, int id)
        {
            Position? Position = await _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (Position == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            Position.Name = postPosition.Name;
            Position.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Position? Position = await _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (Position == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            Position.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Position");
        }

    }
}
