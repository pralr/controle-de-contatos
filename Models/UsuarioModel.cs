using MeuSiteEmMVC.Enums;
using MeuSiteEmMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário.")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // aqui permitiu a criacao dos USUARIOS
        public virtual List<ContatoModel>? Contatos { get; set; }

        // nossa usuario model que representa nossa entidade usuario, a partir do momento que temos um relacionamento com o contato
        // ela passa a ter uma lista. ou seja, um usuario tem varios contatos (pq posso cadastrar n contatos vinculando com o usuario logado)
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
            // basicamente: o que ele digitou na tela, eu vou gerar o hash e comparar com o hash
            // que ja esta no banco de dados
            // se a senha do atributo senha for igual a senha informada pelo metodo, vai ser true
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

    }
}
