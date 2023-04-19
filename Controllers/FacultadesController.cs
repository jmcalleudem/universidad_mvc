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
    public class FacultadesController : Controller
    {
        private readonly universidad_mvcContext _context;

        public FacultadesController(universidad_mvcContext context)
        {
            _context = context;
        }

        // GET: Facultades
        public async Task<IActionResult> Index()
        {
            var universidad_mvcContext = _context.Facultad.Include(f => f.Universidad);
            return View(await universidad_mvcContext.ToListAsync());
        }

        // GET: Facultades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facultad == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad
                .Include(f => f.Universidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // GET: Facultades/Create
        public IActionResult Create()
        {
            ViewData["UniversidadId"] = new SelectList(_context.Set<Universidad>(), "Id", "Id");
            return View();
        }

        // POST: Facultades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,UniversidadId")] Facultad facultad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UniversidadId"] = new SelectList(_context.Set<Universidad>(), "Id", "Id", facultad.UniversidadId);
            return View(facultad);
        }

        // GET: Facultades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facultad == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }
            ViewData["UniversidadId"] = new SelectList(_context.Set<Universidad>(), "Id", "Id", facultad.UniversidadId);
            return View(facultad);
        }

        // POST: Facultades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,UniversidadId")] Facultad facultad)
        {
            if (id != facultad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultadExists(facultad.Id))
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
            ViewData["UniversidadId"] = new SelectList(_context.Set<Universidad>(), "Id", "Id", facultad.UniversidadId);
            return View(facultad);
        }

        // GET: Facultades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facultad == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad
                .Include(f => f.Universidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // POST: Facultades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facultad == null)
            {
                return Problem("Entity set 'universidad_mvcContext.Facultad'  is null.");
            }
            var facultad = await _context.Facultad.FindAsync(id);
            if (facultad != null)
            {
                _context.Facultad.Remove(facultad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
          return (_context.Facultad?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
