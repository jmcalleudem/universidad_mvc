using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using universidad_mvc.Data;
using universidad_mvc.Models;

namespace universidad_mvc.Controllers
{
    public class UniversidadesController : Controller
    {
        private readonly universidad_mvcContext _context;

        public UniversidadesController(universidad_mvcContext context)
        {
            _context = context;
        }

        // GET: Universidades
        public async Task<IActionResult> Index()
        {
              return _context.Universidad != null ? 
                          View(await _context.Universidad.ToListAsync()) :
                          Problem("Entity set 'universidad_mvcContext.Universidad'  is null.");
        }

        // GET: Universidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Universidad == null)
            {
                return NotFound();
            }

            var universidad = await _context.Universidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universidad == null)
            {
                return NotFound();
            }

            return View(universidad);
        }

        // GET: Universidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Universidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Universidad universidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universidad);
        }

        // GET: Universidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Universidad == null)
            {
                return NotFound();
            }

            var universidad = await _context.Universidad.FindAsync(id);
            if (universidad == null)
            {
                return NotFound();
            }
            return View(universidad);
        }

        // POST: Universidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Universidad universidad)
        {
            if (id != universidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversidadExists(universidad.Id))
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
            return View(universidad);
        }

        // GET: Universidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Universidad == null)
            {
                return NotFound();
            }

            var universidad = await _context.Universidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universidad == null)
            {
                return NotFound();
            }

            return View(universidad);
        }

        // POST: Universidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Universidad == null)
            {
                return Problem("Entity set 'universidad_mvcContext.Universidad'  is null.");
            }
            var universidad = await _context.Universidad.FindAsync(id);
            if (universidad != null)
            {
                _context.Universidad.Remove(universidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversidadExists(int id)
        {
          return (_context.Universidad?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
