using MeuSiteEmMVC.Repositorio;
using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Filters;

namespace MeuSiteEmMVC.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        // avisa se de fato quer apagar a informacao
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        // funcao de realmente apagar
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar seu usuário.";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar seu contato. Mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
               if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar seu usuário. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuario); // ele vai cair pra view de editar e nao de alterar, pq ela nao existe

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos alterar seu usuário. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil,
                        DataAtualizacao = DateTime.Now
                    };
                    
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário foi alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario); 

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos alterar seu usuário. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }


    }
}
