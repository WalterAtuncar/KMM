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
    
    public partial class Pagina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pagina()
        {
            this.PerfilPagina = new HashSet<PerfilPagina>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public string Icono { get; set; }
        public int IdMenu { get; set; }
    
        public virtual MenuPagina MenuPagina { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerfilPagina> PerfilPagina { get; set; }
    }
}
