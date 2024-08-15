using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Request
{
    public class Beneficiado
    {
        public int ID { get; set; }
        public string UsersID { get; set; }
        public int IdTipoDocumento { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public bool FlagPrincipal { get; set; }
        //servicio integrado
        public string NombreCompleto { get { return $"{this.Apellidos}, {this.Nombre}"; } }
        public int SolicitudID { get; set; }
        public string RUC { get; set; }
    }
}
