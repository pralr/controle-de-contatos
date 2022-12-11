using MeuSiteEmMVC.Filters;
using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MeuSiteEmMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeModel home = new HomeModel();

            home.Nome = "Priscylla";
            home.Email = "pdar@ic.ufal.br";

            return View(home);
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