using MeuSiteEmMVC.Models;

namespace MeuSiteEmMVC.Helpers
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
        UsuarioModel BuscarSessaoDoUsuario();
    }
}
