using GASTO.DataAccess.Abstracts;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BE = GASTO.Domain;

namespace GASTO.DataAccess
{
    public class Delegaciones : BaseGASTO
    {

        public int Grabar(BE.Delegaciones entDelegacion)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Delegaciones_Ins_Grabar");
            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entDelegacion.IdUsuario);
            db.AddInParameter(dbCommand, "@IdUsuarioDelegado", DbType.Int64, entDelegacion.IdUsuarioDelegado);
            db.AddInParameter(dbCommand, "@Comentarios", DbType.String, entDelegacion.Comentarios);
            db.AddInParameter(dbCommand, "@FechaDesde", DbType.DateTime, entDelegacion.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaHasta", DbType.DateTime, entDelegacion.FechaHasta);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDelegacion.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDelegacion.UserNameRegistro);
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

        public int Actualizar(BE.Delegaciones entDelegacion)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Delegaciones_Upd_Actualizar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, entDelegacion.ID);
            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entDelegacion.IdUsuario);
            db.AddInParameter(dbCommand, "@IdUsuarioDelegado", DbType.Int64, entDelegacion.IdUsuarioDelegado);
            db.AddInParameter(dbCommand, "@Comentarios", DbType.String, entDelegacion.Comentarios);
            db.AddInParameter(dbCommand, "@FechaDesde", DbType.DateTime, entDelegacion.FechaDesde);
            db.AddInParameter(dbCommand, "@FechaHasta", DbType.DateTime, entDelegacion.FechaHasta);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDelegacion.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDelegacion.UserNameRegistro);
            db.AddInParameter(dbCommand, "@Estado", DbType.Int32, entDelegacion.Estado);
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

        public int Eliminar(BE.Delegaciones entDelegaciones)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Delegaciones_Upd_Eliminar");
            //db.AddInParameter(dbCommand, "@IdDelegaciones", DbType.Int64, entDelegaciones.IdDelegaciones);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDelegaciones.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDelegaciones.UserNameRegistro);
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

        public int Revocar(BE.Delegaciones entDelegaciones)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Delegaciones_Upd_Revocar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, entDelegaciones.ID);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDelegaciones.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDelegaciones.UserNameRegistro);
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

        public IEnumerable<BE.Delegaciones> Paginado(BE.Delegaciones entDelegaciones, out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_Delegaciones_List");
            db.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, entDelegaciones.IdUsuario);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, entDelegaciones.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entDelegaciones.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            List<BE.Delegaciones> lista = new List<BE.Delegaciones>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Delegaciones(reader, BE.Delegaciones.Query.Paginado));
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

        


        public BE.Delegaciones RecuperarDetalle(BE.Delegaciones entDelegacion)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Delegaciones_Sel_Recuperar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entDelegacion.ID);
            BE.Delegaciones lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.Delegaciones(reader, BE.Delegaciones.Query.Recuperar);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public IEnumerable<BE.Delegaciones> RecuperarDetalleLista(int IdSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.RendicionSolicitud_Sel_RecuperarDetalleLista");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, IdSolicitud);
            List<BE.Delegaciones> lista = new List<BE.Delegaciones>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    //while (reader.Read())
                        //lista.Add(new BE.Delegaciones(reader, BE.Delegaciones.Query.RecuperarDetalle));
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
