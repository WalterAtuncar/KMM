using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class Destino : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: BusinessLogic.Destino.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Destino, retorna ID generado
        /// </summary>
        public int Grabar(BE.Destino entDestino)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Destino_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entDestino.ID);
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, entDestino.IdSolicitud);
            db.AddInParameter(dbCommand, "@IdProveedor", DbType.Int32, entDestino.IdProveedor);
            db.AddInParameter(dbCommand, "@LatitudOrigen", DbType.Double, entDestino.LatitudOrigen);
            db.AddInParameter(dbCommand, "@LongitudOrigen", DbType.Double, entDestino.LongitudOrigen);
            db.AddInParameter(dbCommand, "@LatitudDestino", DbType.Double, entDestino.LatitudDestino);
            db.AddInParameter(dbCommand, "@LongitudDestino", DbType.Double, entDestino.LongitudDestino);
            db.AddInParameter(dbCommand, "@Orden", DbType.Int32, entDestino.Orden);
            db.AddInParameter(dbCommand, "@DireccionOrigen", DbType.String, entDestino.DireccionOrigen);
            db.AddInParameter(dbCommand, "@NumeroDireccionOrigen", DbType.String, entDestino.NumeroDireccionOrigen);
            db.AddInParameter(dbCommand, "@DireccionDestino", DbType.String, entDestino.DireccionDestino);
            db.AddInParameter(dbCommand, "@NumeroDireccionDestino", DbType.String, entDestino.NumeroDireccionDestino);
            db.AddInParameter(dbCommand, "@DistanciaDestinoKilometro", DbType.Int32, entDestino.DistanciaDestinoKilometro);
            db.AddInParameter(dbCommand, "@Precio", DbType.Double, entDestino.Precio);
            db.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, entDestino.FechaInicio);
            db.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, entDestino.FechaFin);
            db.AddInParameter(dbCommand, "@ZonaOrigen", DbType.String, entDestino.ZonaOrigen);
            db.AddInParameter(dbCommand, "@ZonaDestino", DbType.String, entDestino.ZonaDestino);
            db.AddInParameter(dbCommand, "@Negociado", DbType.Boolean, entDestino.Negociado);
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
        //Utilizado por	: BusinessLogic.Destino.Eliminar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite eliminar el registro de la tabla Destino
        /// </summary>
        public void Eliminar(BE.Destino entDestino)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Destino_Del_Eliminar");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, entDestino.IdSolicitud);
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
