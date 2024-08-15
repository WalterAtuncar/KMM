namespace Service.Xml.ArchivoPlano
{
    public class ArchivoPlanoLiquidacionDetalle
    {
        public int SolicitudID { get; set; }
        public decimal PrecioTarifa { get; set; }
        public decimal PrecioEspera { get; set; }
        public decimal PrecioDesvioRuta { get; set; }
        public decimal PrecioPeaje { get; set; }
        public decimal PrecioEstacionamiento { get; set; }
        public decimal TotalGeneral { get; set; }
        public string OrdenServicio { get; set; }
        public string CentroCostoAfecto { get; set; }
        public decimal PrecioDesplazamiento { get; set; }
    }
}
