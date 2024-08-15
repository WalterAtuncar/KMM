using Dapper;
using Data;
using Data.Common;
using Data.Util;
using Service.Contracts;
using Service.ServiceDTO.Request;
using Service.Util;
using Service.Xml.Transaccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LiquidacionService : ILiquidacionService
    {
        public async Task<Data.Liquidacion> Find(int Id)
        {
            using (var context = new Entities())
            {
                return await context.Liquidacion.FindAsync(Id);
            }
        }

        public IEnumerable<usp_Liquidacion_Listar_Result> List(int? Estado, int? ProveedorTaxiID, DateTime? FechaInicio, DateTime? FechaFin)
        {
            using (var context = new Entities())
            {
                return context.usp_Liquidacion_Listar(Estado, ProveedorTaxiID, FechaInicio, FechaFin).ToList();
            }
        }

        public async Task<int> Cancelar(Data.Common.Liquidacion liquidacion)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacion", liquidacion.ID, dbType: DbType.Int32);
                parameters.Add("@UserName", liquidacion.UserNameCancelo, dbType: DbType.String);
                parameters.Add("@UsuarioCancelo", liquidacion.UsuarioCancelo, dbType: DbType.String);

                var result = (int)await cn.ExecuteAsync("dbo.usp_Liquidacion_Cancelar", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<OperationResult> Insert(LiquidacionTransaccionXML element)
        {

            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@XMLService", element.LiquidacionXML.DocumentElement.InnerXml, dbType: DbType.Xml);

                    var result = (int)await cn.ExecuteScalarAsync("dbo.usp_Liquidacion_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return new OperationResult() { Success = true, NewID = result };
                }
              
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, Errors = new List<string> { ex.Message } };
            }

        }
        
        public async Task<List<LiquidacionApi>> GetListByID(int ID)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Cod_Liquidacion", ID, dbType: DbType.Int32);

                var result = await cn.QueryAsync<LiquidacionApi>("dbo.usp_Liquidacion_List_By_ID", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }

        }

        public async Task<List<Data.Common.IndicadorIGV>> GetListIndicadorIGV()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                var result = await cn.QueryAsync<Data.Common.IndicadorIGV>("dbo.usp_Listar_IndicadorIGV", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<Data.Common.CuentaMayor>> GetListCuentaMayor()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                var result = await cn.QueryAsync<Data.Common.CuentaMayor>("dbo.usp_Listar_CuentaMayor", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Data.Common.Liquidacion> GetByID(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacion", id, DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Liquidacion>("dbo.usp_Liquidacion_By_Id", parameters, commandType: CommandType.StoredProcedure);
                return result ?? new Data.Common.Liquidacion();
            }
        }

        public async Task<List<Data.Common.ProveedorTaxi>> GetListProveedorTaxi()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                var result = await cn.QueryAsync<Data.Common.ProveedorTaxi>("dbo.usp_ProveedorTaxi_List", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> Liquidar(Data.Common.Liquidacion liquidacion)
       {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", liquidacion.ID);
                parameters.Add("@Factura", liquidacion.NumeroFactura);
                parameters.Add("@FechaFactura", liquidacion.FechaFactura);
                parameters.Add("@FechaContable", liquidacion.FechaContable);
                parameters.Add("@FacturaProvision",liquidacion.CodigoIndicador);
                parameters.Add("@Usuario", liquidacion.UsuarioLiquido);
                parameters.Add("@UserName", liquidacion.UserNameLiquido);
                parameters.Add("@AsientoContable", liquidacion.AsientoContable);
                parameters.Add("@Ejercicio", liquidacion.Ejercicio);
                parameters.Add("@DiaRegistro", Convert.ToDateTime(liquidacion.DiaRegistro));
                parameters.Add("@UsuarioRegistroSAP", liquidacion.UsuarioRegistroSAP);
                parameters.Add("@NombreFile", liquidacion.ListaAdjunto.FirstOrDefault().Alias);
                parameters.Add("@RutaCompletaFile", liquidacion.ListaAdjunto.FirstOrDefault().PathAdjunto);

                parameters.Add("@Mensaje", liquidacion.Mensaje);
                parameters.Add("@HoraRegistro", liquidacion.HoraRegistro);
                parameters.Add("@IndicadorIGV", liquidacion.IndicadorIGV);
                parameters.Add("@IndicadorCME", liquidacion.IndicadorCME);
                parameters.Add("@IndicadorDH", liquidacion.CodigoIndicador);
                parameters.Add("@CuentaMayor", liquidacion.CuentaMayor);

                var result = await cn.ExecuteAsync("[dbo].[usp_Liquidacion_Liquidar]", parameters, commandType: CommandType.StoredProcedure);
                return (int)result;
            }
        }

        public async Task<List<SolicitudLiquidacion>> GetListDetalleByLiquidacion(int iD)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacion", iD);
                var result = await cn.QueryAsync<Data.Common.SolicitudLiquidacion>("dbo.usp_LiquidacionDeatlle_By_Liquidacion", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<LiquidacionExport2>> GetLiquidacionExportByID(int idLiquidacion)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacion", idLiquidacion);
                var result = await cn.QueryAsync<Data.Common.LiquidacionExport2>("dbo.usp_Liquidacion_Export_By_ID", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> UpdateLiquidacionDetalle(SolicitudLiquidacion liquidacionDetalle)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacionDetalle", liquidacionDetalle.ID);
                parameters.Add("@CentroCostoAfecto", liquidacionDetalle.CentroCostoAfecto);
                parameters.Add("@OrdenServicio", liquidacionDetalle.OrdenServicio);

                var result = (int) await cn.ExecuteAsync("dbo.usp_LiquidacionDetalle_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<SolicitudLiquidacion>> GetListDetalleByIdLiquidacion(int idLiquidacion)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdLiquidacion", idLiquidacion);
                var result = await cn.QueryAsync<Data.Common.SolicitudLiquidacion>("dbo.usp_LiquidacionDetalle_By_Liquidacion", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public List<Data.Common.Liquidacion> ListarPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
                parameters.Add("@Estado", parameter.Collection["Estado"], DbType.Int32);
                parameters.Add("@ProveedorTaxiID", parameter.Collection["ProveedorTaxiID"], DbType.Int32);
                parameters.Add("@FechaInicio", parameter.Collection["FechaInicio"], DbType.DateTime);
                parameters.Add("@FechaFin", parameter.Collection["FechaFin"], DbType.DateTime);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var result = cn.Query<Data.Common.Liquidacion>("dbo.usp_Liquidacion_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure).ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");

                return result;
            }
        }
    }
}
