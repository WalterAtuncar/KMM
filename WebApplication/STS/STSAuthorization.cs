using Newtonsoft.Json;
using Service.Services;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Models;

namespace Komatsu_SistemaSeguros.STS
{
    public class STSAuthorization : ActionFilterAttribute
    {
        public static UserModel UserModelClaims = null;
        public static UsersService oUsersService = new UsersService();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string TokenValueGet = null;
            string URLWebSeguridad = ConfigurationManager.AppSettings["SERVER_WEB_SEGURIDAD"];
            var flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            if (flagToken)
            {
                TokenValueGet = filterContext.HttpContext.Request.Params.Get("tknS3rvic3");
                if (TokenValueGet != null)
                {
                    ValidateToken(TokenValueGet, filterContext, URLWebSeguridad);
                }
                else
                {
                    if (HttpContext.Current.Session["TokenValueGet"] != null) { TokenValueGet = HttpContext.Current.Session["TokenValueGet"].ToString(); }

                    if (TokenValueGet != null)
                    {
                        ValidateToken(TokenValueGet, filterContext, URLWebSeguridad);
                    }
                    else
                    {
                        RedirectSTS("No tiene autorizacion", filterContext, URLWebSeguridad);
                    }
                }
            }
            else
            {
                if (HttpContext.Current.Session["UserModel"] == null && HttpContext.Current.Session["paginaView"] == null)
                {
                    //var User = new UserModel();
                    ////User.Id = 7136;
                    //User.Id = 2916;
                    //User.UserName = "julio.delacruz";
                    //User.Name = "JULIO CESAR";
                    //User.LastName = "DE LA CRUZ RUIZ";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "CESADO";
                    //User.IdCentroCosto = "10AD05138";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "45371437";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "julio.delacruz@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 2916;
                    //User.UserName = "christian.caballero";
                    //User.Name = "CHRISTIAN JEAN CLAUDE";
                    //User.LastName = "CABALLERO CUSTODIO";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "77920962";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "christian.caballero@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 558;
                    //User.UserName = "gisella.manrique";
                    //User.Name = "GISELLA AZUCENA";
                    //User.LastName = "MANRIQUE MAGUIÑA";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09157";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "10683384";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "gisella.manrique@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 5013;
                    //User.UserName = "kathleen.guerrero";
                    //User.Name = "KATHLEEN JORDANA";
                    //User.LastName = "GUERRERO ROSAS";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "30SS13315";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "73797738";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "kathleen.guerrero@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    var User = new UserModel();
                    User.Id = 76;
                    User.UserName = "victor.guardamino";
                    User.Name = "VICTOR HUGO";
                    User.LastName = "GUARDAMINO GALINDO";
                    User.IdSociedad = "PE03";
                    User.EstadoFractal = "ACTIVO";
                    User.IdCentroCosto = "10AD05192";
                    User.IdAlternativo = "30SS14314";
                    User.TipoDocumento = "DNI";
                    User.NumeroDocumento = "73797738";
                    User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    User.Email = "victor.guardamino@cumminsperu.pe";
                    User.Compania = "Komatsu-Mitsui";
                    User.Cargo = "Analista Senior de Sistemas";
                    var validate = oUsersService.GetValidateByUsername(User.UserName);
                    User.IdPerfil = validate;
                    HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 5171;
                    //User.UserName = "yolanda.rodriguez";
                    //User.Name = "VALERIA YOLANDA";
                    //User.LastName = "RODRIGUEZ CHACON";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "30SS13315";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "76029640";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "yolanda.rodriguez@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 4201;
                    //User.UserName = "sandro.torres";
                    //User.Name = "SANDRO ALEX";
                    //User.LastName = "TORRES SURCO";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10SS15306";
                    //User.IdAlternativo = "30SS13315";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "73797738";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "sandro.torres@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 1388;
                    //User.UserName = "juancarlos.lara";
                    //User.Name = "JUAN CARLOS";
                    //User.LastName = "LARA FLORES";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD02130";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "40757571";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "juan.lara@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 5353;
                    //User.UserName = "masunori.ogawa";
                    //User.Name = "MASUNORI";
                    //User.LastName = "OGAWA .";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD02131";
                    //User.IdAlternativo = "006212230";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "40757571";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "juan.lara@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;


                    //var User = new UserModel();
                    //User.Id = 3;
                    //User.UserName = "marcos.pinto";
                    //User.Name = "MARCOS URIEL";
                    //User.LastName = "PINTO MAMANI";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002004827";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "41220736";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "marcos.pinto@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "GERENTE DE OPERACIONES DE TECNOLOGÍA DE LA INFORMACIÓN";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 5020;
                    //User.UserName = "kathleen.guerrero";
                    //User.Name = "KATHLEEN JORDANA";
                    //User.LastName = "GUERRERO ROSAS";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "73531629";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "kathleen.guerrero@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;
                }
                else
                {
                    //var User = new UserModel();
                    //User.Id = 5020;
                    //User.UserName = "kathleen.guerrero";
                    //User.Name = "KATHLEEN JORDANA";
                    //User.LastName = "GUERRERO ROSAS";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "73531629";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "kathleen.guerrero@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 1020;
                    //User.UserName = "jose.malaver";
                    //User.Name = "JOSE EDUARDO";
                    //User.LastName = "MALAVER MEZA";
                    //User.IdSociedad = "PE04";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "40AD02101";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "70681686";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "jose.malaver@kma.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 2916;
                    //User.UserName = "christian.caballero";
                    //User.Name = "CHRISTIAN JEAN CLAUDE";
                    //User.LastName = "CABALLERO CUSTODIO";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "77920962";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "christian.caballero@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 2127;
                    //User.UserName = "luis.valega";
                    //User.Name = "LUIS MIGUEL";
                    //User.LastName = "VALEGA ALARCON";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10CC01148G";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "40668869";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "luis.valega@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 1771;
                    //User.UserName = "katia.marquina";
                    //User.Name = "KATIA PAOLA";
                    //User.LastName = "MARQUINA DAVILA";
                    //User.IdSociedad = "PE04";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "43641915";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "katia.marquina@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;


                    //var User = new UserModel();
                    //User.Id = 3385;
                    //User.UserName = "maria.morante";
                    //User.Name = "MARIA ISABEL";
                    //User.LastName = "MORANTE RODRIGUEZ";
                    //User.IdSociedad = "PE04";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05192";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "46604657";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "maria.morante@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 3;
                    //User.UserName = "robert.sanchez";
                    //User.Name = "ROBERT";
                    //User.LastName = "SANCHEZ CASTRO";
                    //User.IdSociedad = "PE04";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD05138";
                    //User.IdAlternativo = "0002004827";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "48106677";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "marcos.pinto@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "GERENTE DE OPERACIONES DE TECNOLOGÍA DE LA INFORMACIÓN";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 3;
                    //User.UserName = "marcos.pinto";
                    //User.Name = "MARCOS URIEL";
                    //User.LastName = "PINTO MAMANI";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002004827";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "41220736";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "marcos.pinto@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "GERENTE DE OPERACIONES DE TECNOLOGÍA DE LA INFORMACIÓN";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();
                    //User.Id = 2916;
                    //User.UserName = "christian.caballero";
                    //User.Name = "CHRISTIAN JEAN CLAUDE";
                    //User.LastName = "CABALLERO CUSTODIO";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002006102";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "77920962";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "marcos.pinto@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "ANALISTA DE APLICACIONES TI";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;

                    //var User = new UserModel();                  
                    //User.Id = 760;
                    //User.UserName = "ericson.munoz";
                    //User.Name = "ERICSON FRANKLIN";
                    //User.LastName = "MUÑOZ ABANTO";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "ACTIVO";
                    //User.IdCentroCosto = "10SS15306G";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "44880863";
                    //User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "ericson.munoz@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;
                    //var User = new UserModel();
                    //User.Id = 595;
                    //User.UserName = "julio.calcin";
                    //User.Name = "JULIO CESAR";
                    //User.LastName = "CALCIN ANGELES";
                    //User.IdSociedad = "PE01";
                    //User.EstadoFractal = "CESADO";
                    //User.IdCentroCosto = "10AD09154";
                    //User.IdAlternativo = "0002003293";
                    //User.TipoDocumento = "DNI";
                    //User.NumeroDocumento = "43583070";
                    ////User.sub = "7065ecc8-7959-4c31-9b19-b9115f10381a";
                    //User.Email = "julio.delacruz@kmmp.com.pe";
                    //User.Compania = "Komatsu-Mitsui";
                    //User.Cargo = "Analista Senior de Sistemas";
                    //var validate = oUsersService.GetValidateByUsername(User.UserName);
                    //User.IdPerfil = validate;
                    //HttpContext.Current.Session["UserModel"] = User;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private void ValidateToken(string token, ActionExecutingContext filterContext, string URLWebSeguridad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = ConfigurationManager.AppSettings["SERVER_STS"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();
                    if (responseTask.Result.StatusCode == HttpStatusCode.Forbidden || responseTask.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        RedirectSTS("No tiene autorizacion", filterContext, URLWebSeguridad);
                    }
                    else
                    {
                        var json = responseTask.Result.Content.ReadAsStringAsync().Result;
                        var claims = JsonConvert.DeserializeObject<UserModel>(json);
                        /*VALIDAR SI EXISTE POR USERNAME*/
                        var validate = oUsersService.GetValidateByUsername(claims.UserName);
                        if(validate == 0)
                        {
                            RedirectAuthorization("No tiene acceso", filterContext);
                        }
                        else
                        {
                            claims.IdPerfil = validate;
                            HttpContext.Current.Session["UserModel"] = claims;
                            HttpContext.Current.Session["TokenValueGet"] = token;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                RedirectSTS(msj, filterContext, URLWebSeguridad);
            }
        }

        private void RedirectSTS(string msj, ActionExecutingContext filterContext, string URLWebSeguridad)
        {

            HttpContext.Current.Session["UserModel"] = null;
            HttpContext.Current.Session["TokenValueGet"] = null;
            HttpContext.Current.Session.Remove("UserModel");
            HttpContext.Current.Session.Remove("TokenValueGet");

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JavaScriptResult result = new JavaScriptResult()
                {
                    Script = "window.location='" + URLWebSeguridad + "';"
                };
                filterContext.Result = result;
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Authorization",
                        action = "Index",
                        area = ""
                    }
                ));
            }

        }

        private void RedirectAuthorization(string msj, ActionExecutingContext filterContext)
        {

            HttpContext.Current.Session["UserModel"] = null;
            HttpContext.Current.Session["TokenValueGet"] = null;
            HttpContext.Current.Session.Remove("UserModel");
            HttpContext.Current.Session.Remove("TokenValueGet");

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JavaScriptResult result = new JavaScriptResult()
                {
                    Script = "window.location='" + "/Error/Unauthorized" + "';"
                };
                filterContext.Result = result;
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Error",
                        action = "Unauthorized",
                        area = ""
                    }
                ));
            }

        }
    }
}