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
    public class TipoRecursoController : Controller
    {
        private readonly AppDbContext _context;

        public TipoRecursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TipoRecurso
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposRecurso.ToListAsync());
        }

        // GET: TipoRecurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRecurso = await _context.TiposRecurso
                .FirstOrDefaultAsync(m => m.IdTipoR == id);
            if (tipoRecurso == null)
            {
                return NotFound();
            }

            return View(tipoRecurso);
        }

        // GET: TipoRecurso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoRecurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoR,Nombre,Descripcion")] TipoRecurso tipoRecurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoRecurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRecurso);
        }

        // GET: TipoRecurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRecurso = await _context.TiposRecurso.FindAsync(id);
            if (tipoRecurso == null)
            {
                return NotFound();
            }
            return View(tipoRecurso);
        }

        // POST: TipoRecurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoR,Nombre,Descripcion")] TipoRecurso tipoRecurso)
        {
            if (id != tipoRecurso.IdTipoR)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoRecurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoRecursoExists(tipoRecurso.IdTipoR))
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
            return View(tipoRecurso);
        }

        // GET: TipoRecurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRecurso = await _context.TiposRecurso
                .FirstOrDefaultAsync(m => m.IdTipoR == id);
            if (tipoRecurso == null)
            {
                return NotFound();
            }

            return View(tipoRecurso);
        }

        // POST: TipoRecurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoRecurso = await _context.TiposRecurso.FindAsync(id);
            if (tipoRecurso != null)
            {
                _context.TiposRecurso.Remove(tipoRecurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoRecursoExists(int id)
        {
            return _context.TiposRecurso.Any(e => e.IdTipoR == id);
        }
    }
}
