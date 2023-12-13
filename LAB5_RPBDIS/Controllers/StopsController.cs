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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
        // GET: Stops
        public async Task<IActionResult> Index(string? isRailwayStation, string? hasWaitingRoom)
        {
            // если запрос без параметров 
            // бёрём из куки сохраннёные значения, если их нет то стандартные
            if(isRailwayStation ==null && hasWaitingRoom==null)
            {
                hasWaitingRoom = Request.Cookies["hasWaitingRoom"] ?? "trueadnfalse";
                isRailwayStation = Request.Cookies["isRailwayStation"] ?? "trueadnfalse";
            }

            // переменные для сортировки в LINQ
            bool isRailwayStationBool, hasWaitingRoomBool;

            // возвращаемый списко остановок
            List<Stop> stops;

            // ложим во viewdata данные, для выбора нужной опции в селектах
            ViewData["IsRailwayStation"] = isRailwayStation;
            ViewData["HasWaitingRoom"] = hasWaitingRoom;

            // сохранение в куки выбранные опции
            Response.Cookies.Append("isRailwayStation", isRailwayStation);
            Response.Cookies.Append("hasWaitingRoom", hasWaitingRoom);

            // выбрано Да и Нет и там и там
            if (isRailwayStation == "trueadnfalse" && hasWaitingRoom == "trueadnfalse")
            {
                stops = await _context.Stops
                .ToListAsync();
                return View(stops);
            }

            // выбрано Да и Нет в первом месте
            if (isRailwayStation == "trueadnfalse" && hasWaitingRoom != "trueadnfalse")
            {
                bool.TryParse(hasWaitingRoom, out hasWaitingRoomBool);
                
                stops = await _context.Stops
                    .Where(s => s.HasWaitingRoom == hasWaitingRoomBool)
                .ToListAsync();

                return View(stops);
            }

            // выбрано Да и Нет в втором месте
            if (isRailwayStation != "trueadnfalse" && hasWaitingRoom == "trueadnfalse")
            {
                bool.TryParse(isRailwayStation, out isRailwayStationBool);

                stops = await _context.Stops
                    .Where(s => s.IsRailwayStation == isRailwayStationBool)
                .ToListAsync();

                return View(stops);
            }

            
            bool.TryParse(isRailwayStation, out isRailwayStationBool);
            bool.TryParse(hasWaitingRoom, out hasWaitingRoomBool);

            stops = await _context.Stops
                .Where(s => (s.IsRailwayStation == isRailwayStationBool) && (s.HasWaitingRoom == hasWaitingRoomBool))
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
