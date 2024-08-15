namespace Modelo.Request
{
    public class GastoAdicional
    {
        public int ID { get; set; }
        public int IdConcepto { get; set; }
        public Concepto Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public decimal TiempoMinuto { get; set; }
    }
}
