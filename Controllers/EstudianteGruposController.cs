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
    public class EstudianteGruposController : Controller
    {
        private readonly universidad_mvcContext _context;

        public EstudianteGruposController(universidad_mvcContext context)
        {
            _context = context;
        }

        // GET: EstudianteGrupos
        public async Task<IActionResult> Index()
        {
            var universidad_mvcContext = _context.EstudianteGrupo.Include(e => e.Estudiante).Include(e => e.Grupo);
            return View(await universidad_mvcContext.ToListAsync());
        }

        // GET: EstudianteGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstudianteGrupo == null)
            {
                return NotFound();
            }

            var estudianteGrupo = await _context.EstudianteGrupo
                .Include(e => e.Estudiante)
                .Include(e => e.Grupo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteGrupo == null)
            {
                return NotFound();
            }

            return View(estudianteGrupo);
        }

        // GET: EstudianteGrupos/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Id");
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Id");
            return View();
        }

        // POST: EstudianteGrupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nota,EstudianteId,GrupoId")] EstudianteGrupo estudianteGrupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Id", estudianteGrupo.EstudianteId);
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Id", estudianteGrupo.GrupoId);
            return View(estudianteGrupo);
        }

        // GET: EstudianteGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstudianteGrupo == null)
            {
                return NotFound();
            }

            var estudianteGrupo = await _context.EstudianteGrupo.FindAsync(id);
            if (estudianteGrupo == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Id", estudianteGrupo.EstudianteId);
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Id", estudianteGrupo.GrupoId);
            return View(estudianteGrupo);
        }

        // POST: EstudianteGrupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nota,EstudianteId,GrupoId")] EstudianteGrupo estudianteGrupo)
        {
            if (id != estudianteGrupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteGrupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteGrupoExists(estudianteGrupo.Id))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Id", estudianteGrupo.EstudianteId);
            ViewData["GrupoId"] = new SelectList(_context.Grupo, "Id", "Id", estudianteGrupo.GrupoId);
            return View(estudianteGrupo);
        }

        // GET: EstudianteGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstudianteGrupo == null)
            {
                return NotFound();
            }

            var estudianteGrupo = await _context.EstudianteGrupo
                .Include(e => e.Estudiante)
                .Include(e => e.Grupo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteGrupo == null)
            {
                return NotFound();
            }

            return View(estudianteGrupo);
        }

        // POST: EstudianteGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstudianteGrupo == null)
            {
                return Problem("Entity set 'universidad_mvcContext.EstudianteGrupo'  is null.");
            }
            var estudianteGrupo = await _context.EstudianteGrupo.FindAsync(id);
            if (estudianteGrupo != null)
            {
                _context.EstudianteGrupo.Remove(estudianteGrupo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteGrupoExists(int id)
        {
          return (_context.EstudianteGrupo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
