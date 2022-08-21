using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternalSystems.Models;

namespace InternalSystems.Controllers
{
    public class AccessController : Controller
    {
        private readonly AccessContext _context;

        public AccessController(AccessContext context)
        {
            _context = context;
        }

        // GET: Access
        public async Task<IActionResult> Index()
        {
              return _context.AccessModel != null ? 
                          View(await _context.AccessModel.ToListAsync()) :
                          Problem("Entity set 'AccessContext.AccessModel'  is null.");
        }

        // GET: Access/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccessModel == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessModel == null)
            {
                return NotFound();
            }

            return View(accessModel);
        }

        // GET: Access/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Access/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] AccessModel accessModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessModel);
        }

        // GET: Access/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccessModel == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel.FindAsync(id);
            if (accessModel == null)
            {
                return NotFound();
            }
            return View(accessModel);
        }

        // POST: Access/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] AccessModel accessModel)
        {
            if (id != accessModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessModelExists(accessModel.Id))
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
            return View(accessModel);
        }

        // GET: Access/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccessModel == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessModel == null)
            {
                return NotFound();
            }

            return View(accessModel);
        }

        // POST: Access/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccessModel == null)
            {
                return Problem("Entity set 'AccessContext.AccessModel'  is null.");
            }
            var accessModel = await _context.AccessModel.FindAsync(id);
            if (accessModel != null)
            {
                _context.AccessModel.Remove(accessModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessModelExists(int id)
        {
          return (_context.AccessModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
