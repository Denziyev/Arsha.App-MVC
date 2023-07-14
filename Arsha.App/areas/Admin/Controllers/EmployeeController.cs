using Arsha.App.Context;
using Arsha.App.Extentions;
using Arsha.App.Helpers;
using Arsha.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly ArshaDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(ArshaDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> employees = await _context.Employees.Include(x => x.Position).
                Where(c => !c.IsDeleted).ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _context.Positions.Where(p => !p.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            ViewBag.Positions = await _context.Positions.Where(p => !p.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (employee.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "File must be choosen");
            }

            //if (!Helper.IsImage(employee.FormFile))
            //{
            //    ModelState.AddModelError("FormFile", "File type must be image");
            //    return View();
            //}

            if (!Helper.IsSizeOk(employee.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size must be less than 1mb");
                return View();
            }
            employee.Image = employee.FormFile?.createimage(_environment.WebRootPath, "assets/img/");
            employee.CreatedDate = DateTime.Now;

            await _context.AddAsync(employee);

            await _context.SaveChangesAsync();
            return RedirectToAction("index", "employee");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Positions = await _context.Positions.Where(p => !p.IsDeleted).ToListAsync();
            Employee? employee = await _context.Employees.
                  Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Employee employee)
        {

            Employee? UpdateEmployee = await _context.Employees.
                Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(UpdateEmployee);
            }

            if (employee.FormFile != null)
            {
                //if (!Helper.IsImage(employee.FormFile))
                //{
                //    ModelState.AddModelError("FormFile", "Wronggg!");
                //    return View();
                //}
                if (!Helper.IsSizeOk(employee.FormFile, 1))
                {
                    ModelState.AddModelError("FormFile", "Wronggg!");
                    return View();
                }
                Helper.removeimage(_environment.WebRootPath, "assets/img/", UpdateEmployee.Image);
                UpdateEmployee.Image = employee.FormFile.createimage(_environment.WebRootPath, "assets/img/");
            }

            UpdateEmployee.FullName = employee.FullName;
            UpdateEmployee.Description = employee.Description;
            UpdateEmployee.PositionId = employee.PositionId;
            UpdateEmployee.SocialNetworks = employee.SocialNetworks;
            UpdateEmployee.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Employee? employee = await _context.Employees.
               Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            employee.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
