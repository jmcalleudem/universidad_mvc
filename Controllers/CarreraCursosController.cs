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
    public class CarreraCursosController : Controller
    {
        private readonly universidad_mvcContext _context;

        public CarreraCursosController(universidad_mvcContext context)
        {
            _context = context;
        }

        // GET: CarreraCursos
        public async Task<IActionResult> Index()
        {
            var universidad_mvcContext = _context.CarreraCurso.Include(c => c.Carrera).Include(c => c.Curso);
            return View(await universidad_mvcContext.ToListAsync());
        }

        // GET: CarreraCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarreraCurso == null)
            {
                return NotFound();
            }

            var carreraCurso = await _context.CarreraCurso
                .Include(c => c.Carrera)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carreraCurso == null)
            {
                return NotFound();
            }

            return View(carreraCurso);
        }

        // GET: CarreraCursos/Create
        public IActionResult Create()
        {
            ViewData["CarreraId"] = new SelectList(_context.Carrera, "Id", "Id");
            ViewData["CursoId"] = new SelectList(_context.Set<Curso>(), "Id", "Id");
            return View();
        }

        // POST: CarreraCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Semestre,Creditos,CarreraId,CursoId")] CarreraCurso carreraCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carreraCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carrera, "Id", "Id", carreraCurso.CarreraId);
            ViewData["CursoId"] = new SelectList(_context.Set<Curso>(), "Id", "Id", carreraCurso.CursoId);
            return View(carreraCurso);
        }

        // GET: CarreraCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarreraCurso == null)
            {
                return NotFound();
            }

            var carreraCurso = await _context.CarreraCurso.FindAsync(id);
            if (carreraCurso == null)
            {
                return NotFound();
            }
            ViewData["CarreraId"] = new SelectList(_context.Carrera, "Id", "Id", carreraCurso.CarreraId);
            ViewData["CursoId"] = new SelectList(_context.Set<Curso>(), "Id", "Id", carreraCurso.CursoId);
            return View(carreraCurso);
        }

        // POST: CarreraCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Semestre,Creditos,CarreraId,CursoId")] CarreraCurso carreraCurso)
        {
            if (id != carreraCurso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carreraCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraCursoExists(carreraCurso.Id))
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
            ViewData["CarreraId"] = new SelectList(_context.Carrera, "Id", "Id", carreraCurso.CarreraId);
            ViewData["CursoId"] = new SelectList(_context.Set<Curso>(), "Id", "Id", carreraCurso.CursoId);
            return View(carreraCurso);
        }

        // GET: CarreraCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarreraCurso == null)
            {
                return NotFound();
            }

            var carreraCurso = await _context.CarreraCurso
                .Include(c => c.Carrera)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carreraCurso == null)
            {
                return NotFound();
            }

            return View(carreraCurso);
        }

        // POST: CarreraCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarreraCurso == null)
            {
                return Problem("Entity set 'universidad_mvcContext.CarreraCurso'  is null.");
            }
            var carreraCurso = await _context.CarreraCurso.FindAsync(id);
            if (carreraCurso != null)
            {
                _context.CarreraCurso.Remove(carreraCurso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreraCursoExists(int id)
        {
          return (_context.CarreraCurso?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
