using Modelo.Request;
using System;
using System.Collections.Generic;

namespace Modelo.General
{
    public class Correo
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
        
        public int ID { get; set; }
        public string NumeroDocumentoAprobador { get; set; }
        public string NombreCompletoAprobador { get; set; }
        public string CodigoSociedadAprobador { get; set; }
        public string NombreSociedadAprobador { get; set; }
        public string CodigoCecoAprobador { get; set; }
        public string DescripcionCecoAprobador { get; set; }
        public Conductor Conductor { get; set; }
        public AutoMovil Automovil { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public List<GastoAdicional> ListaGastoAdicional { get; set; }


        public string RazonSocialProveedor { get; set; }
        public string RucProveedor { get; set; }
    }
}
