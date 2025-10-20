using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMetropolis.Controllers
{
    // Integrantes del equipo (obligatorio según la guía)
    // [Agrega los demás nombres aquí]
    public class RecursosController : Controller
    {
        [HttpGet]
        public IActionResult Buscar()
        {
            // Aquí luego pondremos los filtros (autor, etiquetas, editorial, tipo, país, año)
            return View();
        }

        [HttpGet]
        public IActionResult PorTipo(int? tipoId, bool soloUltimos10Anios = true)
        {
            // Aquí mostraremos los materiales por tipo y antigüedad
            return View();
        }
    }
}
