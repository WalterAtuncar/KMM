using System;

namespace GASTO.Domain
{
    public partial class Pais
    {
        public Int32 IdPais { get; set; }
        public string CodAbreviatura { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
