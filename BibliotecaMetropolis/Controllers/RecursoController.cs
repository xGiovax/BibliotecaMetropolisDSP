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
    public class RecursoController : Controller
    {
        private readonly AppDbContext _context;

        public RecursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recurso
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Recursos.Include(r => r.Editorial).Include(r => r.Pais).Include(r => r.TipoRecurso);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Recurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .Include(r => r.Editorial)
                .Include(r => r.Pais)
                .Include(r => r.TipoRecurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // GET: Recurso/Create
        public IActionResult Create()
        {
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre");
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre");
            return View();
        }

        // POST: Recurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRec,Titulo,AnnoPublic,Edicion,PalabrasBusqueda,Descripcion,CantidadUnidades,PrecioIndividual,IdPais,IdTipoR,IdEdit")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre", recurso.IdTipoR);
            return View(recurso);
        }

        // GET: Recurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre", recurso.IdTipoR);
            return View(recurso);
        }

        // POST: Recurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRec,Titulo,AnnoPublic,Edicion,PalabrasBusqueda,Descripcion,CantidadUnidades,PrecioIndividual,IdPais,IdTipoR,IdEdit")] Recurso recurso)
        {
            if (id != recurso.IdRec)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursoExists(recurso.IdRec))
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
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre", recurso.IdTipoR);
            return View(recurso);
        }

        // GET: Recurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .Include(r => r.Editorial)
                .Include(r => r.Pais)
                .Include(r => r.TipoRecurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // POST: Recurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso != null)
            {
                _context.Recursos.Remove(recurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursoExists(int id)
        {
            return _context.Recursos.Any(e => e.IdRec == id);
        }

        // 🔹 Método auxiliar para cargar combos (País, Tipo, Editorial)
        private void CargarListasDesplegables(Recurso? recurso = null)
        {
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso?.IdPais);
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre", recurso?.IdTipoR);
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso?.IdEdit);
        }
    }
}
