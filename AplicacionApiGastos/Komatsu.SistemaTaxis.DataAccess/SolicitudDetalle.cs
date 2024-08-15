using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using BE = Data.Common;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class SolicitudDetalle : Base
    {

        public int Grabar(BE.SolicitudDetalle entSolicitudDetalle)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.SolicitudDetalle_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitudDetalle.ID);
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, entSolicitudDetalle.IdSolicitud);
            db.AddInParameter(dbCommand, "@IdEstado", DbType.Int32, entSolicitudDetalle.IdEstado);
            db.AddInParameter(dbCommand, "@IdConductor", DbType.Int32, entSolicitudDetalle.IdConductor);
            db.AddInParameter(dbCommand, "@IdAutomovil", DbType.Int32, entSolicitudDetalle.IdAutomovil);
            db.AddInParameter(dbCommand, "@Latitud", DbType.Int32, entSolicitudDetalle.Latitud);
            db.AddInParameter(dbCommand, "@Longitud", DbType.Int32, entSolicitudDetalle.Longitud);
            db.AddInParameter(dbCommand, "@NombreConductor", DbType.String, entSolicitudDetalle.NombreConductor);
            db.AddInParameter(dbCommand, "@DocumentoConductor", DbType.String, entSolicitudDetalle.DocumentoConductor);
            db.AddInParameter(dbCommand, "@ModeloAutomovil", DbType.String, entSolicitudDetalle.ModeloAutomovil);
            db.AddInParameter(dbCommand, "@PlacaAutomovil", DbType.String, entSolicitudDetalle.PlacaAutomovil);
            db.AddInParameter(dbCommand, "@IdSituacion", DbType.Int32, entSolicitudDetalle.IdSituacion);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entSolicitudDetalle.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, entSolicitudDetalle.FechaRegistro);
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
    }
}
