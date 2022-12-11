using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeuSiteEmMVC.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            { // se a sessao for nula ou vazia, ele vai chamar nosso context
                // vai ser um redirect para rota. qual rota
                // controller login e action index
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login"}, {"action", "Index"} } );
            } else
            {
                UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

// vamos herdar action filter atribute, que eh do asp net mvc
// on action execute, significa que antes dele executar qlq acao da controller, vai cair primeiro 
// nesse metodo execute. 

