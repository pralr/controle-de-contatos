using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeuSiteEmMVC.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario); 
            
            return View(usuario);

            // ja tenho o http context aqui dentro nessa view component
        }
    }
}
