using System;
using System.Data;

namespace GASTO.Domain
{
    public partial class HistorialLog
    {
        public enum Query
        {
            Historial,
        }
        public HistorialLog() {}
        public HistorialLog(IDataReader reader, Query query = Query.Historial)
        {
            switch (query)
            {
                case Query.Historial:
                    UsuarioRegistro = reader.GetSafeString("UsuarioRegistro");
                    FechaRegistroStr = reader.GetSafeDateTime("FechaRegistro")?.ToString("dd/MM/yyyy HH:mm:ss");
                    Estado = reader.GetSafeString("Estado");
                    CodigoGenerado = reader.GetSafeString("CodigoGenerado");
                    break;
                default:
                    break;
            }
        }
    }
}
