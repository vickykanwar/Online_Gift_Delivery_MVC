using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Gift_Delivery_MVC.Data;
using Online_Gift_Delivery_MVC.Models;

namespace Online_Gift_Delivery_MVC.Controllers
{
    public class ShippingMethodsController : Controller
    {
        private readonly Online_Gift_Delivery_DBContext _context;

        public ShippingMethodsController(Online_Gift_Delivery_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: ShippingMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShippingMethod.ToListAsync());
        }
        [Authorize]
        // GET: ShippingMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethod
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            return View(shippingMethod);
        }
        [Authorize]
        // GET: ShippingMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShippingMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Charge")] ShippingMethod shippingMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shippingMethod);
        }
        [Authorize]
        // GET: ShippingMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethod.FindAsync(id);
            if (shippingMethod == null)
            {
                return NotFound();
            }
            return View(shippingMethod);
        }

        // POST: ShippingMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Charge")] ShippingMethod shippingMethod)
        {
            if (id != shippingMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingMethodExists(shippingMethod.Id))
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
            return View(shippingMethod);
        }
        [Authorize]
        // GET: ShippingMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethod
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            return View(shippingMethod);
        }

        // POST: ShippingMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingMethod = await _context.ShippingMethod.FindAsync(id);
            _context.ShippingMethod.Remove(shippingMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingMethodExists(int id)
        {
            return _context.ShippingMethod.Any(e => e.Id == id);
        }
    }
}
