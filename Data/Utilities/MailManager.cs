namespace Portail_OptiVille.Data.Utilities
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using System.Net;
    using System.Net.Mail;

    public class MailManager
    {
        private readonly DefaultMail _defaultMail;

        // Constructeur avec injection des paramètres de configuration
        public MailManager(IOptions<DefaultMail> defaultMail)
        {
            _defaultMail = defaultMail.Value;

        }
        // public void SendMail(string _destinataire, string _objet, object _contenuMail)
        // {
        //     // Envoie de mail
        //     MailMessage mail = new MailMessage();
        //     //mail.From = new MailAddress(_defaultMail.MailAddress);
        //     mail.From = new MailAddress("kevenmichel@outlook.fr");
        //     mail.To.Add(_destinataire);
        //     mail.Subject = _objet;
        //     mail.Body = _contenuMail.ToString();
        //     //mail.IsBodyHtml = true;
        //         Console.WriteLine(_defaultMail.CredMail);
        //         Console.WriteLine(_defaultMail.CredPassApp);

        //     var client = new SmtpClient(_defaultMail.SmtpAddr, _defaultMail.SmtpPort)
        //     {
        //         //Credentials = new System.Net.NetworkCredential(_defaultMail.CredMail, _defaultMail.CredPassApp),
        //         Credentials = new System.Net.NetworkCredential("kevenmichel@outlook.fr", "kchkuskpukeajhjf"),
        //         EnableSsl = true
        //         //EnableSsl = _sslEnable ?? _defaultMail.EnableSsl
        //     };
        //     client.Send(mail);
        // }
            public void SendMail(string _destinataire, string _objet, string _contenuMail)
                {
                    using (var client = new SmtpClient("smtp-mail.outlook.com", 587)) // Serveur SMTP d'Outlook
                    {
                        client.Credentials = new NetworkCredential("kevenmichel@outlook.fr", "kchkuskpukeajhjf"); // Remplace par ton email et mot de passe
                        client.EnableSsl = true; // Active le chiffrement SSL pour STARTTLS
                        
                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("kevenmichel@outlook.fr"),
                            Subject = _objet,
                            Body = _contenuMail,
                            IsBodyHtml = true // Si le contenu est en HTML
                        };
                        mailMessage.To.Add(_destinataire);

                        client.Send(mailMessage); // Envoie le mail
                    }
                }
        public class DefaultMail
        {
            public string MailAddress { get; set; }
            public string CredMail { get; set; }
            public string CredPassApp { get; set; }
            public string SmtpAddr { get; set; }
            public int SmtpPort { get; set; }
            public bool EnableSsl { get; set; }
        }
    }
}
