using Dapper;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GASTO.Service.Services
{
    public class SolicitudService_Gasto : ISolicitudService_Gasto
    {
        public async Task<GASTO.Domain.Solicitud> GetDatoSolicitudByID(int IdSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", IdSolicitud, DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<GASTO.Domain.Solicitud>("Gastos.usp_GetDatosSolicitud", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<GASTO.Domain.Solicitud> AprobarDelegacion(int IdSolicitud, string DocNewAprobador)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", IdSolicitud, DbType.String);
                parameters.Add("@DocNewAprobador", DocNewAprobador, DbType.String);
                //parameters.Add("@IdNewAprobador", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await cn.QueryFirstOrDefaultAsync<GASTO.Domain.Solicitud>("Gastos.usp_SolicitudAprobarDelegacion_By_Id", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Correo_GV> GetSolicitudCorreo(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Correo_GV>("Gastos.usp_SolicitudCorreo_By_Id", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<Usuario>> GetListBeneficiadoBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", id, dbType: DbType.Int32);
                var result = await cn.QueryAsync<Usuario>("Gastos.usp_Beneficiado_List_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<TerminosCondiciones> GetTerminosCondiciones(int idsolicitud)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", idsolicitud, dbType: DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<TerminosCondiciones>("Gastos.usp_TerminosCondiciones", parameters, commandType: CommandType.StoredProcedure);
                return result ?? new TerminosCondiciones();
            }
        }
        public async Task<TerminosCondiciones> GetTerminosCondicionesNew(int idusuario)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", idusuario, dbType: DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<TerminosCondiciones>("Gastos.usp_TerminosCondiciones_New", parameters, commandType: CommandType.StoredProcedure);
                return result ?? new TerminosCondiciones();
            }
        }

        public async Task<int> ValidarCreacionAnticipo(int idbeneficiario,decimal ImporteSolicitud, int IdSolicitud, double tcambio)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdBeneficiario", idbeneficiario, DbType.Int32);
                parameters.Add("@ImporteSolicitud",ImporteSolicitud , DbType.Decimal);
                parameters.Add("@IdSolicitud", IdSolicitud, DbType.Int32);
                parameters.Add("@TipoCambio", (decimal)tcambio, DbType.Decimal);
                var result = await cn.ExecuteScalarAsync("Gastos.usp_Anticipo_Validar", parameters, commandType: CommandType.StoredProcedure);
                return (int)result;
            }
        }
        public async Task<string> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RUC", ruc, DbType.String);
                parameters.Add("@IdTipoDocumento", idtipodocumento, DbType.Int32);
                parameters.Add("@NroComprobante", numerocomprobante, DbType.String);
                parameters.Add("@IdDetRendicionSolicitud", idrendicion, DbType.Int32);
                var result = await cn.ExecuteScalarAsync("Gastos.usp_ValidarNumeroComprobanteDuplicado", parameters, commandType: CommandType.StoredProcedure);
                return (string)result;
            }
        }
        public async Task<GASTO.Domain.Proveedor> ValidarDatosProveedorExterno(string documento)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RUC", documento, DbType.String);
                var result = await cn.QueryFirstOrDefaultAsync<GASTO.Domain.Proveedor>("Gastos.usp_GetDatosProveedorExterno", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
