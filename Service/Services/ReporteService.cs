using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common;
using System.Data.SqlClient;
using Service.Util;
using Dapper;
using System.Data;

namespace Service.Services
{
    public class ReporteService : IReporteService
    {
        public List<ComparacionMensual> GetComparacionMensual(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdResponsable", idResponsable, dbType: DbType.Int32);
                parameters.Add("@IdSociedad", idSociedad, dbType: DbType.Int32);
                parameters.Add("@Fecha_Inicio", fechaInicio, dbType: DbType.String);
                parameters.Add("@Fecha_Fin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.ComparacionMensual>("dbo.usp_ReporteComparacionMensual_Listar", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<DetalleGastoTotal> GetDetalleGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdResponsable", idResponsable, dbType: DbType.Int32);
                parameters.Add("@IdSociedad", idSociedad, dbType: DbType.Int32);
                parameters.Add("@Fecha_Inicio", fechaInicio, dbType: DbType.String);
                parameters.Add("@Fecha_Fin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.DetalleGastoTotal>("dbo.usp_ReporteDetalleGastoTotal_Listar", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<GastoTotal> GetGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdResponsable", idResponsable, dbType: DbType.Int32);
                parameters.Add("@IdSociedad", idSociedad, dbType: DbType.Int32);
                parameters.Add("@Fecha_Inicio", fechaInicio, dbType: DbType.String);
                parameters.Add("@Fecha_Fin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.GastoTotal>("dbo.usp_ReporteGastoTotal_Listar", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<SolicitudLiquidacion> GetListReporteDetallado(string listaCentroCosto, string listaUsuario)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ListaUsuario", listaUsuario, dbType: DbType.String);
                parameters.Add("@ListaCentroCosto", listaCentroCosto, dbType: DbType.String);

                var result = cn.Query<Data.Common.SolicitudLiquidacion>("dbo.usp_LiquidacionDetalle_By_Liquidacion_Export_Detallado", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<PenalidadTiempoEspera> GetPenalidadTiempoEspera(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdResponsable", idResponsable, dbType: DbType.Int32);
                parameters.Add("@IdSociedad", idSociedad, dbType: DbType.Int32);
                parameters.Add("@Fecha_Inicio", fechaInicio, dbType: DbType.String);
                parameters.Add("@Fecha_Fin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.PenalidadTiempoEspera>("dbo.usp_ReportePenalidadGastosAdicionales_Listar", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<AhorroProveedor> GetListAhorroProveedor(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdResponsable", idResponsable, dbType: DbType.Int32);
                parameters.Add("@IdSociedad", idSociedad, dbType: DbType.Int32);
                parameters.Add("@Fecha_Inicio", fechaInicio, dbType: DbType.String);
                parameters.Add("@Fecha_Fin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.AhorroProveedor>("dbo.usp_ReporteAhorroProveedor_Listar", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<Data.Common.ReporteAuditoria> GetListReporteAuditoria(string IdSolicitud, string fechaInicio, string fechaFin, string sociedad)
        {
     

            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Codigo", IdSolicitud == "undefined" ? "" : IdSolicitud, dbType: DbType.String);
                parameters.Add("@Sociedad", sociedad == "undefined" ? "" : sociedad, dbType: DbType.String);
                parameters.Add("@FechaIni", fechaInicio == "undefined" ? "" : fechaInicio, dbType: DbType.String);
                parameters.Add("@FechaFin", fechaFin == "undefined" ? "" : fechaFin , dbType: DbType.String);

                var result = cn.Query<Data.Common.ReporteAuditoria>("Gastos.usp_ReporteAuditoria", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }


        public List<Data.Common.ReporteRepresentacion> GetListReporteRepresentacion()
        {


            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                //var parameters = new DynamicParameters();
                //parameters.Add("@Codigo", IdSolicitud, dbType: DbType.String);
                //parameters.Add("@Sociedad", sociedad, dbType: DbType.String);
                //parameters.Add("@FechaIni", fechaInicio, dbType: DbType.String);
                //parameters.Add("@FechaFin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.ReporteRepresentacion>("[Gastos].[usp_reportegastos_representacion]", commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<Data.Common.ReporteRepresentacion> GetListReporteDetallado(int Anio)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Anio", Anio, dbType: DbType.Int32);
                //parameters.Add("@Sociedad", sociedad, dbType: DbType.String);
                //parameters.Add("@FechaIni", fechaInicio, dbType: DbType.String);
                //parameters.Add("@FechaFin", fechaFin, dbType: DbType.String);

                var result = cn.Query<Data.Common.ReporteRepresentacion>("[Gastos].[usp_reportegastos_detallado]", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }
    }
}
