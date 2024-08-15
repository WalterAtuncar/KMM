using System;

namespace GASTO.Domain
{
    public partial class TipoGastosViaje
    {
        public Int64 IdTipoGastoViaje { get; set; }
        public string CodigoGastoViaje { get; set; }
        public string TipoGastoViaje { get; set; }
        public string CuentaContable { get; set; }
        public string NombreCuentaContable { get; set; }
        public string Origen { get; set; }
        public string Estado { get; set; }
    }
}
