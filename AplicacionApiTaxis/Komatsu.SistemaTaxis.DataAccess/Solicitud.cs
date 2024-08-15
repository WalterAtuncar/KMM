using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;
using System;
using System.Data;
using System.Data.Common;
using BE = Data.Common;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class Solicitud : Base
    {
        
        public BE.SolicitudRechazo Recuperar(BE.SolicitudRechazo entSolicitud)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Solicitud_Sel_Recuperar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entSolicitud.ID);
            BE.SolicitudRechazo lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.SolicitudRechazo(reader, BE.SolicitudRechazo.Query.Recuperar);
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
