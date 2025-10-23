using System.Diagnostics;
using BibliotecaMetropolis.Models;
using Microsoft.AspNetCore.Mvc;

//INTEGRANTES DE EQUIPO
//Giovanni Alberto Ruano Martinez RM250065
//Christopher Steven Jovel Beltran JB251834
//William Aaron Peralta Cruz PC210574
//Emerson Amilcar Molina Sandoval MS250062
//Manuel Aaron Aleman Cruz AC250515

namespace BibliotecaMetropolis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
