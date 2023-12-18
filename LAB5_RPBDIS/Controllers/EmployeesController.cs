using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayTrafficSolution.Data;
using RailwayTrafficSolution.Models;
using Microsoft.AspNetCore.Authorization;
using RailwayTrafficSolution.ViewModels;
using RailwayTrafficSolution.ViewModels.EmployeeViewModels;
using Bogus;

namespace RailwayTrafficSolution.Controllers
{
    
    public class EmployeesController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public EmployeesController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Index(string name, int position = 0, int page = 1, 
            SortState sortOrder = SortState.NameAsc)
        {
            
            var employees = _context.Employees.Include(e => e.Position).AsQueryable();

            //фильтрация
            if (position != 0)
            {
                employees = employees.Where(p => p.PositionID == position);
            }
            if (!string.IsNullOrEmpty(name))
            {
                employees = employees.Where(p => p.EmployeeName!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    employees = employees.OrderByDescending(s => s.EmployeeName);
                    break;
                case SortState.AgeAsc:
                    employees = employees.OrderBy(s => s.age);
                    break;
                case SortState.AgeDesc:
                    employees = employees.OrderByDescending(s => s.age);
                    break;
                case SortState.WorkExperienceAsc:
                    employees = employees.OrderBy(s => s.WorkExperience);
                    break;
                case SortState.WorkExperienceDesc:
                    employees = employees.OrderByDescending(s => s.WorkExperience);
                    break;
                default:
                    employees = employees.OrderBy(s => s.EmployeeName);
                    break;
            }

            // пагинация
            int pageSize =8;
            var count = await employees.CountAsync();
            var items = await employees.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            EmployeeIndexViewModel viewModel = new EmployeeIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new EmployeeFilterViewModel(_context.Positions.ToList(), position, name),
                new EmployeeSortViewModel(sortOrder)
            );

            return View(viewModel);
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,age,WorkExperience,PositionID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,age,WorkExperience,PositionID")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
