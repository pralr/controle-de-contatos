using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeuSiteEmMVC.Filters
{
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            // alem de estar logado, verifica se ele tem o perfil admin. se for diferente, 
            // joga para uma pagina restrita falando que o usuario nao tem acesso

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(usuario.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

// vamos herdar action filter atribute, que eh do asp net mvc
// on action execute, significa que antes dele executar qlq acao da controller, vai cair primeiro 
// nesse metodo execute. 

