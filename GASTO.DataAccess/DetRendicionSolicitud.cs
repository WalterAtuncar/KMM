using GASTO.DataAccess.Abstracts;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using BE = GASTO.Domain;

namespace GASTO.DataAccess
{
    public class DetRendicionSolicitud : BaseGASTO
    {

        public int Grabar(BE.DetRendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.DetRendicionSolicitud_Ins_Grabar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entRendicionSolicitud.IdRendicionSolicitud);
            db.AddInParameter(dbCommand, "@IdDetRendicionSolicitud", DbType.Int64, entRendicionSolicitud.IdDetRendicionSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entRendicionSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entRendicionSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@Tipo", DbType.Boolean, entRendicionSolicitud.Tipo);

            db.AddInParameter(dbCommand, "@OrdenServicio", DbType.String, entRendicionSolicitud.OrdenServicio);
            db.AddInParameter(dbCommand, "@RUT", DbType.String, entRendicionSolicitud.RUT);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entRendicionSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@NroComprobante", DbType.String, entRendicionSolicitud.NroComprobante);
            db.AddInParameter(dbCommand, "@Tratamiento", DbType.String, entRendicionSolicitud.Tratamiento);
            db.AddInParameter(dbCommand, "@Nombre1", DbType.String, entRendicionSolicitud.Nombre1);
            db.AddInParameter(dbCommand, "@Nombre2", DbType.String, entRendicionSolicitud.Nombre2);
            db.AddInParameter(dbCommand, "@Nombre3", DbType.String, entRendicionSolicitud.Nombre3);
            db.AddInParameter(dbCommand, "@Nombre4", DbType.String, entRendicionSolicitud.Nombre4);
            db.AddInParameter(dbCommand, "@Calle", DbType.String, entRendicionSolicitud.Calle);
            db.AddInParameter(dbCommand, "@Poblacion", DbType.String, entRendicionSolicitud.Poblacion);
            db.AddInParameter(dbCommand, "@Pais", DbType.String, entRendicionSolicitud.Pais);
            db.AddInParameter(dbCommand, "@TipoCambio", DbType.Decimal, entRendicionSolicitud.TipoCambio);
            db.AddInParameter(dbCommand, "@RUC", DbType.String, entRendicionSolicitud.RUC);
            db.AddInParameter(dbCommand, "@RazonSocial", DbType.String, entRendicionSolicitud.RazonSocial);
            db.AddInParameter(dbCommand, "@Externo", DbType.Boolean, entRendicionSolicitud.Externo);
            db.AddInParameter(dbCommand, "@IdTipoDocumento", DbType.Int64, entRendicionSolicitud.IdTipoDocumento);
            db.AddInParameter(dbCommand, "@IdTipoGastoViaje", DbType.Int64, entRendicionSolicitud.IdTipoGastoViaje);
            db.AddInParameter(dbCommand, "@GlosaDetalle", DbType.String, entRendicionSolicitud.GlosaDetalle);
            db.AddInParameter(dbCommand, "@IdMoneda", DbType.Int64, entRendicionSolicitud.IdMoneda);
            db.AddInParameter(dbCommand, "@Importe", DbType.Decimal, entRendicionSolicitud.Importe);
            db.AddInParameter(dbCommand, "@BaseInafecta", DbType.Decimal, entRendicionSolicitud.BaseInafecta);
            db.AddInParameter(dbCommand, "@IdImpuesto", DbType.Int64, entRendicionSolicitud.IdImpuesto);
            db.AddInParameter(dbCommand, "@Fecha", DbType.DateTime, entRendicionSolicitud.Fecha);
            db.AddInParameter(dbCommand, "@RutaCompletaFile", DbType.String, entRendicionSolicitud.ListaAdjunto == null ? "" : entRendicionSolicitud.ListaAdjunto.FirstOrDefault().PathAdjunto);
            db.AddInParameter(dbCommand, "@NombreFile", DbType.String, entRendicionSolicitud.ListaAdjunto == null ? "" : entRendicionSolicitud.ListaAdjunto.FirstOrDefault().Alias);
            db.AddInParameter(dbCommand, "@CodigoSAPProveedor", DbType.String, entRendicionSolicitud.CodigoSAPProveedor);
            db.AddInParameter(dbCommand, "@NombreGR", DbType.String, entRendicionSolicitud.NombreGR);
            db.AddInParameter(dbCommand, "@RUCGR", DbType.String, entRendicionSolicitud.RUCGR);
            db.AddInParameter(dbCommand, "@ObjetivoGR", DbType.String, entRendicionSolicitud.ObjetivoGR);
            db.AddInParameter(dbCommand, "@DetalleGR", DbType.String, entRendicionSolicitud.DetalleGR);
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

        public int Actualizar(BE.DetRendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.DetRendicionSolicitud_Upd_Actualizar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entRendicionSolicitud.IdRendicionSolicitud);
            db.AddInParameter(dbCommand, "@IdDetRendicionSolicitud", DbType.Int64, entRendicionSolicitud.IdDetRendicionSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entRendicionSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entRendicionSolicitud.UserNameRegistro);
            db.AddInParameter(dbCommand, "@Tipo", DbType.Boolean, entRendicionSolicitud.Tipo);
            db.AddInParameter(dbCommand, "@RUC", DbType.String, entRendicionSolicitud.RUC);
            db.AddInParameter(dbCommand, "@RazonSocial", DbType.String, entRendicionSolicitud.RazonSocial);
            db.AddInParameter(dbCommand, "@Externo", DbType.Boolean, entRendicionSolicitud.Externo);
            db.AddInParameter(dbCommand, "@IdTipoDocumento", DbType.Int64, entRendicionSolicitud.IdTipoDocumento);
            db.AddInParameter(dbCommand, "@IdTipoGastoViaje", DbType.Int32, entRendicionSolicitud.IdTipoGastoViaje);

            db.AddInParameter(dbCommand, "@OrdenServicio", DbType.String, entRendicionSolicitud.OrdenServicio);
            db.AddInParameter(dbCommand, "@RUT", DbType.String, entRendicionSolicitud.RUT);
            db.AddInParameter(dbCommand, "@CentroCostoAfectoCodigoSap", DbType.String, entRendicionSolicitud.CentroCostoAfectoCodigoSap);
            db.AddInParameter(dbCommand, "@NroComprobante", DbType.String, entRendicionSolicitud.NroComprobante);
            db.AddInParameter(dbCommand, "@Tratamiento", DbType.String, entRendicionSolicitud.Tratamiento);
            db.AddInParameter(dbCommand, "@Nombre1", DbType.String, entRendicionSolicitud.Nombre1);
            db.AddInParameter(dbCommand, "@Nombre2", DbType.String, entRendicionSolicitud.Nombre2);
            db.AddInParameter(dbCommand, "@Nombre3", DbType.String, entRendicionSolicitud.Nombre3);
            db.AddInParameter(dbCommand, "@Nombre4", DbType.String, entRendicionSolicitud.Nombre4);
            db.AddInParameter(dbCommand, "@Calle", DbType.String, entRendicionSolicitud.Calle);
            db.AddInParameter(dbCommand, "@Poblacion", DbType.String, entRendicionSolicitud.Poblacion);
            db.AddInParameter(dbCommand, "@Pais", DbType.String, entRendicionSolicitud.Pais);
            db.AddInParameter(dbCommand, "@TipoCambio", DbType.Decimal, entRendicionSolicitud.TipoCambio);
            db.AddInParameter(dbCommand, "@GlosaDetalle", DbType.String, entRendicionSolicitud.GlosaDetalle);
            db.AddInParameter(dbCommand, "@IdMoneda", DbType.Int64, entRendicionSolicitud.IdMoneda);
            db.AddInParameter(dbCommand, "@Importe", DbType.Decimal, entRendicionSolicitud.Importe);
            db.AddInParameter(dbCommand, "@BaseInafecta", DbType.Decimal, entRendicionSolicitud.BaseInafecta);
            db.AddInParameter(dbCommand, "@IdImpuesto", DbType.Int64, entRendicionSolicitud.IdImpuesto);
            db.AddInParameter(dbCommand, "@RutaCompletaFile", DbType.String, entRendicionSolicitud.ListaAdjunto == null ? "" : entRendicionSolicitud.ListaAdjunto.FirstOrDefault().PathAdjunto);
            db.AddInParameter(dbCommand, "@NombreFile", DbType.String, entRendicionSolicitud.ListaAdjunto == null ? "" : entRendicionSolicitud.ListaAdjunto.FirstOrDefault().Alias);
            db.AddInParameter(dbCommand, "@Fecha", DbType.DateTime, entRendicionSolicitud.Fecha);
            db.AddInParameter(dbCommand, "@CodigoSAPProveedor", DbType.String, entRendicionSolicitud.CodigoSAPProveedor);
            db.AddInParameter(dbCommand, "@NombreGR", DbType.String, entRendicionSolicitud.NombreGR);
            db.AddInParameter(dbCommand, "@RUCGR", DbType.String, entRendicionSolicitud.RUCGR);
            db.AddInParameter(dbCommand, "@ObjetivoGR", DbType.String, entRendicionSolicitud.ObjetivoGR);
            db.AddInParameter(dbCommand, "@DetalleGR", DbType.String, entRendicionSolicitud.DetalleGR);
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

        public int Eliminar(BE.DetRendicionSolicitud entDetRendicionSolicitud)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.DetRendicionSolicitud_Upd_Eliminar");
            db.AddInParameter(dbCommand, "@IdDetRendicionSolicitud", DbType.Int64, entDetRendicionSolicitud.IdDetRendicionSolicitud);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entDetRendicionSolicitud.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entDetRendicionSolicitud.UserNameRegistro);
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

        public IEnumerable<BE.DetRendicionSolicitud> Paginado(BE.DetRendicionSolicitud entDetRendicionSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_DetRendicionSolicitud_List");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entDetRendicionSolicitud.IdRendicionSolicitud);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32,entDetRendicionSolicitud.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entDetRendicionSolicitud.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);
            List<BE.DetRendicionSolicitud> lista = new List<BE.DetRendicionSolicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.DetRendicionSolicitud(reader, BE.DetRendicionSolicitud.Query.Paginado));
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

        public IEnumerable<BE.DetRendicionSolicitud> PaginadoAll(int IdSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.usp_DetRendicionSolicitud_ListAll");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, IdSolicitud);
            List<BE.DetRendicionSolicitud> lista = new List<BE.DetRendicionSolicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.DetRendicionSolicitud(reader, BE.DetRendicionSolicitud.Query.Paginado));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }


        public BE.DetRendicionSolicitud RecuperarDetalle(BE.DetRendicionSolicitud entRendicionSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.RendicionSolicitud_Sel_RecuperarDetalle");
            db.AddInParameter(dbCommand, "@IdDetRendicionSolicitud", DbType.Int32, entRendicionSolicitud.IdDetRendicionSolicitud);
            BE.DetRendicionSolicitud lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.DetRendicionSolicitud(reader, BE.DetRendicionSolicitud.Query.RecuperarDetalle);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

        public IEnumerable<BE.DetRendicionSolicitud> RecuperarDetalleLista(int IdSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.RendicionSolicitud_Sel_RecuperarDetalleLista");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, IdSolicitud);
            List<BE.DetRendicionSolicitud> lista = new List<BE.DetRendicionSolicitud>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.DetRendicionSolicitud(reader, BE.DetRendicionSolicitud.Query.RecuperarDetalle));
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
