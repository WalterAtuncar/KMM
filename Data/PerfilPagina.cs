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
    
    public partial class PerfilPagina
    {
        public int ID { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public Nullable<int> IdPagina { get; set; }
    
        public virtual Pagina Pagina { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}
