using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public class UsuarioCentroCosto
    {
        public int Id { get; set; }
        public string CodigoCentroCosto { get; set; }
        public int IdUsuario { get; set; }
        public string NombresApellido { get; set; }
        public bool FlagAprobador { get; set; }
        public bool FlagResponsable { get; set; }

        public UsuarioCentroCosto()
        {

        }

        public UsuarioCentroCosto(IDataReader reader)
        {
            Id = reader.GetInt32("IdUsuarioCentroCosto");
            CodigoCentroCosto = reader.GetString("CodigoCentroCosto");
            IdUsuario = reader.GetInt32("IdUsuario");
            NombresApellido = reader.GetString("NombresApellido");
            FlagAprobador = reader.GetBoolean("FlagAprobador");
            FlagResponsable = reader.GetBoolean("FlagResponsable");
        }
    }
}
