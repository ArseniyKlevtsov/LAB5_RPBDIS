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
using RailwayTrafficSolution.ViewModels.TrainTypeViewModels;
using RailwayTrafficSolution.ViewModels;

namespace RailwayTrafficSolution.Controllers
{
    public class TrainTypesController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public TrainTypesController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: TrainTypes
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Index(string name, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {

            var trainTypes = _context.TrainTypes.AsQueryable();

            //фильтрация
            if (!string.IsNullOrEmpty(name))
            {
                trainTypes = trainTypes.Where(p => p.TypeName!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    trainTypes = trainTypes.OrderByDescending(s => s.TypeName);
                    break;
                default:
                    trainTypes = trainTypes.OrderBy(s => s.TypeName);
                    break;
            }

            // пагинация
            int pageSize = 10;
            var count = await trainTypes.CountAsync();
            var items = await trainTypes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            TrainTypeIndexViewModel viewModel = new TrainTypeIndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new TrainTypeFilterViewModel(name),
                new TrainTypeSortViewModel(sortOrder)
            );

            return View(viewModel);
        }

        // GET: TrainTypes/Details/5
        [Authorize(Roles = "MainAdmin,Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes
                .FirstOrDefaultAsync(m => m.TrainTypeId == id);
            if (trainType == null)
            {
                return NotFound();
            }

            return View(trainType);
        }

        // GET: TrainTypes/Create
        [Authorize(Roles = "MainAdmin,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Create([Bind("TrainTypeId,TypeName")] TrainType trainType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainType);
        }

        // GET: TrainTypes/Edit/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes.FindAsync(id);
            if (trainType == null)
            {
                return NotFound();
            }
            return View(trainType);
        }

        // POST: TrainTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TrainTypeId,TypeName")] TrainType trainType)
        {
            if (id != trainType.TrainTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainTypeExists(trainType.TrainTypeId))
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
            return View(trainType);
        }

        // GET: TrainTypes/Delete/5
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes
                .FirstOrDefaultAsync(m => m.TrainTypeId == id);
            if (trainType == null)
            {
                return NotFound();
            }

            return View(trainType);
        }

        // POST: TrainTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MainAdmin,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainTypes == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.TrainTypes'  is null.");
            }
            var trainType = await _context.TrainTypes.FindAsync(id);
            if (trainType != null)
            {
                _context.TrainTypes.Remove(trainType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainTypeExists(int id)
        {
          return (_context.TrainTypes?.Any(e => e.TrainTypeId == id)).GetValueOrDefault();
        }
    }
}
