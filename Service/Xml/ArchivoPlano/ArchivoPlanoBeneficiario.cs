namespace Service.Xml.ArchivoPlano
{
    public class ArchivoPlanoBeneficiario
    {
        public int ID { get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string Telefono { get; set; }
        public string CodigoCentroCosto { get; set; }
        public bool FlagPrincipal { get; set; }
    }
}
