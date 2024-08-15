using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using BE = Komatsu.SistemaTaxis.BusinessEntities;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class SolicitudProveedorTaxi : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: BusinessLogic.SolicitudProveedorTaxi.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla SolicitudProveedorTaxi, retorna ID generado
        /// </summary>
        public int Grabar(BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.SolicitudProveedorTaxi_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitudProveedorTaxi.ID);
            db.AddInParameter(dbCommand, "@SolicitudID", DbType.Int32, entSolicitudProveedorTaxi.SolicitudID);
            db.AddInParameter(dbCommand, "@ProveedorTaxiID", DbType.Int32, entSolicitudProveedorTaxi.ProveedorTaxiID);
            db.AddInParameter(dbCommand, "@PrecioServicio", DbType.Int32, entSolicitudProveedorTaxi.PrecioServicio);
            db.AddInParameter(dbCommand, "@Seleccionado", DbType.String, entSolicitudProveedorTaxi.Seleccionado);
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
        //Utilizado por	: BusinessLogic.SolicitudProveedorTaxi.Eliminar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite eliminar el registro de la tabla SolicitudProveedorTaxi
        /// </summary>
        public void Eliminar(BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.SolicitudProveedorTaxi_Del_Eliminar");
            db.AddInParameter(dbCommand, "@SolicitudID", DbType.Int32, entSolicitudProveedorTaxi.SolicitudID);
            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
