using MeuSiteEmMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
