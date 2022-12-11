using Microsoft.Web.Services3.Addressing;
using System.Net;
using System.Net.Mail;

namespace MeuSiteEmMVC.Helpers
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        { // smtp eh nossa chave principal e dentro do objeto temos a nossa outra chave, host
            // ele traz a info que ta dentro de host
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage(username, nome);

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true; // podemos passar codigo html para dentro do email e nao vai ter problema
                mail.Priority = MailPriority.High; // email de prioridade, entao manda o mais rapido possivel

                // agora fazer de fato o envio do email

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true; // vai ser um envio de email seguro

                    smtp.Send(mail); // enviar
                    return true;
                }

            } catch(System.Exception ex)
            {
                // Gravar log de erro ao enviar e-mail 
                return false;
            }
        }
    }
}
