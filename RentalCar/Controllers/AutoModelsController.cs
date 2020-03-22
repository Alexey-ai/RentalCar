using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar
{
    public class AutoModelsController : Controller
    {
        private readonly RentalCarContext _context;

        public AutoModelsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: AutoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auto.ToListAsync());
        }

        // GET: AutoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoModel = await _context.Auto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (autoModel == null)
            {
                return NotFound();
            }

            return View(autoModel);
        }

        // GET: AutoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CarMake,Model,Issue,Capacity,FuelConsuption,EngineType,EngineCapacity,TransmissionType,Price,Aviability,Mileage")] AutoModel autoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autoModel);
        }

        // GET: AutoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoModel = await _context.Auto.FindAsync(id);
            if (autoModel == null)
            {
                return NotFound();
            }
            return View(autoModel);
        }

        // POST: AutoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CarMake,Model,Issue,Capacity,FuelConsuption,EngineType,EngineCapacity,TransmissionType,Price,Aviability,Mileage")] AutoModel autoModel)
        {
            if (id != autoModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoModelExists(autoModel.ID))
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
            return View(autoModel);
        }

        // GET: AutoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoModel = await _context.Auto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (autoModel == null)
            {
                return NotFound();
            }

            return View(autoModel);
        }

        // POST: AutoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autoModel = await _context.Auto.FindAsync(id);
            _context.Auto.Remove(autoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoModelExists(int id)
        {
            return _context.Auto.Any(e => e.ID == id);
        }
    }
}
