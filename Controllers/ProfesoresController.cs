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
    public class ProfesoresController : Controller
    {
        private readonly universidad_mvcContext _context;

        public ProfesoresController(universidad_mvcContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            var universidad_mvcContext = _context.Profesor.Include(p => p.Facultad);
            return View(await universidad_mvcContext.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profesor == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
                .Include(p => p.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            ViewData["FacultadId"] = new SelectList(_context.Facultad, "Id", "Id");
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Nivel,FacultadId")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultadId"] = new SelectList(_context.Facultad, "Id", "Id", profesor.FacultadId);
            return View(profesor);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profesor == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            ViewData["FacultadId"] = new SelectList(_context.Facultad, "Id", "Id", profesor.FacultadId);
            return View(profesor);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Nivel,FacultadId")] Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["FacultadId"] = new SelectList(_context.Facultad, "Id", "Id", profesor.FacultadId);
            return View(profesor);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profesor == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
                .Include(p => p.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profesor == null)
            {
                return Problem("Entity set 'universidad_mvcContext.Profesor'  is null.");
            }
            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor != null)
            {
                _context.Profesor.Remove(profesor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
          return (_context.Profesor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
