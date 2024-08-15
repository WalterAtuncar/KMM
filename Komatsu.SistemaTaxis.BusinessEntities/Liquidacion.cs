using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Liquidacion
    {
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public string Codigo { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public string FechaPaginadoStr
        {
            get
            {
                return this.Fecha.HasValue ? Convert.ToDateTime(this.Fecha).ToString("dd/MM/yyyy") : string.Empty;
            }
        }
        public Nullable<int> ProveedorTaxiID { get; set; }
        public string CodigoSociedad { get; set; }
        public string Total { get; set; }
        public Nullable<int> Estado { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UsuarioCancelo { get; set; }
        public string UserNameCancelo { get; set; }
        public string UsuarioLiquido { get; set; }
        public Nullable<DateTime> FechaLiquidacion { get; set; }
        public Nullable<DateTime> FechaCancelacion { get; set; }
        public string UserNameRegistro { get; set; }
        public string UserNameLiquido { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
        public Nullable<int> UsuarioModifica { get; set; }
        public Nullable<DateTime> FechaModifica { get; set; }
        public Nullable<int> SociedadID { get; set; }
        public string Observacion { get; set; }
        public string Factura { get; set; }
        public Nullable<DateTime> FechaFactura { get; set; }
        public string FechaFacturaPaginadoStr
        {
            get
            {
                return this.FechaFactura.HasValue ? Convert.ToDateTime(this.FechaFactura).ToString("dd/MM/yyyy") : string.Empty;
            }
        }
        public string AsientoContable { get; set; }
        public string Ejercicio { get; set; }
        public string DiaRegistro { get; set; }
        public string UsuarioRegistroSAP { get; set; }
        public string HoraRegistro { get; set; }
        public string Mensaje { get; set; }
        public string FacturaProvision { get; set; }
        public string HoraServicio { get; set; }
        public Nullable<DateTime> Fecha_Contable { get; set; }
        public string Moneda { get; set; }
        public string Flag_Imp { get; set; }
        public string Indicadore_Igv { get; set; }
        public string Lugar_Comer { get; set; }
        public string Indicador_Cme { get; set; }
        public string Cta_Mayor { get; set; }
        public string Indicador_D_H { get; set; }
        public string NombreFile { get; set; }
        public string RutaCompletaFile { get; set; }
    }
}
