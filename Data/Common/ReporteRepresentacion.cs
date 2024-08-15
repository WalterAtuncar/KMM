using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class ReporteRepresentacion
    {
        public int IdSolicitud { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Comentario { get; set; }

        public string Codigo { get; set; }

        public string Motivo { get; set; }

        public DateTime FechaSalida { get; set; }

        public DateTime FechaRetorno { get; set; }

        public string IdSociedad { get; set; }

        public string CentroCostoAfectoCodigoSapSolicitud { get; set; }

        public string CentroCostoAfectoDescripcion { get; set; }

        public int IdUsuario { get; set; }

        public string NumeroDocumento { get; set; }

        public int IdAprobador { get; set; }

        public DateTime FechaAprobacion { get; set; }

        public string AsientoContable { get; set; }

        public string CodigoLiquidacion { get; set; }

        public string Tipo { get; set; }

        public int IdSituacionServicio { get; set; }

        public int IdTipo { get; set; }

        public int IdTipoReembolso { get; set; }

        public string CodigoReembolso { get; set; }

        public Decimal ImporteSolicitud { get; set; }

        public string CORREL { get; set; }

        public int Activo { get; set; }

        public int IdTipoDocumento { get; set; }

        public string Clase { get; set; }

        public string Denominacion { get; set; }

        public string NroComprobante { get; set; }

        public string CodigoLocal { get; set; }

        public int IdProveedor { get; set; }

        public string RUC { get; set; }

        public string CodigoSAPProveedor { get; set; }

        public string RazonSocial { get; set; }

        public int IdMoneda { get; set; }

        public string Moneda { get; set; }

        public Decimal Importe { get; set; }

        public Decimal BaseInafecta { get; set; }

        public Decimal TotalSoles { get; set; }

        public int IdTipoGastoViaje { get; set; }

        public string CuentaContable { get; set; }

        public string CentroCostoAfectoCodigoSapGasto { get; set; }

        public string OrdenServicio { get; set; }

        public int IdImpuesto { get; set; }

        public string IndicadorImpuesto { get; set; }

        public string CodigoLiquidacionDetalle { get; set; }

        public string NombreGR { get; set; }

        public string RUCGR { get; set; }

        public string ObjetivoGR { get; set; }

        public string DetalleGR { get; set; }

        public int NumeroAcreedor { get; set; }

        public string NombreColaborador { get; set; }

        public string EstadoMaestroMotivo { get; set; }
    }
}
