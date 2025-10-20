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

        public async Task<IActionResult> Index(
            string searchTitulo, int? idTipoR, int? idPais, int? idEdit, bool? ultimos10)
        {
            // 🔹 Cargar combos de filtros
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre");
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre");
            ViewData["Ultimos10"] = ultimos10;

            // 🔹 Consulta base
            var recursos = _context.Recursos
                .Include(r => r.Editorial)
                .Include(r => r.Pais)
                .Include(r => r.TipoRecurso)
                .Include(r => r.AutoresRecursos)
                    .ThenInclude(ar => ar.Autor)
                .AsQueryable();

            // 🔹 Filtro por texto (título o autor)
            if (!string.IsNullOrEmpty(searchTitulo))
            {
                recursos = recursos.Where(r =>
                    r.Titulo.Contains(searchTitulo) ||
                    r.AutoresRecursos.Any(a =>
                        a.Autor.Nombres.Contains(searchTitulo) ||
                        a.Autor.Apellidos.Contains(searchTitulo))
                );
            }

            // 🔹 Filtros adicionales
            if (idTipoR.HasValue)
                recursos = recursos.Where(r => r.IdTipoR == idTipoR.Value);

            if (idPais.HasValue)
                recursos = recursos.Where(r => r.IdPais == idPais.Value);

            if (idEdit.HasValue)
                recursos = recursos.Where(r => r.IdEdit == idEdit.Value);

            // 🔹 Filtro por antigüedad (últimos 10 años)
            if (ultimos10 == true)
            {
                int anioLimite = DateTime.Now.Year - 10;
                recursos = recursos.Where(r => r.AnnoPublic >= anioLimite);
            }

            return View(await recursos.ToListAsync());
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
            // 🔹 Cargar lista de autores disponibles
            ViewData["Autores"] = new MultiSelectList(_context.Autores, "IdAutor", "Nombres");
            return View();
        }

        // POST: Recurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("IdRec,Titulo,AnnoPublic,Edicion,PalabrasBusqueda,Descripcion,CantidadUnidades,PrecioIndividual,IdPais,IdTipoR,IdEdit")]
    Recurso recurso,
    int[] autoresSeleccionados,
    int? autorPrincipal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recurso);
                await _context.SaveChangesAsync();

                // 🔹 Relacionar autores seleccionados
                if (autoresSeleccionados != null && autoresSeleccionados.Any())
                {
                    foreach (var idAutor in autoresSeleccionados)
                    {
                        var rel = new AutoresRecursos
                        {
                            IdAutor = idAutor,
                            IdRec = recurso.IdRec,
                            EsPrincipal = (autorPrincipal.HasValue && autorPrincipal.Value == idAutor)
                        };
                        _context.AutoresRecursos.Add(rel);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // 🔹 Si falla el guardado, recargamos listas y devolvemos la vista
            ViewData["IdEdit"] = new SelectList(_context.Editoriales, "IdEdit", "Nombre", recurso.IdEdit);
            ViewData["IdPais"] = new SelectList(_context.Paises, "IdPais", "Nombre", recurso.IdPais);
            ViewData["IdTipoR"] = new SelectList(_context.TiposRecurso, "IdTipoR", "Nombre", recurso.IdTipoR);
            ViewData["Autores"] = new MultiSelectList(_context.Autores, "IdAutor", "Nombres");

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
