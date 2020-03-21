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
    public class DriverModelsController : Controller
    {
        private readonly RentalCarContext _context;

        public DriverModelsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: DriverModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drivers.ToListAsync());
        }

        // GET: DriverModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.Drivers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // GET: DriverModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DriverModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Passport,DriveLisence,BirthdayDate,RentalJoinDate,DriverPicturePath,DistanceTraveled")] DriverModel driverModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driverModel);
        }

        // GET: DriverModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.Drivers.FindAsync(id);
            if (driverModel == null)
            {
                return NotFound();
            }
            return View(driverModel);
        }

        // POST: DriverModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Passport,DriveLisence,BirthdayDate,RentalJoinDate,DriverPicturePath,DistanceTraveled")] DriverModel driverModel)
        {
            if (id != driverModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverModelExists(driverModel.ID))
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
            return View(driverModel);
        }

        // GET: DriverModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.Drivers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // POST: DriverModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverModel = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driverModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverModelExists(int id)
        {
            return _context.Drivers.Any(e => e.ID == id);
        }
    }
}
