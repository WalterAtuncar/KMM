using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[CentroCosto")]
    public class CentroCosto
    {
        [Key]
        public string Codigo { get; set; }
        public string Soc { get; set; }
        public string Denominacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public string FechaInicioStr { get { return this.FechaInicio.ToString("dd/MM/yyyy"); } }
        public DateTime FechaFin { get; set; }
        public string FechaFinStr { get { return this.FechaFin.ToString("dd/MM/yyyy"); } }
        public int IdResponsable { get; set; }
        public string TextoBreve { get; set; }
        public bool FlagCosto { get; set; }
        public bool FlagGasto { get; set; }
        public string Tipo { get; set; }
    }
}
