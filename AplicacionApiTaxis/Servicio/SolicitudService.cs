
//using Data;
//using Data.Common;
//using Data.Util;
using Dapper;
using Data.Common;
using Modelo.Util;
using Service.Contracts;
using Servicio.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using Service.Util;
//using Service.Xml.Transaccion;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Transactions;

namespace Service.Services
{
    public class SolicitudService : ISolicitudService
    {
        //public async Task<OperationResult> Cancel(int id, string motivoCancelacion, string usuarioRegistro)
        //{
        //    try
        //    {
        //        using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@ID", id, dbType: DbType.Int32);
        //            parameters.Add("@MotivoCancelacion", motivoCancelacion, dbType: DbType.String);
        //            parameters.Add("@UsuarioRegistro", usuarioRegistro, dbType: DbType.String);

        //            var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Solicitud_Cancel", parameters, commandType: CommandType.StoredProcedure);

        //            return new OperationResult() { Success = true };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
        //    }
        //}

        //public async Task<Data.Solicitud> Find(int Id)
        //{
        //    using (var context = new Entities())
        //    {
        //        var solicitud = await context.Solicitud.Include("DestinoServicio").Include("Beneficiado")
        //            .Include("SolicitudProveedorTaxi")
        //            .FirstOrDefaultAsync(x => x.ID == Id);
        //        return solicitud;
        //    }
        //}

        //public async Task<int> Refuse(int? id, string UsuarioRegistro, string motivoRechazo)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@ID", id, dbType: DbType.Int32);
        //        parameters.Add("@UsuarioRegistro", UsuarioRegistro, dbType: DbType.String);
        //        parameters.Add("@MotivoRechazo", motivoRechazo, dbType: DbType.String);

        //        var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Solicitud_Refuse", parameters, commandType: CommandType.StoredProcedure);

        //        return result;
        //    }
        //}

        //public async Task<int> Add(ServicioTransaccionXML element)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@XMLService", element.ServicioXML.DocumentElement.InnerXml, dbType: DbType.Xml);

        //        var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Solicitud_Insert", parameters, commandType: CommandType.StoredProcedure);

        //        return result;
        //    }

        //}

        //public async Task<int> Update(ServicioTransaccionXML element)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@XMLService", element.ServicioXML.DocumentElement.InnerXml, dbType: DbType.Xml);

        //        var result = await cn.QueryFirstOrDefaultAsync<int>("dbo.usp_Solicitud_Update", parameters, commandType: CommandType.StoredProcedure);

        //        return result;
        //    }
        //}

        public async Task<Modelo.General.Correo> GetSolicitudCorreo(int id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicio", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Modelo.General.Correo>("dbo.usp_SolicitudCorreo_By_Servicio", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Data.Common.Solicitud> GetById(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var query = await cn.QueryAsync<Data.Common.Solicitud, Data.Common.MotivoCreacionSolicitud,
                                      Data.Common.TipoPago, Data.Common.TipoServicio,
                                      Data.Common.TipoDestino, Data.Common.Sociedad,
                                      Data.Common.Solicitud>("dbo.usp_Solicitud_GetByID",
                                      (solicitud, motivocreacion,
                                       tipopago, tiposervicio,
                                       tipodestino, sociedad
                                      )
                                      =>
                                      {
                                          solicitud.MotivoCreacionSolicitud = motivocreacion;
                                          solicitud.TipoPago = tipopago;
                                          solicitud.TipoServicio = tiposervicio;
                                          solicitud.TipoDestino = tipodestino;
                                          solicitud.Sociedad = sociedad;
                                          return solicitud;
                                      }, parameters, commandType: CommandType.StoredProcedure);

                var result = query.ToList().FirstOrDefault();
                return result;
            }
        }

        public async Task<Data.Common.EstadoProveedor> GetEstadoProveedorBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.EstadoProveedor>("dbo.usp_EstadoProveedor_GetBySolicitud", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.EstadoProveedor();
            }
        }

        public async Task<Data.Common.CentroCosto> GetCentroCostoBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.CentroCosto>("dbo.usp_CentroCosto_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<List<Data.Common.Destino>> GetGestinoBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryAsync<Data.Common.Destino>("dbo.usp_Destino_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        //public async Task<Data.Common.Solicitud> GetSolicitudByUsernameRegistro(string username)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@UserNameRegistro", username, dbType: DbType.String);

        //        var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Solicitud>("dbo.usp_Solicitud_GetByCalificacion", parameters, commandType: CommandType.StoredProcedure);

        //        return result;
        //    }
        //}

        //public OperationResult UpdateCalificacion(int id, int calificacion, string comentario)
        //{
        //    try
        //    {
        //        using (var context = new Entities())
        //        {
        //            using (var ts = new TransactionScope())
        //            {
        //                context.usp_Solicitud_UpdateCalificacion(id, calificacion, comentario);
        //                ts.Complete();
        //                return new OperationResult() { Success = true };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
        //    }
        //}

        public async Task<List<Modelo.Request.Beneficiado>> GetListBeneficiadoBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryAsync<Modelo.Request.Beneficiado>("dbo.usp_Beneficiado_List_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        //public List<Data.Common.Solicitud> GetListSolicitud(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
        //        parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
        //        parameters.Add("@Sociedad", parameter.Collection["Sociedad"], DbType.String);
        //        parameters.Add("@IdPerfil", parameter.Collection["IdPerfil"], DbType.Int32);
        //        parameters.Add("@UserNameRegistro", parameter.Collection["UserNameRegistro"], DbType.String);
        //        parameters.Add("@FechaDesde", parameter.Collection["FechaDesde"], DbType.String);
        //        parameters.Add("@FechaHasta", parameter.Collection["FechaHasta"], DbType.String);
        //        parameters.Add("@IdSituacionServicio", parameter.Collection["IdSituacionServicio"], DbType.Int32);
        //        parameters.Add("@CodigoSolicitud", parameter.Collection["NroSolicitud"], DbType.String);
        //        parameters.Add("@CentroCosto", parameter.Collection["CentroCosto"], DbType.String);
        //        parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
        //        parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

        //        var result = cn.Query<Data.Common.Solicitud>("dbo.usp_Solicitud_List", parameters, commandType: CommandType.StoredProcedure).ToList();
        //        totalRows = parameters.Get<int>("@TotalRows");
        //        cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
        //        return result;
        //    }
        //}

        //public List<Data.Common.Solicitud> GetListSolicitudAprobador(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
        //        parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
        //        parameters.Add("@Sociedad", parameter.Collection["Sociedad"], DbType.String);
        //        parameters.Add("@IdPerfil", parameter.Collection["IdPerfil"], DbType.Int32);
        //        parameters.Add("@UserNameRegistro", parameter.Collection["UserNameRegistro"], DbType.String);
        //        parameters.Add("@FechaDesde", parameter.Collection["FechaDesde"], DbType.String);
        //        parameters.Add("@FechaHasta", parameter.Collection["FechaHasta"], DbType.String);
        //        parameters.Add("@IdSituacionServicio", parameter.Collection["IdSituacionServicio"], DbType.Int32);
        //        parameters.Add("@CodigoSolicitud", parameter.Collection["NroSolicitud"], DbType.String);
        //        parameters.Add("@CentroCosto", parameter.Collection["CentroCosto"], DbType.String);
        //        parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
        //        parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

        //        var result = cn.Query<Data.Common.Solicitud>("dbo.usp_Solicitud_Aprobador_List", parameters, commandType: CommandType.StoredProcedure).ToList();
        //        totalRows = parameters.Get<int>("@TotalRows");
        //        cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
        //        return result;
        //    }
        //}

        public async Task<Data.Common.SituacionServicio> GetSituacionServicioBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.SituacionServicio>("dbo.usp_SituacionServicio_GetBySolicitud", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<List<Data.Common.SolicitudProveedorTaxi>> GetListProveedorTaxiBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryAsync<Data.Common.SolicitudProveedorTaxi, Data.Common.ProveedorTaxi, Data.Common.SolicitudProveedorTaxi>
                                      ("dbo.usp_ProveedorTaxi_List_By_Solicitud",
                                      (modelo, proveedor) => { modelo.ProveedorTaxi = proveedor; return modelo; },
                                      parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<List<Modelo.Request.Destino>> GetListDestinoByProveedor(int idSolicitud, int idProveedor)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", idSolicitud, dbType: DbType.Int32);
                parameters.Add("@IdProveedor", idProveedor, dbType: DbType.Int32);

                var result = await cn.QueryAsync<Modelo.Request.Destino>("dbo.usp_Destinio_List_By_Proveedor", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<Data.Common.Conductor> GetConductorBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Conductor>("dbo.usp_Conductor_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.Conductor();
            }
        }

        public async Task<Data.Common.Automovil> GetAutomovilByAutomovil(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Automovil>("dbo.usp_Automovil_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.Automovil();
            }
        }

        //public IEnumerable<usp_Solicitud_GetByLiquidacionID_Result> GetByLiquidacionID(int LiquidacionID, int PageSize, int PageIndex, out int TotalRows, out int CantidadRegistros)
        //{
        //    var _TotalRows = new ObjectParameter("TotalRows", typeof(Int32));
        //    var _CantidadRegistros = new ObjectParameter("CantidadRegistros", typeof(Int32));

        //    var list = new List<usp_Solicitud_GetByLiquidacionID_Result>();
        //    using (var context = new Entities())
        //    {
        //        list = context.usp_Solicitud_GetByLiquidacionID(LiquidacionID, PageSize, PageIndex, _TotalRows, _CantidadRegistros).ToList();
        //    }

        //    TotalRows = (int)_TotalRows.Value;
        //    CantidadRegistros = (int)_CantidadRegistros.Value;
        //    return list;
        //}

        //public OperationResult UpdateCentroCostoByID(int LiquidacionID, string CentroCosto)
        //{
        //    using (var context = new Entities())
        //    {
        //        using (var ts = new TransactionScope())
        //        {
        //            try
        //            {
        //                var result = context.usp_Solicitud_UpdateCentroCosto(LiquidacionID, CentroCosto);
        //                ts.Complete();
        //                return new OperationResult() { Success = true };
        //            }
        //            catch (Exception ex)
        //            {
        //                ts.Dispose();
        //                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
        //            }

        //        }
        //    }
        //}

        //public IEnumerable<usp_Solicitud_GetByLiquidacionIDForExcel_Result> GetByLiquidacionIDForExcel(int LiquidacionID)
        //{
        //    using (var context = new Entities())
        //    {
        //        return context.usp_Solicitud_GetByLiquidacionIDForExcel(LiquidacionID);
        //    }
        //}

        //public async Task<List<Data.Common.SolicitudDetalle>> GetListSolicitudDetalleBySolicitud(int idSolicitud)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IdSolicitud", idSolicitud, dbType: DbType.Int32);

        //        var result = await cn.QueryAsync<Data.Common.SolicitudDetalle>("dbo.usp_DetalleSolicitud_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

        //        return result.ToList();
        //    }
        //}

        //public async Task<List<Data.Common.GastoAdicional>> GetListGastoAdicionalBySolicitud(int idSolicitud)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IdSolicitud", idSolicitud, dbType: DbType.Int32);

        //        var result = await cn.QueryAsync<Data.Common.GastoAdicional>("dbo.usp_GastoAdicional_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

        //        return result.ToList();
        //    }
        //}

        //public async Task<string> GetRUCByServicio(int? idServicio)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IdServicio", idServicio, dbType: DbType.Int32);

        //        var result = await cn.ExecuteScalarAsync("dbo.usp_Proveedor_Sel_Get_RUC_By_Servicio", parameters, commandType: CommandType.StoredProcedure);

        //        return result.ToString()?? string.Empty;
        //    }
        //}

        //public async Task<List<General.Tracking>> GetTracking(int idServicio)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IdServicio", idServicio, dbType: DbType.Int32);

        //        var result = await cn.QueryAsync<General.Tracking>("dbo.usp_Tracking_Sel_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

        //        return result.ToList();
        //    }
        //}

        public async Task<Data.Common.Usuario> GetUsuarioByUserName(string userName)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName, dbType: DbType.String);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Usuario>("dbo.usp_User_Get_By_UserName", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.Usuario();
            }
        }

        public async Task<string> GetUserNameAproboBySolicitud(int? id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", id, dbType: DbType.Int32);

                var result = await cn.ExecuteScalarAsync("dbo.usp_UserNameAprobo_By_Solicitu", parameters, commandType: CommandType.StoredProcedure);

                return result == null ? string.Empty : result.ToString();
            }
        }
        public async Task<SolicitudRechazo> Recuperar(SolicitudRechazo entSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", entSolicitud.ID, DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<SolicitudRechazo>("dbo.Solicitud_Sel_Recuperar", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task Actualizar(SolicitudRechazo entSolicitud)
        {
                using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", entSolicitud.ID, DbType.Int32);
                    parameters.Add("@IdServicioProveedor", entSolicitud.IdServicioProveedor, DbType.Int32);
                    parameters.Add("@IdTipoServicio", entSolicitud.IdTipoServicio, DbType.Int32);
                    parameters.Add("@IdTipoPago", entSolicitud.IdTipoPago, DbType.Int32);
                    parameters.Add("@IdProveedorTaxi", entSolicitud.IdProveedorTaxi, DbType.Int32);
                    parameters.Add("@IdEstadoServicioProveedor", entSolicitud.IdEstadoServicioProveedor, DbType.Int32);
                    parameters.Add("@TipoServicio", entSolicitud.TipoServicio, DbType.String);
                    parameters.Add("@FechaServicio", entSolicitud.FechaServicio, DbType.DateTime);
                    parameters.Add("@HoraServicioEnMinutos", entSolicitud.HoraServicioEnMinutos, DbType.Int32);
                    parameters.Add("@CentroCostoAfectoCodigoSap", entSolicitud.CentroCostoAfectoCodigoSap, DbType.String);
                    parameters.Add("@CentroCostoAfectoDescripcion", entSolicitud.CentroCostoAfectoDescripcion, DbType.String);
                    parameters.Add("@NroOrdenServicio", entSolicitud.NroOrdenServicio, DbType.String);
                    parameters.Add("@CantidadHoras", entSolicitud.CantidadHoras, DbType.Int32);
                    parameters.Add("@OrigenDireccion", entSolicitud.OrigenDireccion, DbType.String);
                    parameters.Add("@FechaRegistro", entSolicitud.FechaRegistro, DbType.DateTime);
                    parameters.Add("@TipoDestino", entSolicitud.TipoDestino, DbType.String);
                    parameters.Add("@MotivoCancelacion", entSolicitud.MotivoCancelacion, DbType.String);
                    parameters.Add("@FechaCancelacion", entSolicitud.FechaCancelacion, DbType.DateTime);
                    parameters.Add("@MotivoCreacionSolicitudID", entSolicitud.MotivoCreacionSolicitudID, DbType.Int32);
                    parameters.Add("@MotivoRechazo", entSolicitud.MotivoRechazo, DbType.String);
                    parameters.Add("@Observaciones", entSolicitud.Observaciones, DbType.String);
                    parameters.Add("@TotalServicio", entSolicitud.TotalServicio, DbType.Double);
                    parameters.Add("@TelefonoPrincipal", entSolicitud.TelefonoPrincipal, DbType.String);
                    parameters.Add("@DistanciaKilometro", entSolicitud.DistanciaKilometro, DbType.Double);
                    parameters.Add("@TotalGasto", entSolicitud.TotalGasto, DbType.Double);
                    parameters.Add("@TotalGeneral", entSolicitud.TotalGeneral, DbType.Double);
                    parameters.Add("@SituacionServicio", entSolicitud.SituacionServicio, DbType.Int32);
                    parameters.Add("@Sociedad", entSolicitud.Sociedad, DbType.String);
                    parameters.Add("@Liquidado", entSolicitud.Liquidado, DbType.Boolean);
                    parameters.Add("@IdConductor", entSolicitud.IdConductor, DbType.Int32);
                    parameters.Add("@IdAutomovil", entSolicitud.IdAutomovil, DbType.Int32);
                    parameters.Add("@IdTipoDestino", entSolicitud.IdTipoDestino, DbType.Int32);
                    parameters.Add("@IdMotivoCreacionSolicitud", entSolicitud.IdMotivoCreacionSolicitud, DbType.Int32);
                    parameters.Add("@IdSituacionServicio", entSolicitud.IdSituacionServicio, DbType.Int32);
                    parameters.Add("@IdSociedad", entSolicitud.IdSociedad, DbType.Int32);
                    parameters.Add("@IdUsuarioAprobador", entSolicitud.IdUsuarioAprobador, DbType.Int32);
                    parameters.Add("@UsuarioRegistro", entSolicitud.UsuarioRegistro, DbType.String);
                    parameters.Add("@FechaAprobacion", entSolicitud.FechaAprobacion, DbType.DateTime);
                    parameters.Add("@UsuarioAprobo", entSolicitud.UsuarioAprobo, DbType.String);
                    parameters.Add("@Calificacion", entSolicitud.Calificacion, DbType.Int32);
                    parameters.Add("@UserNameRegistro", entSolicitud.UserNameRegistro, DbType.String);
                    parameters.Add("@UserNameAprobo", entSolicitud.UserNameAprobo, DbType.String);
                    parameters.Add("@ComentarioCalificacion", entSolicitud.ComentarioCalificacion, DbType.String);
                    parameters.Add("@Codigo", entSolicitud.Codigo, DbType.String);
                    parameters.Add("@FechaModificacion", entSolicitud.FechaModificacion, DbType.DateTime);
                    parameters.Add("@UsuarioModifico", entSolicitud.UsuarioModifico, DbType.String);
                    try
                    {
                        var result = await cn.ExecuteAsync("dbo.Solicitud_Upd_Actualizar", parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
        }

        public async Task<int> Grabar(SolicitudDetalle entSolicitudDetalle)
        {
            int valor = 0;
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", entSolicitudDetalle.ID, DbType.Int32);
                parameters.Add("@IdSolicitud", entSolicitudDetalle.IdSolicitud, DbType.Int32);
                parameters.Add("@IdEstado", entSolicitudDetalle.IdEstado, DbType.Int32);
                parameters.Add("@IdConductor", entSolicitudDetalle.IdConductor, DbType.Int32);
                parameters.Add("@IdAutomovil", entSolicitudDetalle.IdAutomovil, DbType.Int32);
                parameters.Add("@Latitud", entSolicitudDetalle.Latitud, DbType.Int32);
                parameters.Add("@Longitud", entSolicitudDetalle.Longitud, DbType.Int32);
                parameters.Add("@NombreConductor", entSolicitudDetalle.NombreConductor, DbType.String);
                parameters.Add("@DocumentoConductor", entSolicitudDetalle.DocumentoConductor, DbType.String);
                parameters.Add("@ModeloAutomovil", entSolicitudDetalle.ModeloAutomovil, DbType.String);
                parameters.Add("@PlacaAutomovil", entSolicitudDetalle.PlacaAutomovil, DbType.String);
                parameters.Add("@IdSituacion", entSolicitudDetalle.IdSituacion, DbType.Int32);
                parameters.Add("@UsuarioRegistro", entSolicitudDetalle.UsuarioRegistro, DbType.String);
                parameters.Add("@FechaRegistro", entSolicitudDetalle.FechaRegistro, DbType.DateTime);
                parameters.Add("@RetVal", 0, direction: ParameterDirection.ReturnValue);
                try
                {
                    await cn.QueryFirstOrDefaultAsync<SolicitudDetalle>("dbo.SolicitudDetalle_Ins_Grabar", parameters, commandType: CommandType.StoredProcedure);
                    valor = parameters.Get<int>("@RetVal");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return valor;
            }
        }

        //public List<SolicitudLiquidacion> GetListSolicitudLiquidacion(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
        //        parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
        //        parameters.Add("@IdProveedor", parameter.Collection["IdProveedor"], DbType.Int32);
        //        parameters.Add("@FechaInicio", parameter.Collection["FechaInicio"], DbType.DateTime);
        //        parameters.Add("@FechaFin", parameter.Collection["FechaFin"], DbType.DateTime);
        //        parameters.Add("@IdSociedad", parameter.Collection["IdSociedad"], DbType.Int32);
        //        parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
        //        parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

        //        var result = cn.Query<SolicitudLiquidacion>("dbo.usp_Solicitud_Buscar_Paginado", parameters, commandType: CommandType.StoredProcedure).ToList();

        //        totalRows = parameters.Get<int>("@TotalRows");
        //        cantidadRegistros = parameters.Get<int>("@CantidadRegistros");

        //        return result;
        //    }
        //}

        //public List<SolicitudLiquidacion> GetSolicitudByLiquidacionID(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
        //        parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
        //        parameters.Add("@IdLiquidacion", parameter.Collection["IdLiquidacion"], DbType.Int32);
        //        parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
        //        parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

        //        var result = cn.Query<SolicitudLiquidacion>("dbo.usp_Solicitud_GetByLiquidacionID", parameters, commandType: CommandType.StoredProcedure).ToList();

        //        totalRows = parameters.Get<int>("@TotalRows");
        //        cantidadRegistros = parameters.Get<int>("@CantidadRegistros");

        //        return result;
        //    }
        //}

        //public async Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IdSolicitud", idSolicitud, dbType: DbType.Int32);

        //        var result = await cn.QueryAsync<Data.Common.Destino>("dbo.usp_Solicitud_Tarifa_Negociada", parameters, commandType: CommandType.StoredProcedure);

        //        return result.ToList();
        //    }
        //}
    }
}
