using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpRon.Models;

namespace HelpRon.Controllers
{
    public class NeedsController : Controller
    {
        private readonly HelpRonContext _context;

        public NeedsController(HelpRonContext context)
        {
            _context = context;
        }

        // GET: Needs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Need.ToListAsync());
        }

        // GET: Needs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var need = await _context.Need
                .FirstOrDefaultAsync(m => m.Id == id);
            if (need == null)
            {
                return NotFound();
            }

            return View(need);
        }

        // GET: Needs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Needs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DueDate,Helper")] Need need)
        {
            if (ModelState.IsValid)
            {
                _context.Add(need);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(need);
        }

        // GET: Needs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var need = await _context.Need.FindAsync(id);
            if (need == null)
            {
                return NotFound();
            }
            return View(need);
        }

        // POST: Needs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DueDate,Helper")] Need need)
        {
            if (id != need.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(need);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NeedExists(need.Id))
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
            return View(need);
        }

        // GET: Needs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var need = await _context.Need
                .FirstOrDefaultAsync(m => m.Id == id);
            if (need == null)
            {
                return NotFound();
            }

            return View(need);
        }

        // POST: Needs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var need = await _context.Need.FindAsync(id);
            _context.Need.Remove(need);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NeedExists(int id)
        {
            return _context.Need.Any(e => e.Id == id);
        }
    }
}
