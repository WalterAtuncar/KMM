using System.Data;

namespace GASTO.Domain
{
    public partial class Delegaciones
    {
        public enum Query
        {
            None,
            Paginado,
            Recuperar,
   
        }

        public Delegaciones()
        {
        }

        public Delegaciones(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Paginado:
                    ID = (int)reader.GetSafeInt32("ID");
                    IdUsuario = (int)reader.GetSafeInt32("IdUsuario");
                    IdUsuarioDelegado = (int)reader.GetSafeInt32("IdUsuarioDelegado");
                    FechaDesdeStr = reader.GetSafeDateTime("FechaDesde")?.ToString("dd/MM/yyyy");
                    FechaHastaStr = reader.GetSafeDateTime("FechaHasta")?.ToString("dd/MM/yyyy");
                    FechaAsignacionStr = reader.GetSafeDateTime("FechaAsignacion")?.ToString("dd/MM/yyyy");
                    FechaRevocacionStr = reader.GetSafeDateTime("FechaRevocacion")==null ? "" : reader.GetSafeDateTime("FechaRevocacion")?.ToString("dd/MM/yyyy");
                    Principal = (bool)reader.GetSafeBoolean("Principal");
                    EsPrincipal = (string)reader.GetSafeString("EsPrincipal");
                    Estado = (int)reader.GetSafeInt32("Estado");
                    DescripcionEstado = reader.GetSafeString("DescripcionEstado");
                    Comentarios = reader.GetSafeString("Comentarios") == null ? "" : reader.GetSafeString("Comentarios");
                    Delegado = reader.GetSafeString("Delegado");
                    break;

                case Query.Recuperar:

                    ID = (int)reader.GetSafeInt32("ID");
                    IdUsuario = (int)reader.GetSafeInt32("IdUsuario");
                    IdUsuarioDelegado = (int)reader.GetSafeInt32("IdUsuarioDelegado");
                    FechaDesdeStr = reader.GetSafeDateTime("FechaDesde")?.ToString("dd/MM/yyyy");
                    FechaHastaStr = reader.GetSafeDateTime("FechaHasta")?.ToString("dd/MM/yyyy");
                    FechaAsignacionStr = reader.GetSafeDateTime("FechaAsignacion")?.ToString("dd/MM/yyyy");
                    FechaRevocacionStr = reader.GetSafeDateTime("FechaRevocacion") == null ? "" : reader.GetSafeDateTime("FechaRevocacion")?.ToString("dd/MM/yyyy");
                    Principal = (bool)reader.GetSafeBoolean("Principal");
                    EsPrincipal = (string)reader.GetSafeString("EsPrincipal");
                    Estado = (int)reader.GetSafeInt32("Estado");
                    DescripcionEstado = reader.GetSafeString("DescripcionEstado");
                    Comentarios = reader.GetSafeString("Comentarios") == null ? "" : reader.GetSafeString("Comentarios");
                    Delegado = reader.GetSafeString("Delegado");
                    break;
                default:
                    break;
            }
        }
    }
}
