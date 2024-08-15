using GASTO.DataAccess.Abstracts;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using BE = GASTO.Domain;

namespace GASTO.DataAccess
{
    public class RendicionSolicitud : BaseGASTO
    {

        

        public BE.RendicionSolicitud RecuperarRendicion(BE.RendicionSolicitud entRendicionSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("Gastos.Solicitud_Sel_RecuperarRendicionDetalle");
            db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int64, entRendicionSolicitud.IdSolicitud);
            BE.RendicionSolicitud lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.RendicionSolicitud(reader, BE.RendicionSolicitud.Query.RecuperarDetalle);
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
