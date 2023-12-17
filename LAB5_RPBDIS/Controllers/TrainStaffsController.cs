using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayTrafficSolution.Data;
using RailwayTrafficSolution.Models;
using Microsoft.AspNetCore.Authorization;
using RailwayTrafficSolution.ViewModels.TrainStuffViewModels;
using RailwayTrafficSolution.ViewModels;

namespace RailwayTrafficSolution.Controllers
{
    public class TrainStaffsController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public TrainStaffsController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: TrainStaffs
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Index(int train = 0, int employee = 0, int page = 1,
            SortState sortOrder = SortState.DayOfWeekAsc)
        {
            var trainStaffs = _context.TrainStaffs.Include(t => t.Employee).Include(t => t.Train).AsQueryable();

            //фильтрация
            if (train != 0)
            {
                trainStaffs = trainStaffs.Where(p => p.TrainId == train);
            }
            if (employee != 0)
            {
                trainStaffs = trainStaffs.Where(p => p.EmployeeId == employee);
            }
            

            // сортировка
            switch (sortOrder)
            {
                case SortState.DayOfWeekAsc:
                    trainStaffs = trainStaffs.OrderByDescending(s => s.DayOfWeek);
                    break;
                default:
                    trainStaffs = trainStaffs.OrderBy(s => s.DayOfWeek);
                    break;
            }

            // пагинация
            int pageSize = 10;
            var count = await trainStaffs.CountAsync();
            var items = await trainStaffs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            TrainStuffIndexViewModel viewModel = new TrainStuffIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new TrainStuffFilterViewModel(_context.Trains.ToList(), train, _context.Employees.ToList(), employee),
                new TrainStuffSortViewModel(sortOrder)
            );

            return View(viewModel);
        }

        // GET: TrainStaffs/Details/5
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs
                .Include(t => t.Employee)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainStaffId == id);
            if (trainStaff == null)
            {
                return NotFound();
            }

            return View(trainStaff);
        }

        // GET: TrainStaffs/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber");
            return View();
        }

        // POST: TrainStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("TrainStaffId,DayOfWeek,TrainId,EmployeeId")] TrainStaff trainStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", trainStaff.TrainId);
            return View(trainStaff);
        }

        // GET: TrainStaffs/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs.FindAsync(id);
            if (trainStaff == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", trainStaff.TrainId);
            return View(trainStaff);
        }

        // POST: TrainStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TrainStaffId,DayOfWeek,TrainId,EmployeeId")] TrainStaff trainStaff)
        {
            if (id != trainStaff.TrainStaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainStaffExists(trainStaff.TrainStaffId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", trainStaff.TrainId);
            return View(trainStaff);
        }

        // GET: TrainStaffs/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs
                .Include(t => t.Employee)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainStaffId == id);
            if (trainStaff == null)
            {
                return NotFound();
            }

            return View(trainStaff);
        }

        // POST: TrainStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainStaffs == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.TrainStaffs'  is null.");
            }
            var trainStaff = await _context.TrainStaffs.FindAsync(id);
            if (trainStaff != null)
            {
                _context.TrainStaffs.Remove(trainStaff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainStaffExists(int id)
        {
          return (_context.TrainStaffs?.Any(e => e.TrainStaffId == id)).GetValueOrDefault();
        }
    }
}
