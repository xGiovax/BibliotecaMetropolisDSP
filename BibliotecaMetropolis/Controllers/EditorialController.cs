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
    public class EditorialController : Controller
    {
        private readonly AppDbContext _context;

        public EditorialController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Editorial
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editoriales.ToListAsync());
        }

        // GET: Editorial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.IdEdit == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // GET: Editorial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editorial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEdit,Nombre,Descripcion")] Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editorial);
        }

        // GET: Editorial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return View(editorial);
        }

        // POST: Editorial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEdit,Nombre,Descripcion")] Editorial editorial)
        {
            if (id != editorial.IdEdit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialExists(editorial.IdEdit))
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
            return View(editorial);
        }

        // GET: Editorial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.IdEdit == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // POST: Editorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial != null)
            {
                _context.Editoriales.Remove(editorial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorialExists(int id)
        {
            return _context.Editoriales.Any(e => e.IdEdit == id);
        }
    }
}
