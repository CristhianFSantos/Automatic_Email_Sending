using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Email
{
    class Program
    {
        static void Main(string[] args)
        {
            // PARAMÊTROS
            string host         = "smtp.outlook.com";
            string emailFrom    = "seuEmail@outlook.com";
            string subject      = "E-mail de envio automático com aplicação C# .NET";
            string attachments  = @"C:\Users\Desktop\ArquivoDeTeste.pdf";

            string bodyHTML = @"
                                <p style=""font-size: 14px;color: black; font-family:Calibri;"">
                                Prezados, bom dia!
                                <br />
                                <br />
                                Segue e-mail de teste encaminhado por aplicação .NET
                                </p>




                                <p style=""font-size: 14px;color: black; font-family:Helvetica;"">
                                Atenciosamente,
                                </p>
                                <p style=""font-size: 15px;color: #81A400;  font-family:Helvetica;"">
                                <strong>Cristhian Santos</strong>
                                </p>
                                <p style=""font-size: 14px;color: black; font-family:Helvetica;"">
                                Cargo | email
                                </p>";

            
            // SENHA
            Console.WriteLine($"Digite a senha de: {emailFrom}");
            string password = Console.ReadLine();
            Console.Clear();

            // VALIDANDO EMAIL
            string emailToOne = "emailParaEnviar@gmail.com";
            string emailToTwo = "emailParaEnviar@hotmail.com";

            // DEFININDO CONFIGURAÇÕES DO CLIENT
            SmtpClient client = new SmtpClient();

            client.Host = host;
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;


            // DEFININDO CREDENCIAIS
            NetworkCredential credential = new NetworkCredential();


            credential.UserName = emailFrom;
            credential.Password = password;

            client.Credentials = credential;


            // DEFININDO PARAMÊTROS DO EMAIL
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom);
            message.Subject = subject;



            message.IsBodyHtml = true;
            message.Body = bodyHTML;
            // message.Body = body;  (Mensagem simples)
            message.Attachments.Add(new Attachment(attachments));
            message.To.Add(emailToOne);
            message.To.Add(emailToTwo);

            //COPIA OCULTA
            message.Bcc.Add("emailParaEnviarCopiaOculta@gmail.com");
            message.CC.Add("emailParaEnviarCopia@gmail.com");

            // DEFININDO IMAGEM, NO CORPO DO EMAIL
            var contentID = "Image";
            var inlineLogo = new Attachment(@"C:\Users\Documents\ass.png");
            inlineLogo.ContentId = contentID;
            inlineLogo.ContentDisposition.Inline = true;
            inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            message.Attachments.Add(inlineLogo);
            message.Body += "<img src=\"cid:" + contentID + "\"><br />";

            message.Body += @" <p style=""font-size: 10px;color: gray;"">
                                <strong> Confidencialidade:</strong> A informação contida nesta mensagem de e-mail, incluindo quaisquer anexos, é confidencial e está reservada apenas à pessoa ou entidade para a qual
                                foi endereçada.Se você não é o destinatário ou a pessoa responsável por encaminhar esta mensagem ao destinatário, você está, por meio desta, notificado que não deverá
                                rever, retransmitir, imprimir, copiar, usar ou distribuir esta mensagem de e - mail ou quaisquer anexos.Caso você tenha recebido esta mensagem por engano, por favor, 
                                contate o remetente imediatamente e apague esta mensagem de seu computador ou de qualquer outro banco de dados. Muito obrigado.
                                </p> ";

            // ENVIANDOO EMAIL
            try
            {
                client.Send(message);
                Console.WriteLine("Email enviado");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

    }
}


