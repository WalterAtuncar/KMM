using Dapper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using BE = Komatsu.SistemaTaxis.BusinessEntities;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class Solicitud : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: BusinessLogic.Solicitud.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Solicitud, retorna ID generado
        /// </summary>
        public int Grabar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Solicitud_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitud.ID);
            db.AddInParameter(dbCommand, "@IdServicioProveedor", DbType.Int32, entSolicitud.IdServicioProveedor);
            db.AddInParameter(dbCommand, "@IdTipoServicio", DbType.Int32, entSolicitud.IdTipoServicio);
            db.AddInParameter(dbCommand, "@IdTipoPago", DbType.Int32, entSolicitud.IdTipoPago);
            db.AddInParameter(dbCommand, "@IdProveedorTaxi", DbType.Int32, entSolicitud.IdProveedorTaxi);
            db.AddInParameter(dbCommand, "@IdEstadoServicioProveedor", DbType.Int32, entSolicitud.IdEstadoServicioProveedor);
            db.AddInParameter(dbCommand, "@TipoServicio", DbType.String, entSolicitud.TipoServicio);
            db.AddInParameter(dbCommand, "@FechaServicio", DbType.DateTime, entSolicitud.FechaServicio);
            db.AddInParameter(dbCommand, "@HoraServicioEnMinutos", DbType.Int32, entSolicitud.HoraServicioEnMinutos);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoDescripcion", DbType.String, entSolicitud.CentroCostoAfectoDescripcion);
            db.AddInParameter(dbCommand, "@NroOrdenServicio", DbType.String, entSolicitud.OrdenServicio);
            db.AddInParameter(dbCommand, "@CantidadHoras", DbType.Int32, entSolicitud.CantidadHoras);
            db.AddInParameter(dbCommand, "@OrigenDireccion", DbType.String, entSolicitud.OrigenDireccion);
            db.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, entSolicitud.FechaRegistro);
            db.AddInParameter(dbCommand, "@TipoDestino", DbType.String, entSolicitud.TipoDestino);
            db.AddInParameter(dbCommand, "@MotivoCancelacion", DbType.String, entSolicitud.MotivoCancelacion);
            db.AddInParameter(dbCommand, "@FechaCancelacion", DbType.DateTime, entSolicitud.FechaCancelacion);
            db.AddInParameter(dbCommand, "@MotivoCreacionSolicitudID", DbType.Int32, entSolicitud.MotivoCreacionSolicitudID);
            db.AddInParameter(dbCommand, "@MotivoRechazo", DbType.String, entSolicitud.MotivoRechazo);
            db.AddInParameter(dbCommand, "@Observaciones", DbType.String, entSolicitud.Observaciones);
            db.AddInParameter(dbCommand, "@TotalServicio", DbType.Double, entSolicitud.TotalServicio);
            db.AddInParameter(dbCommand, "@TelefonoPrincipal", DbType.String, entSolicitud.TelefonoPrincipal);
            db.AddInParameter(dbCommand, "@DistanciaKilometro", DbType.Double, entSolicitud.DistanciaKilometro);
            db.AddInParameter(dbCommand, "@TotalGasto", DbType.Double, entSolicitud.TotalGasto);
            db.AddInParameter(dbCommand, "@TotalGeneral", DbType.Double, entSolicitud.TotalGeneral);
            db.AddInParameter(dbCommand, "@SituacionServicio", DbType.Int32, entSolicitud.SituacionServicio);
            db.AddInParameter(dbCommand, "@Sociedad", DbType.String, entSolicitud.Sociedad);
            db.AddInParameter(dbCommand, "@Liquidado", DbType.Boolean, entSolicitud.Liquidado);
            db.AddInParameter(dbCommand, "@IdConductor", DbType.Int32, entSolicitud.IdConductor);
            db.AddInParameter(dbCommand, "@IdAutomovil", DbType.Int32, entSolicitud.IdAutomovil);
            db.AddInParameter(dbCommand, "@IdTipoDestino", DbType.Int32, entSolicitud.IdTipoDestino);
            db.AddInParameter(dbCommand, "@IdMotivoCreacionSolicitud", DbType.Int32, entSolicitud.IdMotivoCreacionSolicitud);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.Int32, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@IdUsuarioAprobador", DbType.Int32, entSolicitud.IdUsuarioAprobador);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@FechaAprobacion", DbType.DateTime, entSolicitud.FechaAprobacion);
            db.AddInParameter(dbCommand, "@UsuarioAprobo", DbType.String, entSolicitud.UsuarioAprobo);
            db.AddInParameter(dbCommand, "@Calificacion", DbType.Int32, entSolicitud.Calificacion);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@UserNameAprobo", DbType.String, entSolicitud.UserNameAprobo);
            db.AddInParameter(dbCommand, "@ComentarioCalificacion", DbType.String, entSolicitud.ComentarioCalificacion);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
            db.AddInParameter(dbCommand, "@FechaModificacion", DbType.DateTime, entSolicitud.FechaModificacion);
            db.AddInParameter(dbCommand, "@UsuarioModifico", DbType.String, entSolicitud.UsuarioModifico);
            db.AddOutParameter(dbCommand, "@RetVal", DbType.Int32, 4);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = Convert.ToInt32(db.GetParameterValue(dbCommand, "@RetVal"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (25/07/2018)
        //Utilizado por	: BusinessLogic.Solicitud.Actualizar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite actualizar un registro de la tabla Solicitud
        /// </summary>
        public void Actualizar(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Solicitud_Upd_Actualizar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitud.ID);
            db.AddInParameter(dbCommand, "@IdServicioProveedor", DbType.Int32, entSolicitud.IdServicioProveedor);
            db.AddInParameter(dbCommand, "@IdTipoServicio", DbType.Int32, entSolicitud.IdTipoServicio);
            db.AddInParameter(dbCommand, "@IdTipoPago", DbType.Int32, entSolicitud.IdTipoPago);
            db.AddInParameter(dbCommand, "@IdProveedorTaxi", DbType.Int32, entSolicitud.IdProveedorTaxi);
            db.AddInParameter(dbCommand, "@IdEstadoServicioProveedor", DbType.Int32, entSolicitud.IdEstadoServicioProveedor);
            db.AddInParameter(dbCommand, "@TipoServicio", DbType.String, entSolicitud.TipoServicio);
            db.AddInParameter(dbCommand, "@FechaServicio", DbType.DateTime, entSolicitud.FechaServicio);
            db.AddInParameter(dbCommand, "@HoraServicioEnMinutos", DbType.Int32, entSolicitud.HoraServicioEnMinutos);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoDescripcion", DbType.String, entSolicitud.CentroCostoAfectoDescripcion);
            db.AddInParameter(dbCommand, "@NroOrdenServicio", DbType.String, entSolicitud.NroOrdenServicio);
            db.AddInParameter(dbCommand, "@CantidadHoras", DbType.Int32, entSolicitud.CantidadHoras);
            db.AddInParameter(dbCommand, "@OrigenDireccion", DbType.String, entSolicitud.OrigenDireccion);
            db.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, entSolicitud.FechaRegistro);
            db.AddInParameter(dbCommand, "@TipoDestino", DbType.String, entSolicitud.TipoDestino);
            db.AddInParameter(dbCommand, "@MotivoCancelacion", DbType.String, entSolicitud.MotivoCancelacion);
            db.AddInParameter(dbCommand, "@FechaCancelacion", DbType.DateTime, entSolicitud.FechaCancelacion);
            db.AddInParameter(dbCommand, "@MotivoCreacionSolicitudID", DbType.Int32, entSolicitud.MotivoCreacionSolicitudID);
            db.AddInParameter(dbCommand, "@MotivoRechazo", DbType.String, entSolicitud.MotivoRechazo);
            db.AddInParameter(dbCommand, "@Observaciones", DbType.String, entSolicitud.Observaciones);
            db.AddInParameter(dbCommand, "@TotalServicio", DbType.Double, entSolicitud.TotalServicio);
            db.AddInParameter(dbCommand, "@TelefonoPrincipal", DbType.String, entSolicitud.TelefonoPrincipal);
            db.AddInParameter(dbCommand, "@DistanciaKilometro", DbType.Double, entSolicitud.DistanciaKilometro);
            db.AddInParameter(dbCommand, "@TotalGasto", DbType.Double, entSolicitud.TotalGasto);
            db.AddInParameter(dbCommand, "@TotalGeneral", DbType.Double, entSolicitud.TotalGeneral);
            db.AddInParameter(dbCommand, "@SituacionServicio", DbType.Int32, entSolicitud.SituacionServicio);
            db.AddInParameter(dbCommand, "@Sociedad", DbType.String, entSolicitud.Sociedad);
            db.AddInParameter(dbCommand, "@Liquidado", DbType.Boolean, entSolicitud.Liquidado);
            db.AddInParameter(dbCommand, "@IdConductor", DbType.Int32, entSolicitud.IdConductor);
            db.AddInParameter(dbCommand, "@IdAutomovil", DbType.Int32, entSolicitud.IdAutomovil);
            db.AddInParameter(dbCommand, "@IdTipoDestino", DbType.Int32, entSolicitud.IdTipoDestino);
            db.AddInParameter(dbCommand, "@IdMotivoCreacionSolicitud", DbType.Int32, entSolicitud.IdMotivoCreacionSolicitud);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.Int32, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@IdUsuarioAprobador", DbType.Int32, entSolicitud.IdUsuarioAprobador);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@FechaAprobacion", DbType.DateTime, entSolicitud.FechaAprobacion);
            db.AddInParameter(dbCommand, "@UsuarioAprobo", DbType.String, entSolicitud.UsuarioAprobo);
            db.AddInParameter(dbCommand, "@Calificacion", DbType.Int32, entSolicitud.Calificacion);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@UserNameAprobo", DbType.String, entSolicitud.UserNameAprobo);
            db.AddInParameter(dbCommand, "@ComentarioCalificacion", DbType.String, entSolicitud.ComentarioCalificacion);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
            db.AddInParameter(dbCommand, "@FechaModificacion", DbType.DateTime, entSolicitud.FechaModificacion);
            db.AddInParameter(dbCommand, "@UsuarioModifico", DbType.String, entSolicitud.UsuarioModifico);
            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (01/08/2018)
        //Utilizado por	: BusinessLogic.Solicitud.Recuperar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite recuperar un registro de la tabla Solicitud
        /// </summary>
        public BE.Solicitud Recuperar(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Solicitud_Sel_Recuperar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitud.ID);
            BE.Solicitud lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.Solicitud(reader, BE.Solicitud.Query.Recuperar);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (08/05/2018)
        //Utilizado por	: BusinessLogic.Solicitud.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.Solicitud> List(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Sociedad", entSolicitud.IdSociedad, DbType.Int32);
                    parameters.Add("@IdPerfil", entSolicitud.IdPerfil, DbType.Int32);
                    parameters.Add("@UserNameRegistro", entSolicitud.UserNameRegistro, DbType.String);
                    parameters.Add("@FechaDesde", entSolicitud.FechaDesde, DbType.String);
                    parameters.Add("@FechaHasta", entSolicitud.FechaHasta, DbType.String);
                    parameters.Add("@IdSituacionServicio", entSolicitud.IdSituacionServicio, DbType.Int32);
                    parameters.Add("@CodigoSolicitud", entSolicitud.CodigoSolicitud, DbType.String);
                    parameters.Add("@CentroCosto", entSolicitud.CentroCosto, DbType.String);
                    parameters.Add("@PageSize", entSolicitud.PageSize, DbType.Int32);
                    parameters.Add("@PageIndex", entSolicitud.PageIndex, DbType.Int32);
                    parameters.Add("@TotalRows", 4, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@CantidadRegistros", 4, DbType.Int32, ParameterDirection.InputOutput);


                    lista = cn.Query<BE.Solicitud>("dbo.usp_Solicitud_List", parameters, commandType: CommandType.StoredProcedure).ToList();

                    intTotalRows = Convert.ToInt32(parameters.Get<int>("@TotalRows"));
                    intCantidadRegistros = Convert.ToInt32(parameters.Get<int>("@CantidadRegistros"));
                }
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            return lista;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (09/05/2018)
        //Utilizado por	: BusinessLogic.Solicitud.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.Solicitud> AprobadorList(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_Solicitud_Aprobador_List");
            db.AddInParameter(dbCommand, "@Sociedad", DbType.Int32, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@IdPerfil", DbType.Int32, entSolicitud.IdPerfil);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@FechaDesde", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaHasta", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@CodigoSolicitud", DbType.String, entSolicitud.CodigoSolicitud);
            db.AddInParameter(dbCommand, "@CentroCosto", DbType.String, entSolicitud.CentroCosto);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, entSolicitud.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entSolicitud.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.AprobadorList));
                }
                intTotalRows = Convert.ToInt32(dbCommand.Parameters["@TotalRows"].Value);
                intCantidadRegistros = Convert.ToInt32(dbCommand.Parameters["@CantidadRegistros"].Value);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //Utilizado por	: BusinessLogic.Solicitud.Paginado
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.SolicitudBuscarPaginado> Paginado(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_Solicitud_Buscar_Paginado");
            db.AddInParameter(dbCommand, "@IdProveedor", DbType.Int32, entSolicitud.IdProveedor);
            db.AddInParameter(dbCommand, "@FechaInicioStr", DbType.String, entSolicitud.FechaInicioStr);
            db.AddInParameter(dbCommand, "@FechaFinStr", DbType.String, entSolicitud.FechaFinStr);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.Int32, entSolicitud.IdSociedad);
            List<BE.SolicitudBuscarPaginado> lista = new List<BE.SolicitudBuscarPaginado>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.SolicitudBuscarPaginado(reader, BE.SolicitudBuscarPaginado.Query.Paginado));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Marcos Silverio (19/06/2018)
        //Utilizado por	: BusinessLogic.Solicitud.GetUsuarioCentroCostoByCeco
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.UsuarioCentroCosto> List_UsuarioCentroCostoByCeco(string centroCosto)
        {
            List<BE.UsuarioCentroCosto> lista = new List<BE.UsuarioCentroCosto>();

            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_UsuarioCentroCosto_By_Ceco_List");
            db.AddInParameter(dbCommand, "@CodigoCentroCosto", DbType.String, centroCosto);
            
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.UsuarioCentroCosto(reader));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lista;
        }

        public bool Update_UsuarioAprobador(int idSolicitud, int idUsuarioAprobador)
        {
            bool success = false;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Usp_Upd_Solicitud_IdUsuarioAprobador");
            db.AddInParameter(dbCommand, "@Id", DbType.Int32, idSolicitud);
            db.AddInParameter(dbCommand, "@IdUsuarioAprobador", DbType.Int32, idUsuarioAprobador);

            try
            {
                var count =  db.ExecuteNonQuery(dbCommand);

                success = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return success;
        }

        public Dictionary<string, object> GetSolicitudById(int id)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Usp_Sel_Solicitud_By_Id");
            db.AddInParameter(dbCommand, "@Id", DbType.Int32, id);

            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        int IdSolicitud = reader.GetInt32("Id");
                        int IdUsuarioAprobador = reader.GetInt32("IdUsuarioAprobador");
                        string CentroCostoAfectoCodigoSap = reader.GetString("CentroCostoAfectoCodigoSap");

                        data.Add("Id", IdSolicitud);
                        data.Add("IdUsuarioAprobador", IdUsuarioAprobador);
                        data.Add("CentroCostoAfectoCodigoSap", CentroCostoAfectoCodigoSap);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return data;
        }
    }
}
