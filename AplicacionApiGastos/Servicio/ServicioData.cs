using Common.Util;
using Dapper;
using Modelo.General;
using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Modelo.XML.Transaccion;
using Servicio.Contract;
using Servicio.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio
{

    public class ServicioData : IServicioData
    {
        ApliClient service;
        Modelo.Util.Mail mail = new Modelo.Util.Mail();
        bool flagToken = false;
        Utilitario utilitario;

        public ServicioData()
        {
            service = new ApliClient();
            mail = new Modelo.Util.Mail();
            flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            utilitario = new Utilitario();
        }
        public async Task<OperationService> SaveService(ServicioRequest request)
        {
            var result = await service.CallService<ServicioResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, flagToken: flagToken, token: request.Token);
            return result;
        }

        public async Task<OperationService> CancelService(ServicioRequest request)
        {
            var result = await service.CallService<ServicioResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, flagToken: flagToken, token: request.Token);
            return result;
        }
        public async Task<Token> GetToken(Token request)
        {
            var parametroToken = new Dictionary<string, string>();
            parametroToken.Add("UserName", request.UserName);
            parametroToken.Add("Password", request.Password);
            parametroToken.Add("grant_type", request.grant_type);
            var result = await service.CallService<Token>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, parameterToken: parametroToken, flagGetToken: true);
            return (Token)result.ResponseObject; 
        }

        public async Task<OperationService> AnularService(ServicioRequest request)
        {
            var result = await service.CallService<ServicioResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, flagToken: flagToken, token: request.Token);
            return result;
        }

        public async Task<OperationService> GetEstateService(ServicioRequest request)
        {
            var result = await service.CallService<ServicioResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, flagToken: flagToken, token: request.Token);
            return result;
        }

        public async Task<OperationService> GetInfoService(ServicioRequest request)
        {
            var result = await service.CallService<ServicioResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request, flagToken: flagToken, token: request.Token);
            return result;
        }

        public async Task<int> SaveSolicitud(ServicioTransaccionXML request)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@XMLService", request.ServiceXML.DocumentElement.InnerXml, dbType: DbType.Xml);

                return await cn.ExecuteAsync("dbo.usp_Solicitud_Approve", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> HistorialInsert(int ID, int IdSituacion, string usuarioRegistro)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", ID, dbType: DbType.Int32);
                parameters.Add("@IdSituacion", IdSituacion, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", usuarioRegistro, dbType: DbType.String);

                var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Historial_Insert", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> HistorialInsertAnular(int ID, int IdSituacion, string usuarioRegistro,string codigoServicio)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", ID, dbType: DbType.Int32);
                parameters.Add("@IdSituacion", IdSituacion, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", usuarioRegistro, dbType: DbType.String);
                parameters.Add("@Conductor", codigoServicio, dbType: DbType.String);

                var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Historial_Insert_Anular", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }


        public async Task<int> PushService(ServicioTransaccionXML request)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@XMLPush", request.ServiceXML.DocumentElement.InnerXml , dbType : DbType.Xml);

                var result = await cn.ExecuteAsync("dbo.usp_PushServicioProveedorTaxi", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Correo> GetSolicitudCorreo(int IdSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicio", IdSolicitud, dbType: DbType.Int32);
                return await cn.QueryFirstOrDefaultAsync<Correo>("dbo.usp_SolicitudCorreo_By_Servicio", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Correo> GetSolicitudCorreoFinalizado(int IdServicioProveedor)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicioProveedor", IdServicioProveedor, dbType: DbType.Int32);
                return await cn.QueryFirstOrDefaultAsync<Correo>("dbo.usp_SolicitudCorreo_By_ServicioFinalizado", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<EmailAdjunto>> GetListEmailBySolicitud(int? IdSolicitud, int? IdServicioProveedor, int IdTipoCorreo)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", IdSolicitud, dbType: DbType.Int32);
                parameters.Add("@IdServicioProveedor", IdServicioProveedor, dbType: DbType.Int32);
                parameters.Add("@IdTipoCorreo", IdTipoCorreo, dbType: DbType.Int32);

                var result = await cn.QueryAsync<EmailAdjunto>("dbo.usp_GetListEmailBySolicitud", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<List<Beneficiado>> GetListClienteByServicio(int IdSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicio", IdSolicitud, dbType: DbType.Int32);
                var lista = await cn.QueryAsync<Beneficiado>("dbo.usp_Beneficiado_Lis_By_Servicio", parameters, commandType: CommandType.StoredProcedure);
                return lista.ToList();
            }
        }

        public async Task<List<Usuario>> GetListAprobadorByCentroCosto(string codigo)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoCentroCosto", codigo, dbType: DbType.String);
                var lista = await cn.QueryAsync<Usuario>("dbo.usp_AprobadorByCentroCosto_List", parameters, commandType: CommandType.StoredProcedure);
                return lista.ToList();
            }
        }

        public async Task<Usuario> GetListResponsableByCentroCosto(string codigo)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoCentroCosto", codigo, dbType: DbType.String);

                return await cn.QueryFirstOrDefaultAsync<Usuario>("dbo.usp_ResponsableByCentroCosto_List", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Usuario>> GetListSoporteAdministrativo()
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var lista = await cn.QueryAsync<Usuario>("dbo.usp_SoporteAdministrativo_List", commandType: CommandType.StoredProcedure);
                return lista.ToList();
            }
        }

        public async Task<Usuario> GetUsuarioRegistroBySolicitud(int id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                return await cn.QueryFirstOrDefaultAsync<Usuario>("dbo.usp_UsuarioRegistroBySolicitud_List", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Beneficiado>> GetListBeneficiadoByServicioProveedor(int IdServicioProveedor)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicioProveedor", IdServicioProveedor, dbType: DbType.Int32);

                var lista = await cn.QueryAsync<Beneficiado>("dbo.usp_Beneficiado_List_By_ServicioProveedor", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();

                return result;
            }
        }
    }
}
