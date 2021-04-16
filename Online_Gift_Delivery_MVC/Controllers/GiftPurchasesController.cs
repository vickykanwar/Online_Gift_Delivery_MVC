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
    public class GiftPurchasesController : Controller
    {
        private readonly Online_Gift_Delivery_DBContext _context;

        public GiftPurchasesController(Online_Gift_Delivery_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: GiftPurchases
        public async Task<IActionResult> Index()
        {
            var online_Gift_Delivery_DBContext = _context.GiftPurchase.Include(g => g.Gift).Include(g => g.GiftSender).Include(g => g.ShippingMethod);
            return View(await online_Gift_Delivery_DBContext.ToListAsync());
        }
        [Authorize]
        // GET: GiftPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftPurchase = await _context.GiftPurchase
                .Include(g => g.Gift)
                .Include(g => g.GiftSender)
                .Include(g => g.ShippingMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftPurchase == null)
            {
                return NotFound();
            }

            return View(giftPurchase);
        }
        [Authorize]
        // GET: GiftPurchases/Create
        public IActionResult Create()
        {
            ViewData["GiftId"] = new SelectList(_context.Gift, "Id", "Id");
            ViewData["GiftSenderId"] = new SelectList(_context.Set<GiftSender>(), "Id", "Id");
            ViewData["ShippingMethodId"] = new SelectList(_context.Set<ShippingMethod>(), "Id", "Id");
            return View();
        }

        // POST: GiftPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GiftSenderId,GiftId,ShippingMethodId,Reciever,RecieverAddress")] GiftPurchase giftPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giftPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftId"] = new SelectList(_context.Gift, "Id", "Id", giftPurchase.GiftId);
            ViewData["GiftSenderId"] = new SelectList(_context.Set<GiftSender>(), "Id", "Id", giftPurchase.GiftSenderId);
            ViewData["ShippingMethodId"] = new SelectList(_context.Set<ShippingMethod>(), "Id", "Id", giftPurchase.ShippingMethodId);
            return View(giftPurchase);
        }
        [Authorize]
        // GET: GiftPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftPurchase = await _context.GiftPurchase.FindAsync(id);
            if (giftPurchase == null)
            {
                return NotFound();
            }
            ViewData["GiftId"] = new SelectList(_context.Gift, "Id", "Id", giftPurchase.GiftId);
            ViewData["GiftSenderId"] = new SelectList(_context.Set<GiftSender>(), "Id", "Id", giftPurchase.GiftSenderId);
            ViewData["ShippingMethodId"] = new SelectList(_context.Set<ShippingMethod>(), "Id", "Id", giftPurchase.ShippingMethodId);
            return View(giftPurchase);
        }

        // POST: GiftPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiftSenderId,GiftId,ShippingMethodId,Reciever,RecieverAddress")] GiftPurchase giftPurchase)
        {
            if (id != giftPurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giftPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftPurchaseExists(giftPurchase.Id))
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
            ViewData["GiftId"] = new SelectList(_context.Gift, "Id", "Id", giftPurchase.GiftId);
            ViewData["GiftSenderId"] = new SelectList(_context.Set<GiftSender>(), "Id", "Id", giftPurchase.GiftSenderId);
            ViewData["ShippingMethodId"] = new SelectList(_context.Set<ShippingMethod>(), "Id", "Id", giftPurchase.ShippingMethodId);
            return View(giftPurchase);
        }
        [Authorize]
        // GET: GiftPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftPurchase = await _context.GiftPurchase
                .Include(g => g.Gift)
                .Include(g => g.GiftSender)
                .Include(g => g.ShippingMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftPurchase == null)
            {
                return NotFound();
            }

            return View(giftPurchase);
        }

        // POST: GiftPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giftPurchase = await _context.GiftPurchase.FindAsync(id);
            _context.GiftPurchase.Remove(giftPurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftPurchaseExists(int id)
        {
            return _context.GiftPurchase.Any(e => e.Id == id);
        }
    }
}
