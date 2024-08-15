namespace Modelo.Request
{
    public class ServiceRequest
    {
        public int IdServicio { get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public string RUCEmpresa { get; set; }
        public string RUCProveedor { get; set; }
    }
}
