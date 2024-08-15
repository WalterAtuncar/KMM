using Data.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication.AppCode
{
    public class Mail
    {
        public string EnviarCorreo(string correoReceptor = null, List<string> listaCorreoCopia = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        {
            string nombreEmisor = ConfigurationManager.AppSettings["nombreEmisor"];
            string correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
            string contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisor"];
            string host = ConfigurationManager.AppSettings["host"];

            using (var mail = new MailMessage())
            {
                mail.To.Add(correoReceptor); //Destinatario
                if (listaCorreoCopia != null)
                {
                    foreach (var item in listaCorreoCopia)
                    {
                        mail.CC.Add(item);//Copia
                    }
                }
                mail.From = new MailAddress(correoEmisor, nombreEmisor, System.Text.Encoding.UTF8);//Emisor y nombre de usuario
                mail.Subject = asunto;//Asunto del mensaje
                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;
                mail.Body = mensaje;

                if (Attachments != null)
                {
                    foreach (var ruta in Attachments)
                    {
                        Attachment oAttachmentTemp = new Attachment(ruta.Values.ToString());
                        oAttachmentTemp.ContentId = ruta.Keys.ToString();
                        mail.Attachments.Add(oAttachmentTemp);
                    }
                }

                if (ArchivosAdjuntos != null)
                {
                    foreach (var ruta in ArchivosAdjuntos)
                    {
                        mail.Attachments.Add(new Attachment(ruta));
                    }

                }

                using (var client = new SmtpClient())
                {
                    client.Host = host;
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(correoEmisor, contraseniaEmisor);

                    try
                    {
                        Utilitario.Logger();
                        Log.Information("Preparando envío de correo");
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        client.Send(mail);//Enviamos el mensaje
                        Log.Information("Correo enviado correctamente");
                        Log.CloseAndFlush();
                        return "Mensaje Enviado Correctamente";
                    }
                    catch (SmtpException ex)
                    {
                        var x = ex.ToString();
                        x = ex.Message.ToString();
                        Log.Error(ex, "Error");
                        Log.CloseAndFlush();
                        return ex.Message.ToString();
                    }
                    catch (Exception ex)
                    {
                        var x = ex.ToString();
                        x = ex.Message.ToString();
                        Log.Error(ex, "Error");
                        Log.CloseAndFlush();
                        return ex.Message.ToString();
                    }
                }
            }
        }
    }
}