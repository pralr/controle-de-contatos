using MeuSiteEmMVC.Helpers;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            // se o usuario estiver logado, redirecionar para a home 

            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            // se nao vai pra view normal
            return View();
        }

        // rota
        public IActionResult RedefinirSenha()
        {

            return View();
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha inválida.";
                        // temos que passar a action e a controller
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s)";

                }
                return View("Index"); // vai pra view index de login

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível realizar o login, pois: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        _usuarioRepositorio.Atualizar(usuario);
                        string mensagem = $"Sua nova senha é: {novaSenha}";


                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova Senha", 
                            mensagem);

                        if(emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        } else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar a senha para o email," +
                                $"por favor, tente novamente.";

                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha, verifique os dados informados.";

                }
                return View("Index"); // vai pra view index de login

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível redefinir sua senha, pois: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}


    
