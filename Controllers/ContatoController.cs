using MeuSiteEmMVC.Repositorio;
using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Filters;
using MeuSiteEmMVC.Helpers;

namespace MeuSiteEmMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;   
        }
        public IActionResult Index()
        {
            UsuarioModel usuarioLogado =_sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        // avisa se de fato quer apagar a informacao
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        // funcao de realmente apagar
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if(apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                } else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar seu contato.";
                }
                return RedirectToAction("Index");
            } catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar seu contato. Mais detalhes do erro: {erro.Message}" ;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Adicionar(contato);
                    
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(contato);
            } catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar seu cadastro. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Atualizar(contato);


                    TempData["MensagemSucesso"] = "Contado alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                    return View("Editar", contato); // ele vai cair pra view de editar e nao de alterar, pq ela nao existe

            } catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos alterar seu cadastro. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
          
        }


    }
}
