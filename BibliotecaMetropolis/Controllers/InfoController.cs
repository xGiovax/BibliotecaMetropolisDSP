using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMetropolis.Controllers
{
    // Este es el nuevo controlador dedicado a páginas de información
    public class InfoController : Controller
    {
        // GET: /Info/SobreNosotros
        public IActionResult SobreNosotros()
        {
            return View();
        }

        // --- (Opcional) ---
        // Si quisieras agregar una página de "Contacto" o "Privacidad",
        // las agregarías aquí también. Por ejemplo:
        // public IActionResult Contacto()
        // {
        //     return View();
        // }
    }
}