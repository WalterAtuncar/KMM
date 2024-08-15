namespace GASTO.Domain
{
    public class ResponseSolicitudRendicionSap
    {
        public string Correlativo { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string MensajeRendicion { get; set; }
        public string DetalleOk { get; set; }
    }
}
