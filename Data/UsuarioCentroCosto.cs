//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsuarioCentroCosto
    {
        public int ID { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string CodigoCentroCosto { get; set; }
        public Nullable<bool> FlagAprobador { get; set; }
        public Nullable<bool> FlagResponsable { get; set; }
    
        public virtual CentroCosto CentroCosto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
