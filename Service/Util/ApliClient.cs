using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Util
{
    public class ApliClient
    {
        public async Task<OperationService> CallService<T>(string httpUri, string httpRoute, int httpVerb, List<string> parameters = null, object objectSerialize = null, string token = null, bool flagToken = false, bool flagLista = false) where T : new()
        {
            using (var client = new HttpClient())
            {
                var result = new OperationService();
                client.BaseAddress = new Uri(httpUri);
                if (flagToken) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await SendResponse(client, httpVerb, httpRoute, parameters, objectSerialize);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = await response.Content.ReadAsStringAsync();
                 
                    if (readTask.StartsWith("[") && readTask.EndsWith("]") && flagLista == false)
                    {
                        result.Success = false;
                        return result;
                    }
                    else
	                {
                        result.Success = true;
                        if (flagLista)
                        {
                            result.ResponseObject = JsonConvert.DeserializeObject<List<T>>(readTask);
                            return result;
                        }
                        else
                        {
                            result.ResponseObject = JsonConvert.DeserializeObject<T>(readTask);
                            return result;
                        }
                    }
                    
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = false;
                    result.ResponseObject = await response.Content.ReadAsStringAsync();
                    result.Message = (string)result.ResponseObject;
                    /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error Interno";
                    /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    result.Success = false;
                    result.Message = await response.Content.ReadAsStringAsync();
                    /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    result.Success = false;
                    result.Message = "Unauthorized";
                    /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    return result;
                }
                else //web api sent error response 
                {
                    result.Success = false;
                    result.Message = "No esta disponible el servicio";
                    /*LOG DE ERRORES PARA LOS PROVEEDORES*/
                    return result;
                }
            }
        }

        public async Task<HttpResponseMessage> SendResponse(HttpClient client, int httVerb, string httRoute, List<string> parameters = null, object objectSerialize = null)
        {
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
                var content = new StringContent(JsonConvert.SerializeObject(objectSerialize), Encoding.UTF8, "application/json");
                var responseTask = await client.PostAsync(httRoute, content);
                response = responseTask;
            }

            return response;
        }
    }
}
