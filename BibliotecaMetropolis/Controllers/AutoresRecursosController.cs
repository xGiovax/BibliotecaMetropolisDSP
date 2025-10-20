using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMetropolis.Data;
using BibliotecaMetropolis.Models;

namespace BibliotecaMetropolis.Controllers
{
    public class AutoresRecursosController : Controller
    {
        private readonly AppDbContext _context;

        public AutoresRecursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AutoresRecursos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AutoresRecursos.Include(a => a.Autor).Include(a => a.Recurso);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AutoresRecursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoresRecursos = await _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);
            if (autoresRecursos == null)
            {
                return NotFound();
            }

            return View(autoresRecursos);
        }

        // GET: AutoresRecursos/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Apellidos");
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo");
            return View();
        }

        // POST: AutoresRecursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRec,IdAutor,EsPrincipal")] AutoresRecursos autoresRecursos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoresRecursos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Apellidos", autoresRecursos.IdAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecursos.IdRec);
            return View(autoresRecursos);
        }

        // GET: AutoresRecursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoresRecursos = await _context.AutoresRecursos.FindAsync(id);
            if (autoresRecursos == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Apellidos", autoresRecursos.IdAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecursos.IdRec);
            return View(autoresRecursos);
        }

        // POST: AutoresRecursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRec,IdAutor,EsPrincipal")] AutoresRecursos autoresRecursos)
        {
            if (id != autoresRecursos.IdRec)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoresRecursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresRecursosExists(autoresRecursos.IdRec))
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
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Apellidos", autoresRecursos.IdAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecursos.IdRec);
            return View(autoresRecursos);
        }

        // GET: AutoresRecursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoresRecursos = await _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);
            if (autoresRecursos == null)
            {
                return NotFound();
            }

            return View(autoresRecursos);
        }

        // POST: AutoresRecursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autoresRecursos = await _context.AutoresRecursos.FindAsync(id);
            if (autoresRecursos != null)
            {
                _context.AutoresRecursos.Remove(autoresRecursos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoresRecursosExists(int id)
        {
            return _context.AutoresRecursos.Any(e => e.IdRec == id);
        }
    }
}
