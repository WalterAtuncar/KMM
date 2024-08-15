using Common.Util;
using Modelo.Util;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Util
{
    public class ApliClient
    {
        public async Task<OperationService> CallService<T>(string httpUri, string httpRoute, int httpVerb , List<string> parameters = null , object objectSerialize = null , string token =  null, bool flagToken = false , bool flagLista = false, Dictionary<string,string> parameterToken = null, bool flagGetToken = false) where T : new ()
        {
            using (var client = new HttpClient())
            {
                var result = new OperationService();
                client.BaseAddress = new Uri(httpUri);
                client.DefaultRequestHeaders.Clear();
                if (flagToken) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await SendResponse(client, httpVerb, httpRoute, parameters, objectSerialize, flagGetToken, parameterToken);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var readTask = await response.Content.ReadAsStringAsync();
                        result.Success = true;
                        if (flagLista)
                        {
                            result.ResponseObject = JsonConvert.DeserializeObject<List<T>>(readTask);
                            Utilitario.Logger();
                            Log.Information(JsonConvert.SerializeObject(result.ResponseObject));
                            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(result.ResponseObject));
                            Log.CloseAndFlush();
                            return result;
                        }
                        else
                        {
                            result.ResponseObject = JsonConvert.DeserializeObject<T>(readTask);
                            Utilitario.Logger();
                            Log.Information(JsonConvert.SerializeObject(result.ResponseObject));
                            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(result.ResponseObject));
                            Log.CloseAndFlush();
                            return result;
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Utilitario.Logger();
                        var mensaje = await response.Content.ReadAsStringAsync();
                        Utilitario.WriteErrorLog(mensaje, "Error HttpStatusCode.BadRequest");
                        Log.CloseAndFlush();

                        result.Success = false;
                        result.Message = mensaje;
                        result.ResponseObject = mensaje;
                        /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                        return result;
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Utilitario.Logger();
                        var mensaje = await response.Content.ReadAsStringAsync();
                        Utilitario.WriteErrorLog(mensaje, "Error HttpStatusCode.InternalServerError");
                        Log.CloseAndFlush();
                        result.Success = false;
                        result.Message = "Ocurrio un error Interno";
                        /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                        return result;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Utilitario.Logger();
                        var mensaje = await response.Content.ReadAsStringAsync();
                        Utilitario.WriteErrorLog(mensaje, "Error HttpStatusCode.NotFound");
                        Log.CloseAndFlush();
                        result.Success = false;
                        result.Message = mensaje;
                        /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                        return result;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Utilitario.Logger();
                        var mensaje = await response.Content.ReadAsStringAsync();
                        Utilitario.WriteErrorLog(mensaje, "Error HttpStatusCode.Unauthorized");
                        Log.CloseAndFlush();
                        result.Success = false;
                        result.Message = mensaje;
                        if (flagLista)
                        {
                            result.ResponseObject = new List<T>();
                            return result;
                        }
                        else
                        {
                            result.ResponseObject = new T();
                            return result;
                        }
                        /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    }
                    else //web api sent error response 
                    {
                        Utilitario.Logger();
                        Utilitario.WriteInfoLog("No esta disponible el servicio");
                        Log.CloseAndFlush();
                        result.Success = false;
                        result.Message = "No esta disponible el servicio";
                        /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Utilitario.Logger();
                    var mensaje = ex.Message;
                    result.Success = false;
                    result.Message = " EXCEPCION DEL SERVICIO DEL PROVEEDOR ";
                    Utilitario.WriteErrorLog(mensaje, "EXCEPCION DEL SERVICIO DEL PROVEEDOR");
                    Log.CloseAndFlush();
                    return result;
                }
            }
        }

        public async Task<HttpResponseMessage> SendResponse(HttpClient client, int httVerb, string httRoute, List<string> parameters = null, object objectSerialize = null, bool flagGetToken = false, Dictionary<string, string> parameterToken = null)
        {
            Utilitario.Logger();
            var response = new HttpResponseMessage();

            if (httVerb == (int)HttpVerbEnum.HttGet)
            {
                if (parameters != null)
                {
                    httRoute += string.Join("&", parameters);
                    var responseTask = await client.GetAsync(httRoute);
                    response = responseTask;
                }
                else
                {
                    var responseTask = await client.GetAsync(httRoute);
                    response = responseTask;
                }
            }
            else if (httVerb == (int)HttpVerbEnum.HttPost)
            {
                if (flagGetToken)
                {
                    Log.Information(JsonConvert.SerializeObject(parameterToken));
                    Utilitario.WriteInfoLog(JsonConvert.SerializeObject(parameterToken));
                    var content = new HttpRequestMessage(HttpMethod.Post, httRoute) { Content = new FormUrlEncodedContent(parameterToken) };
                    var responseTask = await client.SendAsync(content);
                    response = responseTask;
                }
                else
                {
                    Log.Information(JsonConvert.SerializeObject(objectSerialize));
                    Utilitario.WriteInfoLog(JsonConvert.SerializeObject(objectSerialize));
                    var content = new StringContent(JsonConvert.SerializeObject(objectSerialize), Encoding.UTF8, "application/json");
                    var responseTask = await client.PostAsync(httRoute, content);
                    response = responseTask;
                }
            }
            Log.CloseAndFlush();

            return response;
        }

        public async Task<string> GenerarToken()
        {
            string client_id = "PROVEEDOR01";
            string client_secret = "$KMMP$.W3BAP1s3rV1c3!";
            string grant_type = "client_credentials";
            string scope = "W3B_4P1_S3RV1C3$";
            string httpUri = "http://190.116.25.18:9080/KomatsuSTS/";
            string httRoute = "connect/token";

            var parameterToken = new Dictionary<string, string>();
            parameterToken.Add("client_id", client_id);
            parameterToken.Add("client_secret", client_secret);
            parameterToken.Add("grant_type", grant_type);
            parameterToken.Add("scope", scope);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(httpUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    var response = new HttpResponseMessage();
                    var multi = new MultipartFormDataContent { { new StringContent(client_id), "\"client_id\"" },
                                                               { new StringContent(client_secret), "\"client_secret\"" },
                                                               { new StringContent(grant_type), "\"grant_type\"" },
                                                               { new StringContent(scope), "\"scope\"" },
                                                             };

                    var content = new HttpRequestMessage(HttpMethod.Post, httRoute) { Content = multi };
                    var responseTask = await client.SendAsync(content);
              
                    response = responseTask;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var readTask = response.Content.ReadAsStringAsync().Result;
                        return readTask.ToString();
                    }
                    else
                    {
                        return "NO HAY TOKEN";
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}
