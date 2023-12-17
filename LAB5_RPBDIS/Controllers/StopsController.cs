using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayTrafficSolution.Data;
using RailwayTrafficSolution.Models;
using Microsoft.AspNetCore.Authorization;
using RailwayTrafficSolution.ViewModels;
using RailwayTrafficSolution.ViewModels.StopViewModels;

namespace RailwayTrafficSolution.Controllers
{
    public class StopsController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public StopsController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Stops
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Index(string name, int isRailwayStation=0, int hasWaitingRoom=0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {

            var stops = _context.Stops.AsQueryable();

            //фильтрация
            if (isRailwayStation != 0)
            {
                // {0; "Все"},{1; "Да"},{2, "Нет"}
                stops = stops.Where(p => p.IsRailwayStation == (isRailwayStation== 1?true:false));
            }
            if (hasWaitingRoom != 0)
            {
                // {0; "Все"},{1; "Да"},{2, "Нет"}
                stops = stops.Where(p => p.HasWaitingRoom == (hasWaitingRoom== 1 ? true : false));
            }
            if (!string.IsNullOrEmpty(name))
            {
                stops = stops.Where(p => p.Name!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    stops = stops.OrderByDescending(s => s.Name);
                    break;
                default:
                    stops = stops.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            int pageSize = 5;
            var count = await stops.CountAsync();
            var items = await stops.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            StopIndexViewModel viewModel = new StopIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new StopFilterViewModel(isRailwayStation, hasWaitingRoom, name),
                new StopSortViewModel(sortOrder)
            );

            return View(viewModel);
        }


        // GET: Stops/Details/5
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Details(int? id, string? searchStartTimeString, string? searchEndTimeString)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .Include(s => s.Schedules)
                    .ThenInclude(s => s.Train)
                .FirstOrDefaultAsync(m => m.StopId == id);
            if (stop == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(searchStartTimeString))
            {
                int startTime = int.Parse(searchStartTimeString);
                stop.Schedules = stop.Schedules.Where(s => s.ArrivalTime.HasValue && s.ArrivalTime.Value.Hours >= startTime).ToList();
                ViewBag.StartTime = startTime;
            }

            if (!String.IsNullOrEmpty(searchEndTimeString))
            {
                int endTime = int.Parse(searchEndTimeString);
                stop.Schedules = stop.Schedules.Where(s => s.ArrivalTime.HasValue && s.ArrivalTime.Value.Hours <= endTime).ToList();
                ViewBag.EndTime = endTime;

            }
            return View(stop);
        }

        // GET: Stops/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("StopId,Name,IsRailwayStation,HasWaitingRoom")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stop);
        }

        // GET: Stops/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        // POST: Stops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("StopId,Name,IsRailwayStation,HasWaitingRoom")] Stop stop)
        {
            if (id != stop.StopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StopExists(stop.StopId))
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
            return View(stop);
        }

        // GET: Stops/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .FirstOrDefaultAsync(m => m.StopId == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        // POST: Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stops == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.Stops'  is null.");
            }
            var stop = await _context.Stops.FindAsync(id);
            if (stop != null)
            {
                _context.Stops.Remove(stop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StopExists(int id)
        {
          return (_context.Stops?.Any(e => e.StopId == id)).GetValueOrDefault();
        }
    }
}
