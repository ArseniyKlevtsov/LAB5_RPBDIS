using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayTrafficSolution.Data;
using RailwayTrafficSolution.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using RailwayTrafficSolution.ViewModels.TrainViewModels;
using RailwayTrafficSolution.ViewModels;

namespace RailwayTrafficSolution.Controllers
{
    public class TrainsController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public TrainsController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Trains
        public async Task<IActionResult> Index(string name, int trainType = 0, float distanceInKm = 0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {

            var trains = _context.Trains.Include(e => e.TrainType).AsQueryable();

            //фильтрация
            if (trainType != 0)
            {
                trains = trains.Where(p => p.TrainTypeId == trainType);
            }
            if (distanceInKm != 0)
            {
                trains = trains.Where(p => p.DistanceInKm >= distanceInKm);
            }
            if (!string.IsNullOrEmpty(name))
            {
                trains = trains.Where(p => p.TrainNumber!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    trains = trains.OrderByDescending(s => s.TrainNumber);
                    break;
                case SortState.DistanceInKmAsc:
                    trains = trains.OrderBy(s => s.DistanceInKm);
                    break;
                case SortState.DistanceInKmDesc:
                    trains = trains.OrderByDescending(s => s.DistanceInKm);
                    break;
                case SortState.TrainTypeAsc:
                    trains = trains.OrderBy(s => s.TrainType);
                    break;
                case SortState.TrainTypeDesc:
                    trains = trains.OrderByDescending(s => s.TrainType);
                    break;
                default:
                    trains = trains.OrderBy(s => s.TrainNumber);
                    break;
            }

            // пагинация
            int pageSize = 10;
            var count = await trains.CountAsync();
            var items = await trains.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            TrainIndexViewModel viewModel = new TrainIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new TrainFilterViewModel(_context.TrainTypes.ToList(), trainType, name),
                new TrainSortViewModel(sortOrder)
            );

            return View(viewModel);
        }

        // GET: Employees/Details/5
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

        // GET: Trains/Details/5
        public async Task<IActionResult> Details(int? id, string? sortOrder)
        {
            ViewBag.ArivalSortParm = String.IsNullOrEmpty(sortOrder) ? "arival_desc" : "";
            ViewBag.DepartureSortParm = sortOrder == "departure" ? "departure_desc" : "departure";
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains
                .Include(t => t.TrainType)
                .Include(t => t.Schedules)
                    .ThenInclude(s => s.Stop)
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (train == null)
            {
                return NotFound();
            }
            List<Schedule> sortedSchedules;

            switch (sortOrder)
            {
                case "arival_desc":
                    sortedSchedules = train.Schedules.OrderByDescending(s => s.ArrivalTime).ToList();
                    break;
                case "departure":
                    sortedSchedules = train.Schedules.OrderBy(s => s.DepartureTime).ToList();
                    break;
                case "departure_desc":
                    sortedSchedules = train.Schedules.OrderByDescending(s => s.DepartureTime).ToList();
                    break;
                default:
                    sortedSchedules = train.Schedules.OrderBy(s => s.ArrivalTime).ToList();
                    break;
            }

            var sortedTrain = new Train
            {
                TrainId = train.TrainId,
                // Копирование других свойств поезда
                Schedules = sortedSchedules
            };
            return View(sortedTrain);
        }

        // GET: Trains/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["TrainTypeId"] = new SelectList(_context.TrainTypes, "TrainTypeId", "TypeName");
            return View();
        }

        // POST: Trains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("TrainId,TrainNumber,DistanceInKm,IsBrandedTrain,TrainTypeId")] Train train)
        {
            if (ModelState.IsValid)
            {
                _context.Add(train);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainTypeId"] = new SelectList(_context.TrainTypes, "TrainTypeId", "TypeName", train.TrainTypeId);
            return View(train);
        }

        // GET: Trains/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            ViewData["TrainTypeId"] = new SelectList(_context.TrainTypes, "TrainTypeId", "TypeName", train.TrainTypeId);
            return View(train);
        }

        // POST: Trains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TrainId,TrainNumber,DistanceInKm,IsBrandedTrain,TrainTypeId")] Train train)
        {
            if (id != train.TrainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(train);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainExists(train.TrainId))
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
            ViewData["TrainTypeId"] = new SelectList(_context.TrainTypes, "TrainTypeId", "TypeName", train.TrainTypeId);
            return View(train);
        }

        // GET: Trains/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains
                .Include(t => t.TrainType)
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (train == null)
            {
                return NotFound();
            }

            return View(train);
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trains == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.Trains'  is null.");
            }
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TrainStuff(int dayOfWeek, int trainId)
        {
            ViewBag.DayOfWeek = dayOfWeek;
            var train = _context.Trains
                .Include(t => t.TrainStaffs)
                .ThenInclude(ts => ts.Employee)
                .ThenInclude(e => e!.Position)
                .FirstOrDefault(t => t.TrainId == trainId);
            if (train == null)
            {
                return NotFound();
            }

            return View(train);
        }

        private bool TrainExists(int id)
        {
            return (_context.Trains?.Any(e => e.TrainId == id)).GetValueOrDefault();
        }
    }
}

