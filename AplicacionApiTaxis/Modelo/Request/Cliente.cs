namespace Modelo.Request
{
    public class Cliente
    {
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NombreCompleto { get { return $"{this.NombreCliente} {this.ApellidosCliente}"; } }
        public string ApellidosCliente { get; set; }
        public string Telefono { get; set; }
        public string CodigoCentroCosto { get; set; }
        public bool FlagPrincipal { get; set; }
    }
}
