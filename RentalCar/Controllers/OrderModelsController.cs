using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Controllers
{
    public class OrderModelsController : Controller
    {
        private readonly RentalCarContext _context;

        public OrderModelsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: OrderModels
        public async Task<IActionResult> Index()
        {
            var rentalCarContext = _context.Orders.Include(o => o.Auto).Include(o => o.Driver);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: OrderModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .Include(o => o.Auto)
                .Include(o => o.Driver)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: OrderModels/Create
        public IActionResult Create()
        {
            AutoAviableDropDownList();
            ViewData["DriverID"] = new SelectList(_context.Drivers, "ID", "FullName");
            return View();
        }

        // POST: OrderModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AutoID,DriverID,OrderStartDate")] OrderModel order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();

                var auto = await _context.Auto.FindAsync(order.AutoID);
                auto.Aviability = false;
                _context.Update(auto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        // GET: OrderModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            AutoAviableDropDownList();
            ViewData["DriverID"] = new SelectList(_context.Drivers, "ID", "DriveLisence", orderModel.DriverID);
            return View(orderModel);
        }

        // POST: OrderModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AutoID,DriverID,OrderStartDate,OrderEndDate,OrderMilleage,OrderDayCount,TotalPrice")] OrderModel orderModel)
        {
            if (id != orderModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.ID))
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
            ViewData["AutoID"] = new SelectList(_context.Auto, "ID", "CarMake", orderModel.AutoID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "ID", "DriveLisence", orderModel.DriverID);
            return View(orderModel);
        }

        // GET: OrderModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .Include(o => o.Auto)
                .Include(o => o.Driver)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: OrderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CloseOrderList()
        {
            var orderQuery = from o in _context.Orders.Include(o => o.Auto).Include(o => o.Driver)
                             orderby o.ID
                             where o.OrderEndDate == null
                             select o;
            return View(orderQuery.ToList());
        }
            public async Task<IActionResult> CloseOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.Auto).Include(o => o.Driver).FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Close
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseOrder([Bind("ID,AutoID,DriverID,OrderStartDate,OrderEndDate,OrderMilleage")] OrderModel order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDayCount = (order.OrderEndDate.GetValueOrDefault()-order.OrderStartDate).Days + 1;

                var auto = await _context.Auto.FindAsync(order.AutoID);
                auto.Aviability = true;
                auto.Mileage += order.OrderMilleage.GetValueOrDefault();

                order.TotalPrice = auto.Price * order.OrderDayCount;

                //если пробег больше 100км в день каждый оплачивается 20р/км
                int extraMilleage = (order.OrderMilleage.Value - order.OrderDayCount.Value * 100);
                if (extraMilleage > 0)
                {
                    order.TotalPrice += extraMilleage * 20;
                }
                
                _context.Update(order);

                await _context.SaveChangesAsync();
                _context.Update(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }

        private void AutoAviableDropDownList()
        {
            var autosQuery = from b in _context.Auto
                             orderby b.ID
                             where b.Aviability != false
                             select b;
            ViewData["AutoID"] = new SelectList(autosQuery.AsNoTracking(), "ID", "FullName");
        }
        private void AutoNotAviableDropDownList()
        {
            var autosQuery = from b in _context.Auto
                             orderby b.ID
                             where b.Aviability == false
                             select b;
            ViewData["AutoID"] = new SelectList(autosQuery.AsNoTracking(), "ID", "FullName");
        }
    }
}
