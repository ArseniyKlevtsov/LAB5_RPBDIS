using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB5_RPBDIS.Data;
using LAB5_RPBDIS.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace LAB5_RPBDIS.Controllers
{
    public class StopsController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public StopsController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Stops
        public async Task<IActionResult> Index(bool? isRailwayStation, bool? hasWaitingRoom)
        {
            // Загрузка сохраненных значений фильтрации из куки
            if (!isRailwayStation.HasValue)
            {
                isRailwayStation = bool.Parse(Request.Cookies["isRailwayStation"] ?? "false");
            }
            else
            {
                // Сохранение значения фильтрации в куки
                Response.Cookies.Append("isRailwayStation", isRailwayStation.ToString());
            }

            if (!hasWaitingRoom.HasValue)
            {
                hasWaitingRoom = bool.Parse(Request.Cookies["hasWaitingRoom"] ?? "false");
            }
            else
            {
                Response.Cookies.Append("hasWaitingRoom", hasWaitingRoom.ToString());
            }

            ViewData["IsRailwayStation"] = isRailwayStation;
            ViewData["HasWaitingRoom"] = hasWaitingRoom;

            // Получение списка остановок в зависимости от фильтров
            var stops = await _context.Stops
                .Where(s => (!isRailwayStation.HasValue || s.IsRailwayStation == isRailwayStation.Value) &&
                            (!hasWaitingRoom.HasValue || s.HasWaitingRoom == hasWaitingRoom.Value))
                .ToListAsync();

            return View(stops);
        }
        // GET: Stops/Details/5

        public async Task<IActionResult> Details(int? id)
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

        // GET: Stops/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
