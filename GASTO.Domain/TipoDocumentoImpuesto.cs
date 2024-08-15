using System;

namespace GASTO.Domain
{
    public partial class TipoDocumentoImpuesto
    {
        public Int64 IdTipoDocImpuesto { get; set; }
        public Int64 IdTipoDocumento { get; set; }
        public Int64 IdImpuesto { get; set; }
        public Int64 idTipoDocSAP { get; set; }
    }
}
