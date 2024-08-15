using Dapper;
using GASTO.DataAccess.Abstracts;
using Komatsu.SistemaTaxis.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using BE = GASTO.Domain;


namespace GASTO.DataAccess
{
    public class Solicitud : BaseGASTO
    {

        public int Grabar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Ins_Grabar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            db.AddInParameter(dbCommand, "@EstadoLogico", DbType.Int32, entSolicitud.EstadoLogico);

            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);

            db.AddInParameter(dbCommand, "@IdAprobador", DbType.Int64, entSolicitud.IdAprobador);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int64, entSolicitud.IdSucursal);

            db.AddInParameter(dbCommand, "@IdDestino", DbType.Int64, entSolicitud.IdDestino);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int64, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdTipoReembolso", DbType.Int64, entSolicitud.IdTipoReembolso);
            db.AddInParameter(dbCommand, "@Motivo", DbType.String, entSolicitud.Motivo);

            db.AddInParameter(dbCommand, "@FechaSalida", DbType.DateTime, entSolicitud.FechaSalida);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.DateTime, entSolicitud.FechaRetorno);

            db.AddInParameter(dbCommand, "@AsientoContable", DbType.String, entSolicitud.AsientoContable);
            db.AddInParameter(dbCommand, "@Competencia", DbType.String, entSolicitud.Competencia);
            db.AddInParameter(dbCommand, "@CtaBancaria", DbType.String, entSolicitud.CtaBancaria);
            db.AddInParameter(dbCommand, "@IdMoneda", DbType.Int64, entSolicitud.IdMoneda);

            db.AddInParameter(dbCommand, "@ImporteSolicitud", DbType.Decimal, entSolicitud.ImporteSolicitud);
            db.AddInParameter(dbCommand, "@IdUsuarioAprobador", DbType.Int64, entSolicitud.IdUsuarioAprobador);
            db.AddInParameter(dbCommand, "@IdUsuarioM", DbType.Int64, entSolicitud.IdUsuarioM);
            db.AddInParameter(dbCommand, "@IdUsuarioR", DbType.Int64, entSolicitud.IdUsuarioR);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int64, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSituacionServicio2", DbType.Int64, entSolicitud.IdSituacionServicio2);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoDescripcion", DbType.String, entSolicitud.CentroCostoAfectoDescripcion);
            db.AddInParameter(dbCommand, "@Comentario", DbType.String, entSolicitud.Comentario);
            db.AddInParameter(dbCommand, "@RutaCompletaFile", DbType.String, entSolicitud.ListaAdjunto==null ? "" : entSolicitud.ListaAdjunto.FirstOrDefault().PathAdjunto);
            db.AddInParameter(dbCommand, "@NombreFile", DbType.String, entSolicitud.ListaAdjunto == null ? "" : entSolicitud.ListaAdjunto.FirstOrDefault().Alias);
            db.AddInParameter(dbCommand, "@TipoCambio", DbType.Decimal, entSolicitud.TipoCambio);
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

        public int Actualizar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_Actualizar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            db.AddInParameter(dbCommand, "@EstadoLogico", DbType.Int32, entSolicitud.EstadoLogico);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@IdAprobador", DbType.Int64, entSolicitud.IdAprobador);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int64, entSolicitud.IdSucursal);
            db.AddInParameter(dbCommand, "@IdDestino", DbType.Int64, entSolicitud.IdDestino);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int64, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@Motivo", DbType.String, entSolicitud.Motivo);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.DateTime, entSolicitud.FechaSalida);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.DateTime, entSolicitud.FechaRetorno);
            db.AddInParameter(dbCommand, "@AsientoContable", DbType.String, entSolicitud.AsientoContable);
            db.AddInParameter(dbCommand, "@IdTipoReembolso", DbType.Int64, entSolicitud.IdTipoReembolso);
            db.AddInParameter(dbCommand, "@Competencia", DbType.String, entSolicitud.Competencia);
            db.AddInParameter(dbCommand, "@CtaBancaria", DbType.String, entSolicitud.CtaBancaria);
            db.AddInParameter(dbCommand, "@IdMoneda", DbType.Int64, entSolicitud.IdMoneda);
            db.AddInParameter(dbCommand, "@ImporteSolicitud", DbType.Decimal, entSolicitud.ImporteSolicitud);
            db.AddInParameter(dbCommand, "@IdUsuarioAprobador", DbType.Int64, entSolicitud.IdUsuarioAprobador);
            db.AddInParameter(dbCommand, "@IdUsuarioM", DbType.Int64, entSolicitud.IdUsuarioM);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int64, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSituacionServicio2", DbType.Int64, entSolicitud.IdSituacionServicio2);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoDescripcion", DbType.String, entSolicitud.CentroCostoAfectoDescripcion);
            db.AddInParameter(dbCommand, "@Comentario", DbType.String, entSolicitud.Comentario);
            db.AddInParameter(dbCommand, "@RutaCompletaFile", DbType.String, entSolicitud.ListaAdjunto == null ? "" : entSolicitud.ListaAdjunto.FirstOrDefault().PathAdjunto);
            db.AddInParameter(dbCommand, "@NombreFile", DbType.String, entSolicitud.ListaAdjunto == null ? "" : entSolicitud.ListaAdjunto.FirstOrDefault().Alias);

            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int Aprobar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_Aprobar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            //db.AddInParameter(dbCommand, "@IdDestino", DbType.Int64, entSolicitud.IdDestino);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int RendicionSolicitud(BE.RendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.RendicionSolicitud_Upd_Rendir");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entRendicionSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@Envia", DbType.Int32, entRendicionSolicitud.Envia);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entRendicionSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entRendicionSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@Comentario", DbType.String, entRendicionSolicitud.Comentario);
            db.AddInParameter(dbCommand, "@CodigoLiquidacion", DbType.String, entRendicionSolicitud.CodigoLiquidacion);
            
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int Rechazar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_Rechazar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            //db.AddInParameter(dbCommand, "@IdDestino", DbType.Int64, entSolicitud.IdDestino);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int Eliminar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_Eliminar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int AprobarContabilidad(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_AprobarContabilidad");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@AsientoContable", DbType.String, entSolicitud.CuentaContable);
            db.AddInParameter(dbCommand, "@CodigoLiquidacion", DbType.String, entSolicitud.CodigoLiquidacion);
            db.AddInParameter(dbCommand, "@CodigoReembolso", DbType.String, entSolicitud.CodigoReembolso);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int AprobarContabilidadDetalle(BE.DetRendicionSolicitud entDetRendicionSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.SolicitudRendicionDetalle_Upd_AprobarContabilidad");
            db.AddInParameter(dbCommand, "@IdDetRendicionSolicitud", DbType.Int32, entDetRendicionSolicitud.IdDetRendicionSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDetRendicionSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDetRendicionSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entDetRendicionSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@CodigoLiquidacion", DbType.String, entDetRendicionSolicitud.CodigoLiquidacion);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public int RechazarContabilidad(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_RechazarContabilidad");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }
        public int CambiarEstadoSolicitud(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Upd_CambioEstadoContabilidad");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entSolicitud.IdSolicitud);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entSolicitud.UserNameRegistro);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }

        public BE.Solicitud Recuperar(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Sel_Recuperar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, entSolicitud.IdSolicitud);
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

        public BE.Solicitud ValidarAprobadorExterior(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_UsuarioAprobadorExterior");
            db.AddInParameter(dbCommand, "@CeCo", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            BE.Solicitud lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.Solicitud(reader, BE.Solicitud.Query.AprobadorExterior);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public BE.Solicitud DatosEmail(int IdSolicitud)
        {
            Database database = (Database)new GenericDatabase(this.ConexionSQL, (DbProviderFactory)SqlClientFactory.Instance);
            DbCommand storedProcCommand = database.GetStoredProcCommand("Gastos.Solicitud_Sel_DatosEmail");
            database.AddInParameter(storedProcCommand, "@IdSolicitud", DbType.Int32, (object)IdSolicitud);
            BE.Solicitud solicitud = (BE.Solicitud)null;
            try
            {
                using (IDataReader reader = database.ExecuteReader(storedProcCommand))
                {
                    if (reader.Read())
                        solicitud = new BE.Solicitud(reader, BE.Solicitud.Query.DatosEmail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return solicitud;
        }

        public BE.Solicitud RecuperarDetalle(BE.Solicitud entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Sel_RecuperarDetalle");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, entSolicitud.IdSolicitud);
            BE.Solicitud lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.Solicitud(reader, BE.Solicitud.Query.RecuperarDetalle);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public IEnumerable<BE.Solicitud> List(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@IdUsuario", entSolicitud.IdUsuario, DbType.Int64);
                    parameters.Add("@IdTipo", entSolicitud.IdTipo, DbType.Int32);
                    parameters.Add("@FechaSalida", entSolicitud.FechaSalida, DbType.String);
                    parameters.Add("@FechaRetorno", entSolicitud.FechaRetorno, DbType.String);
                    parameters.Add("@CentroCostoAfectoCodigoSap", entSolicitud.CentroCostoAfectoCodigoSap, DbType.String);
                    parameters.Add("@PageSize", entSolicitud.PageSize, DbType.Int32);
                    parameters.Add("@PageIndex", entSolicitud.PageIndex, DbType.Int32);
                    parameters.Add("@TotalRows", 4, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@CantidadRegistros", 4, DbType.Int32, ParameterDirection.InputOutput);
                    lista = cn.Query<BE.Solicitud>("Gastos.usp_Solicitud_List", parameters, commandType: CommandType.StoredProcedure).ToList();
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

        public IEnumerable<BE.Solicitud> Paginado(BE.Solicitud entSolicitud, string UsuarioSociedad , out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Solicitud_List");

            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            //db.AddInParameter(dbCommand, "@UsuarioSociedad", DbType.String, UsuarioSociedad);
            db.AddInParameter(dbCommand, "@IdUsuarioR", DbType.Int64, entSolicitud.IdUsuarioR);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32,entSolicitud.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entSolicitud.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.Paginado));
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

        public IEnumerable<BE.Solicitud> PaginadoAprobacion(BE.Solicitud entSolicitud, string UsuarioSociedad ,out int intTotalRows, out int intCantidadRegistros,out int IdDestino)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Solicitud_List_Aprobar");

            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            //db.AddInParameter(dbCommand, "@UsuarioSociedad", DbType.String, UsuarioSociedad);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int32, entSolicitud.IdSucursal);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, entSolicitud.UserName);
            db.AddInParameter(dbCommand, "@AnioEjercicio", DbType.String, entSolicitud.AnioEjercicio);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, entSolicitud.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entSolicitud.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@IdDestino", DbType.Int32, 4);
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.Paginado));
                }
                intTotalRows = Convert.ToInt32(dbCommand.Parameters["@TotalRows"].Value);
                intCantidadRegistros = Convert.ToInt32(dbCommand.Parameters["@CantidadRegistros"].Value);
                IdDestino = Convert.ToInt32(db.GetParameterValue(dbCommand, "@IdDestino"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public IEnumerable<BE.Solicitud> PaginadoAprobacionDelegaciones(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros, out int IdDestino)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Solicitud_List_AprobarDelegaciones");

            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int32, entSolicitud.IdSucursal);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, entSolicitud.UserName);
            db.AddInParameter(dbCommand, "@AnioEjercicio", DbType.String, entSolicitud.AnioEjercicio);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, entSolicitud.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entSolicitud.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@IdDestino", DbType.Int32, 4);
            List<BE.Solicitud> lista = new List<BE.Solicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.Paginado));
                }
                intTotalRows = Convert.ToInt32(dbCommand.Parameters["@TotalRows"].Value);
                intCantidadRegistros = Convert.ToInt32(dbCommand.Parameters["@CantidadRegistros"].Value);
                IdDestino = Convert.ToInt32(db.GetParameterValue(dbCommand, "@IdDestino"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public IEnumerable<BE.Solicitud> PaginadoAprobacionContabilidad(BE.Solicitud entSolicitud, string UsuarioSociedad ,out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Solicitud_List_AprobarContabilidad");

            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            //db.AddInParameter(dbCommand, "@UsuarioSociedad", DbType.String, UsuarioSociedad);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int32, entSolicitud.IdSucursal);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, entSolicitud.UserName);
            db.AddInParameter(dbCommand, "@AnioEjercicio", DbType.String, entSolicitud.AnioEjercicio);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
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
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.Paginado));
                    //intTotalRows = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRows"));
                    //intCantidadRegistros = Convert.ToInt32(db.GetParameterValue(dbCommand, "@CantidadRegistros"));
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

        public IEnumerable<BE.Solicitud> PaginadoDelegacion(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Solicitud_List_Delegacion");

            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entSolicitud.IdUsuario);
            db.AddInParameter(dbCommand, "@IdSituacionServicio", DbType.Int32, entSolicitud.IdSituacionServicio);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.Int32, entSolicitud.IdSucursal);
            db.AddInParameter(dbCommand, "@IdTipo", DbType.Int32, entSolicitud.IdTipo);
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, entSolicitud.IdSociedad);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, entSolicitud.UserName);
            db.AddInParameter(dbCommand, "@AnioEjercicio", DbType.String, entSolicitud.AnioEjercicio);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.String, entSolicitud.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaRetorno", DbType.String, entSolicitud.FechaHasta);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@Codigo", DbType.String, entSolicitud.Codigo);
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
                        lista.Add(new BE.Solicitud(reader, BE.Solicitud.Query.Paginado));
                    //intTotalRows = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRows"));
                    //intCantidadRegistros = Convert.ToInt32(db.GetParameterValue(dbCommand, "@CantidadRegistros"));
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

        public IEnumerable<UsuarioCentroCosto> List_UsuarioBySucursal(string idsociedad, int idsucursal)
        {
            List<UsuarioCentroCosto> lista = new List<UsuarioCentroCosto>();

            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Usuario_By_IdSucursal_List");
            db.AddInParameter(dbCommand, "@IdSociedad", DbType.String, idsociedad);
            db.AddInParameter(dbCommand, "@IdSucursal", DbType.String, idsucursal);

            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new UsuarioCentroCosto(reader));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lista;
        }
 
        public IEnumerable<UsuarioCentroCosto> List_UsuarioByCentroCosto(string codigocentrocosto)
        {
            List<UsuarioCentroCosto> lista = new List<UsuarioCentroCosto>();
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Usuario_By_CentroCosto_List");
            db.AddInParameter(dbCommand, "@CodigoCentroCosto", DbType.String, codigocentrocosto);
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new UsuarioCentroCosto(reader));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

    }
}
