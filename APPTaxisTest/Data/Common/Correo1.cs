using System;
using System.Collections.Generic;

namespace Data.Common
{
    public class Correo1
    {
        public int IdTipoCorreo { get; set; }
        public string Codigo { get; set; }
        public string DocumentoUsuario { get; set; }
        public string NombreCompletoUsuario { get; set; }
        public string CompaniaUsuario { get; set; }
        public string CodigoCentroCostoUsuario { get; set; }
        public string DescripcionCentroCostoUsuario { get; set; }
        public string CodigoCentroCostoAfecto { get; set; }
        public string DescripcionCentroCostoAfecto { get; set; }
        public string OrdenServicio { get; set; }
        public string MotivoCreacion { get; set; }
        public string ZonaOrigen { get; set; }
        public string ZonaDestino { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaServicio { get; set; }
        public string FechaServicioStr { get { return this.FechaServicio.ToString("dd/MM/yyyy HH:mm:ss"); } }
        public List<Beneficiado> ListaBeneficiado { get; set; }
        public Usuario Responsable { get; set; }
        public Usuario UsuarioRegistro { get; set; }
        public List<Usuario> ListaAprobador { get; set; }
        public List<Usuario> ListaSoporteAdministrativo { get; set; }
        public List<EmailAdjunto> ListaEmailAdjunto { get; set; }

        public string RazonSocialProveedor { get; set; }
        public string RucProveedor { get; set; }
    }
}
