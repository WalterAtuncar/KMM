using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[GastoAdicional")]
    public class GastoAdicional
    {
        public string MontoStr { get { return string.Format(GlobalFormat.Decimal2, this.Monto); } }
        public int IdSolicitud { get; set; }
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public decimal TiempoMinuto { get; set; }
        public string TiempoMinutoStr { get { return string.Format(GlobalFormat.Decimal2, this.TiempoMinuto); } }
    }
}
