using System;

namespace GASTO.Domain
{
    public partial class TipoDocumento
    {
         public Int64 IdTipoDocumento { get; set; }
         public string Clase { get; set; }
         public string Denominacion { get; set; }
         public int Estado { get; set; }
    }
}
