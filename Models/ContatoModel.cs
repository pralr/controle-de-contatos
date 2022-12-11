using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class ContatoModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "Email não é válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o celular do contato")]
        [StringLength(11)]
        [Phone(ErrorMessage = "Número de celular não é válido.")]
        public string Celular { get; set; }
 
        // nullable pq ja temos registros no banco de dados e se n colocar assim, ele vai pedir para deletar os registros

        // na tabela contato, criamos uma coluna usuario id, ou seja, um contato eh amarrado a um usuario
        public int UsuarioId { get; set; }

        // agora permite o cadastro e alteracao dos CONTATOS
        public UsuarioModel? Usuario { get; set; }
    }
}
