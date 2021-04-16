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
    public class GiftSendersController : Controller
    {
        private readonly Online_Gift_Delivery_DBContext _context;

        public GiftSendersController(Online_Gift_Delivery_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: GiftSenders
        public async Task<IActionResult> Index()
        {
            return View(await _context.GiftSender.ToListAsync());
        }
        [Authorize]
        // GET: GiftSenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftSender = await _context.GiftSender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftSender == null)
            {
                return NotFound();
            }

            return View(giftSender);
        }
        [Authorize]
        // GET: GiftSenders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiftSenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderName,MobileNumber")] GiftSender giftSender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giftSender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giftSender);
        }
        [Authorize]
        // GET: GiftSenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftSender = await _context.GiftSender.FindAsync(id);
            if (giftSender == null)
            {
                return NotFound();
            }
            return View(giftSender);
        }

        // POST: GiftSenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderName,MobileNumber")] GiftSender giftSender)
        {
            if (id != giftSender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giftSender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftSenderExists(giftSender.Id))
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
            return View(giftSender);
        }
        [Authorize]
        // GET: GiftSenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giftSender = await _context.GiftSender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giftSender == null)
            {
                return NotFound();
            }

            return View(giftSender);
        }

        // POST: GiftSenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giftSender = await _context.GiftSender.FindAsync(id);
            _context.GiftSender.Remove(giftSender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftSenderExists(int id)
        {
            return _context.GiftSender.Any(e => e.Id == id);
        }
    }
}
