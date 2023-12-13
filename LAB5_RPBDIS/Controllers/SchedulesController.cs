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

namespace RailwayTrafficSolution.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public SchedulesController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Schedules.CountAsync();
            var railwayTrafficContext = _context.Schedules.Include(s => s.Stop).Include(s => s.Train).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            if(totalItems% pageSize==0)
                ViewData["TotalPages"] = totalItems/pageSize; 
            else
                ViewData["TotalPages"] = totalItems/pageSize+1; 
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(await railwayTrafficContext);
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Stop)
                .Include(s => s.Train)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "Name");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("ScheduleId,DayOfWeek,TrainId,StopId,ArrivalTime,DepartureTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopName", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", schedule.TrainId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "Name", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", schedule.TrainId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,DayOfWeek,TrainId,StopId,ArrivalTime,DepartureTime")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopName", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainNumber", schedule.TrainId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Stop)
                .Include(s => s.Train)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
          return (_context.Schedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
    }
}
