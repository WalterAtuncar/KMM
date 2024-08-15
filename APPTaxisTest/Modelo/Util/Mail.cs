﻿using Common.Util;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Modelo.General;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Modelo.Util
{
    public class Mail
    {
        //public async Task<OperationService> EnviarCorreo(string compania,string correoResponsable = null, string correoUsuarioRegistro = null, List<string> listaAprobador = null, List<Modelo.General.Usuario> listaSoporteAdministrativo = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        //{
        //    string nombreEmisor = ConfigurationManager.AppSettings["nombreEmisor"];
        //    string correoEmisor;
        //    string contraseniaEmisor;

        //    if (compania.Contains("Amazonía"))
        //    {
        //        correoEmisor = ConfigurationManager.AppSettings["correoEmisorKMA"];
        //        contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
        //    }
        //    else
        //    {
        //        correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
        //        contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisor"];
        //    }
        //    string host = ConfigurationManager.AppSettings["host"];

        //    using (var mail = new MailMessage())
        //    {
        //        if(!string.IsNullOrEmpty(correoResponsable))
        //        {
        //            mail.To.Add(correoResponsable); //Destinatario
        //        }

        //        if(!string.IsNullOrEmpty(correoUsuarioRegistro))
        //        {
        //            mail.To.Add(correoUsuarioRegistro); //Destinatario
        //        }

        //        if (listaAprobador != null)
        //        {
        //            foreach (var item in listaAprobador)
        //            {
        //                if (!string.IsNullOrEmpty(item))
        //                {
        //                    mail.CC.Add(item);//Copia
        //                }
        //            }
        //        }
        //        if (listaSoporteAdministrativo != null)
        //        {
        //            foreach (var item in listaSoporteAdministrativo)
        //            {
        //                mail.CC.Add(item.Email);//Copia
        //            }
        //        }
        //        mail.From = new MailAddress(correoEmisor, nombreEmisor);//Emisor y nombre de usuario
        //        mail.Subject = asunto;//Asunto del mensaje
        //        mail.SubjectEncoding = System.Text.Encoding.UTF8;

        //        var oAttachment = new Attachment(Utilitario.ServerPath(ConfigurationManager.AppSettings["urlImg"]));
        //        string contentID = "img001";
        //        oAttachment.ContentId = contentID;

        //        mail.Attachments.Add(oAttachment);

        //        mail.Body = mensaje;
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        mail.IsBodyHtml = true;

        //        if (Attachments != null)
        //        {
        //            foreach (var ruta in Attachments)
        //            {
        //                Attachment oAttachmentTemp = new Attachment(ruta.Values.ToString());
        //                oAttachmentTemp.ContentId = ruta.Keys.ToString();
        //                mail.Attachments.Add(oAttachmentTemp);
        //            }
        //        }

        //        if (ArchivosAdjuntos != null)
        //        {
        //            foreach (var ruta in ArchivosAdjuntos)
        //            {
        //                mail.Attachments.Add(new Attachment(ruta));
        //            }

        //        }
        //        using (var client = new SmtpClient())
        //        {
        //            client.Host = host;
        //            client.Port = 587;
        //            client.EnableSsl = true;
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            client.DeliveryFormat = SmtpDeliveryFormat.International;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(correoEmisor, contraseniaEmisor);

        //            try
        //            {
        //                Utilitario.Logger();
        //                Log.Information("Preparando envío de correo");
        //                Utilitario.WriteInfoLog("Preparando envío de correo");
        //                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
        //                {
        //                    return true;
        //                };
        //                await client.SendMailAsync(mail);//Enviamos el mensaje
        //                Log.Information("Correo enviado correctamente");
        //                Utilitario.WriteInfoLog("Correo enviado correctamente");
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = true, Message = "Mensaje Enviado Correctamente" };
        //            }
        //            catch (SmtpException ex)
        //            {
        //                var x = ex.ToString();
        //                x = ex.Message.ToString();
        //                Utilitario.WriteErrorLog(ex.ToString(), "Error");
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = false, Message = ex.Message.ToString() };
        //            }
        //            catch(Exception ex)
        //            {
        //                Utilitario.WriteErrorLog(ex.ToString(), "Error");
        //                var x = ex.ToString();
        //                x = ex.Message.ToString();
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = false, Message = ex.Message.ToString() };
        //            }
        //        }
        //    }
        //}
        public async Task<OperationService> EnviarCorreo(string compania, string correoResponsable = null, string correoUsuarioRegistro = null, List<string> listaAprobador = null, List<Modelo.General.Usuario> listaSoporteAdministrativo = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        {
            try
            {
                string nombreEmisor = ConfigurationManager.AppSettings["nombreEmisor"];
                string correoEmisor;
                string contraseniaEmisor;

                if (compania.Contains("Amazonía"))
                {
                    correoEmisor = ConfigurationManager.AppSettings["correoEmisorKMA"];
                    contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
                }
                else
                {
                    correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
                    contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisor"];
                }

                string host = ConfigurationManager.AppSettings["host"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(nombreEmisor, correoEmisor));

                if (!string.IsNullOrEmpty(correoResponsable))
                    message.To.Add(MailboxAddress.Parse(correoResponsable));

                if (!string.IsNullOrEmpty(correoUsuarioRegistro))
                    message.To.Add(MailboxAddress.Parse(correoUsuarioRegistro));

                if (listaAprobador != null)
                {
                    foreach (var item in listaAprobador.Where(item => !string.IsNullOrEmpty(item)))
                    {
                        message.Cc.Add(MailboxAddress.Parse(item));
                    }
                }

                if (listaSoporteAdministrativo != null)
                {
                    foreach (var item in listaSoporteAdministrativo.Where(item => !string.IsNullOrEmpty(item.Email)))
                    {
                        message.Cc.Add(MailboxAddress.Parse(item.Email));
                    }
                }

                message.Subject = asunto;

                var multipart = new Multipart("mixed");

                // Adjunto para la imagen
                var oAttachment = new MimePart("image", "png")
                {
                    Content = new MimeContent(File.OpenRead(Utilitario.ServerPath(ConfigurationManager.AppSettings["urlImg"]))),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                    ContentTransferEncoding = ContentEncoding.Base64,
                };

                string contentID = "img001";
                oAttachment.ContentId = contentID;
                multipart.Add(oAttachment);

                // Cuerpo del mensaje HTML
                multipart.Add(new TextPart("html")
                {
                    Text = mensaje,
                });

                // Adjuntos adicionales
                if (Attachments != null)
                {
                    foreach (var ruta in Attachments)
                    {
                        var attachment = new MimePart("application", "octet-stream")
                        {
                            Content = new MimeContent(File.OpenRead(ruta.Values.ToString())),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = ruta.Keys.ToString(),
                        };

                        multipart.Add(attachment);
                    }
                }

                if (ArchivosAdjuntos != null)
                {
                    foreach (var ruta in ArchivosAdjuntos)
                    {
                        var attachment = new MimePart("application", "octet-stream")
                        {
                            Content = new MimeContent(File.OpenRead(ruta)),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = Path.GetFileName(ruta),
                        };

                        multipart.Add(attachment);
                    }
                }

                // Asignar el multipart al cuerpo del mensaje
                message.Body = multipart;

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                    await client.ConnectAsync(host, 587, SecureSocketOptions.StartTls);

                    // Autenticar con el servidor SMTP
                    await client.AuthenticateAsync(correoEmisor, contraseniaEmisor);

                    // Enviar el mensaje
                    await client.SendAsync(message);

                    // Desconectar del servidor SMTP
                    await client.DisconnectAsync(true);

                    return new OperationService() { Success = true, Message = "Mensaje Enviado Correctamente" };
                }
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.ToString(), "Error");
                return new OperationService() { Success = false, Message = ex.Message.ToString() };
            }
        }

        public string CuerpoCorreo(Correo modelo)
        {
            string correo = string.Empty;
            string cadenaCorreo = string.Empty;
            //string contentID = "img001";
            string rowBeneficiado = string.Empty;
            string rowDestino = string.Empty;
            string rowGastosAdicionales = string.Empty;
            string final = string.Empty;

            if (modelo.IdTipoCorreo == (int)Common.Enum.TipoCorreoEnum.Registrado)
            {
                cadenaCorreo = $"<!DOCTYPE html><html><head><title>Correo</title><!-- <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' integrity='sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u' crossorigin='anonymous'><link rel='stylesheet' type='text/css' href='css/style.css'> --></head><body style='background: #C9C9C9;padding: 0;margin: 0;font-size: 12px;'><table style='width: 100%;font-family: Segoe ui'><thead><tr><th colspan='4' style='background: #0B0B61;width: 100%;border-top-left-radius: 8px;border-top-right-radius: 8px;position: relative;border: 1px solid transparent;'><table width='100%'><tbody><tr><td colspan='2'><span style='color: white;float: left;padding: 15px 15px;font-size: 14px;line-height: 20px;'>KMMP - Servicio de Taxis</span></td><td colspan='2'><span style='color: white;float: right;padding: 15px 15px;font-size: 14px;line-height: 20px;'>Contacto: (511) 615-8400</span></td></tr></tbody></table></th></tr></thead><tbody><!-- <tr> --><!-- <td colspan='4'> --><!-- <img src='img/captura.jpg' style='width: 100%;display: block;'> --><!-- </td> --><!-- </tr> --><tr><td><div style='background: #fff;padding:15px;margin: 15px 0;'><div style='100%'><table style='width: 100%'><tbody><tr colspan='8'><td><p>{modelo.Mensaje} Para mas detalle del servicio, lo mostramos en la parte inferior :</p><td></tr></tbody></table></div><hr /><div style='width: 100%'><div style='width: 50%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del registrador</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° de documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DocumentoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.NombreCompletoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Compañia: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CompaniaUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom; text-align: right;'>BU: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DescripcionCentroCostoUsuario}' readonly='' /></td></tr></tbody></table></div><div style='width: 49%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Detalles del servicio</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Fecha y hora: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.FechaServicioStr}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco / N° Orden:</label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoAfecto} - {modelo.DescripcionCentroCostoAfecto} / {modelo.OrdenServicio}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Origen: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionOrigen}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Destino: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionDestino}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Motivo: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><textarea style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' rows='2' cols='50' readonly=''>{modelo.MotivoCreacion}</textarea></td></tr></tbody></table></div></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Lista de beneficiarios</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Nombres completos</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Telefono</th></tr></thead><tbody>";
                //cadenaCorreo = $"<!DOCTYPE html><html><head><title>Correo</title></head><body style='background: #C9C9C9;padding: 0;margin: 0'><table style='width: 100%;font-family: Segoe ui'><thead><tr><th colspan='4' style='background: #0B0B61;width: 100%;border-top-left-radius: 8px;border-top-right-radius: 8px;position: relative;border: 1px solid transparent;'><table width='100%'><tbody><tr><td colspan='2'><span style='color: white;float: left;padding: 15px 15px;font-size: 18px;line-height: 20px;'>KMMP - Servicio de Taxis</span></td><td colspan='2'><span style='color: white;float: right;padding: 15px 15px;font-size: 18px;line-height: 20px;'>Contacto: (511) 615-8400</span></td></tr></tbody></table></th></tr></thead><tbody><tr><td colspan='4' style='padding: 0 15px'><img src='cid:{contentID}' style='width: 100%;display: block;'></td></tr><tr><td style='padding: 0 15px'><div style='background: #fff;padding:15px;margin: 15px 0;'><div style='100%'><table style='width: 100%'><tbody><tr colspan='8'><td><p> {modelo.Mensaje} .Para mas detalle del servicio, lo mostramos en la parte inferior :</p><td></tr></tbody></table></div><div style='display: inline-flex; width: 100%'><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del registrador</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° de documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DocumentoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.NombreCompletoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Compañia: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CompaniaUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoUsuario} - {modelo.DescripcionCentroCostoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom; text-align: right;'>BU: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly=''/></td></tr></tbody></table><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Detalles del servicio</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Fecha y hora: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.FechaServicioStr}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco / N° Orden:</label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoAfecto} - {modelo.DescripcionCentroCostoAfecto} / {modelo.OrdenServicio}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Origen: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionOrigen}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Destino: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionDestino}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Motivo: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><textarea style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' rows='2' cols='50' readonly=''>{modelo.MotivoCreacion}</textarea></td></tr></tbody></table></div><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Lista de beneficiarios</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>Nombres completos</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>Telefono</th></tr></thead><tbody>";
                foreach (var beneficiado in modelo.ListaBeneficiado)
                {
                    rowBeneficiado += $"<tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Apellidos}, {beneficiado.Nombre}</td> <td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Telefono}</td> </tr>";
                }

                final = "</tbody></table></td></tr><tbody></table></div></div></td></tr></tbody></table></body></html>";

                correo = $"{cadenaCorreo}{rowBeneficiado}{final}";
            }

            else if ((modelo.IdTipoCorreo == (int)Common.Enum.TipoCorreoEnum.Aprobado) || (modelo.IdTipoCorreo == (int)Common.Enum.TipoCorreoEnum.Anulado) || (modelo.IdTipoCorreo == (int)Common.Enum.TipoCorreoEnum.Cancelado) || (modelo.IdTipoCorreo == (int)Common.Enum.TipoCorreoEnum.Rechazado))
            {
                cadenaCorreo = $"<!DOCTYPE html><html><head><title>Correo</title><!-- <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' integrity='sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u' crossorigin='anonymous'><link rel='stylesheet' type='text/css' href='css/style.css'> --></head><body style='background: #C9C9C9;padding: 0;margin: 0;font-size: 12px;'><table style='width: 100%;font-family: Segoe ui'><thead><tr><th colspan='4' style='background: #0B0B61;width: 100%;border-top-left-radius: 8px;border-top-right-radius: 8px;position: relative;border: 1px solid transparent;'><table width='100%'><tbody><tr><td colspan='2'><span style='color: white;float: left;padding: 15px 15px;font-size: 14px;line-height: 20px;'>KMMP - Servicio de Taxis</span></td><td colspan='2'><span style='color: white;float: right;padding: 15px 15px;font-size: 14px;line-height: 20px;'>Contacto: (511) 615-8400</span></td></tr></tbody></table></th></tr></thead><tbody><!-- <tr> --><!-- <td colspan='4'> --><!-- <img src='img/captura.jpg' style='width: 100%;display: block;'> --><!-- </td> --><!-- </tr> --><tr><td><div style='background: #fff;padding:15px;margin: 15px 0;'><div style='100%'><table style='width: 100%'><tbody><tr colspan='8'><td><p>{modelo.Mensaje} Para mas detalle del servicio, lo mostramos en la parte inferior :</p><td></tr></tbody></table></div><hr /><div style='width: 100%'><div style='width: 50%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del registrador</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° de documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DocumentoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.NombreCompletoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Compañia: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CompaniaUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoUsuario}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom; text-align: right;'>BU: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DescripcionCentroCostoUsuario}' readonly='' /></td></tr></tbody></table></div><div style='width: 49%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Detalles del servicio</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Fecha y hora: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.FechaServicioStr}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco / N° Orden:</label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoAfecto} - {modelo.DescripcionCentroCostoAfecto} / {modelo.OrdenServicio}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Origen: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionOrigen}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Destino: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionDestino}' readonly='' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Motivo: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><textarea style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' rows='2' cols='50' readonly=''>{modelo.MotivoCreacion}</textarea></td></tr></tbody></table></div></div><hr /><div style='width: 100%'><div style='width: 50%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del proveedor</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Razón social: </label></td><td style='padding: 0 15px 0px 105px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.RazonSocialProveedor}' readonly='' /></td></tr></tbody></table></div><div style='width: 49%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'></h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>RUC: </label></td><td style='padding: 0 15px 0px 35px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.RucProveedor}' readonly='' /></td></tr></tbody></table></div></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Lista de beneficiarios</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Nombres completos</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Telefono</th></tr></thead><tbody>";
                //cadenaCorreo = $"<!DOCTYPE html><html><head><title>Correo</title></head><body style='background: #C9C9C9;padding: 0;margin: 0'><table style='width: 100%;font-family: Segoe ui'><thead><tr><th colspan='4' style='background: #0B0B61;width: 100%;border-top-left-radius: 8px;border-top-right-radius: 8px;position: relative;border: 1px solid transparent;'><table width='100%'><tbody><tr><td colspan='2'><span style='color: white;float: left;padding: 15px 15px;font-size: 18px;line-height: 20px;'>KMMP - Servicio de Taxis</span></td><td colspan='2'><span style='color: white;float: right;padding: 15px 15px;font-size: 18px;line-height: 20px;'>Contacto: (511) 615-8400</span></td></tr></tbody></table></th></tr></thead><tbody><tr><td colspan='4' style='padding: 0 15px'><img src='cid:{contentID}' style='width: 100%;display: block;'></td></tr><tr><td style='padding: 0 15px'><div style='background: #fff;padding:15px;margin: 15px 0;'><div style='100%'><table style='width: 100%'><tbody><tr colspan='8'><td><p> {modelo.Mensaje} .Para mas detalle del servicio, lo mostramos en la parte inferior :</p><td></tr></tbody></table></div><div style='display: inline-flex; width: 100%'><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del registrador</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° de documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DocumentoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.NombreCompletoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Compañia: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CompaniaUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoUsuario} - {modelo.DescripcionCentroCostoUsuario}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom; text-align: right;'>BU: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly=''/></td></tr></tbody></table><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Detalles del servicio</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Fecha y hora: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.FechaServicioStr}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco / N° Orden:</label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.CodigoCentroCostoAfecto} - {modelo.DescripcionCentroCostoAfecto} / {modelo.OrdenServicio}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Origen: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionOrigen}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Dirección Destino: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.DireccionDestino}' readonly=''/></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Motivo: </label></td><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><textarea style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' rows='2' cols='50' readonly=''>{modelo.MotivoCreacion}</textarea></td></tr></tbody></table></div><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Lista de beneficiarios</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>Nombres completos</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>Telefono</th></tr></thead><tbody>";
                foreach (var beneficiado in modelo.ListaBeneficiado)
                {
                    rowBeneficiado += $"<tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Apellidos}, {beneficiado.Nombre}</td> <td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Telefono}</td> </tr>";
                }

                final = "</tbody></table></td></tr><tbody></table></div></div></td></tr></tbody></table></body></html>";

                correo = $"{cadenaCorreo}{rowBeneficiado}{final}";
            }
            else
            {
                cadenaCorreo = $"<!DOCTYPE html><html><head><title>Correo</title></head><body style='background: #C9C9C9;padding: 0;margin: 0; font-size: 12px;'><table style='width: 100%;font-family: Segoe ui;padding: 0 200px;'><thead><tr><th colspan='4' style='background: #0e1271;width: 100%;border-top-left-radius: 8px;border-top-right-radius: 8px;position: relative;border: 1px solid transparent;'><table width='100%'><tbody><tr><td colspan='2'><span style='color: white;float: left;padding: 15px 15px;font-size: 14px;line-height: 20px;'>KMMP - Servicio de Taxis</span></td><td colspan='2'><span style='color: white;float: right;padding: 15px 15px;font-size: 14px;line-height: 20px;'>Contacto: (511) 615-8400</span></td></tr></tbody></table></th></tr></thead><tbody><tr><td><div style='background: #fff;padding:15px;margin: 15px 0;'><div style='width: 100%'><table style='width: 100%'><tbody><tr colspan='8'><td><p> {modelo.Mensaje} Para mas detalle, lo mostramos en la parte inferior :</p><td></tr></tbody></table></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del Aprobador</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° Documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.NumeroDocumentoAprobador}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre Completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.NombreCompletoAprobador}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Compañia: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.NombreSociedadAprobador}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Ceco: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.CodigoCecoAprobador}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>BU: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.DescripcionCecoAprobador}' /></td></tr></tbody></table></div><hr /><div style='width: 100%'><div style='width: 50%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del proveedor</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Razón social: </label></td><td style='padding: 0 15px 0px 50px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.RazonSocialProveedor}' readonly='' /></td></tr></tbody></table></div><div style='width: 49%; display: inline-block;'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'></h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>RUC: </label></td><td style='padding: 0 7px 0px 110px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' value='{modelo.RucProveedor}' readonly='' /></td></tr></tbody></table></div></div><hr /><div style='width: 100%; display: inline-flex;'><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del Conductor</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° Documento: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Conductor.Documento}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Nombre Completo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Conductor.NombreCompleto}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Celular: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Conductor.Celular}' /></td></tr></tbody></table><table style='width: 50%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Datos del Automóvil</h4></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>N° Placa: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Automovil.Placa}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Marca / Modelo: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Automovil.Marca} {modelo.Automovil.Modelo}' /></td></tr><tr><td style='padding: 0 15px;-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'><label style='padding-top: 7px;margin-bottom: 0;text-align: right;'>Color: </label></td><td style='padding: 0 15px'><input style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: block;width: 100%;height: 34px;padding: 6px 12px;font-size: 12px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;border-radius: 4px;margin-bottom: 10px' type='text' readonly='' value='{modelo.Automovil.Color}' /></td></tr></tbody></table></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='4'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Trayectoria</h4></td></tr> <tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color: #0e1271;color:#fff;border-radius: 1px;'>Fecha Inicio</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color: #0e1271;color:#fff;border-radius: 1px;'>Fecha Fin</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Origen</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Destino</th></tr></thead><tbody>";
                decimal totalPrecio = 0m;
                decimal totalGasto = 0m;
                foreach (var destino in modelo.ListaDestino)
                {
                    totalPrecio += decimal.Round(destino.Precio, 2);
                    rowDestino += $"<tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{destino.FechaInicio}</td><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{destino.FechaFin}</td><td style='text-align: center;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{destino.DireccionOrigen}</td><td style='text-align: center;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{destino.DireccionDestino}</td></tr>";
                }
                cadenaCorreo += $"{rowDestino}";
                cadenaCorreo += $"</tbody></table></td></tr></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Tu Tarifa</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color: #0e1271;color:#fff;border-radius: 1px;'>Nombre</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Precio (S/.)</th></tr></thead><tbody><tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>Base</td><td style='text-align: center;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>##PrecioBase##</td></tr>";
                foreach (var gastoadicional in modelo.ListaGastoAdicional)
                {
                    totalGasto += Decimal.Round(gastoadicional.Monto, 2);
                    totalPrecio -= Decimal.Round(gastoadicional.Monto, 2);
                    rowGastosAdicionales += $"<tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{gastoadicional.Concepto.Nombre}</td><td style='text-align: center;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{gastoadicional.Monto.ToString("N2")}</td></tr>";
                }

                cadenaCorreo = cadenaCorreo.Replace("##PrecioBase##", totalPrecio.ToString("N2"));

                decimal totalGeneral = totalPrecio + totalGasto;
                rowGastosAdicionales += $"<tr><td style='text-align: center;font-weight: bold;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Total</td><td style='text-align: center;font-weight: bold;padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>{totalGeneral.ToString("N2")}</td></tr>";
                cadenaCorreo += $"{rowGastosAdicionales}";

                cadenaCorreo += $"</tbody></table></td></tr></div><hr /><div style='width: 100%'><table style='width: 100%'><tbody><tr><td colspan='2'><h4 style='color: #0B0B61; font-weight: bold; margin-top: 10px;margin-bottom: 10px;'>Lista de beneficiarios</h4></td></tr><tr><td colspan='2'><table style='width: 100%;border-spacing: 0;border-collapse: collapse;'><thead><tr><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Nombres completos</th><th style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;background-color:#0e1271;color:#fff;'>Telefono</th></tr></thead><tbody>";
                foreach (var beneficiado in modelo.ListaBeneficiado)
                {
                    rowBeneficiado += $"<tr><td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Apellidos}, {beneficiado.Nombre}</td> <td style='padding: 8px;line-height: 1.42857143;vertical-align: top;border: 1px solid #ddd;'>{beneficiado.Telefono}</td> </tr>";
                }

                final = "</tbody></table></td></tr><tbody></table></div></div></td></tr></tbody></table></body></html>";

                correo = $"{cadenaCorreo}{rowBeneficiado}{final}";
            }

            return correo;
        }

        //public async Task<OperationService> EnviarCorreo_GV(string compania,string correoResponsable = null, string correoUsuarioRegistro = null, List<string> listaAprobador = null, List<Modelo.General.Usuario> listaSoporteAdministrativo = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        //public async Task<OperationService> EnviarCorreo_GV(string compania, string correoResponsable = null, string correoUsuarioRegistro = null, List<string> listaAprobador = null, List<string> listaSoporteAdministrativo = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        //{
        //    string nombreEmisor = ConfigurationManager.AppSettings["nombreEmisorGV"];
        //    string correoEmisor;
        //    string contraseniaEmisor;

        //    if (compania.Contains("Amazonía"))
        //    {
        //        correoEmisor = ConfigurationManager.AppSettings["correoEmisorKMA"];
        //        contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
        //    }
        //    else
        //    {
        //        correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
        //        contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisor"];
        //    }
        //    string host = ConfigurationManager.AppSettings["host"];

        //    using (var mail = new MailMessage())
        //    {
        //        if (!string.IsNullOrEmpty(correoResponsable))
        //        {
        //            mail.To.Add(correoResponsable); //Destinatario
        //        }

        //        if (!string.IsNullOrEmpty(correoUsuarioRegistro))
        //        {
        //            mail.To.Add(correoUsuarioRegistro); //Destinatario
        //        }

        //        if (listaAprobador != null)
        //        {
        //            foreach (var item in listaAprobador)
        //            {
        //                if (!string.IsNullOrEmpty(item))
        //                {
        //                    //mail.CC.Add(item);//Copia
        //                }
        //            }
        //        }
        //        if (listaSoporteAdministrativo != null)
        //        {
        //            foreach (var item in listaSoporteAdministrativo)
        //            {
        //                //mail.CC.Add(item.Email);//Copia
        //                mail.CC.Add(item);//Copia
        //            }
        //        }
        //        mail.From = new MailAddress(correoEmisor, nombreEmisor);//Emisor y nombre de usuario
        //        mail.Subject = asunto;//Asunto del mensaje
        //        mail.SubjectEncoding = System.Text.Encoding.UTF8;

        //        //var oAttachment = new Attachment(Utilitario.ServerPath(ConfigurationManager.AppSettings["urlImg"]));
        //        //string contentID = "img001";
        //        //oAttachment.ContentId = contentID;

        //        //mail.Attachments.Add(oAttachment);

        //        mail.Body = mensaje;
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        mail.IsBodyHtml = true;

        //        if (Attachments != null)
        //        {
        //            foreach (var ruta in Attachments)
        //            {
        //                Attachment oAttachmentTemp = new Attachment(ruta.Values.ToString());
        //                oAttachmentTemp.ContentId = ruta.Keys.ToString();
        //                mail.Attachments.Add(oAttachmentTemp);
        //            }
        //        }

        //        if (ArchivosAdjuntos != null)
        //        {
        //            foreach (var ruta in ArchivosAdjuntos)
        //            {
        //                mail.Attachments.Add(new Attachment(ruta));
        //            }

        //        }
        //        using (var client = new SmtpClient())
        //        {
        //            client.Host = host;
        //            client.Port = 587;
        //            client.EnableSsl = true;
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            client.DeliveryFormat = SmtpDeliveryFormat.International;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(correoEmisor, contraseniaEmisor);

        //            try
        //            {
        //                Utilitario.Logger();
        //                Log.Information("Preparando envío de correo");
        //                Utilitario.WriteInfoLog("Preparando envío de correo");
        //                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //                {
        //                    return true;
        //                };
        //                await client.SendMailAsync(mail);//Enviamos el mensaje
        //                Log.Information("Correo enviado correctamente");
        //                Utilitario.WriteInfoLog("Correo enviado correctamente");
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = true, Message = "Mensaje Enviado Correctamente" };
        //            }
        //            catch (SmtpException ex)
        //            {
        //                var x = ex.ToString();
        //                x = ex.Message.ToString();
        //                Utilitario.WriteErrorLog(ex.ToString(), "Error");
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = false, Message = ex.Message.ToString() };
        //            }
        //            catch (Exception ex)
        //            {
        //                Utilitario.WriteErrorLog(ex.ToString(), "Error");
        //                var x = ex.ToString();
        //                x = ex.Message.ToString();
        //                Log.CloseAndFlush();
        //                return new OperationService() { Success = false, Message = ex.Message.ToString() };
        //            }
        //        }
        //    }
        //}

        public async Task<OperationService> EnviarCorreo_GV(string compania, string correoResponsable = null, string correoUsuarioRegistro = null, List<string> listaAprobador = null, List<string> listaSoporteAdministrativo = null, string asunto = null, string mensaje = null, List<string> ArchivosAdjuntos = null, List<Dictionary<string, string>> Attachments = null)
        {
            string nombreEmisor = ConfigurationManager.AppSettings["nombreEmisorGV"];
            string correoEmisor;
            string contraseniaEmisor;

            if (compania.Contains("Amazonía"))
            {
                correoEmisor = ConfigurationManager.AppSettings["correoEmisorKMA"];
                contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
            }
            else
            {
                correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
                contraseniaEmisor = ConfigurationManager.AppSettings["contraseniaEmisor"];
            }
            string host = ConfigurationManager.AppSettings["host"];
            int puerto = 587;

            using (var message = new MimeMessage())
            {
                if (!string.IsNullOrEmpty(correoResponsable))
                {
                    message.To.Add(MailboxAddress.Parse(correoResponsable)); // Destinatario
                }

                if (!string.IsNullOrEmpty(correoUsuarioRegistro))
                {
                    message.To.Add(MailboxAddress.Parse(correoUsuarioRegistro)); // Destinatario
                }

                if (listaAprobador != null)
                {
                    foreach (var item in listaAprobador)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            // Puedes usar CC o Bcc según tus necesidades
                            //message.Cc.Add(MailboxAddress.Parse(item)); // Copia
                        }
                    }
                }

                if (listaSoporteAdministrativo != null && !string.IsNullOrEmpty(listaSoporteAdministrativo[0]))
                {
                    foreach (var item in listaSoporteAdministrativo)
                    {
                        message.Cc.Add(MailboxAddress.Parse(item)); // Copia
                    }
                }

                message.From.Add(new MailboxAddress(nombreEmisor, correoEmisor)); // Emisor y nombre de usuario
                message.Subject = asunto; // Asunto del mensaje

                var builder = new BodyBuilder
                {
                    TextBody = mensaje,
                    HtmlBody = mensaje // Puedes cambiar esto según tus necesidades
                };

                if (Attachments != null)
                {
                    foreach (var ruta in Attachments)
                    {
                        var filePath = ruta.Keys.FirstOrDefault(); // Obtener la ruta del archivo
                        var contentId = ruta.Values.FirstOrDefault(); // Obtener el contentId (puede ser nulo)
                        builder.Attachments.Add(filePath);
                    }
                }

                if (ArchivosAdjuntos != null)
                {
                    foreach (var ruta in ArchivosAdjuntos)
                    {
                        builder.Attachments.Add(ruta);
                    }
                }

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    try
                    {
                        await client.ConnectAsync(host, puerto, SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync(correoEmisor, contraseniaEmisor);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);

                        return new OperationService() { Success = true, Message = "Mensaje Enviado Correctamente" };
                    }
                    catch (Exception ex)
                    {
                        Utilitario.WriteErrorLog(ex.ToString(), "Error");
                        return new OperationService() { Success = false, Message = ex.Message.ToString() };
                    }
                }
            }
        }

        public string CuerpoCorreo_GV(Correo_GV modelo)
        {
            string correo = string.Empty;
            string inicio = string.Empty;
            string cuerpo = string.Empty;
            string final = string.Empty;

            try
            {
                inicio = "<!DOCTYPE html><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'/><title>Correo</title></head><body><blockquote style='margin:0 0 0 0.8ex;padding-left:1ex;border-left:1px solid #CCCCCC;'><span style='font-size:12px;'><fieldset><legend><font color='navy'>";
                // Condición para tipo de correo Reembolsos (IdTipoCorreo == 2)
                if (modelo.IdTipoCorreo == 2)
                {
                    cuerpo += $"<b>REGISTRO DE REEMBOLSOS</b></font></legend><br>Estimado(a) Usuario <br><br>Se generó el reembolso: <b>{modelo.Codigo}</b><br><br><b>DATOS DE LA SOLICITUD:</b><br><br>Nombre del Solicitante: <b>{modelo.NombreCompletoUsuario}</b><br>Motivo de la Solicitud: <b>{modelo.Motivo}</b><br>Monto Total: {modelo.Moneda}.<b>{modelo.ImporteSolicitud} {modelo.DescripcionMoneda}.</b><br>Fecha de registro: <b>{modelo.FechaSolicitudStr}</b><br><br></fieldset><br><fieldset><legend><font color='navy'><b>TERMINOS Y CONDICIONES DE RENDICION DE REEMBOLSOS</b></font></legend><br>Usted <b>{modelo.NombreCompletoUsuario}</b> con DNI Nº <b>{modelo.DocumentoUsuario}</b> declara conocer que <b>{modelo.CompaniaUsuario}</b> está obligada a registrar los comprobantes de pago dentro del mes de emisión de los mismos, de acuerdo a lo estipulado por Sunat y Usted deberá rendir los anticipos entregados por <b>{modelo.CompaniaUsuario}</b> en estricta observancia de la normativa interna sobre rendiciones de gastos, la misma que declara conocer, cuya copia se encuentra a su disposición en el Sistema de Gestión Documentaria de <b>KMMP</b> (<a href='https://v8-dot-komatsu-gd.appspot.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>https://v8-dot-komatsu-gd.appspot.com/</a>).<br> <br>En este sentido, por medio la presente, Usted otorga su consentimiento informado, libre y expreso a <b>{modelo.CompaniaUsuario}</b> para que descuente de su remuneración, liquidación de beneficios sociales, participación de utilidades y/u otros beneficios que la Compañía le otorgue, hasta por el equivalente al saldo total de los anticipos solicitados y no rendidos dentro de los plazos establecidos en la referida política.<br>";
                }
                // Condición para tipo de correo Caja (IdTipoCorreo == 3)
                else if (modelo.IdTipoCorreo == 3)
                {
                    cuerpo += $"<b>REGISTRO DE CAJAS</b></font></legend><br>Estimado(a) Usuario <br><br>Se generó la caja: <b>{modelo.Codigo}</b><br><br><b>DATOS DE LA SOLICITUD:</b><br><br>Nombre del Solicitante: <b>{modelo.NombreCompletoUsuario}</b><br>Motivo de la Solicitud: <b>{modelo.Motivo}</b><br>Monto Total: {modelo.Moneda}.<b>{modelo.ImporteSolicitud} {modelo.DescripcionMoneda}.</b><br>Fecha de registro: <b>{modelo.FechaSolicitudStr}</b><br><br></fieldset><br><fieldset><legend><font color='navy'><b>TERMINOS Y CONDICIONES DE RENDICION DE CAJAS</b></font></legend><br>Usted <b>{modelo.NombreCompletoUsuario}</b> con DNI Nº <b>{modelo.DocumentoUsuario}</b> declara conocer que <b>{modelo.CompaniaUsuario}</b> está obligada a registrar los comprobantes de pago dentro del mes de emisión de los mismos, de acuerdo a lo estipulado por Sunat y Usted deberá rendir los anticipos entregados por <b>{modelo.CompaniaUsuario}</b> en estricta observancia de la normativa interna sobre rendiciones de gastos, la misma que declara conocer, cuya copia se encuentra a su disposición en el Sistema de Gestión Documentaria de <b>KMMP</b> (<a href='https://v8-dot-komatsu-gd.appspot.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>https://v8-dot-komatsu-gd.appspot.com/</a>).<br> <br>En este sentido, por medio la presente, Usted otorga su consentimiento informado, libre y expreso a <b>{modelo.CompaniaUsuario}</b> para que descuente de su remuneración, liquidación de beneficios sociales, participación de utilidades y/u otros beneficios que la Compañía le otorgue, hasta por el equivalente al saldo total de los anticipos solicitados y no rendidos dentro de los plazos establecidos en la referida política.<br>";
                }
                // Condición para tipo de correo Tarjeta (IdTipoCorreo == 4)
                else if (modelo.IdTipoCorreo == 4)
                {
                    cuerpo += $"<b>REGISTRO DE TARJETAS</b></font></legend><br>Estimado(a) Usuario <br><br>Se generó la tarjeta: <b>{modelo.Codigo}</b><br><br><b>DATOS DE LA SOLICITUD:</b><br><br>Nombre del Solicitante: <b>{modelo.NombreCompletoUsuario}</b><br>Motivo de la Solicitud: <b>{modelo.Motivo}</b><br>Monto Total: {modelo.Moneda}.<b>{modelo.ImporteSolicitud} {modelo.DescripcionMoneda}.</b><br>Fecha de registro: <b>{modelo.FechaSolicitudStr}</b><br><br></fieldset><br><fieldset><legend><font color='navy'><b>TERMINOS Y CONDICIONES DE RENDICION DE TARJETAS</b></font></legend><br>Usted <b>{modelo.NombreCompletoUsuario}</b> con DNI Nº <b>{modelo.DocumentoUsuario}</b> declara conocer que <b>{modelo.CompaniaUsuario}</b> está obligada a registrar los comprobantes de pago dentro del mes de emisión de los mismos, de acuerdo a lo estipulado por Sunat y Usted deberá rendir los anticipos entregados por <b>{modelo.CompaniaUsuario}</b> en estricta observancia de la normativa interna sobre rendiciones de gastos, la misma que declara conocer, cuya copia se encuentra a su disposición en el Sistema de Gestión Documentaria de <b>KMMP</b> (<a href='https://v8-dot-komatsu-gd.appspot.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>https://v8-dot-komatsu-gd.appspot.com/</a>).<br> <br>En este sentido, por medio la presente, Usted otorga su consentimiento informado, libre y expreso a <b>{modelo.CompaniaUsuario}</b> para que descuente de su remuneración, liquidación de beneficios sociales, participación de utilidades y/u otros beneficios que la Compañía le otorgue, hasta por el equivalente al saldo total de los anticipos solicitados y no rendidos dentro de los plazos establecidos en la referida política.<br>";
                }
                else
                {
                    cuerpo = $"<b>REGISTRO DE ANTICIPOS</b></font></legend><br>Estimado(a) Usuario <br><br>Se generó el anticipo: <b>{modelo.Codigo}</b><br><br><b>DATOS DE LA SOLICITUD:</b><br><br>Nombre del Solicitante: <b>{modelo.NombreCompletoUsuario}</b><br>Motivo de la Solicitud: <b>{modelo.Motivo}</b><br>Monto Total: {modelo.Moneda}.<b>{modelo.ImporteSolicitud} {modelo.DescripcionMoneda}.</b><br>Fecha de registro: <b>{modelo.FechaSolicitudStr}</b><br><br></fieldset><br><fieldset><legend><font color='navy'><b>TERMINOS Y CONDICIONES DE RENDICION DE ANTICIPOS</b></font></legend><br>Usted <b>{modelo.NombreCompletoUsuario}</b> con DNI Nº <b>{modelo.DocumentoUsuario}</b> declara conocer que <b>{modelo.CompaniaUsuario}</b> está obligada a registrar los comprobantes de pago dentro del mes de emisión de los mismos, de acuerdo a lo estipulado por Sunat y Usted deberá rendir los anticipos entregados por <b>{modelo.CompaniaUsuario}</b> en estricta observancia de la normativa interna sobre rendiciones de gastos, la misma que declara conocer, cuya copia se encuentra a su disposición en el Sistema de Gestión Documentaria de <b>KMMP</b> (<a href='https://v8-dot-komatsu-gd.appspot.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>https://v8-dot-komatsu-gd.appspot.com/</a>).<br> <br>En este sentido, por medio la presente, Usted otorga su consentimiento informado, libre y expreso a <b>{modelo.CompaniaUsuario}</b> para que descuente de su remuneración, liquidación de beneficios sociales, participación de utilidades y/u otros beneficios que la Compañía le otorgue, hasta por el equivalente al saldo total de los anticipos solicitados y no rendidos dentro de los plazos establecidos en la referida política.<br>";
                }

                final = "</fieldset></span></blockquote></body></html>";
                correo = $"{inicio}{cuerpo}{final}";
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.ToString(), "Error Cuerpo Correo");
            }
            return correo;
        }
    }
}
