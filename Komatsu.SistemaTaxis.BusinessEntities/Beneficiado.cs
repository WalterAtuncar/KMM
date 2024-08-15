using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Beneficiado
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public Nullable<int> SolicitudID { get; set; }
        public Nullable<int> IdTipoDocumento { get; set; }
        public Nullable<int> UsersID { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Nullable<bool> FlagPrincipal { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string Apellidos { get; set; }
    }
}
