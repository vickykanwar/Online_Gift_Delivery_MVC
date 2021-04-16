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
    public class GiftsController : Controller
    {
        private readonly Online_Gift_Delivery_DBContext _context;

        public GiftsController(Online_Gift_Delivery_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gift.ToListAsync());
        }
        [Authorize]
        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }
        [Authorize]
        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,ItemPrice")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }
        [Authorize]
        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,ItemPrice")] Gift gift)
        {
            if (id != gift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftExists(gift.Id))
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
            return View(gift);
        }
        [Authorize]
        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gift = await _context.Gift.FindAsync(id);
            _context.Gift.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftExists(int id)
        {
            return _context.Gift.Any(e => e.Id == id);
        }
    }
}
