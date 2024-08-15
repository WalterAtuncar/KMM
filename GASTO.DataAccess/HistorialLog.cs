using GASTO.DataAccess.Abstracts;

namespace GASTO.DataAccess
{
    public class HistorialLog : BaseGASTO
    {
        //public IEnumerable<BE.HistorialLog> GetHistorialByIdSolicitud(int IdSolicitud)
        //{
        //    List<BE.HistorialLog> lista = new List<BE.HistorialLog>();
        //    Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
        //    DbCommand dbCommand = db.GetStoredProcCommand("GASTO.usp_HistorialAprobacion_By_Solicitud");
        //    db.AddInParameter(dbCommand, "@IdSolicitud", DbType.Int32, IdSolicitud);
        //    try
        //    {
        //        using (IDataReader reader = db.ExecuteReader(dbCommand))
        //        {
        //            while (reader.Read())
        //                lista.Add( new BE.HistorialLog(reader, BE.HistorialLog.Query.Historial));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return lista;
           
        //}
    }
}
